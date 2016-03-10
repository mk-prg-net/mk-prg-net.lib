//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 21.2.2012
//
//  Projekt.......: mkoAsp
//  Name..........: HtmlContainerCtrlBase
//  Aufgabe/Fkt...: Basisklasse für Html- Containerkontrols
//                  
//
//
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

using css = mkoIt.Xhtml.Css;


namespace mkoIt.Asp.HtmlCtrl
{
    public abstract class HtmlContainerCtrlBase : System.Web.UI.HtmlControls.HtmlGenericControl, ICss
    {
        public HtmlContainerCtrlBase(string tagname)
            : base(tagname)
        {
            PreRender += (sender, e) =>
            {
                // Überarbeitung des Html- Elementes vor der Auslieferung in abgeleiteten Klassen
                PreRenderReworking(sender);

                if (!string.IsNullOrEmpty(CssClassName)) Attributes.Add("class", CssClassName);
                if (CssStyleBld != null) Attributes.Add("style", CssStyleBld.ToString());

            };
        }

        public virtual void PreRenderReworking(Object sender)
        {
        }

        void ICss.PreRenderReworking()
        {
            PreRenderReworking(this);
        }

        public string CssClassName
        {
            get;
            set;
        }

        public css.StyleBuilder CssStyleBld
        {
            get;
            set;
        }

        /// <summary>
        /// Über diese Eigenschaft kann in einem Objektinitilisierer der Inhalt eines Containerelements
        /// definiert werden.
        /// </summary>
        public Control[] Content
        {
            get
            {
                var ct = new Control[Controls.Count];
                int ix = 0;
                foreach (var ctrl in Controls)
                {
                    ct[ix] = (Control)ctrl;
                }

                return ct;
            }
            set
            {
                foreach (var ctrl in value)
                {                    
                    Controls.Add(ctrl);
                }
            }
        }

        string ICss.CssClassName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        css.StyleBuilder ICss.CssStyleBld
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        Control ICss.Ctrl
        {
            get { return this; }
        }

        AttributeCollection ICss.CtrlAttributes
        {
            get {return this.Attributes; }
        }
    }
}
