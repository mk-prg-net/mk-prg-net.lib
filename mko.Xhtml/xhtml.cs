using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml
{
    public partial class Xhtml
    {

        public static System.Globalization.NumberFormatInfo XhtmlNumberFormatInfo = new System.Globalization.NumberFormatInfo();

        static Xhtml()
        {
            XhtmlNumberFormatInfo.CurrencyDecimalSeparator = ".";
            XhtmlNumberFormatInfo.NumberDecimalSeparator = ".";
        }

        public class XhtmlException : ApplicationException
        {
            public XhtmlException() : base("XhtmlException") { }
            public XhtmlException(string msg) : base(msg) { }
            public XhtmlException(string msg, Exception innerException) : base(msg, innerException) { }
        }

        public enum HtmlTags
        {            
            h1, h2, h3, h4, h5, h6,
            ul, ol, li,
            p, div,
            span,
            table, colgroup, col, tableRow, tableHeaderCell, tableDataCell,
            image, imagemap,
        }

        public static System.Xml.Linq.XName GetHtmlXTagName(HtmlTags tag)
        {
            return System.Xml.Linq.XName.Get(GetHtmlTagName(tag));
        }

        public static string GetHtmlTagName(HtmlTags tag)
        {
            switch (tag)
            {
                case HtmlTags.col:
                    return "col";
                case HtmlTags.colgroup:
                    return "colgroup";
                case HtmlTags.div:
                    return "div";
                case HtmlTags.h1:
                    return "h1";
                case HtmlTags.h2:
                    return "h2";
                case HtmlTags.h3:
                    return "h3";
                case HtmlTags.h4:
                    return "h4";
                case HtmlTags.h5:
                    return "h5";
                case HtmlTags.h6:
                    return "h6";
                case HtmlTags.ul:
                    return "ul";
                case HtmlTags.ol:
                    return "ol";
                case HtmlTags.li:
                    return "li";
                case HtmlTags.p:
                    return "p";
                case HtmlTags.imagemap:
                    return "imagemap";
                case HtmlTags.image:
                    return "img";
                case HtmlTags.span:
                    return "span";
                case HtmlTags.table:
                    return "table";
                case HtmlTags.tableDataCell:
                    return "td";
                case HtmlTags.tableHeaderCell:
                    return "th";
                case HtmlTags.tableRow:
                    return "tr";
                default:
                    throw new XhtmlException("GetHtmlTagName: unknown html- Tag: " + tag.ToString());
            }
        }

        public static HtmlTags[] PushTag(HtmlTags[] subStack, HtmlTags tag)
        {
            HtmlTags[] newStack = new HtmlTags[subStack.Length + 1];
            Array.Copy(subStack, newStack, subStack.Length);
            newStack[newStack.GetUpperBound(0)] = tag;
            return newStack;
        }

        public enum HtmlAttributes
        {
            img_alt_text,
            style, style_class,
            bgcolor,
            width, height,
            href, src, target,
            colspan, rowspan,  
            border, cellspacing
        }

        public static System.Xml.Linq.XName GetHtmlXAttributeName(HtmlAttributes att)
        {
            return System.Xml.Linq.XName.Get(GetHtmlAttributeName(att));
        }

        public static string GetHtmlAttributeName(HtmlAttributes att)
        {
            switch (att)
            {
                case HtmlAttributes.img_alt_text:
                    return "alt";
                case HtmlAttributes.bgcolor:
                    return "bgcolor";
                case HtmlAttributes.colspan:
                    return "colspan";
                case HtmlAttributes.height:
                    return "height";
                case HtmlAttributes.href:
                    return "href";
                case HtmlAttributes.rowspan:
                    return "rowspan";
                case HtmlAttributes.src:
                    return "src";
                case HtmlAttributes.style:
                    return "style";
                case HtmlAttributes.style_class:
                    return "class";
                case HtmlAttributes.target:
                    return "target";
                case HtmlAttributes.width:
                    return "width";                   
                case HtmlAttributes.border:
                    return "border";
                case HtmlAttributes.cellspacing:
                    return "cellspacing";
                default:
                    throw new XhtmlException("GetHtmlAttributeName: unknown html- Attribute: " + att.ToString());
            }
        }


    }
}
