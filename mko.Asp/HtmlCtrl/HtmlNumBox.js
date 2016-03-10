/// <reference name="MicrosoftAjax.js" />
/*
    (c) MKIT Martin Korneffel IT-Beratung/Softwareentwicklung
    Stuttgart, den 3.4.2012

    Funktionen der Clientkomponente von NumBox

    Eingebettete Resource:
     => Eigenschaften/Buildvorgang := EmbeddedResource
     => AssemblyInfo/[assembly: System.Web.UI.WebResource("mkoIt.HtmlNumBox.js", "text/javascript")]
*/

//---------------------------------------------------------------------------------
// Namensraum Registrieren

Type.registerNamespace("mkoIt.Asp.Html.Ajax");

//---------------------------------------------------------------------------------
// Klasse NumBox anlegen
Sys.Debug.trace("Deklaration der NumBox- Clientklasse");

// Konstruktor
mkoIt.Asp.Html.Ajax.NumBox = function (element) {
    mkoIt.Asp.Html.Ajax.NumBox.initializeBase(this, [element]);

    this._numPadId = "";
    Sys.Debug.assert($get(numPadId) != null, "Die NumPad- Dom- Struktur für eine NumBox existiert nicht", true);

    this._d0 = "";
    this._d1 = "";
    this._d2 = "";
    this._d3 = "";
    this._d4 = "";
    this._d5 = "";
    this._d6 = "";
    this._d7 = "";
    this._d8 = "";
    this._d9 = "";
    this._hide = "";
    this._back = "";

}

// Prototype
mkoIt.Asp.Html.Ajax.NumBox.prototype = {

    initialize: function () {
        mkoIt.Asp.Html.Ajax.NumBox.callBaseMethod(this, "initialize");
    },

    get_NumPadId: function () {
        return this._numPadId;
    },

    set_NumPadId: function (value) {
        this._numPadId = value;
    },

    // NumPad Tasten definieren
    set_d0: function (value) {
        $addHandler($get(value), "click", this.AddD0);
    },

    set_d1: function (value) {
        $addHandler($get(value), "click", this.AddD1);
    },

    set_d2: function (value) {
        $addHandler($get(value), "click", this.AddD2);
    },


    ShowNumPad: function () {
        $get(this._numPadId).setAttribute("style", "visibility: visible");
    },

    HideNumPad: function () {
        $get(this._numPadId).setAttribute("style", "visibility: collapse");
    },

    AddDigit: function (digit) {
        if (this.get_element() && !this.get_element().disabled) {
            var input = this.get_element().getAttribute("value");
            input += digit;
            this.get_element().setAttribute("value", input);
        }
    },

    AddD0: function (e) {
        this.AddDigit("0");
    },




    BackSpace: function () {
        var input = this.get_element().getAttribute("value");
        if (input.length > 0) {
            input = input.substring(0, input.length - 1);
        }
        this.get_element().setAttribute("value", input);
    },

    Clear: function () {
        this.get_element().setAttribute("value", "");
    }
}

// Typ registrieren
mkoIt.Asp.Html.Ajax.NumBox.registerClass("mkoIt.Asp.Html.Ajax.NumBox", Sys.UI.Control);



//---------------------------------------------------------------------------------
// Klasse NumBoxUtils anlegen
Sys.Debug.trace("Deklaration der NumBoxUtils- Clientklasse");

// Konstruktor
mkoIt.Asp.Html.Ajax.NumBoxUtils = function () {
    mkoIt.Asp.Html.Ajax.NumBoxUtils.initializeBase(this);

    this._textBoxId = "";
    this._numPadId = "";

}

// Prototype
mkoIt.Asp.Html.Ajax.NumBoxUtils.prototype = {

    initialize: function () {
        mkoIt.Asp.Html.Ajax.NumBoxUtils.callBaseMethod(this, "initialize");
    },

    get_TextBoxId: function () {
        return this._textBoxId;
    },

    set_TextBoxId: function (value) {
        Sys.Debug.assert($get(value) !== null, "Die TextBox für eine NumBox existiert nicht", true);
        this._textBoxId = value;
    },

    get_NumPadId: function () {
        return this._numPadId;
    },

    set_NumPadId: function (value) {
        Sys.Debug.assert($get(value) !== null, "Die NumPad- Struktur für eine NumBox existiert nicht", true);
        this._numPadId = value;
    },  


    ShowNumPad: function () {
        $get(this._numPadId).setAttribute("style", "visibility: visible");
    },

    HideNumPad: function () {
        $get(this._numPadId).setAttribute("style", "visibility: collapse");
    },

    AddDigit: function (digit) {
        if ($get(this._textBoxId) && !$get(this._textBoxId).disabled) {
            var input = $get(this._textBoxId).getAttribute("value");
            input += digit;
            $get(this._textBoxId).setAttribute("value", input);
        }
    },

    BackSpace: function () {
        var input = $get(this._textBoxId).getAttribute("value");
        if (input.length > 0) {
            input = input.substring(0, input.length - 1);
        }
        $get(this._textBoxId).setAttribute("value", input);
    },

    Clear: function () {
        $get(this._textBoxId).setAttribute("value", "");
    }
}

// Typ registrieren
mkoIt.Asp.Html.Ajax.NumBoxUtils.registerClass("mkoIt.Asp.Html.Ajax.NumBoxUtils", Sys.Component);


// Rückmelden, falls Script bereit geladen wurde
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();









