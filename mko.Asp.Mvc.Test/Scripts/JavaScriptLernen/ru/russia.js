
// Initialisierung des Dokumentes Russland.html

// $ als jQuery- Symbol abschalten-> keine Kollision mit anderen Lib's, die Dollar einsetzen mehr möglich
jQuery.noConflict();

jQuery(document).ready(function () {
    jQuery("#map_russ_interactive")
        .children("area")
        .click(function () {
            console.log("click auf area")

            jQuery("div.linklist").toggle(false);

            // Id vom Divblock bestimmen, der angezeigt oder versteckt werden soll
            var idDiv = jQuery(this).attr("id");
            if (!idDiv) {
                console.log("area- Element hat keine id");
                return;
            }

            idDiv += "_info";
            console.log("toggle für" + idDiv)

            jQuery("#" + idDiv).toggle();
        });


});
