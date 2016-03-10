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
    public class BoBaseSessionState<TEntity> : SessionStateFilterAndSortEntities<TEntity>
        where TEntity : class
    {
        public void SetFilterAndSort<TORMContext, TKey, TEntityView>(mkoIt.Db.BoBaseSqlToLinq<TORMContext, TEntity, TKey, TEntityView> bo)
            where TKey : struct
            where TORMContext : System.Data.Linq.DataContext, new()             
            where TEntityView : class, new()
        {
            bo.SortColumn = SortExpression;
            bo.SortDirection = SortDirection == System.Web.UI.WebControls.SortDirection.Ascending ? Db.EnumSortDirection.Ascending: Db.EnumSortDirection.Descending;

            foreach (var key in Filters.Keys)
            {
                bo.AllFilter.Add(Filters[key]);
            }
        }
    }
}
