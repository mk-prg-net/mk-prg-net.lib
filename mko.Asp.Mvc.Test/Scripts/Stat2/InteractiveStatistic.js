// Martin Korneffel, 27.2.2014
// Clientseitige Logik der View InteractiveStatistic

jQuery.noConflict();



// Logik der Editorkonsole
var mko = {
    CreateError: function (errMsg) {
        console.warn(errMsg);
        throw errMsg;
    },
    JLisp: {
        Editor: {
            Select: function () {
                var id = jQuery(this).parent().attr("id");
                if (!id) {
                    mko.CreateError("Select failed: id Attribute missing");
                }
                jQuery("#SelectedList").val(id);
                console.log("New selected list: " + id);
            },
            // Listenelement vor einer Liste anfügen
            AddAfter: function (IdList, ListConstructor) {
                // Element mit der ID suchen
                var list = jQuery("#" + IdList);
                if (!list.length) {
                    mko.CreateError("AddAfter failed: Element " + IdList + " not found");
                }

                list.after(ListConstructor());
                list.children("div").last().children("form :button").click(mko.JLisp.Editor.Select);;

            },
            // Listenelement nach einer Liste anfügen
            AddBefore: function (IdList, ListConstructor) {
                // Element mit der ID suchen
                var list = jQuery("#" + IdList);
                if (!list.length) {
                    mko.CreateError("AddAfter failed: Element " + IdList + " not found");
                }

                list.before(ListConstructor());
                list.children("div").last().children("form :button").click(mko.JLisp.Editor.Select);;
            },
            // Liste mit der ID wird komplett gelöscht
            Delete: function () {
                // Element mit der ID suchen
                var ElementIdToRemove = jQuery("#SelectedList").val();
                if (!ElementIdToRemove) {
                    mko.CreateError("AddAfter failed: Element " + SelectedList + " not found");
                }

                var ElementsToRemove = jQuery("#" + ElementIdToRemove);
                if (!ElementsToRemove.length) {
                    console.log("no elements to delete");
                }

                ElementsToRemove.remove();
            }
        },

        Lists: {
            CreateDblVal: function () {
                return "<div id=\"" + ClientViewModelInstance.GetNextId() + "\" class=\"lisp_list\">" +
                    "<input type=\"button\" value=\"select\" />" +
                    "<input type=\"text\" value=\"0\" style=\"text-align: right\" />" +
                    "</div>";
            }
        }
    }
};


function AddClickHandler(id, ListConstructor) {
    jQuery("#" + id).click(function () {

        // id des aktuell selektierten Elementes bestimmen
        var idSelected = jQuery("#SelectedList").val();

        if (!idSelected) {
            mko.CreateError("No list selected");
        }

        // Editmode bestimmen
        if (jQuery("#rbtAddBeforeFunc").attr("checked")) {
            mko.JLisp.Editor.AddBefore(idSelected, ListConstructor);
        } else if (jQuery("#rbtAddAfterFunc").attr("checked")) {
            mko.JLisp.Editor.AddAfter(idSelected, ListConstructor);
        }
    });
}


jQuery(document).ready(function () {
    // Eventhandler für den Editor registrieren
    jQuery("#btnLispEditRoot").click(mko.JLisp.Editor.Select);
    jQuery("#btnDelete").click(mko.JLisp.Editor.Delete);
    AddClickHandler("btnVal", mko.JLisp.Lists.CreateDblVal);

});