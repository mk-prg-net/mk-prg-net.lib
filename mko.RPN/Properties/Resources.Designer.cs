﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mko.RPN.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("mko.RPN.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to begin größer als end.
        /// </summary>
        internal static string begin_greater_then_end {
            get {
                return ResourceManager.GetString("begin_greater_then_end", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Parameter {0:D} befindet sich vor dem mit begin ... end definierten Subset von Tokens.
        /// </summary>
        internal static string begin_starts_later_then_param_i_of_function {
            get {
                return ResourceManager.GetString("begin_starts_later_then_param_i_of_function", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to boolscher Wert erwartet. Tatsächlich [0:s}. BoolToken oder die Zeichenketten &apos;true&apos; und &apos;false&apos; sind zulässig.
        /// </summary>
        internal static string boolean_expected {
            get {
                return ResourceManager.GetString("boolean_expected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to In dem String wurde ein boolsches Literal wie true oder false erwartet.
        /// </summary>
        internal static string boolean_literal_expected {
            get {
                return ResourceManager.GetString("boolean_literal_expected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Variadische Parameterliste ist nicht mit Lend abgeschlossen worden.
        /// </summary>
        internal static string ErrEval_VariadicParameterlistNotTerminiated {
            get {
                return ResourceManager.GetString("ErrEval_VariadicParameterlistNotTerminiated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Die Anzahl der Parameter für {0:s} ist ungültig..
        /// </summary>
        internal static string EvalErr_InvalidParameterCount {
            get {
                return ResourceManager.GetString("EvalErr_InvalidParameterCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der Funktionsname ist null oder leer.
        /// </summary>
        internal static string Functionname_null_or_empty {
            get {
                return ResourceManager.GetString("Functionname_null_or_empty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ein Funktions- Subtree wurde erwartet.
        /// </summary>
        internal static string FunctionSubtreeExpected {
            get {
                return ResourceManager.GetString("FunctionSubtreeExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Funktionstoken erwartet.
        /// </summary>
        internal static string functiontoken_expected {
            get {
                return ResourceManager.GetString("functiontoken_expected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Index lag außerhalb des Bereiches von Tokens, oder mit Index referenziertes Token ist keine Funktion.
        /// </summary>
        internal static string GetFunction_index_out_of_range {
            get {
                return ResourceManager.GetString("GetFunction_index_out_of_range", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der in Token aufzulösende Datenstrom/String wurde noch nicht definiert.
        /// </summary>
        internal static string missing_tokenizer_input_stream {
            get {
                return ResourceManager.GetString("missing_tokenizer_input_stream", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Die Tokens verweisen auf null.
        /// </summary>
        internal static string Null_Ref_Tokens {
            get {
                return ResourceManager.GetString("Null_Ref_Tokens", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to nummerischer Wert erwarter. Stattdessen erhalten: {0:s}.
        /// </summary>
        internal static string nummeric_expected {
            get {
                return ResourceManager.GetString("nummeric_expected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der {0:D}. Parameter existiert nicht. Es kann kein Parameter Subtree bestimmt werden.
        /// </summary>
        internal static string ParameterSubtreeDontExists {
            get {
                return ResourceManager.GetString("ParameterSubtreeDontExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der Index der Funktion, dessen Parameterindex bestimmt werden soll, war außerhalb des gültigen Bereiches.
        /// </summary>
        internal static string ParserHelpler_IndexOfFunctionParameter_Function_Index_out_of_Range {
            get {
                return ResourceManager.GetString("ParserHelpler_IndexOfFunctionParameter_Function_Index_out_of_Range", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Der Beginn des Suchbereiches liegt innerhalb der Parameterliste einer Funktion.
        /// </summary>
        internal static string range_of_search_starts_inside_parameterlist {
            get {
                return ResourceManager.GetString("range_of_search_starts_inside_parameterlist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Beim Tokenisieren ist ein Fehler aufgetreten. Siehe innere Ausnahme..
        /// </summary>
        internal static string tokenizing_failed {
            get {
                return ResourceManager.GetString("tokenizing_failed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Einem durch Delimiter begrenzten String fehlt der abschließende Delimiter. .
        /// </summary>
        internal static string tokenizing_failed_final_string_delimiter_missing {
            get {
                return ResourceManager.GetString("tokenizing_failed_final_string_delimiter_missing", resourceCulture);
            }
        }
    }
}
