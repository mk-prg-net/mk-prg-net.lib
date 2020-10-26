using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{


    /// <summary>
    /// mko, 27.2.2019
    /// Komplexe Strukturen zur Dokumentation von Programmzuständen wie Fehlern oder Statuswechseln in einer standardisierten Form
    /// erzeugen. Dadurch wird eine automatisierte Auswertung dieser Strukturen ermöglicht.
    /// </summary>
    public static class ComposerSubTrees
    {
        //-------------------------------------------------------------------------------------------------------------------------
        // Common

        /// <summary>
        /// mko, 29.8.2019
        /// Allgemeine Anzeige, das eine Methode zwar aufgerufen, in dieser aber noch keine wesentlichen Berechnungen stattgefunden 
        /// haben.
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="MethodName"></param>
        /// <param name="MethodParameters"></param>
        /// <returns></returns>
        public static IDocuEntity ReturnNotCompleted(this IComposer dct, string MethodName, params IDocuEntity[] MethodParameters)
            => dct.m(MethodName,
                dct.KillIfNot(MethodParameters.Any(), () => dct.EmbedMembers(MethodParameters)),
                dct.eNotCompleted());


        //-------------------------------------------------------------------------------------------------------------------------
        // Access

        /// <summary>
        /// mko, 4.3.2019
        /// Dokumentiert den erfolgreichen/fehlgeschlagenen Zugriff auf ein Objekt, dessen Existenz vorausgesetzt wurde.
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="DataSource">Datenquelle</param>
        /// <param name="CompositeKeyParts">Schlüsselattribute, die beim Abruf verwendet wurden</param>
        /// <returns></returns>
        public static IDocuEntity ReturnFetch(this IComposer dct, bool Succeeded, IDocuEntity DataSource, params IDocuEntity[] CompositeKeyParts)
            => dct.m(TechTerms.Access.fetch,
                        dct.p(TechTerms.Access.DataSource, DataSource),
                        dct.EmbedMembers(CompositeKeyParts),
                        dct.ret(Succeeded ? dct.eSucceeded() : dct.eFails()));

        /// <summary>
        /// mko, 4.3.2019
        /// Dokumentiert den erfolgreichen/fehlgeschlagenen Zugriff auf ein Objekt, dessen Existenz vorausgesetzt wurde.
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="Succeeded">Zugriff erfolgreich ja/nein</param>
        /// <param name="DataSource">Datenquelle, aus der das Objekt geholt werden sollte</param>
        /// <param name="Details">Dateils zum erfolgreichen/fehlgeschlagenen Zugriff</param>
        /// <param name="CompositeKeyParts">Details zum Schlüssel</param>
        /// <returns></returns>
        public static IDocuEntity ReturnFetchWithDetails(this IComposer dct, bool Succeeded, IDocuEntity DataSource, IDocuEntity Details, params IDocuEntity[] CompositeKeyParts)
            => dct.m(TechTerms.Access.fetch,
                        dct.p(TechTerms.Access.DataSource, DataSource),
                        dct.EmbedMembers(CompositeKeyParts),
                        dct.ret(Succeeded ? dct.eSucceeded(Details) : dct.eFails(Details)));


        //-------------------------------------------------------------------------------------------------------------------------
        // Search

        /// <summary>
        /// mko, 27.2.2019
        /// Signalisiert für eine Search- Methode, das die Suche zwar fehlerfrei ausgeführt wurde, jedoch zu keinem Ergebnis führte
        /// </summary>
        /// <param name="dct"></param>
        /// <returns></returns>
        public static IDocuEntity ReturnSearchWarnEmptyResult(this IComposer dct, params IDocuEntity[] FilterTerms)
            => dct.m(TechTerms.Search.mSearch,

                    // Hier können Filterkriterien beschrieben werden (Parameterliste wird bei Bedarf erweitert)    
                    dct.KillIfNot(FilterTerms.Any(), () => dct.EmbedMembers(FilterTerms)),

                    // Dokumentation des leeren Menge als Ergebnis (Warnung)
                    dct.ret(dct.eWarn(TechTerms.Sets.EmptySet)));


        /// <summary>
        /// Signalisiert für eine Search- Methode, dass die Suche kein ergebnis geliefert hat und dies als Fehler zu werten ist.
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IDocuEntity ReturnSearchFailsEmptyResult(this IComposer dct, params IDocuEntity[] FilterTerms)
            => dct.m(TechTerms.Search.mSearch,

                    // Hier können Filterkriterien beschrieben werden (Parameterliste wird bei Bedarf erweitert)
                    dct.KillIfNot(FilterTerms.Any(), () => dct.EmbedMembers(FilterTerms)),

                    // Dokumentation des leeren Menge als Ergebnis
                    dct.ret(dct.eFails(TechTerms.Sets.EmptySet)));


        //--------------------------------------------------------------------------------------------------------------------------
        // Preconditions

        /// <summary>
        /// mko, 27.2.2019
        /// Beschreibt die fehlgeschlagene Validierung einer Vorbedingung: Argument liegt außerhalb eines gültigen Bereiches
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="ArgumentName">Argument, das der Vorbedingung nicht genügte</param>
        /// <param name="Range">Optional: Definition des Bereiches</param>
        /// <returns></returns>
        public static IDocuEntity ReturnValidatePreconditionFailedArgumentOutOfRange(this IComposer dct, IDocuEntity Argument, IDocuEntity Range = null)
            => dct.m(TechTerms.Validation.mValidate,

                    // Anzeige, das Validierung gescheitert ist
                    dct.ret(dct.eFails(

                        // Vorbedingung, welche vom Parameter nicht erfüllt wurde 
                        dct.i(TechTerms.Validation.iPreCondition,
                            dct.m(TechTerms.Validation.mCheckIfValueInRange,
                                    //Parameter, der out of range ist
                                    Argument,

                                    // Bereich, auf den geprüft wurde
                                    dct.EmbedMembers(Range),

                                    dct.ret(false))))));

        /// <summary>
        /// mko, 27.2.2019
        /// Beschreibt einen Bereich aus Long- Werten
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IDocuEntity Range(this IComposer dct, long begin, long end)
            => dct.i(TechTerms.Sets.Range.semCtx, dct.p(TechTerms.Sets.Range.Begin, begin), dct.p(TechTerms.Sets.Range.End, end));

        /// <summary>
        /// mko, 27.2.2019
        /// Beschreibt einen Bereich aus Long- Werten
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IDocuEntity Range(this IComposer dct, int begin, int end)
            => dct.i(TechTerms.Sets.Range.semCtx, dct.p(TechTerms.Sets.Range.Begin, begin), dct.p(TechTerms.Sets.Range.End, end));


        /// <summary>
        /// mko, 27.2.2019
        /// Beschreibt einen Bereich aus Long- Werten
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IDocuEntity Range(this IComposer dct, double begin, double end)
            => dct.i(TechTerms.Sets.Range.semCtx, dct.p(TechTerms.Sets.Range.Begin, begin), dct.p(TechTerms.Sets.Range.End, end));


        /// <summary>
        /// mko, 27.2.2019
        /// Beschreibt eine Fehlgeschlagene Validierung einer allgemeine Vorbedingung. Die Vorbedingung sollte durch eine boolsche Funktion,
        /// die false zurückgibt, beschrieben werden wie:
        /// dct.m(TechTerms.RelationalOperators.mGtEq,
        ///    dct.p(TechTerms.MetaData.Arg, "arg1"),
        ///    dct.p(TechTerms.MetaData.Val, 100),
        ///    dct.ret(false))
        ///    
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="PreconditionAsPredicate"></param>
        /// <returns></returns>
        public static IDocuEntity ReturnValidatePreconditionFailed(this IComposer dct, IDocuEntity PreconditionAsPredicate)
            => dct.m(TechTerms.Validation.mValidate,

                    // Anzeige, das Validierung gescheitert ist
                    dct.ret(dct.eFails(PreconditionAsPredicate)));


        public static IDocuEntity ReturnValidatePreconditionNotNullFailed(this IComposer dct, IDocuEntity PropertyArgToBeNotNull)
            => dct.ReturnValidatePreconditionFailed(
                            dct.i(TechTerms.Validation.iPreCondition,                                
                                    dct.m(TechTerms.RelationalOperators.mNotEq,
                                        dct.EmbedMembers(PropertyArgToBeNotNull),
                                        dct.p(TechTerms.MetaData.Val, "null"),
                                        dct.ret(false))));

        //-----------------------------------------------------------------------------------------------
        // Authentifizierung
        // Achtung: Parameter wie UserId werden als optionale Parameter übergeben, um so subTree- Vergleiche
        //          der allgemeinen Struktur einer Fehlermeldung (ohne konkrete UserId) mit den detaillierten 
        //          Fehlerbeschreibungen aus den Rückgabewerten durchführen zu können.

        /// <summary>
        /// mko, 11.3.2019
        /// Beschreibt einen allgemeinen Fehler bei der Authentifizierung
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="UserId"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static IDocuEntity ReturnAuthenticationGeneralError(this IComposer pnL, string UserId = null, string LoginInStep = null, IDocuEntity ErrorDescription = null)
            => pnL.i(TechTerms.FinStateDescr,
                pnL.p(TechTerms.MetaData.pSemCtx, TechTerms.Authentication.semCtx),
                pnL.m(TechTerms.Authentication.Login,
                    pnL.KillIf(UserId == null, () => pnL.p(TechTerms.Authentication.UserId, UserId)),
                    pnL.KillIf(LoginInStep == null, () => pnL.p(TechTerms.Authentication.pLoginStep, LoginInStep)),
                    pnL.ret(pnL.eFails(pnL.KillIf(ErrorDescription == null, () => ErrorDescription)))));

        /// <summary>
        /// mko, 11.3.2019
        /// Der User wird weder als Kunde noch als Atmo- Mitarbeiter erkannt.
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static IDocuEntity ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(this IComposer pnL, string UserId = null)
            => pnL.i(TechTerms.FinStateDescr,
                pnL.p(TechTerms.MetaData.pSemCtx, TechTerms.Authentication.semCtx),
                pnL.m(TechTerms.Authentication.Login,
                    pnL.p(TechTerms.Authentication.pLoginStep, TechTerms.Authentication.LoginStepAuthenticateUser),
                    pnL.KillIf(UserId == null, () => pnL.p(TechTerms.Authentication.UserId, UserId)),
                    pnL.ret(pnL.eFails($"{UserId} is not a customer nor an ATMO employee"))));

        /// <summary>
        /// mko, 8.3.2019
        /// Der User ist keiner DFC- Rolle zugeordnet-> kein Zugriff möglich.
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static IDocuEntity ReturnAuthenticationNoDFCRolesAssigned(this IComposer pnL, string UserId = null)
            => pnL.i(TechTerms.FinStateDescr,
                pnL.p(TechTerms.MetaData.pSemCtx, TechTerms.Authentication.semCtx),
                pnL.m(TechTerms.Authentication.Login,
                    pnL.KillIf(UserId == null, () => pnL.p(TechTerms.Authentication.UserId, UserId)),
                    pnL.p(TechTerms.Authentication.pUserClass, TechTerms.Authentication.UserClassCoWorker),
                    pnL.p(TechTerms.Authentication.pLoginStep, TechTerms.Authentication.mGetRolesUserIsMemeber),
                    pnL.ret(pnL.eFails(TechTerms.Sets.EmptySet))));


        /// <summary>
        /// mko, 11.3.2019
        /// Der User wurde als Kunde identifiziert, jedoch ist er keiner Kundengruppe zugeordnet.
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static IDocuEntity ReturnAuthenticationCustomerIsNotMemberOfAnyCustomerGroup(this IComposer pnL, string UserId = null)
            => pnL.i(TechTerms.FinStateDescr,
                    pnL.p(TechTerms.MetaData.pSemCtx, TechTerms.Authentication.semCtx),
                    pnL.m(TechTerms.Authentication.Login,
                        pnL.KillIf(UserId == null, () => pnL.p(TechTerms.Authentication.UserId, UserId)),
                        pnL.p(TechTerms.Authentication.pUserClass, TechTerms.Authentication.UserClassCustomer),
                        pnL.p(TechTerms.Authentication.pLoginStep, TechTerms.Authentication.LoginStepGetCustomerGroups),
                        pnL.ret(pnL.eFails(TechTerms.Sets.EmptySet))));


        /// <summary>
        /// mko, 11.3.2019
        /// Wird vom Userbuilder zurückgegeben, wenn die Liste der Kundengruppen nicht abgerufen werden konnte.
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="UserId"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public static IDocuEntity ReturnAuthenticationWarningCantCreateListOfCustomerGroups(this IComposer pnL, string UserId = null, IDocuEntity Reason = null)
            => pnL.i(TechTerms.FinStateDescr,
                pnL.p(TechTerms.MetaData.pSemCtx, TechTerms.Authentication.semCtx),
                pnL.m(TechTerms.Authentication.Login,
                    pnL.KillIf(UserId == null, () => pnL.p(TechTerms.Authentication.UserId, UserId)),
                    pnL.p(TechTerms.Authentication.pUserClass, TechTerms.Authentication.UserClassCoWorker),
                    pnL.p(TechTerms.Authentication.pLoginStep, TechTerms.Authentication.LoginStepGetListOfCustomerGroups),
                    pnL.ret(pnL.eWarn(pnL.KillIf(Reason == null, () => Reason)))));


        //-----------------------------------------------------------------------------------------------
        // Authorisierung




    }
}

