using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using mkx = mkoIt.Xhtml.Xhtml;
using css = mkoIt.Xhtml.Css;


namespace mkoIt.Asp.GridView
{
    public class ColHeaderWithDblIntervalFilter<TFilter, TEntity> : ColHeaderTemplateBase
        where TEntity : class, new()
        where TFilter : Db.FilterFunctor<TEntity, mko.Interval<DateTime>>, new()
    {
        public class UIDblIntervallException : Exception
        {
            public UIDblIntervallException(bool vonErr, bool bisErr, string msg)
                : base(msg)
            {
                _vonErr = vonErr;
                _bisErr = bisErr;
            }

            bool _vonErr;
            public bool VonErr
            {
                get
                {
                    return _vonErr;
                }
            }

            bool _bisErr;
            public bool BisErr
            {
                get
                {
                    return _bisErr;
                }
            }
        }

        //-------------------------------------------------------------------------------------------
        // Events
        public class InitMinAndMaxArgs : EventArgs
        {
            //UIDblIntervall ctrl;
            //public InitMinAndMaxArgs(UIDblIntervall uiDblCtrl)
            //{
            //    ctrl = uiDblCtrl;
            //}

            public double MinimumValue
            {
                set
                {
                    //ctrl.MinimumValue = value;
                }
            }

            public double MaximumValue
            {
                set
                {
                    //ctrl.MaximumValue = value;
                }
            }

            public int Steps
            {
                set
                {
                    //ctrl.Steps = value;
                }
            }
        }

        public delegate void InitMinAndMaxEventHandler(object sender, InitMinAndMaxArgs args);
        public event InitMinAndMaxEventHandler InitMinAndMax;

        public event EventHandler IntervalChanged;


        //---------------------------------------------------------------------------------------

        SessionStateFilterAndSortEntities<TEntity> _sessVar;
        bool _grdDataBindingNow = false;
        TextBox _tbxVon;
        TextBox _tbxBis;

        // Kleinster Wert, das als Intervalluntergrenze zulässig ist

        double _MinimumValue;
        public double MinimumValue
        {
            get
            {
                return _MinimumValue;
            }
            set
            {
                _MinimumValue = value;

                //if (!IsPostBack)
                //{
                //    _tbxVon.Text = "min";
                //}
            }
        }

        // Modernstes Datum, das als Intervallobergrenz zulässig ist  
        double _MaximumValue;
        public double MaximumValue
        {
            get
            {
                return _MaximumValue;
            }
            set
            {
                _MaximumValue = value;

                //if (!IsPostBack)
                //{

                //    _tbxBis.Text = "max";
                //}
            }
        }

        int _Steps;
        public int Steps
        {
            set
            {
                _Steps = value;
            }
        }


        public ColHeaderWithDblIntervalFilter(System.Web.UI.WebControls.GridView grd, SessionStateFilterAndSortEntities<TEntity> sessVar)
        {

        }

        void grd_DataBinding(object sender, EventArgs e)
        {
            _grdDataBindingNow = true;
        }

        protected override void CreateFilterCtrl(System.Web.UI.Control NamingContainer, System.Web.UI.ControlCollection content)
        {
            content.Add(
                new HtmlCtrl.DIV()
                {
                    CssStyleBld = new css.StyleBuilder()
                    {
                        Position = css.Position.Relative,
                        Width =  css.Length.Percent(100)
                    },

                    Content = new Control[] {                       

                        // Button: Beginns Zeitintervall zurücksetzen
                        new HtmlCtrl.Button("btn" + ColName + "ResetStart"){
                            CssStyleBld = new css.StyleBuilder() {
                                TextAlign = css.TextAlign.Left,
                                Width = css.Length.Pixel(15),
                                PaddingLeft = css.Length.Pixel(2),
                                PaddingRight = css.Length.Pixel(2)
                            },
                            Text = "[",
                            SetClick = new EventHandler(btnResetVon_Click)                        
                        },

                        //// Zahlenbox Von
                        //new HtmlCtrl.DateBox("dbx" + ColName + "FltBegin"){

                        //    CssStyleBld = new css.StyleBuilder() {
                        //        Width = new css.LengthMeasurePixel {Value = 70}
                        //    },

                        //    SetLoad = new EventHandler(_tbxBegin_Load),
                        //    SetTextChanged = new EventHandler(_tbxBegin_TextChanged)

                        //},    

                        new HtmlCtrl.BR(),
                            
                        // Button: Ende Zeitintervall zurücksetzen
                        new HtmlCtrl.Button("btn" + ColName + "ResetEnd"){
                            CssStyleBld = new css.StyleBuilder() {
                                TextAlign = css.TextAlign.Right,
                                Width = css.Length.Pixel(15) ,
                                PaddingLeft = css.Length.Pixel(2),
                                PaddingRight = css.Length.Pixel(2)

                            },
                            Text = "]",
                            SetClick = new EventHandler(btnResetBis_Click)                        
                        },


                        //// Datumsbox End Zeitintervall
                        //new HtmlCtrl.DateBox("dbx" + ColName + "FltEnd"){

                        //    CssStyleBld = new css.StyleBuilder() {
                        //        Width = new css.LengthMeasurePixel {Value = 70}
                        //    },

                        //    SetLoad = new EventHandler(_tbxEnd_Load),
                        //    SetTextChanged = new EventHandler(_tbxEnd_TextChanged)
                        //}
                    }
                }
            );
        }

        /// <summary>
        /// Eventhandler der Reset- Buttons
        /// </summary>        
        /// 


        protected void btnResetVon_Click(object sender, EventArgs e)
        {
            _tbxVon.Text = "min";
            if (IntervalChanged != null)
                IntervalChanged(this, null);
        }

        protected void btnResetBis_Click(object sender, EventArgs e)
        {
            _tbxBis.Text = "max";
            if (IntervalChanged != null)
                IntervalChanged(this, null);
        }

        public bool Restriktion
        {
            get
            {
                if (Intervall.Begin > MinimumValue || Intervall.End < MaximumValue)
                    return true;
                else
                    return false;
            }
        }

        public mko.Interval<double> Intervall
        {
            get
            {
                //Page.Validate();
                //if (Page.IsValid)
                //{
                mko.Interval<double> _vonBis = new mko.Interval<double>();
                if (_tbxVon.Text.ToLower() == "min" || string.IsNullOrEmpty(_tbxVon.Text))
                    _vonBis.Begin = MinimumValue;
                else
                {
                    double von;
                    if (double.TryParse(_tbxVon.Text, out von))
                        _vonBis.Begin = von;
                    else
                        throw new UIDblIntervallException(true, false, "\"min\" enthält keinen Double- Wert");
                }

                if (_tbxBis.Text.ToLower() == "max" || string.IsNullOrEmpty(_tbxBis.Text))
                    _vonBis.End = MaximumValue;
                else
                {
                    double bis;
                    if (double.TryParse(_tbxBis.Text, out bis))
                        _vonBis.End = bis;
                    else
                        throw new UIDblIntervallException(false, true, "\"max\" enthält keinen Double- Wert");
                }

                //if (_vonBis.Begin > _vonBis.End)
                //    throw new UIDblIntervallException(true, true, "Das Intervallende ist kleiner als der Intervallanfang");

                return _vonBis;
                //}
                //else
                //    throw new UIDblIntervallException(true, true, "Die Definition des Intervalles ist ungültig");
            }

            set
            {
                System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
                //System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");

                if (value.Begin == MinimumValue)
                    _tbxVon.Text = "min";
                else
                    _tbxVon.Text = value.Begin.ToString(ci.NumberFormat);

                if (value.End == MaximumValue)
                    _tbxBis.Text = "max";
                else
                    _tbxBis.Text = value.End.ToString(ci.NumberFormat);
            }

        }



    }
}
