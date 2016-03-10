// Initialisierung des Dokumentes

// $ als jQuery- Symbol abschalten-> keine Kollision mit anderen Lib's, die Dollar einsetzen mehr möglich
jQuery.noConflict();


jQuery(document).ready(function () {


    // Clickhandler für btnAdd, welcher Eingabe in tbxNewValue an die Listbox anhängt
    jQuery("#btnAdd").click(function () {
        try {
            console.log("Bin im btnAdd- Clickhandler");

            // Neuen Wert aus der Textbox abrufen
            var newValue = jQuery("#tbxNewValue").val();

            console.log("Neuer Wert: " + newValue);

            // Listbox mit dem neuen Wert erweitern

            jQuery("#Data").append('<option style="text-align: right" value="' + newValue + '">' + newValue + '</option>');

            jQuery("#tbxNewValue").focus();

            console.log("append erfolgreich");
        } catch (err) {
            console.log("fehler in btnAdd: " + err.toString());
        }
    });


    jQuery("#btnDeleteSelected").click(function () {
        try {
            // Selektierten Wert in der Listbox bestimmen
            jQuery("#Data").children('option[selected="selected"]').remove();
        } catch (err) {
            cosole.log("fehler in btnDeleteSelected: " + err.toString());
        }
    });

    
    jQuery("form").submit(function () {
        try {
            // Alle Elemente in der Listbox selektieren, damit sie an den Server mitgesendet werden (nur selektierte 
            // werden an den Server gesendet
            jQuery("#Data option").attr("selected", "selected");
        } catch (err) {
            cosole.log("fehler in btnDeleteSelected: " + err.toString());
        }
    });


    jQuery("#btnRefreshStatistic").click(function () {

        // 1. Alle Daten aus der ListBox abrufen und in einem StatData- Modell verpacken
        var model = new StatData();
        jQuery("#Data").children().each(function (ix, el) {

            var val = jQuery(el).attr("value")
            model.Data.push(parseFloat(val));

        });

        // 2. Ajax- Aufruf der Controller- Methode public JsonResult GetMinMeanMax(pccKursMVC.Models.StatData model)
        var jsonString = JSON.stringify(model);
        //jQuery.getJSON("/Stat/GetMinMeanMax", '{ "Data": "99" }').done(function (Data) {
        //    console.log(Data.toString());
        //});


        jQuery.ajax({
            type: "GET",
            url: "/Stat2/GetMinMeanMax",
            data: "Data=" + jsonString
        }).done(function (Data,  status, req) {
            console.log(Data.toString());            

            var tbxMin = jQuery("#tbxMin");
            tbxMin.val(Data.Min.toString());
            jQuery("#tbxMean").attr("value", Data.Mean.toString());
            jQuery("#tbxMax").val(Data.Max.toString());

        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.log(Data.toString());
        });

        // 3. Setzen der Controls mit den neuen Werten aus dem StatDataMinMeanMax- Modell
    });
});

