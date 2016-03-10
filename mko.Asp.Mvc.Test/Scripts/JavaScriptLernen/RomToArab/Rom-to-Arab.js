//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 20.2.2014
//
//  Projekt.......: mko.Asp.Test
//  Name..........: Rom-To-Arab.js
//  Aufgabe/Fkt...: Umwandlung von römische in arabische Zahlen
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: WebBrowser HTML5
//  Werkzeuge.....: Visual Studio 2012
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

// Initialisierung des Dokuments

$(document).ready(function () {

    // Eventhandler am Button "römisch in arabisch" registrieren
    $("#transformieren").click(function (event) {

        var romzahl = $("#roemzahl").val();

        var arabzahl = mko.algo.RomToArab(romzahl);

        if (isNaN(arabzahl))
            $("#arabzahl").val("ungültige Romzahl");
        else
            $("#arabzahl").val(arabzahl + "");

    });
});



// Namespace mko
var mko = {

    // Namespace algo
    algo: {
        RomToArab: function (romzahl) {
            var zustand = 'M';
            var wert = 0;           

            var i = 0;

            // Anzahl der Zeichen in eingabe bestimmen
            var laenge = romzahl.length;

            while (i < laenge && zustand != 'E') {

                var x = romzahl.charAt(i);
                // lese das Zeichen an Position i aus

                var res = mko.algo.zuef(zustand, x, wert);
                zustand = res.NeuerZustand;
                wert = res.NeuerWert;
                i++;
            }

            if (zustand == 'E') {
                return undefined;
            } else
                return wert;
        },

        CreateResZustandWert: function (zustand, wert) {
            return { NeuerZustand: zustand, NeuerWert: wert };
        },

        zuef: function (zustand, eingabe, wert) {

            switch (eingabe) {
                case 'M':
                    if (zustand == 'M') {

                        wert += 1000;
                        return mko.algo.CreateResZustandWert('M', wert);

                    } else
                        return mko.algo.CreateResZustandWert('E', wert);

                    break;
                case 'D':

                    if (zustand == 'M') {

                        wert += 500;
                        return mko.algo.CreateResZustandWert('D', wert);

                    } else
                        return mko.algo.CreateResZustandWert('E', wert);


                    break;
                case 'C':
                    if (zustand == 'M' || zustand == 'D' || zustand == 'C') {
                        wert += 100;
                        return mko.algo.CreateResZustandWert('C', wert);

                    } else
                        return mko.algo.CreateResZustandWert('E', wert);
                    break;
                case 'L':
                    if (zustand == 'M' || zustand == 'D' || zustand == 'C') {
                        wert += 50;
                        
                        return mko.algo.CreateResZustandWert('L', wert);

                    } else
                        return mko.algo.CreateResZustandWert('E', wert);


                    break;
                case 'X':
                    if (zustand == 'M' || zustand == 'D' || zustand == 'C' ||
                        zustand == 'L' || zustand == 'X') {
                        wert += 10;
                        
                        return mko.algo.CreateResZustandWert('X', wert);

                    } else
                        return mko.algo.CreateResZustandWert('E', wert);

                    break;
                case 'V':
                    if (zustand == 'M' || zustand == 'D' || zustand == 'C' ||
                        zustand == 'L' || zustand == 'X') {
                        wert += 5;

                        return mko.algo.CreateResZustandWert('V', wert);
                    } else if (zustand == 'I') {
                        wert += 3;
                        return mko.algo.CreateResZustandWert('IV', wert);
                    } else if (zustand == 'II') {
                        wert ++;
                        return mko.algo.CreateResZustandWert('IIV', wert);
                    } else
                        return mko.algo.CreateResZustandWert('E', wert);

                    break;
                case 'I':
                    if (zustand == 'M' || zustand == 'D' || zustand == 'C' ||
                        zustand == 'L' || zustand == 'X' || zustand == 'V') {
                        // 1. Eins
                        wert++;                        
                        return mko.algo.CreateResZustandWert('I', wert);
                    } else if (zustand == 'I') {
                        // 2. Eins
                        wert++;
                        return mko.algo.CreateResZustandWert('II', wert);
                    } else
                        return mko.algo.CreateResZustandWert('E', wert);

                    break;
                case 'E':
                    return mko.algo.CreateResZustandWert('E', wert);

                    break;
                default: return mko.algo.CreateResZustandWert('E', wert);
            }
        }
    }
};
