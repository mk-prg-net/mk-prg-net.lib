#DocuTerm Composer

## DocuTerms

**DocuTerms** dienen zur streng formalisierten  Dokumentation von Fehler-, Warn- und Hinweismeldungen in Ausnahmen und Rückgabewerten von Funktionen.

Ein Programmzustand wie ein mißglückter Methodenaufruf kann durchh einen *DocuTerm* direkt ausgedrückt werden:

    #m Methodenname 
        #_ 
            #p Parametername_1 Parameterwert_1
            :
            #p Parametername_N Parameterwert_N

            #ret
                #_
                    #e fails
                #.
        #.

Zum Beispiel kann eine Fehlgeschlagene Validierung eines Funktionsparameters (arg1 sollte größer 100 sein) wie folgt ausgedrückt werden:

``````C#
    dct.m(TechTerms.Validation.mValidate,
        // Anzeige, das Validierung gescheitert ist
        dct.ret(dct.eFails(
            // Vorbedingung als boolsche Funktion, welche nicht erfüllt wurde (z.B. muss arg1 >= 100 sein)
            dct.i(TechTerms.Validation.iPreCondition,        
                dct.m(TechTerms.RelationalOperators.mGtEq,                          
                    dct.p(TechTerms.MetaData.Arg, "arg1"),
                    dct.p(TechTerms.MetaData.Val, 100),
                    dct.ret(false)))))));

``````
Um Programmzustände zu beschreiben können die aktiven Laufzeitobjekte wie *Objekte*, *Methoden* und *Ereignisse*  direkt durch *DocuTerms* beschrieben werden.

In **C#** werden die **DokuEntities** mittels der **ATMO.mko.Logging.PNDocuTerms.DocuEntites.Composer** Bibliothek im Programm erzeugt.

## Struktur von *DocuTerms*

Die *DocuTerms* werden durch Objkete implementiert, die die Schnittstelle **ATMO.mko.Logging.PNDocuTerms.DocuEntities.IDocuEntity** implementieren:

``````C#

    interface IDocuEntity {

        DocuEntityTypes EntityType {get;}
        IEnumerable<IDocuEntity> Childs {get;}

    }

    enum DocuEntityTypes { Name, Instance, Property, PropertySet, Version, Event, Method, List, String, Text, Date, ReturnValue, KillIfNot, none, ListToEmbed};

``````
*DocuTerms* bilden eine Baumstruktur.


## Analyse von *DokuTerms*

Meldet ein Unterprogramm mittels *DocuTerms* den Erfolg oder Misserfolg seines Aufrufes zurück, dann stellt sich die Aufgabe der Analyse der komplexen hierarchichen *DocuTerm* Bäume. Um die Aufgabe zu erleichtern, werden folgende Werkzeuge bereitgestellt.

### DocuEntityLinqDeco

Stellt einen Decorator für IDocuEntity- Objekte bereit. Das IDocuEntity- Objekt wird um Eigenschaften erweitert, welche potentielle Member wie *Instanzen*, *Eigenschaften*, *Methoden* auflisten. 

### DocuEntityHlp

Hier werden Erweiterungmethoden für die Schnittstelle **IDocuEntity** angeboten, mit denen in denen im durch das DocuEntity aufgespannten Baum nach bestimmten Strukturen gesucht werden kann.

``````C#

    getUser = boschCom.GetBoschUserFromDC("DonaldDuck", pnL);
    
    var warn = getUser.MessageEntity.FindNamedEntity(DocuEntityTypes.Event, Composer.TechTerms.eWarn); 
``````


#### IsSubTreeOf
Prüft, ob eine DocuTerm als Teil in einem anderen enthalten ist. 

``````C#
    // Hier wird nach einem DocuTerm erzeugt, der eine gescheiterte Suche 
    // nach Datensätzen mit der Projektnummer 2998 ausdrückt.
    var tree = dct.i(TechTerms.Dfc.Project,
                    dct.p(TechTerms.Dfc.Project, "2998"),
                    dct.p("Stations", dct.ReturnSearchWarnEmptyResult(
                                            dct.m(TechTerms.RelationalOperators.mEq,
                                                dct.p("col", "Project"),
                                                dct.p("val", "2998")))));



    // Hier wird nach einem Teilbaum gesucht, welcher erzeugt wird, wenn eine Abfrage 
    // keine Datensätze zurückliefert (leere Menge). Als Grund wird der gescheiterte 
    // Vergleich mit der Projektnummer 2998 genannt. 
    var subTree = dct.ReturnSearchWarnEmptyResult(
                    dct.m(TechTerms.RelationalOperators.mEq,
                        dct.p("col", "Project"),
                        dct.p("val", "2998")));

    bool res = subTree.IsSubTreeOf(tree);
    Assert.IsTrue(res);

    // Hier wird nach einem Teilbaum gesucht, welcher erzeugt wird, wenn eine Abfrage 
    // keine Datensätze zurückliefert (leere Menge). Als Grund wird der gescheiterte 
    // Vergleich mit der Projektnummer 2999 genannt. 
    var subTree2 = dct.ReturnSearchWarnEmptyResult(
                    dct.m(TechTerms.RelationalOperators.mEq,
                        dct.p("col", "Project"),
                        dct.p("val", "2999")));

    // Weil ein Vergleich mit einer Projektnummer 2999 im DocuTerm nicht vorkommt,
    // scheitert die Suche nach dem Subtree
    res = subTree2.IsSubTreeOf(tree);
    Assert.IsFalse(res);

    // Hier wird nach einem Teilbaum gesucht, welcher erzeugt wird, wenn eine Abfrage 
    // keine Datensätze zurückliefert (leere Menge). Weitere Details (Grund) werden nicht 
    // betrachtet
    var subTree3 = dct.ReturnSearchWarnEmptyResult();

    // subTree3 ist tatsächlich ein Teilbaum
    res = subTree3.IsSubTreeOf(tree);
    Assert.IsTrue(res);
``````







