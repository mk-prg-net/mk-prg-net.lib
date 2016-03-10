using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;


namespace mkoIt.Asp
{
    public class GrdFilterDblIntervalCtrl<TFilter, TEntity, TDblIntervalCtrl>
        where TFilter : Db.FilterFunctor<TEntity, mko.Interval<double>>, new()
        where TDblIntervalCtrl : System.Web.UI.UserControl, IDblIntervalCtrl
        where TEntity : class, new()
    {
        SessionStateFilterAndSortEntities<TEntity> _sessVar;
        bool _grdDataBindingNow;
        TDblIntervalCtrl _vonBis;

        public GrdFilterDblIntervalCtrl(TDblIntervalCtrl vonBis, bool grdDataBindingNow, SessionStateFilterAndSortEntities<TEntity> sessVar, double Minimum, double Maximum)
        {
            Debug.Assert(vonBis != null);
            _vonBis = vonBis;
            _vonBis.Minimum = Minimum;
            _vonBis.Maximum = Maximum;
            _grdDataBindingNow = grdDataBindingNow;

            _vonBis.Load += new EventHandler(OnLoad);
            _vonBis.VonBisChanged += new EventHandler(OnChanged);
            _sessVar = sessVar;

            OnLoad(_vonBis, null);
        }

        protected void OnLoad(object sender, EventArgs e)
        {
            var filterType = typeof(TFilter);
            if (_grdDataBindingNow && _sessVar.IsFilterOn(filterType))
            {
                var funktor = _sessVar.GetFilter(filterType) as Db.FilterFunctor<TEntity, mko.Interval<double>>;
                _vonBis.VonBis = funktor.RValue;
            }            
        }

        protected void OnChanged(object sender, EventArgs e)
        {
            if (_vonBis.Restriktion)
            {
                var filter = new TFilter();
                filter.RValue = _vonBis.VonBis;
                filter.Description = "Zeitintervall";
                _sessVar.AddFilter(filter);
            }
            else
                _sessVar.RemoveFilter(typeof(TFilter));
        }

    }
}
