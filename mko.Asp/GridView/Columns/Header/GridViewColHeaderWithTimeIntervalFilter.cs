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
    public class ColHeaderWithTimeIntervalFilter<TFilter, TEntity> : ColHeaderTemplateBase
        where TEntity : class, new()
        where TFilter : Db.FilterFunctor<TEntity, mko.Interval<DateTime>>, new()        
    {
        SessionStateFilterAndSortEntities<TEntity> _sessVar;
        bool _grdDataBindingNow = false;
        TextBox _tbxBegin;
        TextBox _tbxEnd;

        /// <summary>
        /// Ältestes Datum, das als Intervalluntergrenze zulässig ist 
        /// </summary>
        public DateTime BeginningOfTime { get; set; }

        /// <summary>
        /// Symbol, das den Zeitbeginn symbolisiert
        /// </summary>
        public string BeginningOfTimeSymbol = "alt";

        /// <summary>
        /// Modernstes Datum, das als Intervallobergrenz zulässig ist 
        /// </summary>
        public DateTime EndOfTime { get; set; }

        /// <summary>
        /// Symbol, das das Ende der Zeit symbolisiert
        /// </summary>
        public string EndOfTimeSymbol = "neu";


        public ColHeaderWithTimeIntervalFilter(System.Web.UI.WebControls.GridView grd, SessionStateFilterAndSortEntities<TEntity> sessVar)
        {
            _sessVar = sessVar;
            grd.DataBinding += new EventHandler(grd_DataBinding);


            BeginningOfTime = new DateTime(1945, 5, 8);
            EndOfTime = new DateTime(3000, 1, 1);
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
                        Width = new css.LengthRealtive() { Value = 100 }
                    },

                    Content = new Control[] {                       

                        // Button: Beginns Zeitintervall zurücksetzen
                        new HtmlCtrl.Button("btn" + ColName + "ResetStart"){
                            CssStyleBld = new css.StyleBuilder() {
                                TextAlign = css.TextAlign.Left,
                                Width = new css.LengthPixel() { Value = 15 },
                                PaddingLeft = new css.LengthPixel() {Value= 2},
                                PaddingRight = new css.LengthPixel() {Value = 2}
                            },
                            Text = "[",
                            SetClick = new EventHandler(btnResetStart_Click)                        
                        },

                        // Datumsbox Beginn Zeitintervall
                        new HtmlCtrl.DateBox("dbx" + ColName + "FltBegin"){

                            CssStyleBld = new css.StyleBuilder() {
                                Width = new css.LengthPixel {Value = 70}
                            },

                            SetLoad = new EventHandler(_tbxBegin_Load),
                            SetTextChanged = new EventHandler(_tbxBegin_TextChanged)

                        },    

                        new HtmlCtrl.BR(),
                            
                        // Button: Ende Zeitintervall zurücksetzen
                        new HtmlCtrl.Button("btn" + ColName + "ResetEnd"){
                            CssStyleBld = new css.StyleBuilder() {
                                TextAlign = css.TextAlign.Right,
                                Width = new css.LengthPixel() { Value = 15} ,
                                PaddingLeft = new css.LengthPixel() {Value= 2},
                                PaddingRight = new css.LengthPixel() {Value = 2}

                            },
                            Text = "]",
                            SetClick = new EventHandler(btnResetEnd_Click)                        
                        },


                        // Datumsbox End Zeitintervall
                        new HtmlCtrl.DateBox("dbx" + ColName + "FltEnd"){

                            CssStyleBld = new css.StyleBuilder() {
                                Width = new css.LengthPixel {Value = 70}
                            },

                            SetLoad = new EventHandler(_tbxEnd_Load),
                            SetTextChanged = new EventHandler(_tbxEnd_TextChanged)
                        }
                    }
                }
            );
        }

        /// <summary>
        ///  Eventhandler der Datumsboxen
        /// </summary>
        /// 
        void _tbxBegin_Load(object sender, EventArgs e)
        {
            _tbxBegin = sender as TextBox;
            var filterType = typeof(TFilter);
            if (_grdDataBindingNow && _sessVar.IsFilterOn(filterType))
            {
                var funktor = _sessVar.GetFilter(filterType) as Db.FilterFunctor<TEntity, mko.Interval<DateTime>>;
                _tbxBegin.Text = funktor.RValue.Begin.ToShortDateString();
            }
        }

        void _tbxEnd_Load(object sender, EventArgs e)
        {
            _tbxEnd = sender as TextBox;
            var filterType = typeof(TFilter);
            if (_grdDataBindingNow && _sessVar.IsFilterOn(filterType))
            {
                var funktor = _sessVar.GetFilter(filterType) as Db.FilterFunctor<TEntity, mko.Interval<DateTime>>;
                _tbxEnd.Text = funktor.RValue.End.ToShortDateString();
            }
        }

        bool OnChangedHandled = false;
        void _tbxBegin_TextChanged(object sender, EventArgs e)
        {
            if (!OnChangedHandled)
            {
                OnChangedHandled = true;
                OnChanged();
            }
        }

        void _tbxEnd_TextChanged(object sender, EventArgs e)
        {
            if (!OnChangedHandled)
            {
                OnChangedHandled = true;
                OnChanged();
            }
        }

        protected void OnChanged()
        {
            if (Restriktion)
            {
                var filter = new TFilter();
                filter.RValue = VonBis;
                filter.Description = ToolTip;
                _sessVar.AddFilter(filter);
            }
            else
                _sessVar.RemoveFilter(typeof(TFilter));
        }

        /// <summary>
        /// Eventhandler der Reset- Buttons
        /// </summary>

        void btnResetStart_Click(object sender, EventArgs e)
        {
            _tbxBegin.Text = BeginningOfTimeSymbol;
        }

        void btnResetEnd_Click(object sender, EventArgs e)
        {
            _tbxEnd.Text = EndOfTimeSymbol;
        }

        //-------------------------------------------------------------------------------


        protected void OnInitBeginAndEndOfTime(object sender, InitBeginAndEndOfTimeArgs e)
        {
            e.BeginningOfTime = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            e.EndOfTime = System.Data.SqlTypes.SqlDateTime.MaxValue.Value;
        }

        public bool Restriktion
        {
            get
            {
                if (VonBis.Begin > BeginningOfTime || VonBis.End < EndOfTime)
                    return true;
                else
                    return false;
            }
        }

        public mko.Interval<DateTime> VonBis
        {
            get
            {
                //Page.Validate();
                //if (Page.IsValid)
                //{
                mko.Interval<DateTime> _vonBis = new mko.Interval<DateTime>();
                if (_tbxBegin.Text.ToLower() == BeginningOfTimeSymbol || string.IsNullOrEmpty(_tbxBegin.Text))
                    _vonBis.Begin = BeginningOfTime;
                else
                {
                    DateTime von;
                    if (DateTime.TryParse(_tbxBegin.Text, out von))
                        _vonBis.Begin = von;
                    else
                        throw new Exception("\"von\" enthält keinen Datumswert");
                }

                if (_tbxEnd.Text.ToLower() == EndOfTimeSymbol || string.IsNullOrEmpty(_tbxEnd.Text))
                    _vonBis.End = EndOfTime;
                else
                {
                    DateTime bis;
                    if (DateTime.TryParse(_tbxEnd.Text, out bis))
                        _vonBis.End = bis;
                    else
                        throw new Exception("\"bis\" enthält keinen Datumswert");
                }

                //if (_vonBis.Begin > _vonBis.End)
                //    throw new VonBisZeitintervallException(true, true, "Das Enddatum für den definierten Zeitraum ist älter als das Startdatum");

                return _vonBis;
                //}
                //else
                //    throw new VonBisZeitintervallException(true, true, "Die Definition des Zeitraumes ist ungültig");
            }

            set
            {
                if (value.Begin == BeginningOfTime)
                    _tbxBegin.Text = BeginningOfTimeSymbol;
                else
                    _tbxBegin.Text = value.Begin.ToString("dd.MM.yyyy");

                if (value.End == EndOfTime)
                    _tbxEnd.Text = EndOfTimeSymbol;
                else
                    _tbxEnd.Text = value.End.ToString("dd.MM.yyyy");
            }

        }
    }
}
