//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 21.04.2016
//
//  Projekt.......: mko.BI
//  Name..........: FilteredSortedSet.cs
//  Aufgabe/Fkt...: Implementierung der IFilteredSortedSet- Schnitstelle für Mengen, die
//                  mittels Linq abgerufen werden.
//                  Aus der Klasse mkoIt.DB.BoBase abgeleitet,
//                  die wiederumg aus GblDbLayer.OdsToLinq vom 13.11.2010
//                  abgeleitet wurde.//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.8.2011
//  Änderungen....: Integration von DynamicQuery, EntityCollection und Implementierung von 
//                  select in der Basisklasse
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.11.2011
//  Änderungen....: Erweitern des Sortieren bzgl. meherer Spalten 
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 13.3.2012
//  Änderungen....: Verschoben in Projekt mkoDb. Ziel ist die Verallgemeinerung auf 
//                  alle Projekte durch Loslösung von Web- Bibliothek.
//                  Eigenschaft SortDirection von Typ System.Web.UI.SortDirection auf 
//                  EnumSortDirection umgestellt.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.5.2012
//  Änderungen....: Aufteilung in BoBase und BoBaseSqlToLinq zwecks nutzten der Basisklassenfunktionalität
//                  in Datasets <> SqlToLinq DataContext
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 23.7.2013
//  Änderungen....: Aufbauen auf Basisklasse BoBaseView, welche Änderungen aufzeichnet und an Entities nachvollzieht.
//                  Methoden GetEntitiesXXX(), GetEntitiyViewsXXX_AsQueryable() und GetEntityViews_AsObservable()... implementiert.
//                  Ersetzen die SelectXXX() Methoden.
//                  Update und Insert sind jetzt vollständig in Basisklasse implementiert.
//
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
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 3.1.2015
//  Änderungen....: Umgewandelt in abstrakte Klasse BoCoBase. Diese stellt nun ein Repository für Geschäftsobjekte dar, bei denen nicht mehr 
//                  die Darstellung in einer Datenbank/Speicher explizit berücksichtigt werden muss. In den Vorläufern war das Geschäftsobjekt 
//                  eine View (TEntityView), und seine Darstellung in der Datenbank war ein Entity (TEntity). 
//                  Jetzt werden nur noch Geschäftsobjekte direkt verwaltet. Damit lassen sich insbesondere Geschäftsobjekte nach dem Code- First Modell von 
//                  EntityFramework 6 verwalten.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 18.9.2015
//  Änderungen....: In eine Implementierung der neuen Schnittstellen ICrud und IFilterAndSort verwandelt. Damit wird BoCo für 
//
//                  Dependency Injection einsetzbar
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 28.9.2015
//  Änderungen....: Member der Schnittstelle ICrud und IFilterAndSort umbenannt.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 13.3.2016
//  Änderungen....: Mehtoden, die das Liskovsche Substitutionsprinzip verletzen wie CreateBo(), AddToEntityCollection(TBo bo) und Insert(TBo bo) 
//                  entfernt
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 15.3.2016
//  Änderungen....: Auf Implementierung der IFilterSort- Schnittstelle beschränkt. Alle anderen Schnittstellen sind optional der 
//                  bei der konkreten Implementierung eines Repositories hinzuzufügen
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 21.4.2016
//  Änderungen....: Umbenannt in FilteredSortedSet. Objekte dieser Klasse werden ab jetzt von Klassen erzeugt, die die IFilteredSortedSetBuilder
//                  Schnittstelle implementieren. Dabei werden die Filterkriterien und Sortierreihenfolgen mittels des Builders definiert. 
//                  Builder.GetSet() liefert dann FilteredSortedSet- Objekte, in denen die Filterkriterien und Sortierreihenfolgen fixiert sind.
//                  Hierdurch können abstrakter Repositiories, die statt Objekte Schnittstellen auf diese liefern, ohne Einschränkungen implementiert
//                  werden. 
//
//</unit_history>
//</unit_header>        
        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Repositories
{
    public class FilteredSortedSet<TBo> : Interfaces.IFilteredSortedSet<TBo>
        where TBo : class
    {

        public FilteredSortedSet(IQueryable<TBo> query, IEnumerable<DefSortOrder<TBo>> DefSortOrders)
        {
            _query = query;
            _DefSortOrders = DefSortOrders;
        }

        IQueryable<TBo> _query;

        protected IEnumerable<DefSortOrder<TBo>> _DefSortOrders;

        protected IOrderedQueryable<TBo> MultiSort(IQueryable<TBo> tab)
        {
            if (_DefSortOrders.Any())
            {
                var firstOrder = _DefSortOrders.First();

                if (_DefSortOrders.Skip(1).Any())
                {
                    var otab = firstOrder.MainOrder(tab);
                    foreach (var defOrder in _DefSortOrders.Skip(1))
                    {
                        otab = defOrder.ThenOrder(otab);
                    }
                    return otab;
                }
                else
                    return firstOrder.MainOrder(tab);
            }
            else
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "MultiSort", "Es muss mindestens eine Standardsortierreihenfolge definiert werden"));

        }

        public bool Any()
        {
            try
            {
                return _query.Any();
            }
            catch (Exception ex)
            {
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "Any"), ex);
            }
            
        }

        public long Count()
        {
            try
            {
                return _query.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "Count"), ex);
            }
            
        }

        public IEnumerable<TBo> Get()
        {
            try
            {
                return MultiSort(_query);
            }
            catch (Exception ex)
            {
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "Get"), ex);
            }
            
        }
    }
}
