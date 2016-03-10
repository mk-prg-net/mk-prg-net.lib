using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using mko.Newton;
using mko.Algo.FunctionalProgramming;

namespace mkoIt.Xhtml.Css
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InheritableAttribute : Attribute
    {
        public InheritableAttribute() { }
    }

    public partial class StyleBuilder
    {
        // Liste aller definierbaren Stilregeln als Eigenschaften des StyleBuilders.
        // 

        // Sichtbarkeit
        [Inheritable]
        public Visiblity Visibility
        {
            get
            {
                return GetStyle<Visiblity>(StyleKeys.visibility);
            }

            set
            {
                SetStyle(StyleKeys.visibility, value, true);
            }
        }

        // Farben im Vorder- und Hintergrund
        [Inheritable]
        public Color BackgroundColor {
            get
            {
                return GetStyle<Color>(StyleKeys.background_color);
            }
            set
            {
                SetStyle(StyleKeys.background_color, value, true);
            }
        }

        [Inheritable]
        public Color ForeColor
        {
            get
            {
                return GetStyle<Color>(StyleKeys.color);
            }
            set
            {
                SetStyle(StyleKeys.color, value, true);
            }
        }

        [Inheritable]
        public TextAlign TextAlign
        {
            get
            {
                return GetStyle<TextAlign>(StyleKeys.text_align);
            }
            set
            {
                SetStyle(StyleKeys.text_align, value, true);
            }
        }

        [Inheritable]
        public TextDecoration TextDecoration
        {
            get
            {
                return GetStyle<TextDecoration>(StyleKeys.text_decoration);
            }
            set
            {
                SetStyle(StyleKeys.text_decoration, value, true);
            }
        }

        [Inheritable]
        public VerticalAlign VerticalAlign
        {
            get
            {
                return GetStyle<VerticalAlign>(StyleKeys.vertical_align);
            }
            set
            {
                SetStyle(StyleKeys.vertical_align, value, true);
            }
        }

        // Schriftgestaltung
        [Inheritable]
        public Font FontFamiliy
        {
            get
            {
                return GetStyle<Font>(StyleKeys.font_family);
            }
            set
            {
                SetStyle(StyleKeys.font_family, value, true);
            }
        }

        [Inheritable]
        public FontSize FontSize
        {
            get
            {
                return GetStyle<FontSize>(StyleKeys.font_size);
            }
            set
            {
                SetStyle(StyleKeys.font_size, value, true);
            }
        }

        [Inheritable]
        public FontWeight FontWeight 
        {
            get
            {
                return GetStyle<FontWeight>(StyleKeys.font_weight);
            }
            set
            {
                SetStyle(StyleKeys.font_weight, value, true);
            }
        }

        [Inheritable]
        public FontStyle FontStyle
        {
            get
            {
                return GetStyle<FontStyle>(StyleKeys.font_style);
            }
            set
            {
                SetStyle(StyleKeys.font_style, value, true);
            }
        }


        // Allgemeine Breite und Höhe
        public Length Width
        {
            get
            {
                return GetStyle<Length>(StyleKeys.width);
            }
            set
            {
                SetStyle(StyleKeys.width, value, false);
            }
        }

        public Length Height
        {
            get
            {
                return GetStyle<Length>(StyleKeys.height);
            }
            set
            {
                SetStyle(StyleKeys.height, value, false);
            }
        }

        // Abstand eines Elements zu den Nachbarn
        public Length Margin
        {
            get
            {
                return GetStyle<Length>(StyleKeys.margin);
            }
            set
            {
                // alle speziellen Formen löschen
                RemoveIfContains(StyleKeys.margin_top);
                RemoveIfContains(StyleKeys.margin_bottom);
                RemoveIfContains(StyleKeys.margin_left);
                RemoveIfContains(StyleKeys.margin_right);
                SetStyle(StyleKeys.margin, value, false);
            }
        }

        public Length MarginTop
        {
            get
            {
                return GetStyle<Length>(StyleKeys.margin_top);
            }
            set
            {
                RemoveIfContains(StyleKeys.margin);
                SetStyle(StyleKeys.margin_top, value, false);
            }
        }

        public Length MarginBottom
        {
            get
            {
                return GetStyle<Length>(StyleKeys.margin_bottom);
            }
            set
            {
                RemoveIfContains(StyleKeys.margin);
                SetStyle(StyleKeys.margin_bottom, value, false);
            }
        }

        public Length MarginLeft
        {
            get
            {
                return GetStyle<Length>(StyleKeys.margin_left);
            }
            set
            {
                RemoveIfContains(StyleKeys.margin);
                SetStyle(StyleKeys.margin_left, value, false);
            }
        }

        public Length MarginRight
        {
            get
            {
                return GetStyle<Length>(StyleKeys.margin_right);
            }
            set
            {
                RemoveIfContains(StyleKeys.margin);
                SetStyle(StyleKeys.margin_right, value, false);
            }
        }

        // Positionierung            
        public Position Position
        {
            get
            {
                return GetStyle<Position>(StyleKeys.position);
            }
            set
            {
                SetStyle(StyleKeys.position, value, false);
            }
        }

        // Textumlauf            
        public Float Float
        {
            get
            {
                return GetStyle<Float>(StyleKeys.FLOAT);
            }
            set
            {
                SetStyle(StyleKeys.FLOAT, value, false);
            }
        }

        // Textumlauf beenden
        public Clear Clear
        {
            get
            {
                return GetStyle<Clear>(StyleKeys.clear);
            }
            set
            {
                SetStyle(StyleKeys.clear, value, false);
            }
        }

        // Abstand des Elementinhaltes zur den Rändern der das Element umschreibenden Box
        public Length Padding
        {
            get
            {
                return GetStyle<Length>(StyleKeys.padding);
            }
            set
            {
                RemoveIfContains(StyleKeys.padding_top);
                RemoveIfContains(StyleKeys.padding_bottom);
                RemoveIfContains(StyleKeys.padding_left);
                RemoveIfContains(StyleKeys.padding_right);
                SetStyle(StyleKeys.padding, value, false);
            }
        }

        public Length PaddingTop
        {
            get
            {
                return GetStyle<Length>(StyleKeys.padding_top);
            }
            set
            {
                RemoveIfContains(StyleKeys.padding);
                SetStyle(StyleKeys.padding_top, value, false);
            }
        }

        public Length PaddingBottom
        {
            get
            {
                return GetStyle<Length>(StyleKeys.padding_bottom);
            }
            set
            {
                RemoveIfContains(StyleKeys.padding);
                SetStyle(StyleKeys.padding_bottom, value, false);
            }
        }

        public Length PaddingLeft
        {
            get
            {
                return GetStyle<Length>(StyleKeys.padding_left);
            }
            set
            {
                RemoveIfContains(StyleKeys.padding);
                SetStyle(StyleKeys.padding_left, value, false);
            }
        }

        public Length PaddingRight
        {
            get
            {
                return GetStyle<Length>(StyleKeys.padding_right);
            }
            set
            {
                RemoveIfContains(StyleKeys.padding);
                SetStyle(StyleKeys.padding_right, value, false);
            }
        }

        // Ränder der das Element umschreibenden Box
        public Color BorderColor
        {
            get
            {
                return GetStyle<Color>(StyleKeys.border_color);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_top_color);
                RemoveIfContains(StyleKeys.border_bottom_color);
                RemoveIfContains(StyleKeys.border_left_color);
                RemoveIfContains(StyleKeys.border_right_color);
                SetStyle(StyleKeys.border_color, value, false);
            }
        }

        public Color BorderColorTop
        {
            get
            {
                return GetStyle<Color>(StyleKeys.border_top_color);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_color);
                SetStyle(StyleKeys.border_top_color, value, false);
            }
        }

        public Color BorderColorBottom
        {
            get
            {
                return GetStyle<Color>(StyleKeys.border_bottom_color);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_color);
                SetStyle(StyleKeys.border_bottom_color, value, false);
            }
        }

        public Color BorderColorLeft
        {
            get
            {
                return GetStyle<Color>(StyleKeys.border_left_color);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_color);
                SetStyle(StyleKeys.border_left_color, value, false);
            }
        }

        public Color BorderColorRight
        {
            get
            {
                return GetStyle<Color>(StyleKeys.border_right_color);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_color);
                SetStyle(StyleKeys.border_right_color, value, false);
            }
        }

        public BorderStyle BorderStyle
        {
            get
            {
                return GetStyle<BorderStyle>(StyleKeys.border_style);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_top_style);
                RemoveIfContains(StyleKeys.border_bottom_style);
                RemoveIfContains(StyleKeys.border_left_style);
                RemoveIfContains(StyleKeys.border_right_style);
                SetStyle(StyleKeys.border_style, value, false);
            }
        }

        public BorderStyle BorderStyleTop
        {
            get
            {
                return GetStyle<BorderStyle>(StyleKeys.border_top_style);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_style);
                SetStyle(StyleKeys.border_top_style, value, false);
            }
        }

        public BorderStyle BorderStyleBottom
        {
            get
            {
                return GetStyle<BorderStyle>(StyleKeys.border_bottom_style);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_style);
                SetStyle(StyleKeys.border_bottom_style, value, false);
            }
        }

        public BorderStyle BorderStyleLeft
        {
            get
            {
                return GetStyle<BorderStyle>(StyleKeys.border_left_style);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_style);
                SetStyle(StyleKeys.border_left_style, value, false);
            }
        }

        public BorderStyle BorderStyleRight
        {
            get
            {
                return GetStyle<BorderStyle>(StyleKeys.border_right_style);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_style);
                SetStyle(StyleKeys.border_right_style, value, false);
            }
        }

        public Length BorderWidth
        {
            get
            {
                return GetStyle<Length>(StyleKeys.border_width);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_top_width);
                RemoveIfContains(StyleKeys.border_bottom_width);
                RemoveIfContains(StyleKeys.border_left_width);
                RemoveIfContains(StyleKeys.border_right_width);
                SetStyle(StyleKeys.border_width, value, false);
            }
        }

        public Length BorderWidthTop
        {
            get
            {
                return GetStyle<Length>(StyleKeys.border_top_width);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_width);
                SetStyle(StyleKeys.border_top_width, value, false);
            }
        }


        public Length BorderWidthBottom
        {
            get
            {
                return GetStyle<Length>(StyleKeys.border_bottom_width);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_width);
                SetStyle(StyleKeys.border_bottom_width, value, false);
            }
        }

        public Length BorderWidthLeft
        {
            get
            {
                return GetStyle<Length>(StyleKeys.border_left_width);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_width);
                SetStyle(StyleKeys.border_left_width, value, false);
            }
        }

        public Length BorderWidthRight
        {
            get
            {
                return GetStyle<Length>(StyleKeys.border_right_width);
            }
            set
            {
                RemoveIfContains(StyleKeys.border_width);
                SetStyle(StyleKeys.border_right_width, value, false);
            }
        }

        // Tabellenattribute
        public TableLayout TableLayout
        {
            get
            {
                return GetStyle<TableLayout>(StyleKeys.table_layout);
            }
            set
            {
                SetStyle(StyleKeys.table_layout, value, false);
            }
        }

        public BorderCollapse TableBorderCollapse
        {
            get
            {
                return GetStyle<BorderCollapse>(StyleKeys.border_collapse);
            }
            set
            {
                SetStyle(StyleKeys.border_collapse, value, false);
            }
        }

        public Length TableBorderSpacing
        {
            get
            {
                return GetStyle<Length>(StyleKeys.border_spacing);
            }
            set
            {
                SetStyle(StyleKeys.border_spacing, value, false);
            }
        }

        public TableShowEmptyCells TableShowEmptyCells
        {
            get
            {
                return GetStyle<TableShowEmptyCells>(StyleKeys.empty_cells);
            }
            set
            {
                SetStyle(StyleKeys.empty_cells, value, false);
            }
        }
        
        /// <summary>
        /// Definition eines CSS-Styles. Besteht im Wesentlichen aus einem Schlüssel und einem 
        /// Wert. Der Wert ist dabei streng typisiert.
        /// </summary>
        class StyleDefinition : ICloneable
        {
            public CssMeasure MeasuredValue { get; set; }            
            public bool IsInheritable { get; set; }
            public StyleBuilder.StyleKeys Key { get; set; }

            /// <summary>
            /// Dient zum Aufbauen eines Css- style- Attributwertes 
            /// </summary>
            /// <param name="Key"></param>
            /// <param name="bld"></param>
            public override string ToString()
            {
                return StyleBuilder.ToString(Key) + ": " + MeasuredValue.TextValue + "; ";
            }

            public string StyleRuleAsString()
            {
                return ToString();
            }

            public object Clone()
            {
                return new StyleDefinition()
                {
                    MeasuredValue = (CssMeasure)MeasuredValue.Clone(),
                    IsInheritable = IsInheritable,
                    Key = Key
                };
            }
        }

        //public List<Action<StringBuilder>> SetStyleDefinitionJobs = new List<Action<StringBuilder>>();

        /// <summary>
        /// Liste aller aktuell definierten Stilregeln.
        /// </summary>
        Dictionary<StyleKeys, StyleDefinition> CurrentStyles = new Dictionary<StyleKeys, StyleDefinition>();

        /// <summary>
        /// Hilfsmethode zum abrufen eines aktuell definierten Stils
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="StyleKey"></param>
        /// <returns></returns>
        T GetStyle<T>(StyleKeys StyleKey)
            where T : CssMeasure
        {
            return CurrentStyles.ContainsKey(StyleKey) ? (T)CurrentStyles[StyleKey].MeasuredValue : null;
        }

        /// <summary>
        /// Hilfsmethode zum Hinzufügen eines aktuell definierten Stils
        /// </summary>
        /// <param name="StyleKey"></param>
        /// <param name="Value"></param>
        /// <param name="IsInheritable"></param>
        void SetStyle(StyleKeys StyleKey, CssMeasure Value, bool IsInheritable)
        {
            CurrentStyles[StyleKey] = new StyleDefinition()
            {
                Key = StyleKey,
                IsInheritable = IsInheritable,
                MeasuredValue = Value
            };
        }

        /// <summary>
        /// Hilfsmethode zum löschen von Stilangaben im Fall des Setzens einer Übergeordneten oder Untergeordneten
        /// wie z.B. margins, margins-top
        /// </summary>
        /// <param name="key"></param>
        private void RemoveIfContains(StyleKeys key)
        {
            if (CurrentStyles.Keys.Contains(key))
                CurrentStyles.Remove(key);
        }

        
        /// <summary>
        /// Erzeugt aus den definierten Stilregeln eine CSS- Textkonstante
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder bld = new StringBuilder();

            foreach (var s in CurrentStyles)
                bld.Append(s.Value.StyleRuleAsString());

            return bld.ToString();
        }

        // Konstruktoren
        public StyleBuilder() { }

        // Copy- Konstruktor
        public StyleBuilder(StyleBuilder bld)
        {
            foreach (var s in bld.CurrentStyles)
                CurrentStyles[s.Value.Key] = (StyleDefinition)s.Value.Clone();
        }

        /// <summary>
        /// Liefert einen Style- Builder zurück, der nur die Styledefinitionen aus Child enthält, die nicht von Parent
        /// geerbt wurden oder nicht vererbbar sind. Die Filterung erfolgt nach folgende Regeln:
        /// Hat Parent eine vererbbare Eigenschaft, die Child nicht hat, dann hat Diff diese nicht.
        /// Hat Child eine Eigenschaft, die Parent nicht hat, dann hat Diff diese auch.
        /// Haben Parent und Child eine Eigenschaft, die jedoch im Wert verschieden ist, dann hat Diff die von Child
        /// Haben Parent und Child eine vererbbare Eigenschaft, die im Wert gleich ist, dann hat Diff diese nicht.
        /// </summary>
        /// <param name="Parent"></param>
        /// <param name="Child"></param>
        /// <returns></returns>
        public static StyleBuilder GetDifferenceOf(StyleBuilder Parent, StyleBuilder Child)
        {
            if (Parent == null)
                return Child;

            var diff = new StyleBuilder();

            // Alle nicht vererbbaren Eigenschaften aus Style in Diff übernehmen
            foreach(var csDef in Child.CurrentStyles.Where(s => !s.Value.IsInheritable).Select(s => s.Value)) {
                diff.CurrentStyles[csDef.Key] = (StyleDefinition)csDef.Clone();
            }

            var psDefs = Parent.CurrentStyles.Where(s => s.Value.IsInheritable == true).Select(s => s.Value);
            var csDefs = Child.CurrentStyles.Where(s => s.Value.IsInheritable == true).Select(s => s.Value);

            /// Hat Parent eine vererbbare Eigenschaft, die Child nicht hat, dann hat Diff diese nicht.
            /// => Entscheiden, welche vererbbare StyleDefinitionen aus Child in Diff kopiert werden sollen
            foreach (var csDef in csDefs)
            {
                /// Hat Child eine Eigenschaft, die Parent nicht hat, dann hat Diff diese auch.                
                /// 
                if (!psDefs.Any(s => s.Key == csDef.Key))
                    diff.CurrentStyles[csDef.Key] = (StyleDefinition)csDef.Clone();

                /// Haben Parent und Child eine Eigenschaft, die jedoch im Wert verschieden ist, dann hat Diff die von Child
                /// Vergleich der Werte über ihre textuelle Darstellung-> Optimierungsbedarf ! 
                else if (psDefs.Any(s => s.Key == csDef.Key) && psDefs.First(s => s.Key == csDef.Key).MeasuredValue.TextValue != csDef.MeasuredValue.TextValue)
                    diff.CurrentStyles[csDef.Key] = (StyleDefinition)csDef.Clone();

                /// Haben Parent und Child eine Eigenschaft, die im Wert gleich ist, dann hat Diff diese nicht.
                /// -> nichts in Diff einfügen
            }

            return diff;
        }

        /// <summary>
        /// Gibt true zurück, wenn beide Stylebuilder bezüglich der enthaltenen Stilregeln gleich sind
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var sec = obj as StyleBuilder;
            
            // Bei GLeichheit müssen beide StyleBuilder dieselbe Anzahl an Stilregeln enthalten
            if (CurrentStyles.Count == sec.CurrentStyles.Count)
            {
                foreach (var key in CurrentStyles.Keys)
                {
                    // Jeden enthaltenen Stil in beiden auf Gleichheit überprüfen
                    if (sec.CurrentStyles.Keys.Contains(key))
                    {
                        if (!CurrentStyles[key].MeasuredValue.Equals(sec.CurrentStyles[key].MeasuredValue))
                        {
                            return false;                            
                        }
                    }
                }
                // Alle Vergleiche waren erfolgreich
                return true;
            }
            else return false;
        }

    }

}
