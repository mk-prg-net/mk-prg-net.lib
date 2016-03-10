//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 9.7.2008
//
//  Projekt.......: IRetrunValueContainer
//  Name..........: IReturnValueContainer.cs
//  Aufgabe/Fkt...: Allg. Schnittstelle von Datentypen für Rückgabewerte von
//                  Methoden, die den Rufer über Erfolg oder Misserfogl eines 
//                  Methodenaufrues informieren
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Text;

namespace mko
{
    [Serializable]
    public class ReturnCode<TEnumStatus, TEnumError> : ILogInfo
        where TEnumError : struct
        where TEnumStatus : struct
    {

        bool _Success = true;

        TEnumStatus _Status;
        TEnumError _Error;

        public ReturnCode(TEnumError err)
        {
            _Success = false;
            _Error = err;
        }

        public ReturnCode(TEnumStatus stat)
        {
            _Success = true;
            _Status = stat;
        }

        // Konstruktor, der eine Fehler oder Stausmeldung aus dem Paar 
        // (Success, StatusCode) rekonstruiert. Z.B. für Rückmeldungen von 
        // gespeicherten Prozeduren aus Datenbanken
        // Muß in abgeleiteten Klassen für die speziellen Enum- Typen implementiert werden
        //public ReturnCode(bool Success, int StatusCode)
        //{
        //    _Success = Success;
        //    if (Success)
        //        _Status = (object)StatusCode;
        //    else
        //        _Error = new TEnumError(StatusCode);
        //}

        // Methodenaufruf war erfolgreich
        public bool Success
        {
            get
            {
                return _Success;
            }
        }

        // Nummerischer Code der Statusmeldung.         
        public TEnumStatus Status
        {
            get
            {
                return _Status;
            }
        }

        // Nummerischer Code der Fehlermeldung
        public TEnumError Error
        {
            get
            {
                return _Error;
            }
        }

        // Beschreibung des aktuellen Zustandes
        public virtual string Description
        {
            get
            {
                if (_Success)
                    return StatusToString();
                else
                    return ErrorToString();
            }
        }

        // Zu einem StatusCode wird eine Klartextmeldung zurückgegeben
        public virtual string StatusToString()
        {
            return _Status.ToString();
        }

        // Zu einem FehlerCode wird eine Klartextmeldung zurückgegeben
        public virtual string ErrorToString()
        {
            return _Error.ToString();
        }

        #region ILogInfo Member

        EnumLogType ILogInfo.LogType
        {
            get
            {
                if (Success)
                    return EnumLogType.Message;
                else
                    return EnumLogType.Error;
            }

        }

        string ILogInfo.Message
        {
            get
            {
                return Description;
            }
        }

        string ILogInfo.MessageCodeToString()
        {

            if (Success)
                return Status.ToString();
            else
                return Error.ToString();
        }

        #endregion
    }
}
