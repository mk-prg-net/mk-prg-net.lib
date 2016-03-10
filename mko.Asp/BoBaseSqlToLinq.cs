//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 17.11.2011
//
//  Projekt.......: mkoItAsp
//  Name..........: BoBase.cs
//  Aufgabe/Fkt...: Basisklasse für die Verarbeitung von Geschäftsobjektlisten
//                  
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
//  Version 1.0...: Herbst 2009
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.8.2011
//  Änderungen....: Integration von DynamicQuery, EntityCollection und Implementierung von 
//                  select in der Basisklasse
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.11.2011
//  Änderungen....: Erweitern des Sortieren bzgl. meherer Spalten 
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
    public abstract class BoBaseSqlToLinq<TORMContext, TEntity, TEntityId, TEntityView>
        : IDisposable
        where TORMContext : System.Data.Linq.DataContext, new()
        where TEntity : class
        where TEntityView : class, new()
    {
        /// <summary>
        /// Spezielle Exceptionklasse
        /// </summary> 
        [Serializable]
        public class BoBaseException : System.ApplicationException
        {
            public static BoBaseException Create(string Message)
            {
                string msg = string.Format("Err: BoBaseException{0}: {1}", typeof(TEntity).Name, Message);
                Debug.WriteLine(msg);
                return new BoBaseException(msg);
            }

            public static BoBaseException Create(Exception ex)
            {
                string msg = string.Format("Err: BoBaseException{0}: {1}", typeof(TEntity).Name, ex.Message);
                Debug.WriteLine(msg);
                return new BoBaseException(msg, ex.InnerException);
            }

            public static BoBaseException Create(string MethodName, Exception ex)
            {
                string msg = string.Format("Err: BoBaseException{0}.{1}: {2}", typeof(TEntity).Name, MethodName, ex.Message);
                Debug.WriteLine(msg);
                return new BoBaseException(msg, ex.InnerException);
            }

            // Konstruktoren
            private BoBaseException(string message) : base(message) { }
            private BoBaseException(string message, Exception innerException) : base(message, innerException) { }
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
        /// Konstruktor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="DefaultSortCol"></param>
        /// <param name="otherTEntityViews">Liste der Typen der restlichen Views vom Entity</param>
        public BoBaseSqlToLinq(TORMContext ctx, string DefaultSortCol)
        {
            _ctx = ctx;
            _sortColname = _defaultSortCol = DefaultSortCol;
        }

        /// <summary>
        /// Gibt den Objektkontext zurück, über dem alle EF- Entityauflistungen erreichbar sind
        /// </summary>
        TORMContext _ctx;
        public TORMContext ORMContext
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
                            object boOther = ctor.Invoke(constructorArgs);

                            // Eigenschaften des neuen Business- Objektes setzen
                            System.Reflection.PropertyInfo propSort = GetBoThatInclude(tview).GetProperty("SortColumn");
                            propSort.SetValue(boOther, _sortColname, null);

                            System.Reflection.PropertyInfo propSortDir = GetBoThatInclude(tview).GetProperty("SortDirection");
                            propSortDir.SetValue(boOther, SortDirection, null);

                            // sort- Methode aufrufen
                            Type[] sortArgTypes = new Type[] { typeof(IQueryable<TEntity>) };
                            System.Reflection.MethodInfo miSort = GetBoThatInclude(tview).GetMethod("sort", sortArgTypes);
                            Debug.Assert(miSort != null, "Die Sortiermethode ist im Business- Objekt nicht enthalten");
                            object[] sortArgs = { tab };
                            return miSort.Invoke(boOther, sortArgs) as IQueryable<TEntity>;
                        }
                    }
                }

                //return tab;
                // Fehler, falls Spalte nicht gefunden wurde
                throw BoBaseException.Create("sort", null);
            }
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


        //----------------------------------------------------------------------------------------------
        // Multisort

        public bool MultisortOn { get; set; }

        /// <summary>
        /// Beschreibt nach welcher Spalte wie sortiert werden soll
        /// </summary>
        public class SortColumnDef
        {
            public SortColumnDef() { SortDescending = false; }

            /// <summary>
            /// Typ der View, aus der die sortierende Spalte stammt
            /// </summary>
            public Type ViewType { get; set; }

            /// <summary>
            /// Name der Spalte, nach der sortiert werden soll
            /// </summary>
            public string ColName { get; set; }

            /// <summary>
            /// Soll absteigend (true) oder aufsteigend sortiert werden ?
            /// </summary>
            public bool SortDescending { get; set; }
        }

        /// <summary>
        /// Listet alle Spalten auf, bezüglich der sortiert werden soll.
        /// </summary>
        public List<SortColumnDef> SortJob = new List<SortColumnDef>();


        /// <summary>
        /// Sortieren bezüglich mehrerer Spalten wird hier implementiert
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public IQueryable<TEntity> multiSort(IQueryable<TEntity> tab)
        {
            foreach (SortColumnDef sc in SortJob)
            {
                tab = sort(tab, sc);
            }

            return tab;
        }

        /// <summary>
        /// Funktion gibt eine Linq To SQL Expression zurück, welche bezüglich der gewählten Spalte 
        /// in der gewählten Richtung sortiert.
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="ViewColName"></param>
        /// <param name="SortDescending"></param>
        /// <returns></returns>
        public IQueryable<TEntity> sort(IQueryable<TEntity> tab, SortColumnDef sc)
        {
            Debug.Assert(!string.IsNullOrEmpty(sc.ColName));
            Debug.Assert(tab != null);
            Debug.Assert(tab.Count() > 0);


            if (sc.ViewType == typeof(TEntityView))
            {
                // Fall: Die Spalte stammt aus der aktuellen View
                Debug.Assert(typeof(TEntityView).GetProperty(sc.ColName) != null);

                // Namen der Eigenschaft einer EntityView auf die Eigenschaft eines zugrundeliegenden Entity abbilden
                string Colname = mapPropertyToColName(typeof(TEntityView), sc.ColName);

                // Sortieren bezüglich abgeleiteter Spalten in den Views
                IQueryable<TEntity> tabSorted;
                if (sortByDerivat(tab, Colname, sc.SortDescending, out tabSorted))
                    return tabSorted;

                // Wenn die Spalte keine abgeleitete Spalte ist, dann sortieren nach den tatsächlichen Spalten
                return OrderFunc<TEntity>(tab, Colname, sc.SortDescending);

            }
            else
            {
                // Fall: Die Spalte stammt aus einer anderen View als der aktuellen

                System.Reflection.PropertyInfo pinfo = sc.ViewType.GetProperty(_sortColname);
                Debug.Assert(pinfo != null);

                // Businessobjekt für andere View erstellen
                Type[] constuctorArgTypes = Type.EmptyTypes;
                System.Reflection.ConstructorInfo ctor = GetBoThatInclude(sc.ViewType).GetConstructor(constuctorArgTypes);
                Debug.Assert(ctor != null);

                object[] constructorArgs = { };
                object boOther = ctor.Invoke(constructorArgs);

                // sort- Methode des anderen Businessobjekts aufrufen
                Type[] sortArgTypes = new Type[] { typeof(IQueryable<TEntity>), typeof(SortColumnDef) };
                System.Reflection.MethodInfo miSort = GetBoThatInclude(sc.ViewType).GetMethod("sort", sortArgTypes);
                Debug.Assert(miSort != null, "Die Sortiermethode ist im Business- Objekt nicht enthalten");
                object[] sortArgs = { tab, sc };
                return miSort.Invoke(boOther, sortArgs) as IQueryable<TEntity>;
            }
        }


        /// <summary>
        /// Liefert die Auflistung vom Typ TEntites aus dem EF- Objektkontext zurück
        /// </summary>
        public abstract IQueryable<TEntity> EntityCollection
        {
            get;
        }

        /// <summary>
        /// Erzeugt die spezielle View einer abgeleiteten Klasse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract TEntityView CreateView(TEntity entity);

        /// <summary>
        /// Erzeugen einer Dummyzeile aktivieren oder deaktivieren
        /// </summary>
        bool _CreateDummyOn = false;
        public bool CreateDummyOn
        {
            get
            {
                return _CreateDummyOn;
            }

            set
            {
                _CreateDummyOn = value;
            }
        }


        /// <summary>
        /// Erzeugt einen neuen, leeren EntityView. In diesen können anschließend über 
        /// ein Websteuerelement (z.B. GridView) die Werte für einen anschließend in der DB
        /// neu aufzunehmenden Datensatz eingegeben werden. 
        /// Wurde entwickelt, um auch Websteuerelemente mit fehleder Insert- Funktion (GridView)
        /// zu befähigen, Inserts inline durchzuführen.
        /// </summary>
        /// <returns></returns>
        public virtual TEntityView createDummy()
        {
            return null;
        }


        //------------------------------------------------------------------------------------------
        // Filtern

        /// <summary>
        /// Liste aller Filter
        /// </summary>
        public List<Db.Filter<TEntity>> AllFilter = new List<Db.Filter<TEntity>>();

        /// <summary>
        /// Zuweisen eines Filter zur Liste der Filter
        /// </summary>
        /// <param name="srcFilter"></param>
        public void LetFilter(Db.Filter<TEntity> srcFilter)
        {
            AllFilter.Add(srcFilter);
        }

        /// <summary>
        /// Beschreibung der Filter zurückgeben
        /// </summary>
        /// <returns></returns>
        public string FilterDescription()
        {
            // Aufbauen der aktuellen Filterbeschreibung
            StringBuilder bldFilterDescr = new StringBuilder();
            foreach (var filterFunktor in AllFilter)
            {
                bldFilterDescr.AppendFormat("[{0}] ", filterFunktor.Description);
            }

            return bldFilterDescr.ToString();
        }

        /// <summary>
        /// Hier können vom Anwender Anmerkungen hinzugefügt werden. Diese werden 
        /// im EntitySetDescriptor aufgenommen
        /// </summary>
        /// 
        string _UserAnnotations = "";
        public string UserAnnotations
        {
            get
            {
                return _UserAnnotations;
            }
            set
            {
                _UserAnnotations = value;
            }
        }

        /// <summary>
        /// Eintrag in einem EntitySetDescriptor
        /// </summary>
        public class EntitySetDescriptorEntry
        {
            public string EntryName { get; set; }
            public string Description { get; set; }
        }

        /// <summary>
        /// Gibt zusätzliche Informationen über die aktuell durch Filter definierte 
        /// Menge von Geschäftsobjekten zurück. Dazu gehören z.B. die Anzahl der Datensätze,
        /// Anmerkungen vom Benutzer und eine informelle Beschreibung der Filter.
        /// Diese Informationen können z.B. in Berichten im Kopf ausgegeben werden.
        /// </summary>
        /// <returns></returns>
        public IQueryable<EntitySetDescriptorEntry> GetEntitySetDescriptor()
        {
            var descriptor = new List<EntitySetDescriptorEntry>();

            descriptor.Add(new EntitySetDescriptorEntry()
            {
                EntryName = "Anz. Datensätze",
                Description = selectCount("").ToString()
            });

            descriptor.Add(new EntitySetDescriptorEntry()
            {
                EntryName = "Angewendete Filter",
                Description = FilterDescription()
            });

            descriptor.Add(new EntitySetDescriptorEntry()
            {
                EntryName = "Anmerkungen",
                Description = UserAnnotations
            });

            return descriptor.AsQueryable();
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
                foreach (Db.Filter<TEntity> flt in AllFilter)
                {
                    // Hier wird sukkzesive eine "Where- Klausel" durch UND- Verknüpfung der Filterterme aufgebaut
                    tab = flt.filterImpl(tab);
                }

                return tab;
            }
        }

        //----------------------------------------------------------------------------------------------------------------
        // CRUD Methoden

        /// <summary>
        /// Liefert die Anzahl der Datensätze, die nach der Filterung im Ergebnis verbleiben
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public int selectCount(string sortType)
        {
            try
            {
                int count = filter(EntityCollection).Count();

                // Wenn ein Dummy erzeugt wird, dann ist dieser mitzuzählen
                var dummy = createDummy();
                return dummy == null ? count : count + 1;
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("selectCount", ex);
            }
        }

        public int selectCount(string sortType, int StartRowIndex, int PageSize)
        {
            try
            {
                int count = filter(EntityCollection).Count();

                // Wenn ein Dummy erzeugt wird, dann ist dieser mitzuzählen
                var dummy = createDummy();
                return dummy == null ? count : count + 1;
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("selectCount", ex);
            }
        }



        /// <summary>
        /// Zugriff auf ein Entity, das durch seine id identifiziert wird. Muß in abgeleiteten
        /// Klassen implementiert werden. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract IQueryable<TEntityView> selectById(TEntityId id);


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
                var views = new List<TEntityView>(PageSize);

                // Dummy für die definition neuer Datensätze am Anfang aufnehmen
                var dummy = createDummy();
                if (dummy != null)
                    views.Add(dummy);

                foreach (var entity in sort(filter(EntityCollection)).Skip(StartRowIndex).Take(PageSize))
                {
                    views.Add(CreateView(entity));
                }
                return views.AsQueryable();
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("selectSorted", ex);
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

                    // Dummy für die definition neuer Datensätze am Anfang aufnehmen
                    var dummy = createDummy();
                    if (dummy != null)
                        views.Add(dummy);

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
                throw BoBaseException.Create("selectSorted", ex);
            }
        }


        /// <summary>
        /// Einfügen eines neuen Entity, dessen Eigenschaften im View definiert wurden
        /// </summary>
        /// <param name="view"></param>
        public abstract void Insert(TEntityView view);

        /// <summary>
        /// Aktualisieren eines Entity mit den Daten aus der View
        /// </summary>
        /// <param name="view"></param>
        public abstract void Update(TEntityView view);

        /// <summary>
        /// Löschen des in der View beschribenen Entity
        /// </summary>
        /// <param name="view"></param>
        public abstract void Delete(TEntityView view);

        /// <summary>
        /// Löschen des durch die ID definierten Entity
        /// </summary>
        /// <param name="id"></param>
        public abstract void Delete(TEntityId id);

        /// <summary>
        /// Allg. Resourcenfreigabe
        /// </summary>
        public void Dispose()
        {
            ORMContext.Dispose();
        }
    }
}

