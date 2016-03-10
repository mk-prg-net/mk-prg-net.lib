using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using WebCtrl = System.Web.UI.WebControls;

using System.Diagnostics;
using mkx = mkoIt.Xhtml.Xhtml;


namespace mkoIt.Asp.GridView
{
    public class GridViewColFilterSelectUpdateDecorator<TEntity, TViewState> : GridViewColDecorator<TEntity>
        where TEntity : class, new()
        where TViewState : mkoIt.Asp.SessionStateFilterAndSortEntities<TEntity>
    {
        Page _page;
        TViewState _state;

        public GridViewColFilterSelectUpdateDecorator(System.Web.UI.WebControls.GridView grd, int ColNumber, Page page, TViewState state)
            : base(grd, ColNumber, state)
        {
            _page = page;
            _state = state;
        }

        public GridViewColFilterSelectUpdateDecorator(System.Web.UI.WebControls.GridView grd, string ColHeaderText, Page page, TViewState state)
            : base(grd, ColHeaderText, state)
        {
            _page = page;
            _state = state;
        }

        public virtual void CreateTemplates(WebCtrl.GridView grd, WebCtrl.TemplateField col)
        {
            col.HeaderTemplate = new mkoIt.Asp.GridView.ColHeaderFilterSelectUpdate<TEntity, TViewState>(grd, _page, _state);
            col.ItemTemplate = new mkoIt.Asp.GridView.ColItemFilterSelectUpdate(grd);
            col.EditItemTemplate = new mkoIt.Asp.GridView.ColEditItemFilterSelectUpdate();

        }
    }
}
