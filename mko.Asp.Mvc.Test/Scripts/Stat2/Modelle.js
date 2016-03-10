// Stellvertreter für die Servermodelle als Klassen anlegen
// Klassen, die Struktur der Servermodelle auf dem Client bereitstellen

// StatData- speichert alle erfassten Werte
var StatData = function () {
    this.Data = new Array();
}


var StatDataMinMeanMax = function () {
    this.Min = 0.0;
    this.Mean = 0.0;
    this.Max = 0.0;
}


// Definieren eines ViewModels auf der Clientseite
// 1. Klassendefinition
var StatDataVM = function () {
    this._Data = {};
    this._NextId = 0;
}

StatDataVM.prototype = {
    Clear: function () {
        this._Data = {};
    },

    Add: function (id, newValue) {
        this._Data[id] = newValue;
    },

    Delete: function (id) {
        delete this._Data[id];
    },

    // Einlesen der Daten aus dem Servermodel StatData
    ReadFromStatData: function (StatDataInstance) {

        this._Data = {};
        for (i = 0; i < StatDataInstance.Get().length; i++) {
            this._Data[_NextId++] = StatDataInstance.Get()[i];
        }
    },

    Get: function () {
        return this._Data;
    },

    GetNextId: function () {
        return this._NextId++;
    }
};

// 2. Globale Instanz
var ClientViewModelInstance = new StatDataVM();

// Klassen, die Struktur der Servermodelle auf dem Client bereitstellen


//StatData.prototype = {

//    Clear: function () {
//        this.Data = new Array();
//    },

//    // Einlesen der Daten aus dem ViewModel
//    ReadFromVM: function (VM) {

//        this.Data = new Array();
//        for (var id in VM.Get()) {
//            this.Data.push(VM.Get()[id]);
//        }
//    },
//    Get: function () {
//        return this.Data;
//    }
//};

// StatAnalysis- stellt das Ergebnis einer einfachen statistischen Auswertung dar
var StatAnalysis = function () {
    this.Min = 0.0;
    this.Mean = 0.0;
    this.Max = 0.0;
}
