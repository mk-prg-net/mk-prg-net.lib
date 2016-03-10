//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den November 2012
//
//  Projekt.......: mko.Asp
//  Name..........: BindingDataField
//  Aufgabe/Fkt...: Bindet ein Datenfeld an ein Webcontrol mit dem Ziel, die Daten aus dem 
//                  WebControl in Datenfeld zu übertragen.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.0
//  Werkzeuge.....: Visual Studio 2010
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Asp.DataBind
{
 
    /// <summary>
    /// Definiert die Bindung eines Datenfeldes an eine Datenquelle (z.B. WebControls). 
    /// Die Aktualisierung von Datenfeldern durch Steuerelemente eines Webformulares werden hierdurch 
    /// beschrieben
    /// </summary>
    /// <typeparam name="TField">Typ des Datanfeldes</typeparam>
    public class BindingDataField<TField> : BindingBase
    {

        /// <summary>
        /// Lambda- Ausdruck, der 
        /// </summary>
        Func<TField> _calculateFieldValuesFromInputs;

        /// <summary>
        /// Funktionsausdruck, der den Abruf der Daten aus der Quelle und die Komposition zum neuen Feldwert
        /// beschreibt. 
        /// </summary>
        protected Func<TField> CalculateFieldValuesFromInputs
        {
            get
            {
                return _calculateFieldValuesFromInputs;
            }
        }

        public BindingDataField(Func<TField> CalculateFieldValuesFromInputs)
        {
            _calculateFieldValuesFromInputs = CalculateFieldValuesFromInputs;
        }
    }
}
