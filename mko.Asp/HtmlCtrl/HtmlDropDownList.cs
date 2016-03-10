//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.2.2010
//
//  Projekt.......: mkoItAsp
//  Name..........: HtmlDropDownList.cs
//  Aufgabe/Fkt...: Kapselung des HtmlForm DropDown- Controls
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
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using mkx = mkoIt.Xhtml.Xhtml;

using webUi = System.Web.UI.WebControls;


namespace mkoIt.Asp.HtmlCtrl
{
    public class DropDownList : FormCtrlBase<webUi.DropDownList>
    {
        public DropDownList(string ID)
            : base(ID) { }

        public webUi.ListItem[] Items
        {
            set
            {
                ctrl.Items.AddRange(value);
            }
        }

        public EventHandler SetLoad
        {
            set
            {
                ctrl.Load += value;
            }
        }

        public EventHandler SetSelectedIndexChanged
        {
            set
            {
                ctrl.SelectedIndexChanged += value;
            }
        }

        public ListItem SelectedItem
        {
            get
            {
                return ctrl.SelectedItem;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return ctrl.SelectedIndex;
            }
            set
            {
                ctrl.SelectedIndex = value;
            }
        }

        public string SelectedValue
        {
            get
            {
                return ctrl.SelectedValue;
            }

            set
            {
                ctrl.SelectedValue = value;
            }
        }

        public string DataSourceID
        {
            set
            {
                ctrl.DataSourceID = value;
            }
        }

        public string DataTextField
        {
            set
            {
                ctrl.DataTextField = value;
            }
        }

        public string DataValueField
        {
            set
            {
                ctrl.DataValueField = value;
            }
        }

        public bool AppendDataBoundItems
        {
            set
            {
                ctrl.AppendDataBoundItems = value;
            }
        }
    }
}
