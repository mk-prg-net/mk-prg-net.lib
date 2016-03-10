//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 23.3.2012
//
//  Projekt.......: mkoItAsp
//  Name..........: HtmlNumBox.cs
//  Aufgabe/Fkt...: Html- Fromularelement für nummerische Eingaben.
//                  bietet eine Tastatur für die Eingabe an.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 4.0
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
using MsWebCtrls = System.Web.UI.WebControls;
using MsHtmlCtrls = System.Web.UI.HtmlControls;

using mkx = mkoIt.Xhtml.Xhtml;

namespace mkoIt.Asp.HtmlCtrl
{
    public partial class NumBox : DIV, IScriptControl
    {
        TextBox tbx;
        DIV divNumPad;        

        // ID- Generatoren für die Bausteine der NumBox
        string IdTbx()
        {
            return ID + "_Tbx";            
        }

        string IdBtnNumPadOpen()
        {
            return ID + "_BtnNumPadOpen";
        }

        string IdNumPad()
        {
            return ID + "_NumPad";
        }

        string IdNumPadKey(int digit)
        {
            return ID + "_NumPad_" + digit;
        }

        string IdNumPadBackSpace()
        {
            return ID + "_NumPad_BackSpace";
        }

        string IdNumPadHide()
        {
            return ID + "_NumPad_Hide";
        }

        string IdNumPadClear()
        {
            return ID + "_NumPad_Clear";
        }

        string IdNumPadKomma()
        {
            return ID + "_NumPad_Komma";
        }

        string IdNumPadPoint()
        {
            return ID + "_NumPad_Point";
        }

        string IdClientComponent()
        {
            return ClientID + "_ClientComponent";        
        }

        // Schnittstelle zur Clientkomponente 
        ClientComponentScriptBld myClientComponent;


        // Bausteine der Numbox
        TableCell CreateDigitCell(int digit)
        {
            return new TableCell(
                      new Button(IdNumPadKey(digit))
                      {
                          Text = digit.ToString(),
                          SetClientClick = myClientComponent.AddDigit(digit)
                      });
        }

        TableCell CreateBackSpaceCell()
        {
            return new TableCell(
                new Button(IdNumPadBackSpace())
                {
                    Text = "D",
                    SetClientClick = myClientComponent.BackSpace()
                });
        }

        TableCell CreateClearCell()
        {
            return new TableCell(
                new Button(IdNumPadClear())
                {
                    Text = "CE",
                    SetClientClick = myClientComponent.Clear()
                });
        }

        TableCell CreateHideCell()
        {
            return new TableCell(
                new Button(IdNumPadHide())
                {
                    Text = "X",                    
                    SetClientClick = myClientComponent.HideNumPad(),

                    CssStyleBld = new Xhtml.Css.StyleBuilder() {
                        BackgroundColor = Xhtml.Css.Color.Red
                    }
                });
        }

        TableCell CreateKommaCell()
        {
            return new TableCell(
                new Button(IdNumPadKomma())
                {
                    Text = ",",
                    SetClientClick = myClientComponent.AddKomma()
                });
        }


        TableCell CreatePointCell()
        {
            return new TableCell(
                new Button(IdNumPadPoint())
                {
                    Text = ".",
                    SetClientClick = myClientComponent.AddPoint()
                });

        }

        TableRow CreateRow(int startDigit, TableCell ctrlKey)
        {
            return new TableRow(
                CreateDigitCell(startDigit),
                CreateDigitCell(startDigit + 1),
                CreateDigitCell(startDigit + 2),
                ctrlKey);
        }       

        // Konostruktor
        public NumBox(string ID)
        {
            CreateNumbox(ID);
        }

        public NumBox(string ID, out NumBox numBoxRef)
        {
            numBoxRef = this;
            CreateNumbox(ID);
        }

        private void CreateNumbox(string ID)
        {
            this.ID = ID;
            var ParentCssBld = CssStyleBld;
            myClientComponent = new ClientComponentScriptBld(IdClientComponent());

            Content = new Control[]{
                new TextBox(IdTbx(), out tbx)
                {
                    CssStyleBld = new Xhtml.Css.StyleBuilder() {
                        TextAlign = Xhtml.Css.TextAlign.Right
                    }
                },

                new Button(IdBtnNumPadOpen()){
                    Text = "#",
                    SetClientClick = myClientComponent.ShowNumPad()                    
                },

                new DIV(
                    IdNumPad(), 
                    out divNumPad,
                    new Control[] {
                        new Table(
                                    CreateRow(7, CreateHideCell()), 
                                    CreateRow(4, CreateBackSpaceCell()),
                                    CreateRow(1, CreateClearCell()),
                                    new TableRow(
                                            CreateDigitCell(0),
                                            CreatePointCell(),
                                            CreateKommaCell()
                                        )
                                )
                                {
                                    ID = this.ID + "TabNumPad",
                                    CssStyleBld = new Xhtml.Css.StyleBuilder()
                                    {
                                        TableBorderCollapse = Xhtml.Css.BorderCollapse.Collapse,
                                        Padding =  Xhtml.Css.Length.Pixel(3),
                                        Position = Xhtml.Css.Position.Fixed                                       
                                    }
                                }
                    })
                {
                    CssStyleBld = new Xhtml.Css.StyleBuilder() {
                        Visibility = Xhtml.Css.Visiblity.Collapse
                    }
                }
            };

        }

        /// <summary>
        /// Zugriff auf den Inhalt vom Server aus
        /// </summary>
        public string Text
        {
            get
            {
                return tbx.Text;
            }

            set
            {
                tbx.Text = value;
            }
        }
              

        public override void PreRenderReworking(object sender)
        {

            if (CssStyleBld != null && CssStyleBld.TextAlign == null)
                CssStyleBld.TextAlign = Xhtml.Css.TextAlign.Right;

            // Registrieren der Clientkomponente

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

        public EventHandler SetLoad
        {
            set
            {
                tbx.Load += new EventHandler(value);
            }
        }

        public EventHandler SetTextChanged
        {
            set
            {
                ((System.Web.UI.WebControls.TextBox)tbx).TextChanged += new EventHandler(value);
            }
        } 

        // Implementierung der IScriptControl- Schnittstelle

        IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
        {
            // $create- Aufruf für die Instanzirieung der Componente beschreiben
            var descr = new ScriptComponentDescriptor("mkoIt.Asp.Html.Ajax.NumBoxUtils");

            descr.AddProperty("id", IdClientComponent());
            descr.AddProperty("TextBoxId", tbx.ClientID);
            descr.AddProperty("NumPadId", divNumPad.ClientID);            

            return new ScriptDescriptor[] { descr };
        }

        IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
        {
            // Referenz auf eingebettetes Script zurückgeben
            var reference = new ScriptReference()
            {
                Assembly = "mko.Asp",
                Name = "mko.Asp.HtmlNumBox.js"
            };

            return new ScriptReference[] { reference };
        }
    }
}

