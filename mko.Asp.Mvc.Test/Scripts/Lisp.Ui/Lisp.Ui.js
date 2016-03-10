jQuery.noConflict(false);

// uid erzeugen, siehe http://stackoverflow.com/questions/105034/how-to-create-a-guid-uuid-in-javascript
function CreateUUID() {
    var uuid =
    'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
    return uuid;
}

var mko = {
    // Hilfsfunktionen

    // Fehlermeldung erzeugen
    CreateError: function (errMsg) {
        console.warn(errMsg);
        throw errMsg;
    },
    Lisp: {
        // Editor für Lisp- Ausdrücke
        Ui: {
            Editor: {
                Select: function () {
                    var id = jQuery(this).parent().attr("id");
                    if (!id) {
                        mko.CreateError("Select failed: id Attribute missing");
                    }
                    jQuery("#SelectedList").val(id);
                    console.log("New selected list: " + id);
                },
                // Einfügen eines Listenelementes in einer Liste
                Insert: function (IdList, ListConstructor) {
                    // Element mit der ID suchen
                    var list = jQuery("#" + IdList);
                    if (!list.length) {
                        mko.CreateError("Insert failed: Element " + IdList + " not found");
                    }

                    list.append(ListConstructor());
                    list.children("div").last().children("form :button").click(mko.JLisp.Editor.Select);
                },
                Wrap: function (IdList, ListConstructor) {
                    // Element mit der ID suchen
                    var list = jQuery("#" + IdList);
                    if (list.length) {
                        mko.CreateError("Wrap failed: Element " + IdList + " not found");
                    }

                    list.wrap(ListConstructor());
                    list.children("div").last().children("form :button").click(mko.JLisp.Editor.Select);;
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
                    return "<div id=\"" + CreateUUID() + "\" class=\"lisp_list\">" +
                        "<input type=\"button\" value=\"Val\" />" +
                        "<input type=\"text\" value=\"0\" style=\"text-align: right\" />" +
                        "</div>";
                },

                CreateAdd: function () {
                    return "<div id=\"" + CreateUUID() + "\" class=\"lisp_list\">" +
                        "<input type=\"button\" value=\"Add\" />" +
                        "</div>";
                }
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
        if (jQuery("#rbtInsertFunc").attr("checked")) {
            mko.Lisp.Ui.Editor.Insert(idSelected, ListConstructor);
        } else if (jQuery("#rbtAddBeforeFunc").attr("checked")) {
            mko.Lisp.Ui.Editor.AddBefore(idSelected, ListConstructor);
        } else if (jQuery("#rbtAddAfterFunc").attr("checked")) {
            mko.Lisp.Ui.Editor.AddAfter(idSelected, ListConstructor);
        } else if (jQuery("#rptWrapFunc").attr("checked")) {
            mko.Lisp.Ui.Editor.Wrap(idSelected, ListConstructor);
        }
    });
}


jQuery(document).ready(function () {
    // Eventhandler für den Editor registrieren
    jQuery("#btnLispEditRoot").click(mko.Lisp.Ui.Editor.Select);
    jQuery("#btnDelete").click(mko.Lisp.Ui.Editor.Delete);
    AddClickHandler("btnVal", mko.Lisp.Ui.Lists.CreateDblVal);
    AddClickHandler("btnAdd", mko.Lisp.Ui.Lists.CreateAdd);
});

