/// <reference name="MicrosoftAjax.js" />
/*
(c) MKIT Martin Korneffel IT-Beratung/Softwareentwicklung
Stuttgart, den 3.4.2012

Funktionen der Clientkomponente von CanvasControl

Eingebettete Resource:
=> Eigenschaften/Buildvorgang := EmbeddedResource
=> AssemblyInfo/[assembly: System.Web.UI.WebResource("mkoIt.HtmlCanvas.js", "text/javascript")]
*/

Type.registerNamespace("mkoIt.Asp.Html.Graphic");

//---------------------------------------------------------------------------------
// Klasse D2 für 2D Grafiken anlegen anlegen
Sys.Debug.trace("Deklaration der D2- Clientklasse");

// Konstruktor
mkoIt.Asp.Html.Graphic.D2 = function (element) {
    mkoIt.Asp.Html.Graphic.D2.initializeBase(this, [element]);

    this._ctx = element.getContext('2d');
    this._ctx.fillStyle = 'rgb(255,255,0)';
    this._ctx.strokeStyle = "green";

}

// Prototyp

mkoIt.Asp.Html.Graphic.D2.prototype = {
    initialize: function () {
        mkoIt.Asp.Html.Graphic.D2.callBaseMethod(this, 'initialize');

        //this.get_element().className = ""
    },

    // Contextzustand sichern/wiederherstellen

    saveContext: function () {
        this._ctx.save();
    },

    restoreContext: function () {
        this._ctx.restore();
    },

    // Transformationen im Context setzen

    translate: function (tx, ty) {
        this._ctx.translate(tx, ty);
    },

    scale: function (sx, sy) {
        this._ctx.scale(sx, sy);
    },

    rotate: function (angeleInRad) {
        this._ctx.rotate(angeleInRad);
    },

    // Allgemeine Eigenschaften des Grafikkontextes

    setSolidColorFill: function (color) {
        this._ctx.fillStyle = color;
    },

    setSolidStrokeStyle: function (color, linewidth) {
        this._ctx.lineWidth = linewidth;
        this._ctx.strokeStyle = color;
    },

    // Pfadfunktionen

    beginPath: function () {
        //this._ctx.save();
        this._ctx.beginPath();
    },

    closePath: function () {
        this._ctx.closePath();
        //this._ctx.restore();
    },

    fillPath: function () {
        this._ctx.fill();
    },

    strokePath: function () {
        this._ctx.stroke();
    },

    moveTo: function (x, y) {
        this._ctx.moveTo(x, y);
    },

    lineTo: function (x, y) {
        this._ctx.lineTo(x, y);
    },

    arcTo: function (mx, my, r, startAngle, stopAngle) {
        this._ctx.arc(mx, my, r, startAngle, stopAngle);
    },

    // Rechteck zeichnen

    drawRect: function (left, top, width, hight) {
        this._ctx.fillRect(left + 0.5, top + 0.5, width, hight);
        this._ctx.strokeRect(left + 0.5, top + 0.5, width, hight);
    },

    dispose: function () {
        $clearHandlers(this.get_element());

        mkoIt.Asp.Html.Graphic.D2.callBaseMethod(this, 'dispose');
    }
}

// Klasse registrieren
// Typ registrieren
mkoIt.Asp.Html.Graphic.D2.registerClass("mkoIt.Asp.Html.Graphic.D2", Sys.UI.Control);

// Klassenfabrik zum Erzeugen von D2- Objekten
// CanvasControlClientId: string, der die ClientID des Canvas- Elementes darstellt, für
//                        das ein D2- Objekt erzeugt werden soll
mkoIt.Asp.Html.Graphic.D2.Create = function (CanvasControlClientId) {
    $create(mkoIt.Asp.Html.Graphic.D2, null, null, null, $get(CanvasControlClientId));
}



// Rückmelden, falls Script bereit geladen wurde
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
