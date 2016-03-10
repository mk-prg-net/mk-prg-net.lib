// 15.10.2014, mko
// Javascript- Grundwissen

jQuery.noConflict();

jQuery(document).ready(function () {

    // 
    jQuery("#start").click(function (e) {

        LokalVsGlobal();
        console.assert("number" == typeof bin_auch_global);

        bin_auch_global += 100;

        LokalVsGlobal();

        alert("Bin fertig");
    });

});

// Globale Variablen einrichten
globale_Variable = 99;
First = true;

function LokalVsGlobal() {
    
    console.assert("number" == typeof globale_Variable);
    globale_Variable += 100;
    

    // ohne var deklarierte Variablen innerhalb einer function sind 
    // auch global !
    if (First) {
        First = false;
        console.assert("undefined" == typeof bin_auch_global);
        bin_auch_global = 77;
    } else {
        console.assert("number" == typeof bin_auch_global);
    }
    

    // echte lokale Variablen werden mit var deklariert ! 
    console.assert("undefined" == typeof bin_lokal);
    var bin_lokal = 100;

    console.log("Werte der Variablen");
    console.log("globale_Variable: " + globale_Variable.toString());
    console.log("bin_auch_global : " + bin_auch_global.toString());
    console.log("bin_lokal       : " + bin_lokal.toString());
}
