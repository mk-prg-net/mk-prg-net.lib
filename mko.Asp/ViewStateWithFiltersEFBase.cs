using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;


namespace mkoIt.Asp
{
    [Serializable]
    public class ViewStateWithFiltersEFBase<TEntity>
    {
        public string SortExpression;
        public System.Web.UI.WebControls.SortDirection SortDirection = System.Web.UI.WebControls.SortDirection.Ascending;

        public Dictionary<Type, Filter<TEntity>> Filters = new Dictionary<Type, Filter<TEntity>>();

        public bool IsFilterOn(Type FilterType)
        {
            return Filters.ContainsKey(FilterType);
        }

        public void AddFilter(Filter<TEntity> newFilter) {

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

        public Filter<TEntity> GetFilter(Type FilterType)
        {
            Debug.Assert(Filters.ContainsKey(FilterType));
            return Filters[FilterType];
        }

        public void SetFilterAndSort<TObjectContext, TEntityView>(BoFilterAndSortEfEntitiesBase<TObjectContext, TEntity, TEntityView> bo)
            where TObjectContext : System.Data.Objects.ObjectContext, new()
        {
            bo.SortColumn = SortExpression;
            bo.SortDirection = SortDirection;

            foreach (var key in Filters.Keys)
            {
                bo.AllFilter.Add(Filters[key]);
            }
        }

        public void SetFilters(FiltersCombine<TEntity> fc)
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


    }
}