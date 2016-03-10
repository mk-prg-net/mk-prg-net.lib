using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;

using MsWebCtrls = System.Web.UI.WebControls;
using MsHtmlCtrls = System.Web.UI.HtmlControls;

using mkx = mkoIt.Xhtml.Xhtml;
using Css = mkoIt.Xhtml.Css;
using Ekd = mko.Euklid;
using Graph = mko.Graphic;
using System.Diagnostics;


namespace mkoIt.Asp.HtmlCtrl
{
    public partial class Canvas : HtmlContainerCtrlBase, IScriptControl
    {
        ClientComponentScriptBld myScriptBld;

        public ClientComponentScriptBld ScriptBld
        {
            get
            {
                Debug.Assert(myScriptBld != null);
                return myScriptBld;
            }
        }

        public Canvas(string ID)
            : base("canvas")
        {
            CreateCanvas(ID);
        }

        public Canvas(string ID, out Canvas canvasRef)
            : base("canvas")
        {
            canvasRef = this;
            CreateCanvas(ID);
        }       

        void CreateCanvas(string ID)
        {
            this.ID = ID;
            AlternateText = "Canvas wird von diesem Browser nicht unterstützt";
            Width = 500;
            Height = 100;

            myScriptBld = new ClientComponentScriptBld(ClientID);
        }

        // Eigenschaften des Canvas- Objekte
        public int Width
        {
            get
            {
                try
                {
                    int val;
                    if (int.TryParse(Attributes["width"], out val))
                        return val;
                    else
                        return 0;
                }catch(Exception) {
                    return 0;
                }
            }

            set
            {
                try
                {
                    Attributes["width"] = value.ToString();
                }
                catch (Exception)
                {
                    Attributes.Add("width", value.ToString());
                }

            }
        }

        public int Height
        {
            get
            {
                try
                {
                    int val;
                    if (int.TryParse(Attributes["height"], out val))
                        return val;
                    else
                        return 0;
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            set
            {
                try
                {
                    Attributes["height"] = value.ToString();
                }
                catch (Exception)
                {
                    Attributes.Add("height", value.ToString());
                }

            }
        }

        // Hilfsfunktionen zum Aufbauen der Scripts

        // Zugriff auf Client- Komponente
        //string Get()
        //{
        //    return "$find('" + ClientID + "', null)";
        //}

        //string CreateMethodCall(params string[] mparams)
        //{

        //    string mcall = Get() + "." + mparams[0] + "(";

        //    // Parameter einbauen
        //    for (int i = 1; i < mparams.Length; i++)
        //        mcall += mparams[i] + ",";

        //    if (mcall.EndsWith(","))
        //        mcall = mcall.Substring(0, mcall.Length - 1);

        //    return mcall + ");return false;";

        //}



        public string AlternateText
        {
            set
            {
                InnerText = value;
            }
        }

        public override void PreRenderReworking(object sender)
        {
            base.PreRenderReworking(sender);

            var sm = ScriptManager.GetCurrent(Page);

            if (sm == null)
                throw new HttpException("A ScriptManager control must exist on the current Page.");

            // Script einbinden
            sm.RegisterScriptControl(this);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            // Anlegen der Clientinstanzen mit $create
            if (!this.DesignMode)
                ScriptManager.GetCurrent(Page).RegisterScriptDescriptors(this);
        }



        IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
        {
            // $create- Aufruf für die Instanzirieung der Componente beschreiben
            var descr = new ScriptControlDescriptor("mkoIt.Asp.Html.Graphic.D2", this.ClientID);

            return new ScriptDescriptor[] { descr };
        }

        IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
        {
            // Referenz auf eingebettetes Script zurückgeben
            var reference = new ScriptReference()
            {
                Assembly = "mko.Asp",
                Name = "mko.Asp.HtmlCanvas.js"
            };

            return new ScriptReference[] { reference };
        }
    }
}
