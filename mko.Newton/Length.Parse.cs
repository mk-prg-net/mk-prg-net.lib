using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Newton
{
    partial class Length
    {
        public static Length Parse(string txt)
        {
            return parseImpl(txt,
                System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat);

        }

        public static Length Parse(string txt, System.Globalization.NumberFormatInfo nif)
        {
            return parseImpl(txt, nif);
        }

        private static Length parseImpl(string txt, System.Globalization.NumberFormatInfo nif)
        {
            if (string.IsNullOrEmpty(txt))
                throw new ArgumentNullException("leerer Zeichenkette kann nicht als MeasureedValueS interpretiert werden");

            txt = txt.Trim();

            // Trennen von Wert und Einheit

            var rx = new System.Text.RegularExpressions.Regex(@"\-{0,1}\s*\d+\" + nif.NumberDecimalSeparator + @"{0,1}\d*");
            var m = rx.Match(txt);
            if (!m.Success)
                throw new FormatException(txt + " kann nicht als MeasuredValueS eingelesen werden");

            string valueTxt = m.Value;
            double Value = double.Parse(valueTxt, nif);

            int startRest = m.Index + valueTxt.Length;

            if (startRest < txt.Length)
            {
                string unitTxt = txt.Substring(startRest);
                unitTxt = unitTxt.Trim().ToLower();

                if (unitTxt == "cm")
                    return Centimeter(Value);
                else if (unitTxt == "dm")
                    return Decimeter(Value);
                else if (unitTxt == "km")
                    return Kilometer(Value);
                else if (unitTxt == "m")
                    return Meter(Value);
                else if (unitTxt == "micro_m" || unitTxt == "µm")
                    return Micrometer(Value);
                else if (unitTxt == "mm")
                    return Millimeter(Value);
                else if (unitTxt == "nm")
                    return Nanometer(Value);
                else if (unitTxt == "pt")
                    return Point(Value);
                else if (unitTxt == "in" || unitTxt == "inch")
                    return Inch(Value);
                else
                    throw new ArgumentException("Unbekannte Einheit " + unitTxt);

            }

            throw new ArgumentException("Keine Einheit " + txt);
        }

    }
}
