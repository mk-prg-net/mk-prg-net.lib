using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using css = mkoIt.Xhtml.Css;
using mkx = mkoIt.Xhtml.Xhtml;


namespace mkoIt.Asp.GridView
{
    public class ColEditItemFilterSelectUpdate : ColTemplateBase
    {
        public bool CausesValidation { get; set; }
        public string ValidationGroup { get; set; }

        public ColEditItemFilterSelectUpdate()
        {
            CausesValidation = false;
        }           

        protected override void CreateContent(System.Web.UI.Control NamingContainer, System.Web.UI.ControlCollection content)
        {
            content.Add(new Label() {
                ID="lblZeilenNr",
                Text = "0",
                Visible = false
            });

            content.Add(new Button()
            {
                ID = "btnUpdate",
                Text = "[+]",
                Width = new Unit(30, UnitType.Pixel),
                ToolTip = "Änderungen in Datenbank sichern",
                CausesValidation= this.CausesValidation,
                ValidationGroup = this.ValidationGroup,
                CommandName = "Update"                
            });

            content.Add(new Button()
            {
                ID = "btnCancel",
                Text = " / ",
                Width = new Unit(30, UnitType.Pixel),
                ToolTip = "Keine Änderungen in der Datenbank sichern",
                CausesValidation = this.CausesValidation,
                ValidationGroup = this.ValidationGroup,
                CommandName = "Cancel"
            });

        }
    }
}
