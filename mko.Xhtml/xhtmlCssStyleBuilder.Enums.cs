using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{
    partial class StyleBuilder
    {
        public static string ToString(StyleBuilder.StyleKeys Key)
        {
            return Key.ToString("G").ToLower().Replace('_', '-');
        }

        public enum StyleKeys
        {
            visibility,
            background_color,
            border_collapse,
            border_spacing,
            border_color,
            border_bottom_color,
            border_left_color,
            border_right_color,
            border_top_color,
            border_style,
            border_bottom_style,
            border_left_style,
            border_right_style,
            border_top_style,
            border_width,
            border_bottom_width,
            border_left_width,
            border_right_width,
            border_top_width,
            color,
            clear,
            empty_cells,
            font_family,
            font_size,
            font_weight,
            font_style,
            FLOAT,
            height,
            margin,
            margin_bottom,
            margin_left,
            margin_right,
            margin_top,
            position,
            padding,
            padding_bottom,
            padding_left,
            padding_right,
            padding_top,
            table_layout,
            text_align,
            text_decoration,
            vertical_align,
            width,
        }
    }
}

