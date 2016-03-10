//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den Herbst 2009
//
//  Projekt.......: mkoItAsp- Schulungsunterlagen
//  Name..........: OdsSortEntitiesEF.cs
//  Aufgabe/Fkt...: Bietet Framework zum sortieren nach Spalten in 
//                  eine Objekt- Datasource an
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.8.2011
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

using System.Linq.Dynamic;


namespace mkoIt.Asp
{
    public abstract class BoFilterAndSortEfEntitiesBase<TObjectContext, TEntity, TEntityView>        
        where TObjectContext : System.Data.Objects.ObjectContext, new()
    {
        /// <summary>
        /// Spezielle Exceptionklasse
        /// </summary> 
        [Serializable]
        public class BoFilterAndSortEfEntitiesBaseException : System.ApplicationException
        {
            public static BoFilterAndSortEfEntitiesBaseException Create(Exception ex)
            {
                string msg = string.Format("Err: BoFilterAndSortEfEntitiesBase{0}: {1}", typeof(TEntity).Name, ex.Message);
                Debug.WriteLine(msg);
                return new BoFilterAndSortEfEntitiesBaseException(msg, ex.InnerException);
            }

            public static BoFilterAndSortEfEntitiesBaseException Create(string MethodName, Exception ex)
            {
                string msg = string.Format("Err: BoFilterAndSortEfEntitiesBase{0}.{1}: {2}", typeof(TEntity).Name, MethodName, ex.Message);
                Debug.WriteLine(msg);
                return new BoFilterAndSortEfEntitiesBaseException(msg, ex.InnerException);
            }

            // Konstruktoren
            private BoFilterAndSortEfEntitiesBaseException(string message) : base(message) { }
            private BoFilterAndSortEfEntitiesBaseException(string message, Exception innerException) : base(message, innerException) { }
        }       



        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="DefaultSortCol"></param>
        /// <param name="otherTEntityViews">Liste der Typen der restlichen Views vom Entity</param>
        public BoFilterAndSortEfEntitiesBase(TObjectContext ctx, string DefaultSortCol)            
        {
            _ctx = ctx;
            _sortColname = _defaultSortCol = DefaultSortCol;
        }

        /// <summary>
        /// Gibt den Objektkontext zurück, über dem alle EF- Entityauflistungen erreichbar sind
        /// </summary>
        TObjectContext _ctx;
        public TObjectContext ObjectContext
        {
            get
            {
                return _ctx;
            }
        }


        /// <summary>
        /// Ordnet dem Namen einer Eigenschaft den zugehörigen Namen der Tabellenspalte in der Datenbanktabelle zu.
        /// Wenn der Typ der View ein selbstdefinierter ist (kein Entity aus EF), dann muß der Name der Eigenschaft mit der 
        /// Spalte der Tabelle übereinstimmen, oder mittels des Attributes mko.MapPropertyTocolNameAttribute wird der Eigenschaft der 
        /// Spaltenname zugeordnet.
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static string mapPropertyToColName(Type boRecordType, string propName)
        {
            if (typeof(TEntity) == typeof(TEntityView))
                return propName;

            System.Reflection.PropertyInfo pinfo = boRecordType.GetProperty(propName);
            Debug.Assert(pinfo != null, "Die Eigenschaft " + propName + " existiert nicht im Business- Objekt " + boRecordType.Name);

            // Falls ein MapPropertytoColName- Attribut existiert, wird der Spaltenname aus diesem gelesen und 
            // zurückgegeben, sonst der Name der Eigenschaft
            if (pinfo.GetCustomAttributes(typeof(MapPropertyToColNameAttribute), false).Count() > 0)
            {
                MapPropertyToColNameAttribute att = pinfo.GetCustomAttributes(typeof(MapPropertyToColNameAttribute), false)[0] as MapPropertyToColNameAttribute;
                return att == null ? propName : att.ColName;
            }
            else
                return propName;
        }


        //------------------------------------------------------------------------------------------
        // Sortieren

        string _defaultSortCol;

        // Sortierkriterium
        protected string _sortColname;
        protected bool _sortDesc = true;

        /// <summary>
        /// Legt die Spalte fest, nach der sortiert werden soll
        /// </summary>
        public string SortColumn
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _sortColname = value;
            }
        }

        /// <summary>
        /// Legt fest, ob aufsteigend oder absteigend sortiert werden soll
        /// </summary>
        public System.Web.UI.WebControls.SortDirection SortDirection
        {
            set
            {
                _sortDesc = value == System.Web.UI.WebControls.SortDirection.Descending ? true : false;
            }
            get
            {
                return _sortDesc ? System.Web.UI.WebControls.SortDirection.Descending :
                                   System.Web.UI.WebControls.SortDirection.Ascending;
            }
        }              


        /// <summary>
        /// Implementiert das Sortieren nach beliebiger Spalte für die Objektdatasource
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public IQueryable<TEntity> sort(IQueryable<TEntity> tab)
        {
            if (string.IsNullOrEmpty(_sortColname))
                _sortColname = _defaultSortCol;

            if (tab == null)
                return null;
            if (tab.Count() == 0)
                return tab;
            if (string.IsNullOrEmpty(_sortColname))
                // Fall: keine Sortierung erwünscht
                return tab;
            else if (typeof(TEntityView).GetProperty(_sortColname) != null)
            {
                // Fall: Die Spalte stammt aus der aktuellen View

                // Namen der Eigenschaft einer EntityView auf die Eigenschaft eines zugrundeliegenden Entity abbilden
                string Colname = mapPropertyToColName(typeof(TEntityView), _sortColname);

                // Sortieren bezüglich abgeleiteter Spalten in den Views
                IQueryable<TEntity> tabff;


                if (sortByDerivat(tab, Colname, _sortDesc, out tabff))
                    return tabff;

                // Wenn die Spalte keine abgeleitete Spalte ist, dann sortieren nach den tatsächlichen Spalten
                return OrderFunc<TEntity>(tab, Colname, _sortDesc);

            } throw BoFilterAndSortEfEntitiesBaseException.Create("sort", null);
        }

        /// <summary>
        /// Für eine Entity- Auflistung wird eine spezielle ESQL- Abfrage formuliert, die die gewünschte 
        /// Sortierung bezüglich einer Spalte durchführt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tab"></param>
        /// <param name="ColName"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        protected IQueryable<TEntity> OrderFunc<T>(IQueryable<TEntity> tab, string ColName, bool desc)
        {

            // Folgender Ausdruck ist dank der Beispielbibliothek DynamicQuery aus Microsoft CSharpSamples.zip möglich. 
            // Diese kann bezogen werden über http://msdn.microsoft.com/en-us/vcsharp/bb894665.aspx
            // Die Bibliothek stellt Erweiterungsmethoden bereit, mit denen zur Laufzeit aus Sql- Termen Linq- Eypressions
            // erzeugt werden. Die Standard- Bibliothek (Linq To SQL oder Entity Framework) bieten diese Funktion nicht !
            // Mit Entity SQL können lediglich ObjectQuerys erzeugt werden, die direkt auf den Entity- Auflistungen operieren.
            // Diese Erweiterungsmethode hier hingegen erweitert eine Linq- Expression, die schließlich wieder in reines SQL gewandelt an
            // den SQL- Server zur Ausführung gesendet wird.
            return tab.OrderBy(ColName + (desc ? " DESC" : " ASC"));            
        }

        /// <summary>
        /// Soll nach Eigenschaften sortiert werden aus Entities <> TEntity (z.B. Entities, die mit dem aktuell betrachteten in
        /// einer Master-Detail Beziehung stehen), dann können die dazu notwendigen speziellen Sortierterme in hier implementiert 
        /// werden.
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="ColName"></param>
        /// <param name="desc"></param>
        /// <param name="tabOrdered"></param>
        /// <returns></returns>
        protected abstract bool sortByDerivat(IQueryable<TEntity> tab, string ColName, bool desc, out IQueryable<TEntity> tabOrdered);

        /// <summary>
        /// Hilfmethode zum Implementieren von SortByDerivat
        /// </summary>
        /// <typeparam name="TKey">EF- Entitytyp</typeparam>
        /// <param name="tabUnordered">unsortierte Entityliste</param>
        /// <param name="desc">Aufsteigend od. absteigend sortieren</param>
        /// <param name="keySelector">Lambdaausdruck, der die Spalte festlegt, nach der sortiert werden soll</param>
        /// <returns></returns>
        protected IQueryable<TEntity> sortHlp<TKey>(IQueryable<TEntity> tabUnordered, bool desc, Func<TEntity, TKey> keySelector)
        {
            return desc ?
                tabUnordered.OrderByDescending(keySelector).AsQueryable() :
                tabUnordered.OrderBy(keySelector).AsQueryable();
        }
        

        /// <summary>
        /// Liefert die Auflistung vom Typ TEntites aus dem EF- Objektkontext zurück
        /// </summary>
        public abstract IQueryable<TEntity> EntityCollection
        {
            get;
        }

        /// <summary>
        /// Erzeugt eine spezielle View in einer abgeleiteten Klasse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract TEntityView CreateView(TEntity entity);

        //------------------------------------------------------------------------------------------
        // Filtern
        
        /// <summary>
        /// Liste aller Filter
        /// </summary>
        public List<Filter<TEntity>> AllFilter = new List<Filter<TEntity>>();

        /// <summary>
        /// Zuweisen eines Filter zur Liste der Filter
        /// </summary>
        /// <param name="srcFilter"></param>
        public void LetFilter(Filter<TEntity> srcFilter)
        {
            AllFilter.Add(srcFilter);
        }

        /// <summary>
        /// Muss in abgeleiteten Klassen mit der gewünschten funktionalität zur Konstruktion eines Filters aus einer Auswahl
        /// und der Filterung überschrieben werden
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public IQueryable<TEntity> filter(IQueryable<TEntity> tab)
        {
            if (tab == null)
                return null;
            if (AllFilter.Count == 0)
                // Fall: keine Filterung erwünscht
                return tab;
            else
            {
                // Anwenden aller Filter
                foreach (Filter<TEntity> flt in AllFilter)
                {
                    // Hier wird sukkzesive eine "Where- Klausel" durch UND- Verknüpfung der Filterterme aufgebaut
                    tab = flt.filterImpl(tab);
                }

                return tab;
            }
        }

        /// <summary>
        /// Liefert die Anzahl der Datensätze, die nach der Filterung im Ergebnis verbleiben
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public int selectCount(string sortType)
        {
            try
            {
                return filter(EntityCollection).Count();
            }
            catch (Exception ex)
            {
                throw BoFilterAndSortEfEntitiesBaseException.Create("selectCount", ex);
            }
        }

        /// <summary>
        /// Liefert einen Ausschnitt aus dre mit den aktuellen Einstellungen gefilterte und sortierte Menge zurück.
        /// </summary>
        /// <param name="sortType"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IQueryable<TEntityView> selectSorted(string sortType, int StartRowIndex, int PageSize)
        {
            try
            {
                int entityCount = selectCount("");
                if (entityCount > 0)
                {
                    var views = new List<TEntityView>(entityCount);

                    foreach (var entity in sort(filter(EntityCollection)).Skip(StartRowIndex).Take(PageSize))
                    {
                        views.Add(CreateView(entity));
                    }
                    return views.AsQueryable();
                }
                else return (new List<TEntityView>()).AsQueryable();
            }
            catch (Exception ex)
            {
                throw BoFilterAndSortEfEntitiesBaseException.Create("selectSorted", ex);
            }
        }

        /// <summary>
        /// Liefert die mit den aktuellen Einstellungen gefilterte und sortierte Menge zurück
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public IQueryable<TEntityView> selectSorted(string sortType)
        {
            try
            {
                int entityCount = selectCount("");
                if (entityCount > 0)
                {
                    var views = new List<TEntityView>(entityCount);

                    foreach (var entity in sort(filter(EntityCollection)))
                    {
                        views.Add(CreateView(entity));
                    }
                    return views.AsQueryable();
                }
                else return (new List<TEntityView>()).AsQueryable();
            }
            catch (Exception ex)
            {
                throw BoFilterAndSortEfEntitiesBaseException.Create("selectSorted", ex);
            }
        }
    }
}
