using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;


namespace mkoIt.Asp
{
    public class GridFilterZeitintervallCtrl<TFilter, TEntity, TTimeIntervalCtrl>
        where TFilter : Db.FilterFunctor<TEntity, mko.Interval<DateTime>>, new()
        where TTimeIntervalCtrl : System.Web.UI.UserControl, ITimeIntervalCtrl
        where TEntity : class, new()
    {
        SessionStateFilterAndSortEntities<TEntity> _sessVar;
        bool _grdDataBindingNow;
        TTimeIntervalCtrl _vonBis;

        public GridFilterZeitintervallCtrl(TTimeIntervalCtrl vonBis, bool grdDataBindingNow, SessionStateFilterAndSortEntities<TEntity> sessVar)
        {
            Debug.Assert(vonBis != null);
            _vonBis = vonBis;
            _grdDataBindingNow = grdDataBindingNow;

            _vonBis.InitBeginAndEndOfTime += new InitBeginAndEndOfTimeEventHandler(OnInitBeginAndEndOfTime);
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
                var funktor = _sessVar.GetFilter(filterType) as Db.FilterFunctor<TEntity, mko.Interval<DateTime>>;
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

        protected void OnInitBeginAndEndOfTime(object sender, InitBeginAndEndOfTimeArgs e)
        {
            e.BeginningOfTime = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            e.EndOfTime = System.Data.SqlTypes.SqlDateTime.MaxValue.Value;
        }



    }
}