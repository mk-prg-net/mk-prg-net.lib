//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 17.11.2011
//
//  Projekt.......: mkoDb
//  Name..........: BoBase.cs
//  Aufgabe/Fkt...: Basisklasse für die Verarbeitung von Geschäftsobjektlisten
//                  Aus der Klasse GblDbLayer.OdsToLinq abgeleitet, die am 13.11.2010
//                  entwickelt wurde.
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
//  Version.......: 1.2
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.11.2011
//  Änderungen....: Erweitern des Sortieren bzgl. meherer Spalten 
//
//  Version.......: 1.3
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 13.3.2012
//  Änderungen....: Verschoben in Projekt mkoDb. Ziel ist die Verallgemeinerung auf 
//                  alle Projekte durch Loslösung von Web- Bibliothek.
//                  Eigenschaft SortDirection von Typ System.Web.UI.SortDirection auf 
//                  EnumSortDirection umgestellt.
//
//  Version.......: 1.4
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.5.2012
//  Änderungen....: Aufteilung in BoBase und BoBaseSqlToLinq zwecks nutzten der Basisklassenfunktionalität
//                  in Datasets <> SqlToLinq DataContext
//
//  Version.......: 1.5
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 23.7.2013
//  Änderungen....: Aufbauen auf Basisklasse BoBaseView, welche Änderungen aufzeichnet und an Entities nachvollzieht.
//                  Methoden GetEntitiesXXX(), GetEntitiyViewsXXX_AsQueryable() und GetEntityViews_AsObservable()... implementiert.
//                  Ersetzen die SelectXXX() Methoden.
//                  Update und Insert sind jetzt vollständig in Basisklasse implementiert.
//
//  Version.......: 1.6
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 8.8.2014
//  Änderungen....: Ersatz für SortByDerivat: 
//                  In der View sind statische Eigenschaften mit der Namenskonvention <PropertyName>_Sort anzulegen. Diese liefern
//                  im Getter einen Delegate der Art Func<IQueryable<TEntity>, bool, IQueryable<TEntity>>. Dieser Delegate stellt 
//                  den Sortierterm für die entsprechende Eigenschaft der View dar.
//                  Die Funktion Sort(...) prüft nun auf das vorhandensein einer solchen Eigenschaft. Ist sie vorhanden, dann wird mittels 
//                  des Delegate sortiert.
//                  Vorteil: Sortierlogik kann nun in der View gleich neben der Eigenschaft abgelegt, statt wie vorher weit entfernt in der 
//                  SortByDerivat. Zudem ist nun auch kein MapPropertyToColname- Attribut mehr notwendig.
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


namespace mkoIt.Db
{
    public abstract class BoBase<TEntity, TEntityId, TEntityView>
        where TEntity : class, new()
        where TEntityView : class, IEntityView<TEntity, TEntityId>, new()
    {


        /// <summary>
        /// Spezielle Exceptionklasse
        /// </summary> 
        [Serializable]
        public class BoBaseException : System.ApplicationException
        {
            public static BoBaseException Create(string Message)
            {
                string msg = mko.TraceHlp.FormatErrMsg("BoBase", "?", "Entity: " + typeof(TEntity).Name, "EntityView: " + typeof(TEntityView).Name, Message);
                Trace.WriteLine(TraceEnv.Switch.TraceError, msg);
                return new BoBaseException(msg);
            }

            public static BoBaseException Create(Exception ex)
            {
                string msg = mko.TraceHlp.FormatErrMsg("BoBase", "?", "Entity: " + typeof(TEntity).Name, "EntityView: " + typeof(TEntityView).Name, ex.Message);
                Trace.WriteLine(TraceEnv.Switch.TraceError, msg);
                return new BoBaseException(msg, ex.InnerException);
            }

            public static BoBaseException Create(string MethodName, Exception ex)
            {
                string msg = mko.TraceHlp.FormatErrMsg("BoBase", MethodName, "Entity: " + typeof(TEntity).Name, "EntityView: " + typeof(TEntityView).Name, ex.Message);
                Trace.WriteLine(TraceEnv.Switch.TraceError, msg);
                return new BoBaseException(msg, ex);
            }

            public static BoBaseException Create(object Obj, string MethodName, params string[] Messages)
            {
                var extMessages = new List<string>(Messages.Count() + 3);
                extMessages.Add("Entity: " + typeof(TEntity).Name);
                extMessages.Add("EntityView: " + typeof(TEntityView).Name);
                extMessages.AddRange(Messages);
                string msg = mko.TraceHlp.FormatErrMsg(Obj, MethodName, extMessages.ToArray());
                Trace.WriteLine(TraceEnv.Switch.TraceError, msg);
                return new BoBaseException(msg);
            }


            public static BoBaseException Create(object Obj, string MethodName, Exception ex, params string[] Messages)
            {
                var extMessages = new List<string>(Messages.Count() + 3);
                extMessages.Add("Entity: " + typeof(TEntity).Name);
                extMessages.Add("EntityView: " + typeof(TEntityView).Name);
                extMessages.AddRange(Messages);

                Debug.Assert(ex != null, "Im Parameter 3 (Exception ex) wurde einen null übergeben");
                extMessages.Add("Exception: " + ex.Message);
                string msg = mko.TraceHlp.FormatErrMsg(Obj, MethodName, extMessages.ToArray());
                Trace.WriteLine(TraceEnv.Switch.TraceError, msg);
                return new BoBaseException(msg, ex);
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
        public BoBase(string DefaultSortCol)
        {
            _sortColname = _defaultSortCol = DefaultSortCol;
        }


        /// <summary>
        /// Ordnet dem Namen einer Eigenschaft den zugehörigen Namen der Tabellenspalte in der Datenbanktabelle zu.
        /// Wenn der Typ der View ein selbstdefinierter ist (kein Entity aus EF), dann muß der Name der Eigenschaft mit der 
        /// Spalte der Tabelle übereinstimmen, oder mittels des Attributes mko.MapPropertyTocolNameAttribute wird der Eigenschaft der 
        /// Spaltenname zugeordnet.
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static string MapPropertyToColName(Type boRecordType, string propName)
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
        public EnumSortDirection SortDirection
        {
            set
            {
                _sortDesc = value == EnumSortDirection.Descending ? true : false;
            }
            get
            {
                return _sortDesc ? EnumSortDirection.Descending :
                                   EnumSortDirection.Ascending;
            }
        }


        /// <summary>
        /// Implementiert das Sortieren nach beliebiger Spalte für die Objektdatasource
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Sort(IQueryable<TEntity> tab)
        {
            if (MultisortOn)
            {
                if (tab == null)
                    return null;
                if (tab.Count() == 0)
                    return tab;
                else
                    return MultiSort(tab);
            }

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
                string Colname = MapPropertyToColName(typeof(TEntityView), _sortColname);

                // Sortieren bezüglich abgeleiteter Spalten in den Views
                IQueryable<TEntity> tabff;


                if (SortByDerivat(tab, Colname, _sortDesc, out tabff))
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
        /// Die Standardimplementierung belässt die Entitycolletion in ihrer unsortierten Form.
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="ColName"></param>
        /// <param name="desc"></param>
        /// <param name="tabOrdered"></param>
        /// <returns></returns>
        protected virtual bool SortByDerivat(IQueryable<TEntity> tab, string ColName, bool desc, out IQueryable<TEntity> tabOrdered)
        {
            tabOrdered = tab;
            return false;
        }

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

        public static IQueryable<TEntity> SortByEntityAttribute<TAttribute>(IQueryable<TEntity> UnorderedEntityCollection, bool desc, Func<TEntity, TAttribute> AttributeSelector)
        {
            return desc ?
                UnorderedEntityCollection.OrderByDescending(AttributeSelector).AsQueryable() :
                UnorderedEntityCollection.OrderBy(AttributeSelector).AsQueryable();
        }
        //----------------------------------------------------------------------------------------------
        // Multisort

        public bool MultisortOn { get; set; }

        /// <summary>
        /// 9.8.2014, mko
        /// Erzeugt einen Sortjob passend für diese Business- Objekt Collection
        /// </summary>
        /// <param name="ColName"></param>
        /// <param name="SortDescending"></param>
        /// <returns></returns>
        public static SortColumnDef CreateSortJob(string ColName, bool SortDescending = false)
        {
            return new SortColumnDef()
            {
                ColName = ColName,
                SortDescending = SortDescending,
                ViewType = typeof(TEntityView)
            };
        }


        /// <summary>
        /// Listet alle Spalten auf, bezüglich der sortiert werden soll.
        /// </summary>
        protected List<SortColumnDef> SortJobs = new List<SortColumnDef>();

        /// <summary>
        /// Definiert eine neue Liste von Sortierjobs. Die Reihenfolge wird berücksichtigt.
        /// </summary>
        /// <param name="sortJobDef"></param>
        public void SortJobDefine(params SortColumnDef[] sortJobDef)
        {
            SortJobs.Clear();
            foreach (var colDef in sortJobDef)
            {
                SortJobs.Add(colDef);
            }
        }


        /// <summary>
        /// Sortieren bezüglich mehrerer Spalten wird hier implementiert
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public IQueryable<TEntity> MultiSort(IQueryable<TEntity> tab)
        {
            foreach (SortColumnDef sc in SortJobs)
            {
                tab = Sort(tab, sc);
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
        public IQueryable<TEntity> Sort(IQueryable<TEntity> tab, SortColumnDef sc)
        {
            Debug.Assert(!string.IsNullOrEmpty(sc.ColName));
            Debug.Assert(tab != null);
            Debug.Assert(tab.Count() > 0);


            if (sc.ViewType == typeof(TEntityView))
            {
                // 8.8.2014, mko
                // Eigenschaft mit Sortierfunktion gemäß der Konvention <PropertyName>_Sort bestimmen
                var SortFunctionPropertyInfo = typeof(TEntityView).GetProperty(sc.ColName + "_Sort");
                if (SortFunctionPropertyInfo != null)
                {
                    // Getter der statischen Eigenschaft aufrufen
                    var SortFunc = (Func<IQueryable<TEntity>, bool, IQueryable<TEntity>>)SortFunctionPropertyInfo.GetAccessors()[0].Invoke(null, null);

                    Debug.Assert(SortFunc != null);
                    return SortFunc(tab, sc.SortDescending);
                }
                else
                {
                    // Klassische Vorgehensweise

                    var SortColPinfo = typeof(TEntityView).GetProperty(sc.ColName);

                    // Fall: Die Spalte stammt aus der aktuellen View
                    Debug.Assert(SortColPinfo != null);

                    // Namen der Eigenschaft einer EntityView auf die Eigenschaft eines zugrundeliegenden Entity abbilden
                    string Colname = MapPropertyToColName(typeof(TEntityView), sc.ColName);

                    // Sortieren bezüglich abgeleiteter Spalten in den Views
                    IQueryable<TEntity> tabSorted;
                    if (SortByDerivat(tab, Colname, sc.SortDescending, out tabSorted))
                        return tabSorted;

                    // Wenn die Spalte keine abgeleitete Spalte ist, dann sortieren nach den tatsächlichen Spalten
                    return OrderFunc<TEntity>(tab, Colname, sc.SortDescending);
                }

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
        /// Erzeugt ein neues Entity. Kann bei der Implementierung eines Insert- Befehls 
        /// eines Geschäftsobjektes eingesetzt werden, um z.B. den Schlüssel des Entities zu definieren
        /// </summary>
        /// <returns></returns>
        public abstract TEntity CreateEntity();

        /// <summary>
        /// Erzeugt ein Objekt einer von BoBaseView abgeleiteten Klasse.
        /// </summary>
        /// <returns></returns>
        public abstract TEntityView CreateEntityView();

        /// <summary>
        /// Erzeugt ein Objekt einer von BoBaseView abgeleiteten Klasse. Dieses wird mit einem Entity initialisiert.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected abstract TEntityView CreateEntityView(TEntity entity);

        private TEntityView _CreateEntityView(TEntity entity)
        {
            return CreateEntityView(entity);
        }

        /// <summary>
        /// Erzeugen einer Dummyzeile aktivieren oder deaktivieren.
        /// Dummyzeilen können in Steuerelementen eingesetzt werden, die kein Insert unsterstützen wie die 
        /// GridView in ASP.NET.
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


        //------------------------------------------------------------------------------------------
        // EntityViewListen erzeugen

        public class ObservableEntityViewCollection : ObservableEntityViewCollection<TEntity, TEntityId, TEntityView>
        {
            BoBase<TEntity, TEntityId, TEntityView> Bo;
            public ObservableEntityViewCollection(BoBase<TEntity, TEntityId, TEntityView> Bo)
            {
                this.Bo = Bo;
            }

            protected override TEntityView CreateEntityView(TEntity Entity)
            {
                return Bo.CreateEntityView(Entity);
            }
        }

        public ObservableEntityViewCollection CreateObservableEntityViewCollection()
        {
            return new ObservableEntityViewCollection(this);
        }


        //------------------------------------------------------------------------------------------
        // Filtern

        /// <summary>
        /// Liste aller Filter
        /// </summary>
        public Dictionary<Filter<TEntity>, Filter<TEntity>> AllFilter = new Dictionary<Filter<TEntity>, Filter<TEntity>>();

        /// <summary>
        /// Zuweisen eines Filter zur Liste der Filter
        /// </summary>
        /// <param name="srcFilter"></param>
        public void SetFilter(Filter<TEntity> srcFilter)
        {
            AllFilter.Add(srcFilter, srcFilter);
        }

        private string RemoveFilterErrorMsg(Filter<TEntity> flt)
        {
            if (string.IsNullOrEmpty(flt.Description))
                return "Ein " + flt.GetType().Name + " Filter ist bereits vorhanden";
            else
                return "Ein " + flt.GetType().Name + " Filter (" + flt.Description + ") ist bereits vorhanden";
        }

        /// <summary>
        /// Entfernt ein Filter aus dem Bo
        /// </summary>
        /// <param name="flt"></param>
        public void RemoveFilter(Filter<TEntity> flt)
        {
            if (AllFilter.ContainsKey(flt))
                AllFilter.Remove(flt);
            else
                throw BoBaseException.Create(this, "RemoveFilter", RemoveFilterErrorMsg(flt));
        }

        public void RemoveAllFilters()
        {
            AllFilter.Clear();
        }

        /// <summary>
        /// Beschreibung der Filter zurückgeben
        /// </summary>
        /// <returns></returns>
        public string FilterDescription()
        {
            // Aufbauen der aktuellen Filterbeschreibung
            StringBuilder bldFilterDescr = new StringBuilder();
            foreach (var filterFunctor in AllFilter.Keys)
            {
                bldFilterDescr.AppendFormat("[{0}] ", filterFunctor.Description);
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
                Description = CountFilteredEntities().ToString()
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
        /// Muss in abgeleiteten Klassen mit der gewünschten Funktionalität zur Konstruktion eines Filters aus einer Auswahl
        /// und der Filterung überschrieben werden
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public IQueryable<TEntity> FilterEntities(IQueryable<TEntity> tab)
        {
            if (tab == null)
                return null;
            if (AllFilter.Count == 0)
                // Fall: keine Filterung erwünscht
                return tab;
            else
            {
                // Anwenden aller Filter
                foreach (Filter<TEntity> flt in AllFilter.Keys)
                {
                    // Hier wird sukkzesive eine "Where- Klausel" durch UND- Verknüpfung der Filterterme aufgebaut
                    tab = flt.filterImpl(tab);
                }

                return tab;
            }
        }

        //----------------------------------------------------------------------------------------------------------------
        // Test auf Id       

        /// <summary>
        /// Liefert einen Lambda- Ausdruck zurück, mittels der ein Entity auf seine ID abbildet
        /// Entity --> ID
        /// </summary>
        /// <returns></returns>
        public abstract Func<TEntity, bool> GetEntityIDTest(TEntityId id);

        /// <summary>
        /// Erzeugt ein Filter, welches nur Datensätze passieren lässt mit der übergebenen Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //public abstract Db.FilterFunctor<TEntity, TEntityId> CreateIdFilter(TEntityId Id);

        //----------------------------------------------------------------------------------------------------------------
        // Existenztests

        /// <summary>
        /// Prüft, ob die gefilterte Menge überhaupt ein Element enthält
        /// </summary>
        /// <returns></returns>
        public bool Any()
        {
            try
            {
                return FilterEntities(EntityCollection).Any();
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("Any", ex);
            }
        }


        //----------------------------------------------------------------------------------------------------------------
        // Teilmengen von Entities oder EntityViews berechnen

        /// <summary>
        /// Zugriff auf alle Entities
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetEntities()
        {
            try
            {
                return EntityCollection;
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetEntities", ex);
            }
        }

        /// <summary>
        /// 25.7.2014, mko    
        /// Zugriff auf einzelnes Entity mit der gegebenen id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetEntity(TEntityId id)
        {
            return EntityCollection.FirstOrDefault(GetEntityIDTest(id));
        }

        /// <summary>
        /// Wendet auf die Menge der Entities alle definierten Filter an. Das Ergebnis ist die 
        /// gefilterte Menge
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetEntitiesFiltered()
        {
            try
            {
                return FilterEntities(EntityCollection);
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("ApplyFilter", ex);
            }
        }

        /// <summary>
        /// Wendet auf die Menge der Entites alle definierten Filter an. Das Ergebnis ist die 
        /// gefilterte Menge. Diese wird anschließend sortiert
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetEntitiesFilteredAndSorted()
        {
            try
            {
                if (MultisortOn)
                    return MultiSort(FilterEntities(EntityCollection));
                else
                    return Sort(FilterEntities(EntityCollection));
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetEntitiesFilteredAndSorted", ex);
            }
        }


        /// <summary>
        /// Zählt alle Entities nach der Filterung durch.
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public int CountFilteredEntities()
        {
            try
            {
                return FilterEntities(EntityCollection).Count();
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("CountFilteredEntities", ex);
            }
        }

        /// <summary>
        /// Zählt alle Entities durch
        /// </summary>
        /// <returns></returns>
        public int CountAllEntities()
        {
            try
            {
                return EntityCollection.Count();
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("CountAllEntities", ex);
            }
        }

        /// <summary>
        /// Zählt alle Entites nach der Filterung durch. Da GetViewXXX- Funktionen einen Leere Zeile einfügen,
        /// wenn CreateDummyOn == true, wird in diesem Fall um 1 erhöht.
        /// </summary>
        /// <returns></returns>
        public int CountFilteredViews()
        {
            try
            {
                int count = FilterEntities(EntityCollection).Count();

                // Wenn ein Dummy erzeugt wird, dann ist dieser mitzuzählen
                return CreateDummyOn ? count + 1 : count;
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("CountFilteredEntities", ex);
            }
        }

        /// <summary>
        /// Zählt alle Entites durch. Da GetViewXXX- Funktionen einen Leere Zeile einfügen,
        /// wenn CreateDummyOn == true, wird in diesem Fall um 1 erhöht.
        /// </summary>
        /// <returns></returns>
        public int CountAllViews()
        {
            try
            {
                int count = EntityCollection.Count();
                return CreateDummyOn ? count + 1 : count;
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("CountAllViews", ex);
            }
        }

        /// <summary>
        /// Greift auf ein einzelnes Entity mit der gegebenen id zu und vrpackt es in einer EntityView
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntityView GetView(TEntityId id)
        {
            try
            {
                var Entity = GetEntity(id);
                if (Entity != null)
                    return CreateEntityView(Entity);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create(this, "GetView", ex);
            }
        }

        /// <summary>
        /// 13.8.2014, mko
        /// ruft ein Geschäftsobjekt ab. 
        /// </summary>
        /// <typeparam name="TBoCo"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        protected static TEntityView GetBoImpl<TBoCo>(TEntityId Id)
            where TBoCo : BoBase<TEntity, TEntityId, TEntityView>, new()
        {
            try
            {
                var boCo = new TBoCo();
                return boCo.GetView(Id);
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetBo", ex);
            }
        }

        /// <summary>
        /// 13.8.2014, mko
        /// Ruft ein Geschäftsobjekt aus einem Zwischenöspeicher ab, falls aktuell. Sonst wird ein aktuelles
        /// Objekt aus der Datenbank nachgeladen.
        /// Die Aktualität wird anhand der übergebenen aktuellen Id des Geschäftsobjektes bestimmt
        /// </summary>
        /// <typeparam name="TBoCo"></typeparam>
        /// <param name="BoCached"></param>
        /// <param name="CurrentBoId"></param>
        /// <returns></returns>
        protected static TEntityView GetBoCachedImpl<TBoCo>(TEntityView BoCached, TEntityId CurrentBoId)
            where TBoCo : BoBase<TEntity, TEntityId, TEntityView>, new()
        {
            try
            {
                if (BoCached == null || !BoCached.Id.Equals(CurrentBoId))
                {
                    BoCached = GetBoImpl<TBoCo>(CurrentBoId);
                }

                return BoCached;
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetBoCached", ex);

            }
        }


        protected IQueryable<TEntityView> _GetAsQueryable(IQueryable<TEntity> SubsetOf)
        {
            var EntityViewCollection = new LinkedList<TEntityView>();

            // Dummy für die Definition neuer Datensätze am Anfang aufnehmen, falls gewünscht                
            if (CreateDummyOn)
            {
                var dummy = CreateEntityView();
                dummy.State = BoBaseViewDefs.ViewState.Added;
                EntityViewCollection.AddLast(dummy);
            }

            foreach (var entity in SubsetOf)
            {
                EntityViewCollection.AddLast(CreateEntityView(entity));
            }

            return EntityViewCollection.AsQueryable<TEntityView>();
        }

        /// <summary>
        /// Liefert eine Queryable auf allen Entites, verpackt als EntityViews.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntityView> GetViews_AsQueryable()
        {
            try
            {
                return _GetAsQueryable(EntityCollection);
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViews_AsQueryable()", ex);
            }
        }

        /// <summary>
        /// Liefert einen Ausschnitt aller Entities, beginnend bei StartRowIndex mit PageSize 
        /// Entities. Die Entities werden als EntityViews verpackt geliefert.
        /// </summary>
        /// <param name="StartRowIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IQueryable<TEntityView> GetViews_AsQueryable(int StartRowIndex, int PageSize)
        {
            try
            {
                return _GetAsQueryable(EntityCollection.Skip(StartRowIndex).Take(PageSize));
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViews_AsQueryable()", ex);
            }
        }

        /// <summary>
        /// Liefert die mit den aktuellen Einstellungen gefilterte und sortierte Menge von Entities,
        /// verpackt als EntityViews zurück
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public IQueryable<TEntityView> GetViewsFilteredAndSorted_AsQueryable()
        {
            try
            {
                if (MultisortOn)
                    return _GetAsQueryable(MultiSort(FilterEntities(EntityCollection)));
                else
                    return _GetAsQueryable(Sort(FilterEntities(EntityCollection)));
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViewsFilteredAndSorted_AsQueryable()", ex);
            }
        }

        /// <summary>
        /// 9.8.2014, mko
        /// Filtert für eine temporäre Liste von Filtern. Zum sortieren werden die in der Bo eingestellten Sortierkriterien angewendet
        /// </summary>
        /// <param name="Filters"></param>
        /// <returns></returns>
        public IQueryable<TEntityView> GetViewsFilteredAndSorted_AsQueryable(int StartRowIndex, int PageSize, params Filter<TEntity>[] Filters)
        {
            var backUp = AllFilter;
            try
            {
                foreach (var flt in Filters)
                {
                    SetFilter(flt);
                }

                return _GetAsQueryable(MultiSort(FilterEntities(EntityCollection)).Skip(StartRowIndex).Take(PageSize));
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViewsFilteredAndSorted_AsQueryable(params Filter<TEntity>[] Filters)", ex);
            }
            finally
            {
                AllFilter = backUp;
            }
        }

        /// <summary>
        /// 9.8.2014, mko
        /// Sortiert mit einer Liste temporärer Sortieroptionen. Zum Filtern werden die eingestellten Filter eingesetzt
        /// </summary>
        /// <param name="Filters"></param>
        /// <returns></returns>
        public IQueryable<TEntityView> GetViewsFilteredAndSorted_AsQueryable(int StartRowIndex, int PageSize, params SortColumnDef[] SortDefs)
        {
            var backUpSort = SortJobs;
            try
            {

                SortJobDefine(SortDefs);

                return _GetAsQueryable(MultiSort(FilterEntities(EntityCollection)).Skip(StartRowIndex).Take(PageSize));
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViewsFiltered_AsQueryable(params Filter<TEntity>[] Filters)", ex);
            }
            finally
            {
                SortJobs = backUpSort;
            }
        }


        /// <summary>
        /// Liefert die mit den aktuellen Einstellungen gefilterte und sortierte Menge zurück
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public IQueryable<TEntityView> GetViewsFilteredAndSorted_AsQueryable(int StartRowIndex, int PageSize)
        {
            try
            {
                if (MultisortOn)
                    return _GetAsQueryable(MultiSort(FilterEntities(EntityCollection)).Skip(StartRowIndex).Take(PageSize));
                else
                    return _GetAsQueryable(Sort(FilterEntities(EntityCollection)).Skip(StartRowIndex).Take(PageSize));
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViewsFilteredAndSorted_AsQueryable(StartRowIndex, PageSize)", ex);
            }
        }



        /// <summary>
        /// Zugriff auf ein Entity, das durch seine id identifiziert wird. Muß in abgeleiteten
        /// Klassen implementiert werden. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ObservableEntityViewCollection GetViewFilteredById_AsObservable(TEntityId id)
        {

            try
            {
                var EntityViewCollection = CreateObservableEntityViewCollection();
                //var flt = CreateIdFilter(id);
                //var Subset = flt.filterImpl(EntityCollection);

                var Subset = EntityCollection.Where(GetEntityIDTest(id));
                EntityViewCollection.LoadEntities(Subset);

                return EntityViewCollection;
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViewFilteredById_AsObservable", ex);
            }
        }

        protected ObservableEntityViewCollection _GetAsObservable(IQueryable<TEntity> Subset)
        {
            var EntityViewCollection = CreateObservableEntityViewCollection();

            // Dummy für die Definition neuer Datensätze am Anfang aufnehmen, falls gewünscht                
            if (CreateDummyOn)
                EntityViewCollection.Add(CreateEntityView());

            EntityViewCollection.LoadEntities(Subset);

            return EntityViewCollection;
        }

        /// <summary>
        /// Liefert alle Entities, verpackt in einer Observable Collection
        /// </summary>
        /// <returns></returns>
        public ObservableEntityViewCollection GetViews_AsObservable()
        {
            try
            {
                return _GetAsObservable(EntityCollection);
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViews_AsObservable()", ex);
            }
        }

        /// <summary>
        /// Liefert alle PageSize Entities ab StartRowIndex, verpackt als Observable Collection
        /// </summary>
        /// <param name="StartRowIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public ObservableEntityViewCollection GetViews_AsObservable(int StartRowIndex, int PageSize)
        {
            try
            {
                return _GetAsObservable(EntityCollection.Skip(StartRowIndex).Take(PageSize));
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViews_AsObservable()", ex);
            }
        }




        /// <summary>
        /// Liefert die mit den aktuellen Einstellungen gefilterte und sortierte Menge zurück
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public ObservableEntityViewCollection GetViewsFilteredAndSorted_AsObservableCollection()
        {
            try
            {
                if (MultisortOn)
                    return _GetAsObservable(MultiSort(FilterEntities(EntityCollection)));
                else
                    return _GetAsObservable(Sort(FilterEntities(EntityCollection)));
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViewsFilteredAndSorted_AsObservableCollection()", ex);
            }
        }

        /// <summary>
        /// Liefert einen Ausschnitt aus dre mit den aktuellen Einstellungen gefilterte und sortierte Menge zurück.
        /// </summary>
        /// <param name="sortType"></param>
        /// <param name="StartRowIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public ObservableEntityViewCollection GetViewsFilteredAndSorted_AsObservableCollection(int StartRowIndex, int PageSize)
        {
            try
            {
                if (MultisortOn)
                    return _GetAsObservable(MultiSort(FilterEntities(EntityCollection)).Skip(StartRowIndex).Take(PageSize));
                else
                    return _GetAsObservable(Sort(FilterEntities(EntityCollection)).Skip(StartRowIndex).Take(PageSize));
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("GetViewsFilteredAndSorted", ex);
            }
        }

        //----------------------------------------------------------------------------------------------------------------
        // Einfügen, Löschen und Aktualisieren


        /// <summary>
        /// Hinzufügen eines Entity zu einer Entitycollection. Erst durch 
        /// SubmitChanges wird das Entity der Datenbank hinzugefügt.
        /// </summary>
        /// <param name="entity"></param>
        public abstract void AddToEntityCollection(TEntity entity);

        /// <summary>
        /// Einfügen eines neuen Entity, dessen Eigenschaften im View definiert wurden
        /// </summary>
        /// <param name="EntityView"></param>
        public virtual void Insert(TEntityView EntityView)
        {
            try
            {
                Insert(EntityView.Entity);
                EntityView.State = BoBaseViewDefs.ViewState.Unchanged;
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create(this, "Insert", ex);
            }
        }

        /// <summary>
        /// Eine elementare Einfügeoperation
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(TEntity entity)
        {
            try
            {
                AddToEntityCollection(entity);
                SubmitChanges();
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create(this, "Insert", ex);
            }
        }

        /// <summary>
        /// Aktualisieren eines Entity mit den Daten aus der View
        /// </summary>
        /// <param name="EntityView"></param>
        public virtual void Update(TEntityView EntityView)
        {
            try
            {
                if (EntityView.State == BoBaseViewDefs.ViewState.Added)
                {
                    // Hinzufügen, falls die Entityview für einen neuen Eintrag steht.
                    Debug.WriteLine("Update: Neues Entity wird eingefügt");
                    AddToEntityCollection(EntityView.Entity);
                }
                else
                {
                    var DbEntity = EntityCollection.Single(GetEntityIDTest(EntityView.Id));
                    if (!EntityView.Entity.Equals(DbEntity) && EntityView.PropertiesState == BoBaseViewDefs.ViewPropertyState.Modified)
                        // Nur dann ein externes Entity aktuelisieren, wenn Inhalt von Entityview geändert wurde und Entity der EntityView
                        // ein vom ORMContext- verwalteten Entity verschiedenes ist
                        EntityView.UpdateExternalEntity(DbEntity);
                    else
                        // Das Viewinterne Entity ist ein Entity aus dem ORMContext. Die Anwendung der aufgezeichneten Änderungen auf 
                        // ein exterenes Entity entfällt.
                        EntityView.DeleteAllChangeTrackingEntries();
                }
                SubmitChanges();

                EntityView.State = BoBaseViewDefs.ViewState.Unchanged;
                EntityView.PropertiesState = BoBaseViewDefs.ViewPropertyState.Unchanged;
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create(this, "Update", ex);
            }
        }

        /// <summary>
        /// Die von der BoBase 
        /// </summary>
        /// <param name="EntityViewCollection"></param>
        public abstract void UpdateWithObservableEntityViewCollectionAndSubmit(ObservableEntityViewCollection EntityViewCollection);

        /// <summary>
        /// Löschen des in der View beschribenen Entity
        /// </summary>
        /// <param name="EntityView"></param>
        public virtual void Delete(TEntityView EntityView)
        {
            try
            {
                Delete(EntityView.Id);
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create(this, "Delete", ex);
            }
        }

        /// <summary>
        /// Ein Entity für das Löschen in der EntityCollection markieren
        /// </summary>
        /// <param name="entity"></param>
        public abstract void RemoveFromEntityCollection(TEntity entity);


        /// <summary>
        /// Löschen des durch die ID definierten Entity
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(TEntityId id)
        {
            try
            {
                //var flt = CreateIdFilter(id);
                //var entity = flt.filterImpl(EntityCollection).Single();
                var DbEntity = EntityCollection.Single(GetEntityIDTest(id));
                RemoveFromEntityCollection(DbEntity);
                SubmitChanges();
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create(this, "Delete", ex);
            }
        }

        /// <summary>
        /// Löschen aller Entities
        /// </summary>
        public abstract void DeletAll();


        /// <summary>
        /// Aktualisierungen am ORMContext mit der Datenbank abgleichen
        /// </summary>
        public abstract void SubmitChanges();

    }
}


