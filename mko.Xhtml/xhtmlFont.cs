using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Xhtml.Css
{

    public class Font : CssMeasure
    {
        public string Name { get; set; }

        public override string TextValue
        {
            get { return Name; }
        }

        public override bool Equals(object obj)
        {
            var sec = (Font)obj;
            return Name == sec.Name;
        }

        public static bool operator ==(Font fst, Font sec)
        {
            return fst.Equals(sec);
        }

        public static bool operator !=(Font fst, Font sec)
        {
            return !fst.Equals(sec);
        }



        public Font()
        {
            Name = "Arial, sans-serif";
        }

        public Font(string name)
        {
            Name = name;
        }


        /// <summary>
        /// Liste der allg. Font- Familien
        /// </summary>
        public static string[] BasicFontFamilies = new string[] {
                "serif",
                "sans-serif",
                "monospace",
                "cursive",
                "fantasy"
            };

        /// <summary>
        /// Tabelle der Fonts aus Open Office und ihre entsprechungen in Windows
        /// </summary>
        public static string[,] MapOOtoMsWinFonts = new string[,]
            {
                {"times new roman", "Times New Roman, serif"},
                {"arial", "Arial, sans-serif"},
                {"marlett", "Marlett, fantasy"},
                {"open symbol", "Arial, sans-serif"},                
                {"symbol", "Symbol, serif"},
                {"webdings", "Webdings, fantasy"},
                {"tahoma", "Tahoma, sans-serif"},
                {"courier new", "Courier New, monospace"},
                {"lucida console", "Lucida Console, sans-serif"},
                {"gautami", "Arial, sans-serif"},
                {"Lucida Sans Unicode", "Lucida Sans Unicode, sans-serif"},
                {"georgia", "Times New Roman, serif"},
                {"roman", "Times New Roman, serif"},
                {"mangal", "Arial, sans-serif"},
                {"trebuchet ms", "Trebuchet MS, sans-serif"},
                {"modern", "Trebuchet MS, sans-serif"},
                {"verdana", "Verdana, sans-serif"},
                {"comic sans ms", "Comic Sans MS, cursive"}
            };



        public static Font Parse(string txt)
        {
            if (string.IsNullOrEmpty(txt))
                return null;

            txt = txt.Trim().ToLower();

            if (BasicFontFamilies.Contains(txt))
                return new Font(txt);

            txt.Replace("0", "");
            txt.Replace("1", "");
            txt.Replace("2", "");
            txt.Replace("3", "");

            for (int i = 0; i < MapOOtoMsWinFonts.GetUpperBound(0); i++)
            {
                if (MapOOtoMsWinFonts[i, 0] == txt)
                    return new Font(MapOOtoMsWinFonts[i, 1]);
            }

            return new Font();
        }

        protected override object _Clone()
        {
            return new Font() { Name = this.Name };
        }


        // Objektfabriken
        public static Font Serif
        {
            get
            {
                return new Font() { Name = "serif" };
            }
        }


        public static Font SansSerif
        {
            get
            {
                return new Font() { Name = "sans-serif" };
            }
        }

        public static Font Monospace
        {
            get
            {
                return new Font() { Name = "monospace" };
            }
        }

        public static Font Cursive
        {
            get
            {
                return new Font() { Name = "cursive" };
            }
        }

        public static Font Fantasy
        {
            get
            {
                return new Font() { Name = "fantasy" };
            }
        }

        /// <summary>
        /// Vertreter einer Serifen- Schrift
        /// </summary>
        public static Font TimesNewRoman
        {
            get
            {
                return new Font() { Name = "Times New Roman, serif" };
            }
        }

        /// <summary>
        /// Vertreter einer serifenlosen Schrift
        /// </summary>
        public static Font Arial
        {
            get
            {
                return new Font() { Name = "Arial, sans-serif" };
            }
        }

        /// <summary>
        /// Vertreter einer monospaced- Schrift
        /// </summary>
        public static Font Courier
        {
            get
            {
                return new Font() { Name = "Courier New, monospace" };
            }
        }

        /// <summary>
        /// Vertreter einer Fantasy- Schrift
        /// </summary>
        public static Font Marlett
        {
            get
            {
                return new Font() { Name = "Marlett, fantasy" };
            }
        }


    }


}
