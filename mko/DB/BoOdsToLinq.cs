using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Diagnostics;
using System.Data.SqlClient;


namespace mko.Bo
{
    public abstract class OdsToLinq<TDataContext, TEntity> : IDisposable
        where TDataContext : System.Data.Linq.DataContext, new()
    {
        // Spezielle Exceptionklasse
        [Serializable]
        public class OdsToLinqException : System.ApplicationException
        {
            public static OdsToLinqException Create(Exception ex)
            {
                string msg = string.Format("Err: OdsToLinq{0}: {1}", typeof(TEntity).Name, ex.Message);
                Debug.WriteLine(msg);
                return new OdsToLinqException(msg, ex.InnerException);
            }

            public static OdsToLinqException Create(string MethodName, Exception ex)
            {
                string msg = string.Format("Err: OdsToLinq{0}.{1}: {2}", typeof(TEntity).Name, MethodName, ex.Message);
                Debug.WriteLine(msg);
                return new OdsToLinqException(msg, ex.InnerException);
            }

            // Konstruktoren
            private OdsToLinqException(string message) : base(message) { }
            private OdsToLinqException(string message, Exception innerException) : base(message, innerException) { }
        }

        bool _shareCtx = false;
        protected OdsToLinq(TDataContext ctx)
        {
            _ctx = ctx;
            _shareCtx = false;
        }

        public void SetShareDataContext()
        {
            _shareCtx = true;
        }

        TDataContext _ctx;
        protected TDataContext DataContext
        {
            get
            {
                return _ctx;
            }
        }

        // 


        // Impelmentierung der SelectAll- Methode
        protected abstract System.Linq.IQueryable<TEntity> selectImpl();

        // Implementierung der SelectById- Methode
        protected abstract System.Linq.IQueryable<TEntity> selectByIdImpl(int id);


        // Allgemeines Select mit einstellbarer Sortierung

        // Mittels sort wird ein übergebenes Resultset bezüglich der angegebenen Spalte sortiert und
        // die sortierte Liste zurückgegeben
        protected IQueryable<T> OrderFunc<T>(System.Collections.Generic.IEnumerable<T> tab, string ColName, bool desc)
        {
            System.Reflection.PropertyInfo pinfo = typeof(T).GetProperty(ColName);

            if (desc)
                return tab.OrderByDescending(t => pinfo.GetValue(t, null)).AsQueryable();
            else
                return tab.OrderBy(t => pinfo.GetValue(t, null)).AsQueryable();
        }

        // Liefert das bezüglich sort(...) sortierte Resultset zurück
        protected System.Linq.IQueryable<TEntity> GetSorted(string sortType, System.Linq.IQueryable<TEntity> res)
        {
            bool sortDescending = false;
            string ColName = "";

            if (!String.IsNullOrEmpty(sortType))
            {

                string[] values = sortType.Split(' ');
                ColName = values[0];

                if (values.Length > 1)
                    sortDescending = values[1] == "DESC";

                //res = sort(res, ColName, sortDescending);
                res = OrderFunc(res, ColName, sortDescending);
            }

            return res;
        }

        // Öffentliche Methoden
        public IQueryable<TEntity> select(string sortType)
        {
            try
            {
                System.Linq.IQueryable<TEntity> res = selectImpl();

                //Determining whether to sort ascending or descending 
                //(GridView appends DESC if the column is clicked on twice to indicate a descending sort)

                return GetSorted(sortType, res);

            }
            catch (Exception ex)
            {
                throw OdsToLinqException.Create("select", ex);
            }
        }


        public IQueryable<TEntity> selectById(int id, string sortType)
        {
            try
            {
                System.Linq.IQueryable<TEntity> res = selectByIdImpl(id);

                //Determining whether to sort ascending or descending 
                //(GridView appends DESC if the column is clicked on twice to indicate a descending sort)

                return GetSorted(sortType, res);

            }
            catch (Exception ex)
            {
                throw OdsToLinqException.Create("selectById", ex);
            }
        }


        #region IDisposable Member

        void IDisposable.Dispose()
        {
            // Resourcen freigeben und Verbindung zum Server schließen
            //if(!_shareCtx)
            //    DataContext.Dispose();
        }

        #endregion
    }
}
