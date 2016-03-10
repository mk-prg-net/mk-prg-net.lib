
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 6.2.2012
//
//  Projekt.......: mko.Asp
//  Name..........: GrdViewColHeaderAutoCompleteTbx.cs
//  Aufgabe/Fkt...: HeaderTemplate für GridView, das ein Filter ansteuert.
//                  Der Filterausdruck wird in einer Textbox eingegeben. Ein
//                  AutoCompleteExtender sucht aus einer Liste zur Eingabe passende
//                  Kandidaten für Filterausdrücke.
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
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using mkx = mkoIt.Xhtml.Xhtml;


namespace mkoIt.Asp.GridView
{
    public class ColHeaderWithAutoCompleteTbx<TFilter, TEntity> : ColHeaderTemplateBase
        where TEntity : class, new()
        where TFilter : Db.FilterFunctor<TEntity, string>, new()
    {
        SessionStateFilterAndSortEntities<TEntity> _sessVar;
        bool _grdDataBindingNow = false;
        string _serviceMethod;
        /// <summary>
        /// Dienst- URL, unter dem die AutoComplete Methode abrufbar ist
        /// </summary>        
        public string ServicePath { get; set; }

        /// <summary>
        /// Anzahl der Vorschläge durch den AutoCompleteExtender
        /// </summary>
        public int CompletionSetCount { get; set; }

        /// <summary>
        /// Kleinste Präfixlänge des Suchwortes, bei dem der AutoCompleteExtender startet
        /// </summary>
        public int MinimumPrelfixLength { get; set; }

        /// <summary>
        /// minimale Zeit zwischen zwei Suchläufen des AutoCompleteExtenders
        /// </summary>
        public int CompletionInterval { get; set; }

        public bool AutoCompleteEnableCaching = true;

        public ColHeaderWithAutoCompleteTbx(System.Web.UI.WebControls.GridView grd, SessionStateFilterAndSortEntities<TEntity> sessVar, string ServiceMethod)
        {
            _sessVar = sessVar;
            grd.DataBinding += new EventHandler(grd_DataBinding);
            _serviceMethod = ServiceMethod;
            CompletionSetCount = 5;
            CompletionInterval = 500;
            MinimumPrelfixLength = 1;

        }

        void grd_DataBinding(object sender, EventArgs e)
        {
            _grdDataBindingNow = true;
        }

        protected override void CreateFilterCtrl(Control NamingContainer, ControlCollection content)
        {

            var tbxFilter = new TextBox()
            {
                ID = "tbx" + ColName,
                Width = new Unit(95, UnitType.Percentage)
            };

            tbxFilter.Load += new EventHandler(tbxFilter_Load);
            tbxFilter.TextChanged += new EventHandler(tbxFilter_TextChanged);

            // Laden der aktuell gültigen Filtereinstellungen
            //tbxFilter_Load(tbxFilter, null);

            content.Add(tbxFilter);

            var autoComplete = new AjaxControlToolkit.AutoCompleteExtender()
            {
                TargetControlID = tbxFilter.ID,
                ServiceMethod = _serviceMethod,
                ServicePath = this.ServicePath,
                MinimumPrefixLength = this.MinimumPrelfixLength,
                CompletionSetCount = this.CompletionSetCount,
                CompletionInterval = this.CompletionInterval,
                EnableCaching = AutoCompleteEnableCaching
            };

            content.Add(autoComplete);
        }

        void tbxFilter_Load(object sender, EventArgs e)
        {
            var tbx = (TextBox)sender;
            var filterType = typeof(TFilter);
            if (_grdDataBindingNow && _sessVar.IsFilterOn(filterType))
            {
                var funktor = _sessVar.GetFilter(filterType) as Db.FilterFunctor<TEntity, string>;
                tbx.Text = funktor.RValue;
            }

        }

        void tbxFilter_TextChanged(object sender, EventArgs e)
        {
            var tbx = (TextBox)sender;
            if (!string.IsNullOrEmpty(tbx.Text))
            {
                var filter = new TFilter();
                filter.RValue = tbx.Text;
                _sessVar.AddFilter(filter);
            }
            else
                _sessVar.RemoveFilter(typeof(TFilter));
        }

    }
}
