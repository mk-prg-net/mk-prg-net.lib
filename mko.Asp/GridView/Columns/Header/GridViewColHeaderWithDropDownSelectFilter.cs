//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.2.2012
//
//  Projekt.......: mko.Asp
//  Name..........: GrdViewColHeaderWithDropDownSelectFilter.cs
//  Aufgabe/Fkt...: HeaderTemplate für GridView, das ein Filter ansteuert.
//                  Aus einer DropDown- Liste wird ein Filterausdruck
//                  ausgewählt, nach dem dann die Tabelle gefiltert wird
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.0
//  Werkzeuge.....: Visual Studio 2010
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
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using mkx = mkoIt.Xhtml.Xhtml;


namespace mkoIt.Asp.GridView
{
    public class ColHeaderWithDropDownSelectFilter<TFilter, TEntity, TKey> : ColHeaderTemplateBase
        where TEntity : class, new()
        where TFilter : Db.FilterFunctor<TEntity, TKey>, new()
    {
        SessionStateFilterAndSortEntities<TEntity> _sessVar;
        bool _grdDataBindingNow = false;
        HtmlCtrl.DropDownList _dpd;
        string _DataSourceID;
        string _DataTextField;
        string _DataValueField;

        ListItem[] _listItems;

        string _AllSymbol;

        /// <summary>
        /// Konstruktor, bei dem die DropDownList mit Daten aus einer Datenquelle 
        /// gefüllt wird
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="sessVar"></param>
        /// <param name="DataSourceId"></param>
        /// <param name="DataTextField"></param>
        /// <param name="DataValueField"></param>
        /// <param name="AllSymbol"></param>
        public ColHeaderWithDropDownSelectFilter(
            System.Web.UI.WebControls.GridView grd,
            SessionStateFilterAndSortEntities<TEntity> sessVar,
            string DataSourceId,
            string DataTextField,
            string DataValueField,
            string AllSymbol
            )
        {
            _DataSourceID = DataSourceId;
            _DataTextField = DataTextField;
            _DataValueField = DataValueField;
            _AllSymbol = AllSymbol;
            _sessVar = sessVar;
            grd.DataBinding += new EventHandler(grd_DataBinding);
        }

        /// <summary>
        /// Konstruktor, bei dem die DropDownList mit Daten aus einem ListItemArray gefüllt wird.
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="sessVar"></param>
        /// <param name="listItems"></param>
        /// <param name="AllSymbol"></param>
        public ColHeaderWithDropDownSelectFilter(
            System.Web.UI.WebControls.GridView grd,
            SessionStateFilterAndSortEntities<TEntity> sessVar,
            ListItem[] listItems,
            string AllSymbol
            )
        {
            _listItems = new ListItem[listItems.Length + 1];
            _listItems[0] = new ListItem(_AllSymbol, "-1");
            Array.Copy(listItems, 0, _listItems, 1, listItems.Length);

            _AllSymbol = AllSymbol;
            _sessVar = sessVar;
            grd.DataBinding += new EventHandler(grd_DataBinding);
        }


        void grd_DataBinding(object sender, EventArgs e)
        {
            _grdDataBindingNow = true;
        }

        protected override void CreateFilterCtrl(Control NamingContainer, ControlCollection content)
        {
            if (_listItems != null)            
                _dpd = new HtmlCtrl.DropDownList("dpd" + ColName + "Filter")
                {
                    Items = _listItems,
                    SetLoad = OnLoad,
                    SetSelectedIndexChanged = OnChanged
                };            
            else
                _dpd = new HtmlCtrl.DropDownList("dpd" + ColName + "Filter")
                {
                    DataSourceID = _DataSourceID,
                    DataTextField = _DataTextField,
                    DataValueField = _DataValueField,
                    AppendDataBoundItems = true,
                    Items = new ListItem[]{
                        new ListItem(_AllSymbol, "-1")
                    },
                    SetLoad = OnLoad,
                    SetSelectedIndexChanged = OnChanged
                };

            content.Add(_dpd.ToWebControl());
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
                filter.RValueParse(_dpd.SelectedValue);
                filter.Description = _dpd.SelectedItem.Text;
                _sessVar.AddFilter(filter);
            }
            else
                _sessVar.RemoveFilter(typeof(TFilter));
        }


    }
}
