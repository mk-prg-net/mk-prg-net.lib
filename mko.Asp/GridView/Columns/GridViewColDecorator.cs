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
    public class GridViewColDecorator<TEntity>
        where TEntity : class, new()
    {
        protected WebCtrl.GridView _grd;
        protected WebCtrl.TemplateField _col;
        protected SessionStateFilterAndSortEntities<TEntity> _state;

        public GridViewColDecorator(System.Web.UI.WebControls.GridView grd, int ColNumber, SessionStateFilterAndSortEntities<TEntity> state)
        {
            _grd = grd;
            _col = grd.Columns[ColNumber] as WebCtrl.TemplateField;
            CreateTemplates(grd, _col, state);
        }

        public GridViewColDecorator(System.Web.UI.WebControls.GridView grd, string ColHeaderText, SessionStateFilterAndSortEntities<TEntity> state)
        {
            _grd = grd;
            _col = mkoIt.Asp.GridView.Utils.FindTemplateField(grd, ColHeaderText);
            CreateTemplates(grd, _col, state);
        }

        public virtual void CreateTemplates(WebCtrl.GridView grd, WebCtrl.TemplateField col, SessionStateFilterAndSortEntities<TEntity> state)
        {
        }
    }
}
