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
    public class ColHeaderFilterSelectUpdate<TEntity, TState> : ColTemplateBase
        where TEntity : class, new()
        where TState : mkoIt.Asp.SessionStateFilterAndSortEntities<TEntity>
    {

        public string HeadLine { get; set; }
        public string ToolTip { get; set; }

        public string CssClassHeadLine { get; set; }       
        public css.StyleBuilder StyleHeadLine {get; set;}

        public string FilterOnBtnText { get; set; }
        public string FilterOnBtnCssClass { get; set; }
        public css.StyleBuilder FilterOnBtnStyle { get; set; }
        public string FilterOnBtnToolTip { get; set; }

        public string FilterOffBtnText { get; set; }
        public string FilterOffBtnCssClass { get; set; }
        public css.StyleBuilder FilterOffBtnStyle { get; set; }
        public string FilterOffBtnToolTip { get; set; }


        System.Web.UI.WebControls.GridView _Grd;
        Page _Page;
        TState _State;

        public ColHeaderFilterSelectUpdate(System.Web.UI.WebControls.GridView grd, Page page, TState state)
        {
            _Page = page;
            _Grd = grd;
            _State = state;

            CssClassFrame = "FilterFrame";

            HeadLine = "Filter";
            ToolTip = "Filter auf der Tabelle aktivieren/deaktivieren";

            StyleFrame = new css.StyleBuilder()
            {
                Width = new css.LengthPixel() { Value = 50 },
                TextAlign = new css.TextAlign() { Value = css.TextAlign.Unit.left }
            };

            FilterOnBtnText = "|";
            FilterOnBtnToolTip = "Filter einschalten";
            FilterOnBtnStyle = new css.StyleBuilder()
            {
                ForeColor = css.Color.White,
                BackgroundColor = css.Color.Green,
                Width = new css.LengthPixel() { Value = 20 },
                PaddingLeft = new css.LengthPixel() { Value= 0},
                PaddingRight = new css.LengthPixel() {Value=0},
                TextAlign = new css.TextAlign() { Value = css.TextAlign.Unit.center}                
            };

            FilterOffBtnText = "O";
            FilterOffBtnToolTip = "Filter ausschalten";
            FilterOffBtnStyle = new css.StyleBuilder()
            {
                ForeColor = css.Color.White,
                BackgroundColor = new css.Color("#404040"),
                Width = new css.LengthPixel() { Value = 20 },
                PaddingLeft = new css.LengthPixel() { Value = 0 },
                PaddingRight = new css.LengthPixel() { Value = 0 },
                TextAlign = new css.TextAlign() { Value = css.TextAlign.Unit.center }
            };
            
        }

        protected override void CreateContent(Control NamingContainer, ControlCollection content)
        {
            var upd = new UpdatePanel(){
                ID = "updSetFilter",
                UpdateMode= UpdatePanelUpdateMode.Conditional,                
            };
            content.Add(upd);

            var updContent = upd.ContentTemplateContainer.Controls;


            var lbtHeadline = new LinkButton()
            {
                ID = "lbtCol0Headline",
                Text = HeadLine,
                ToolTip = this.ToolTip,               
                CssClass = CssClassHeadLine
            };

            if (StyleHeadLine != null)
                lbtHeadline.Attributes["style"] = StyleHeadLine.ToString();

            updContent.Add(lbtHeadline);
            updContent.Add(new mkoIt.Asp.HtmlCtrl.BR());

            var btnFilterOn = new Button()
            {
                ID="btnSetFilter",
                Text=FilterOnBtnText,
                ToolTip = FilterOnBtnToolTip                
            };
            if(FilterOnBtnStyle != null)
                btnFilterOn.Attributes["style"] = FilterOnBtnStyle.ToString();

            updContent.Add(btnFilterOn);
            btnFilterOn.Click += new EventHandler(btnSetFilter_Click);  

            var btnFilterOff = new Button()
            {
                ID = "btnRemoveFilter",
                Text = FilterOffBtnText,
                ToolTip = FilterOffBtnToolTip,
            };
            if(FilterOffBtnStyle != null)
                btnFilterOff.Attributes["style"] = FilterOffBtnStyle.ToString();

            updContent.Add(btnFilterOff);
            btnFilterOff.Click += new EventHandler(btnRemoveFilter_Click);     
    

            // Animationen hinzufügen
            var updAnimator = new AjaxControlToolkit.UpdatePanelAnimationExtender();
            updAnimator.TargetControlID = upd.ID;
            updAnimator.OnUpdated = new AjaxControlToolkit.Animation()
            {
                Name = "FadeOut"
            };

            updAnimator.OnUpdated.Properties["duration"] = "1";
            updAnimator.OnUpdated.Properties["maximumOpacity"] = "0.7";
            
            
            updAnimator.OnUpdating = new AjaxControlToolkit.Animation()
            {
                Name = "FadeIn"
            };

            updAnimator.OnUpdating.Properties["duration"] = "1";
            

            content.Add(updAnimator);

            
            
           
        }

        void btnSetFilter_Click(object sender, EventArgs e)
        {
            if (_Page.IsValid && _Grd.HeaderRow != null)
            {
                _Grd.PageIndex = 0;
                _Grd.DataBind();
            }

        }

        void btnRemoveFilter_Click(object sender, EventArgs e)
        {
            _State.RemoveAllFilters();

            // Erneuter Aufruf der Webseite ohne Filter- Restriktionen
            _Page.Response.Redirect(SiteMap.CurrentNode.Url, true);

        }
    }
}
