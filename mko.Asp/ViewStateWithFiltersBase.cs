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
    public class ViewStateWithFiltersBase<TEntity> : SessionStateFilterAndSortEntities<TEntity>
    {
        public void SetFilterAndSort<TDataContext, TEntityView>(FilterAndSortEntities<TDataContext, TEntity, TEntityView> bo)
            where TDataContext : System.Data.Linq.DataContext, new()
        {
            bo.SortColumn = SortExpression;
            bo.SortDirection = SortDirection;

            foreach (var key in Filters.Keys)
            {
                bo.AllFilter.Add(Filters[key]);
            }
        }
    }
}