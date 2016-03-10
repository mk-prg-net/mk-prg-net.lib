 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;


namespace mkoIt.Asp
{
    public class GridFilterNumBoxCtrl<TFilter, TEntity>
        where TFilter: Db.FilterFunctor<TEntity, int>, new()
        where TEntity : class, new()
    {
        SessionStateFilterAndSortEntities<TEntity> _sessVar;
        bool _grdDataBindingNow;
        TextBox _tbx;

        public GridFilterNumBoxCtrl(TextBox tbx, bool grdDataBindingNow, SessionStateFilterAndSortEntities<TEntity> sessVar)
        {
            Debug.Assert(tbx != null);
            _tbx = tbx;
            _grdDataBindingNow = grdDataBindingNow;
            _tbx.Load += new EventHandler(OnLoad);
            _tbx.TextChanged += new EventHandler(OnChanged);
            _sessVar = sessVar;

            OnLoad(_tbx, null);
        }


        protected void OnLoad(object sender, EventArgs e)
        {  
            var filterType = typeof(TFilter);
            if (_grdDataBindingNow && _sessVar.IsFilterOn(filterType))
            {
                var funktor = _sessVar.GetFilter(filterType) as Db.FilterFunctor<TEntity, int>;
                _tbx.Text = funktor.RValue.ToString();                
            }
        }

        protected void OnChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_tbx.Text))
            {
                var filter = new TFilter();
                filter.RValue = int.Parse(_tbx.Text);
                _sessVar.AddFilter(filter);
            }
            else
                _sessVar.RemoveFilter(typeof(TFilter));
        }       

    }
}