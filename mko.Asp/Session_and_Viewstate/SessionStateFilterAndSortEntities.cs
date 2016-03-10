//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.3.2012
//
//  Projekt.......: mkoItAps
//  Name..........: SessionStateFilterAndSortEntites.cs
//  Aufgabe/Fkt...: Basisklasse für Seitenzustände, die Filter und Sortierbedingungen 
//                  für eine aktuelle Tabelle enthalten. Abgeleitet aus der Klasse
//                  GBL.Web.ViewStateWithFiltersBase vom 27.2.2011.
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
//  Datum.........: 18.5.2012
//  Änderungen....: Listen, die das sortieren bezüglich mehrerer Spalten definieren, implementiert 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace mkoIt.Asp
{
    [Serializable]
    public class SessionStateFilterAndSortEntities<TEntity>
        where TEntity : class, new()
    {
        public string SortExpression {get; set;}
        public System.Web.UI.WebControls.SortDirection SortDirection = System.Web.UI.WebControls.SortDirection.Ascending;


        //------------------------------------------------------------------------------------------------
        // Filtern bezüglich mehrerer Spalten

        public Dictionary<Type, Db.Filter<TEntity>> Filters = new Dictionary<Type, Db.Filter<TEntity>>();

        public bool IsFilterOn(Type FilterType)
        {
            return Filters.ContainsKey(FilterType);
        }

        public void AddFilter(Db.Filter<TEntity> newFilter)
        {

            Type fltType = newFilter.GetType();

            // Falls älteres Filter vorhanden, dann dieses löschen
            if (Filters.ContainsKey(fltType))
                Filters.Remove(fltType);
            //throw new Exception("Das Filter mit vom Typ " + fltType.FullName + " wurde bereits angelegt, und kann nun nicht nochmals angelegt werden");

            Filters.Add(fltType, newFilter);
            Debug.WriteLine("Das Filter mit dem Typ " + fltType.FullName + " wurde hinzugefügt");
        }

        public void RemoveFilter(Type filterType)
        {
            if (Filters.ContainsKey(filterType))
            {
                Filters.Remove(filterType);
                Debug.WriteLine("Das Filter mit dem Typ " + filterType.FullName + " wurde entfernt");
            }
        }

        public Db.Filter<TEntity> GetFilter(Type FilterType)
        {
            Debug.Assert(Filters.ContainsKey(FilterType));
            return Filters[FilterType];
        }

        public void SetFilters(Db.FiltersCombine<TEntity> fc)
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

        public bool MultisortOn { get; set; }

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
        
        public void SetFilterAndSort<TKey, TEntityView>(mkoIt.Db.BoBase<TEntity, TKey, TEntityView> bo)
            //where TKey : struct
            //where TORMContext : System.Data.Linq.DataContext, new()
            
            where TEntityView : class, mkoIt.Db.IEntityView<TEntity, TKey>, new()
        {
            bo.MultisortOn = MultisortOn;
            if (MultisortOn)
            {
                bo.SortJobDefine(SortJob.ToArray());
            }
            else
            {
                bo.SortColumn = SortExpression;
                bo.SortDirection = SortDirection == System.Web.UI.WebControls.SortDirection.Ascending ? Db.EnumSortDirection.Ascending : Db.EnumSortDirection.Descending;
            }

            foreach (var key in Filters.Keys)
            {
                bo.SetFilter(Filters[key]);
            }
        }


    }
}
