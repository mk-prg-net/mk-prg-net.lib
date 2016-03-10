//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 20.5.2014
//
//  Projekt.......: mko.Asp.Mvc
//  Name..........: WFStateFilterSortSelectPageSize.cs
//  Aufgabe/Fkt...: Basisklasse für Seitenzustände, die Filter, Sortierbedingungen und Fenstergröße 
//                  von Ausschnitt aus Resultset für einen Workflow definiert.
//                  Hervorgegangen aus: 
//                    - 27.2.2011: GBL.Web.ViewStateWithFiltersBase
//                    - 07.3.2012: mkoIt.Asp.SessionStateFilterAndSortEntities<TEntity>
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
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Asp.Mvc.Session
{
    [Serializable]
    public class WFStateFilterSortSelectPageSize<TEntity>
        where TEntity : class, new()
    {

        /// <summary>
        /// Kopiert Teiinformationen über den WF- Zustand in ein Model
        /// </summary>
        /// <param name="Model"></param>
        public void CopyTo(Models.ModelBase Model)
        {
            Model.FilterDescriptions = Filters.Values.Select(f => f.Description).ToArray();
            Model.GradingRulesDescriptions = SortJob.Select(j => j.ColName + " " + (j.SortDescending ? @"\\" : @"//")).ToArray();
            Model.StartsAt = StartsAt;
            Model.PageSize = PageSize;

        }

        //------------------------------------------------------------------------------------------------
        // Filtern bezüglich mehrerer Spalten

        public Dictionary<Type, mkoIt.Db.Filter<TEntity>> Filters = new Dictionary<Type, mkoIt.Db.Filter<TEntity>>();

        /// <summary>
        /// Liefert True, wenn ein Filter von einem bestimmten Typ gesetzt ist. 
        /// Jedes Filter ist einer Spalte zugeordnet. Jedes Filter kann pro Spalte bei einer Filterung
        /// nur einmal angewendet werden. Indem die Referenz auf das Typobjekt vom Filter als Schlüssel
        /// herangezogen wird, werden diese Regeln implementiert
        /// </summary>
        /// <param name="FilterType"></param>
        /// <returns></returns>
        public bool IsFilterOn(Type FilterType)
        {
            return Filters.ContainsKey(FilterType);
        }

        /// <summary>
        /// Aktiviert ein Filter. Filtertyp und zu filternde Spalte sind fest miteinander verknüpft !
        /// </summary>
        /// <param name="newFilter"></param>
        public void AddFilter(mkoIt.Db.Filter<TEntity> newFilter)
        {

            Type fltType = newFilter.GetType();

            // Falls älteres Filter vorhanden, dann dieses löschen
            if (Filters.ContainsKey(fltType))
                Filters.Remove(fltType);
            //throw new Exception("Das Filter mit vom Typ " + fltType.FullName + " wurde bereits angelegt, und kann nun nicht nochmals angelegt werden");

            Filters.Add(fltType, newFilter);
            Debug.WriteLine("Das Filter mit dem Typ " + fltType.FullName + " wurde hinzugefügt");
        }

        /// <summary>
        /// Entfernt ein Filter. Filtertyp und zu filternde Spalte sind fest miteinander verknüpft !
        /// </summary>
        /// <param name="filterType"></param>
        public void RemoveFilter(Type filterType)
        {
            if (Filters.ContainsKey(filterType))
            {
                Filters.Remove(filterType);
                Debug.WriteLine("Das Filter mit dem Typ " + filterType.FullName + " wurde entfernt");
            }
        }

        /// <summary>
        /// Liefert eine Referenz von einem aktiven Filter. 
        /// Filtertyp und zu filternde Spalte sind fest miteinander verknüpft !
        /// </summary>
        /// <param name="FilterType"></param>
        /// <returns></returns>
        public mkoIt.Db.Filter<TEntity> GetFilter(Type FilterType)
        {
            Debug.Assert(Filters.ContainsKey(FilterType));
            return Filters[FilterType];
        }

        /// <summary>
        /// Set ein Filter auf eine Spalte.
        /// Filtertyp und zu filternde Spalte sind fest miteinander verknüpft !
        /// </summary>
        /// <param name="fc"></param>
        public void SetFilters(mkoIt.Db.FiltersCombine<TEntity> fc)
        {
            foreach (var key in Filters.Keys)
            {
                fc.AllFilter.Add(Filters[key]);
            }
        }

        /// <summary>
        /// Alle Spaltenfilter löschen
        /// </summary>
        public void RemoveAllFilters()
        {
            // Alle Filter löschen
            Filters.Clear();
        }

        //-----------------------------------------------------------------------------------------
        // Sortieren bezüglich mehrerer Spalten


        /// <summary>
        /// Listet alle Spalten auf, bezüglich der sortiert werden soll.
        /// </summary>
        public List<mkoIt.Db.SortColumnDef> SortJob = new List<mkoIt.Db.SortColumnDef>();

        /// <summary>
        /// Löscht alle Einträge in einem sortJob
        /// </summary>
        public void SortJobReset()
        {
            SortJob.Clear();
        }

        /// <summary>
        /// Fügt eine Spaltendefinition für das Sortieren der SortJob- Liste an
        /// </summary>
        /// <param name="colDef">Spaltendefinition</param>
        public void SortJobAppend(mkoIt.Db.SortColumnDef colDef)
        {
            if (!SortJob.Any(r => r.ColName == colDef.ColName && r.ViewType == colDef.ViewType))
            {
                SortJob.Add((mkoIt.Db.SortColumnDef)colDef.Clone());
            }
        }

        //------------------------------------------------------------------------------------

        /// <summary>
        /// Überträgt die Filter und Sortiereinstellungen, die in diesem WF- Zustand definiert sind, in ein Bo
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TEntityView"></typeparam>
        /// <param name="bo"></param>
        public void SetFilterAndSort<TKey, TEntityView>(mkoIt.Db.BoBase<TEntity, TKey, TEntityView> bo)
            where TEntityView : class, mkoIt.Db.IEntityView<TEntity, TKey>, new()
        {
            bo.MultisortOn = true;

            bo.SortJobDefine(SortJob.ToArray());

            foreach (var key in Filters.Keys)
            {
                bo.SetFilter(Filters[key]);
            }
        }


        /// <summary>
        /// Anzahl der Zeilen, die pro Seite auszugeben sind. 
        /// Dient zur seitenweise Ausgabe von gefilterten und sortierten Daten
        /// </summary>
        public int PageSize { get; set; }


        /// <summary>
        /// Zeile im Resultset, ab dem eine Seite auszugeben ist.
        /// Dient zur seitenweise Ausgabe von gefilterten und sortierten Daten
        /// </summary>
        public int StartsAt { get; set; }


    }
}
