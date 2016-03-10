using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Security.Permissions;



namespace mkoIt.Asp
{
    public class TextBoxExt : TextBox
    {
        public TextBoxExt()
        {
            if (AltNullValue != null)
                Text = AltNullValue;
        }

        [Category("Appearance"),
         DefaultValue(""),
         Bindable(true),
         Description("Alternativer Nullwert: Wenn der Benutzer nichts, oder das in NullDisplaySymbol registrierte Symbol eingibt, dann wird der hier registrierte Wert in Text zurückgegeben")]
        public string AltNullValue
        {
            get
            {
                if (ViewState["AltNullValue"] != null)
                {
                    return (string)ViewState["AltNullValue"];
                }
                else
                    return string.Empty;
            }
            set
            {
                ViewState["AltNullValue"] = value;
            }
        }

        [Category("Appearance"),
         DefaultValue(""),
         Description("Symbol, das in einer Textbox anstelle eines Null- Wertes angezeigt werden soll")]
        public string NullDisplaySymbol
        {
            get
            {
                if (ViewState["NullDisplaySymbol"] != null)
                {
                    return (string)ViewState["NullDisplaySymbol"];
                }
                else return string.Empty;
            }
            set
            {
                ViewState["NullDisplaySymbol"] = value;                
            }
        }

        bool _InputValid = true;
        public bool InputValid
        {
            get
            {
                return _InputValid;
            }
            set
            {
                _InputValid = value;
            }
        }

        protected override bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            string presentValue = Text;
            string postedValue = postCollection[postDataKey];

            if (presentValue == null || !presentValue.Equals(postedValue) || postedValue == NullDisplaySymbol)
            {
                if (postedValue == NullDisplaySymbol)
                    Text = AltNullValue;
                else
                    Text = postedValue;
                return true;
            }
            return false;            
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string val = Text;
            string color = "Green";
            if (AltNullValue != null && Text == AltNullValue){
                val = NullDisplaySymbol;
                color = "Yellow";
            }
            if (!_InputValid)
                color = "Red";

            if (!Font.Size.IsEmpty)
            {
                writer.Write(string.Format("<table cellspacing=\"0\" cellpadding=\"0\" ><tr><td style=\"width: {0:D}; font-family: {1}; font-size: {2}\">", (int)Width.Value + 2, Font.Name, Font.Size.ToString()));
                if(ReadOnly)
                    writer.Write(string.Format("<input type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{1}\" style=\"width: {2}; font-family: {3}; font-size: {4}\" readonly />", UniqueID, val, Width.ToString(), Font.Name, Font.Size.ToString()));
                else
                    writer.Write(string.Format("<input type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{1}\" style=\"width: {2}; font-family: {3}; font-size: {4}\" />", UniqueID, val, Width.ToString(), Font.Name, Font.Size.ToString()));
            }
            else
            {
                writer.Write(string.Format("<table cellspacing=\"0\" cellpadding=\"0\" ><tr><td style=\"width: {0:D}\">", (int)Width.Value + 2));
                if(ReadOnly)
                    writer.Write(string.Format("<input type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{1}\" style=\"width: {2};\" readonly />", UniqueID, val, Width.ToString()));
                else
                    writer.Write(string.Format("<input type=\"text\" name=\"{0}\" id=\"{0}\" value=\"{1}\" style=\"width: {2};\" />", UniqueID, val, Width.ToString()));

            }
            
            writer.Write("</td></tr><tr >");
            writer.Write(string.Format("<td style=\"background-color: {0}; height: 5px; font-size: 1pt;\">&nbsp;</td></tr></table>", color));
        }



    }
}

