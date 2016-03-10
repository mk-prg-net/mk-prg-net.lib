using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace mkoIt.Asp
{
    /// <summary>
    /// Entities, die aus Master-Detail- Beziehungen bestehen, können durch verschiedene
    /// Views dargestellt werden. Diese Klasse organisiert das Filtern und sortieren 
    /// solcher "mehrseitiger" Darstellungen solcher komplexer Entities z.B. in Gridviews.
    /// Dabei wird jede View durch eine Gridview dargestellt. Alle GridViews werden
    /// bezüglich Filtern und sortieren synchronisiert.
    /// </summary>
    /// <typeparam name="TDataContext">Linq- Datakontext</typeparam>
    /// <typeparam name="TEntity">Entity- Klasse aus dem Linq- Datakontext</typeparam>
    /// <typeparam name="TEntityView">Klasse, die das Entity und seine Detaildaten abbildet zum Zweck der Darstellung z.B. in einer Gridview</typeparam>
    public abstract class FilterAndSortEntities<TDataContext, TEntity, TEntityView>
        : OdsToLinq<TDataContext, TEntityView>
        where TDataContext : System.Data.Linq.DataContext, new()
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="DefaultSortCol"></param>
        /// <param name="otherTEntityViews">Liste der Typen der restlichen Views vom Entity</param>
        public FilterAndSortEntities(TDataContext ctx, string DefaultSortCol)
            : base(ctx)
        {
            _sortColname = _defaultSortCol = DefaultSortCol;
        }

        /// <summary>
        /// Liste der Typen aller EntityViews zu dem TEntity
        /// </summary>
        /// <returns></returns>
        protected abstract Type[] AllEntityViewTypes();

        /// <summary>
        /// Liefert zum Typ einer Entityview den Typ des Geschäftsobjektes, das dieses verwaltet
        /// </summary>
        /// <param name="typeTEntityView"></param>
        /// <returns></returns>
        protected abstract Type GetBoThatInclude(Type typeTEntityView);

        /// <summary>
        /// Ordnet dem Namen einer Eigenschaft den zugehörigen Namen der Tabellenspalte in der Datenbanktabelle zu
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static string mapPropertyToColName(Type boRecordType, string propName)
        {
            if (typeof(TEntity) == typeof(TEntityView))
                return propName;

            System.Reflection.PropertyInfo pinfo = boRecordType.GetProperty(propName);
#if (DEBUG)
            if (pinfo == null)
            {
                // Liste aller Eigenschaften
                Debug.WriteLine(boRecordType.Name + "-Eigenschaften:");
                foreach (System.Reflection.PropertyInfo pi in boRecordType.GetProperties())
                    Debug.WriteLine("  + hat Eigenschaft " + pi.Name);
            }
#endif
            Debug.Assert(pinfo != null, "Die Eigenschaft " + propName + " existiert nicht im Business- Objekt " + boRecordType.Name);

            // Falls ein MapPropertytoColName- Attribut existiert, wird der Spaltenname aus diesem gelesen und 
            // zurückgegeben, sonst der Name der Eigenschaft
            if (pinfo.GetCustomAttributes(typeof(mkoIt.Db.MapPropertyToColNameAttribute), false).Count() > 0)
            {
                mkoIt.Db.MapPropertyToColNameAttribute att = pinfo.GetCustomAttributes(typeof(mkoIt.Db.MapPropertyToColNameAttribute), false)[0] as mkoIt.Db.MapPropertyToColNameAttribute;
                return att == null ? propName : att.ColName;
            }
            else
                return propName;
        }


        //------------------------------------------------------------------------------------------
        // Filtern

        // Liste aller Filter
        public List<Db.Filter<TEntity>> AllFilter = new List<Db.Filter<TEntity>>();

        public void LetFilter(Db.Filter<TEntity> srcFilter)
        {
            AllFilter.Add(srcFilter);
        }

        object prepareValue(object val)
        {
            return val;
        }

        public int filterRowsCount(IQueryable<TEntity> tab)
        {
            return filter(tab).Count();
        }

        public IQueryable<TEntity> filter(IQueryable<TEntity> tab)
        {
            if (tab == null)
                return null;
            if (AllFilter.Count == 0)
                // Fall: keine Filterung erwünscht
                return tab;
            else
            {
                // Wandeln in Array, damit Linq to Object angewendet werden kann
                //TEntity[] tabA = tab.ToArray();

                // Anwenden aller Filter
                foreach (Db.Filter<TEntity> flt in AllFilter)
                {
                    tab = flt.filterImpl(tab);
                }

                return tab;
            }
        }

        //protected abstract bool filterByDerivat(IQueryable<TEntity> tab, string ColNameOri, FilterFunctor filterFunctor, out IQueryable<TEntity> tabFiltered);

        //------------------------------------------------------------------------------------------
        // Sortieren

        string _defaultSortCol;

        // Sortierkriterium
        protected string _sortColname;
        protected bool _sortDesc = true;
        public string SortColumn
        {
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _sortColname = value;
            }
        }

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

        public IQueryable<TEntity> sort(IQueryable<TEntity> tab)
        {
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
                return base.OrderFunc<TEntity>(tab, Colname, _sortDesc);
            }
            else
            {
                // Fall: Die Spalte stammt aus einer anderen View als der aktuellen
                // Feststellen, aus welcher View der ColName stammt                
                foreach (Type tview in AllEntityViewTypes())
                {
                    // Nur Views != aktuellen View betrachten
                    if (tview != typeof(TEntityView))
                    {
                        System.Reflection.PropertyInfo pinfo = tview.GetProperty(_sortColname);
                        if (pinfo != null)
                        {

                            // Instanz des Sortieres für die andere View erstellen
                            Type[] constuctorArgTypes = Type.EmptyTypes;
                            System.Reflection.ConstructorInfo ctor = GetBoThatInclude(tview).GetConstructor(constuctorArgTypes);
                            Debug.Assert(ctor != null, "Der Konstruktor der Business Objekt");
                            object[] constructorArgs = { };
                            object filterAndSort = ctor.Invoke(constructorArgs);

                            // Eigenschaften des neuen Business- Objektes setzen
                            System.Reflection.PropertyInfo propSort = GetBoThatInclude(tview).GetProperty("SortColumn");
                            propSort.SetValue(filterAndSort, _sortColname, null);

                            System.Reflection.PropertyInfo propSortDir = GetBoThatInclude(tview).GetProperty("SortDirection");
                            propSortDir.SetValue(filterAndSort, SortDirection, null);

                            // sort- Methode aufrufen
                            Type[] sortArgTypes = new Type[] { typeof(IQueryable<TEntity>) };
                            System.Reflection.MethodInfo miSort = GetBoThatInclude(tview).GetMethod("sort", sortArgTypes);
                            Debug.Assert(miSort != null, "Die Sortiermethode ist im Business- Objekt nicht enthalten");
                            object[] sortArgs = { tab };
                            return miSort.Invoke(filterAndSort, sortArgs) as IQueryable<TEntity>;
                        }
                    }
                }

                return tab;
            }
        }

        protected IQueryable<TEntity> sortHlp<TKey>(IQueryable<TEntity> tabUnordered, bool desc, Func<TEntity, TKey> keySelector)         
        {
            return desc ?
                tabUnordered.OrderByDescending(keySelector).AsQueryable() :
                tabUnordered.OrderBy(keySelector).AsQueryable();
        }

        protected abstract bool sortByDerivat(IQueryable<TEntity> tab, string ColName, bool desc, out IQueryable<TEntity> tabOrdered);
        
        protected static IQueryable<T> OrderByColumn<T, TRes>(bool desc, IQueryable<T> tab, Func<T, TRes> sel)
        {
            return  desc ? tab.OrderByDescending(sel).AsQueryable() : tab.OrderBy(sel).AsQueryable();
        }


    }
}
