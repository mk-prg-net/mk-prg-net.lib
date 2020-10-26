using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 15.11.2018
    /// Mit Joachim im April 2018 vereinbarte Typen von Logmeldungen
    /// </summary>
    public enum EnumLogTypeDFC
    {
        /// <summary>
        /// Zustandswechsel in DFC (z.B. Zeichnung auf "in work")
        /// </summary>
        State = 1,

        /// <summary>
        /// Fehlermeldungen vom DFC
        /// </summary>
        Error = 2,
         
        /// <summary>
        /// Allgemeine Informationen
        /// </summary>
        Info = 3,

        /// <summary>
        /// Informationen zum Aufruf/Nutzung von DFC- Funktionen zwecks Planung der 
        /// Weiterentwicklung
        /// </summary>
        Telemetry = 4,

        /// <summary>
        /// Aufzeichnung des Aufrufs unternehmenskritischer Funktionen
        /// </summary>
        Log = 5
    }

    /// <summary>
    /// mko, 6.12.2018
    /// </summary>
    public static class EnumLogTypeDFCHlp
    {
        /// <summary>
        /// mko, 6.12.2018
        /// Bilded die klassische Klassifizierung der Arten von Logmeldungen auf die in DFC vereinbarten ab.
        /// </summary>
        /// <param name="logTypeClassic"></param>
        /// <returns></returns>
        public static EnumLogTypeDFC ToEnumLogTypeDFC(this EnumLogType logTypeClassic)
        {
            switch (logTypeClassic)
            {
                case EnumLogType.Error:
                    return EnumLogTypeDFC.Error;
                case EnumLogType.Message:
                    return EnumLogTypeDFC.Info;
                case EnumLogType.Status:
                    return EnumLogTypeDFC.State;
                default:
                    throw new InvalidCastException($"No EnumLogType value corresponds to {logTypeClassic.ToString()}");
            }
        }
    }
}
