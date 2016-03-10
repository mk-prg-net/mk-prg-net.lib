using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using mko.Newton;

namespace mkoIt.Xhtml.Css
{
    public class StyleBuilderTable : StyleBuilder
    {
        public StyleBuilderTable()
        {
            TableBorderCollapse = new BorderCollapse() { Value = BorderCollapse.Unit.collapse };
            TableBorderSpacing = new LengthPixel() { Value = 0 };
            TableLayout = new TableLayout() { Value = TableLayout.Unit.auto };
            TableShowEmptyCells = new TableShowEmptyCells() { Value = TableShowEmptyCells.Unit.show };

            BorderWidth = new LengthPixel() { Value = 1 };
            BorderStyle = new BorderStyle() { Value = BorderStyle.Unit.solid };
            BorderColor = Color.Silver;
        }
    }

    public class StyleBuilderTableCell : StyleBuilder
    {
        public StyleBuilderTableCell()
        {
            BorderWidth = new LengthPixel() { Value = 1 };
            BorderStyle = new BorderStyle() { Value = BorderStyle.Unit.solid };
            BorderColor = Color.Silver;
            TextAlign = new TextAlign() { Value = TextAlign.Unit.left };
            VerticalAlign = new VerticalAlign() { Value = VerticalAlign.Unit.text_top };
        }
    }


    public class StyleBuilderTableHeaderCell : StyleBuilder
    {
        public StyleBuilderTableHeaderCell()
        {
            BorderWidth = new LengthPixel() { Value = 1 };
            BorderStyle = new BorderStyle() { Value = BorderStyle.Unit.solid };
            BorderColor = Color.Silver;
            TextAlign = new TextAlign() { Value = TextAlign.Unit.center };
            BackgroundColor = Color.Gray;
        }
    }
}

