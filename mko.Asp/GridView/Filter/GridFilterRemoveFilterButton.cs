using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace mkoIt.Asp
{
    public class GridFilterRemoveFilterButton<TEntity>
        where TEntity : class, new()
    {
        SessionStateFilterAndSortEntities<TEntity> _state;
        System.Web.UI.WebControls.GridView _grdAll;
        Button _btnRemoveFilter;
        Page _Page;

        public GridFilterRemoveFilterButton(Page page, System.Web.UI.WebControls.GridView grdAll, string IdBtnRemoveFilter, SessionStateFilterAndSortEntities<TEntity> state)
        {
            _Page = page;
            _grdAll = grdAll;
            _state = state;
            if (_grdAll.HeaderRow != null)
            {
                _btnRemoveFilter = _grdAll.HeaderRow.FindControl(IdBtnRemoveFilter) as Button;
                _btnRemoveFilter.Click += new EventHandler(OnButtonClick);
            }
        }

        public void RemoveAllFilters() {
            _state.RemoveAllFilters();

            // Erneuter Aufruf der Webseite ohne Filter- Restriktionen
            _Page.Response.Redirect(SiteMap.CurrentNode.Url, true);

        }

        void OnButtonClick(object sender, EventArgs e)
        {
            RemoveAllFilters();
        }

    }
}
