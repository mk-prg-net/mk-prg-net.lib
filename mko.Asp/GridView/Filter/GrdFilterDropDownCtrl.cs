//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: Projektkontext
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Kurzbeschreibung
//                  
//
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
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;


namespace mkoIt.Asp
{
    public class GrdFilterDropDownCtrl<TFilter, TEntity>  
        where TEntity : class, new()
        where TFilter: Db.FilterFunctor<TEntity, int>, new()
    {
        SessionStateFilterAndSortEntities<TEntity> _sessVar;
        bool _grdDataBindingNow;
        DropDownList _dpd;

        public GrdFilterDropDownCtrl(DropDownList dpd, bool grdDataBindingNow, SessionStateFilterAndSortEntities<TEntity> sessVar)
        {
            Debug.Assert(dpd != null);
            _dpd = dpd;
            _grdDataBindingNow = grdDataBindingNow;
            _dpd.Load += new EventHandler(OnLoad);
            _dpd.SelectedIndexChanged += new EventHandler(OnChanged);
            _sessVar = sessVar;

            OnLoad(dpd, null);
        }


        protected void OnLoad(object sender, EventArgs e)
        {  
            var filterType = typeof(TFilter);
            if (_grdDataBindingNow && _sessVar.IsFilterOn(filterType))
            {
                var funktor = _sessVar.GetFilter(filterType) as Db.FilterFunctor<TEntity, int>;
                _dpd.SelectedValue = funktor.RValue.ToString();                
            }
        }

        protected void OnChanged(object sender, EventArgs e)
        {
            if (_dpd.SelectedValue != "-1")
            {
                var filter = new TFilter();
                filter.RValue = int.Parse(_dpd.SelectedValue);
                filter.Description = _dpd.SelectedItem.Text;
                _sessVar.AddFilter(filter);
            }
            else
                _sessVar.RemoveFilter(typeof(TFilter));
        }       

    }
}