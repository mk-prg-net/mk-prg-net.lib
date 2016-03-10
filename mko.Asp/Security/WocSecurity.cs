//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: Woc: Web Document
//  Name..........: WocSecurity
//  Aufgabe/Fkt...: Zugriffsschutz für Woc's
//                  Es werden drei Rollen definiert:
//                  1. (Verzeichnisname)_Admin: Hat uneingeschränkten 
//                      Zugriff auf alle Woc's im Verzeichnis
//                  2. (Verzeichnisname)_Writer: Kann lesend und schreibend auf
//                      Woc's im Verzeichnis zugreifen
//                  3. (Verzeichnisname)_Reader: Kann nur lesend auf Woc's im Verzeichnis
//                      zugreifen
//                  Die Rollen müssen in der ASPNetDB für jedes Verzeichnis angelegt
//                  werden. Nur die 3. Rolle /(_Reader) ist optional. Falls sie nicht
//                  angelegt wird, können alle Benutzer lesend auf die Woc's zugreifen.
//                  Wenn sie angelegt ist, dann können nur Benutzer der Rolle ..._Reader
//                  lesend auf die Woc's im Verzeichnis zugreifen.
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
//  Datum.........: 29.12.2014
//  Änderungen....: Alle Prüfroutinen in Try- Catch Blöcke eingeschlossen
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Web.Security;

namespace mkoIt.Asp
{
    public class WocSecurity
    {
        // Name der Rolle der Site- Administratoren. Diese haben auf alle Resourcen der Site
        // Zugriff
        const string GeneralAdmins = "Administratoren";
        public static bool IsAdmin(string wocName)
        {
            try
            {
                if (string.IsNullOrEmpty(wocName))
                    return Roles.IsUserInRole(GeneralAdmins);

                // Der Verzeichnisname ist der erste Partikel in einem WocName
                string[] levels = wocName.Split('.');
                Debug.Assert(levels.Length > 0);

                // Bilden des Rollennamens in Abhängigkeit vom Verzeichnisnamen
                string AdminRoleName = levels[0] + "_Admin";

                Debug.Assert(Roles.RoleExists(AdminRoleName));

                return Roles.IsUserInRole(AdminRoleName) || Roles.IsUserInRole(GeneralAdmins);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(mko.TraceHlp.FormatErrMsg("mkoIt.Asp.WocSecurity", "IsAdmin", mko.ExceptionHelper.FlattenExceptionMessages(ex)));
                return false;
            }
        }

        public static bool IsAdmin(string wocName, System.Security.Principal.IPrincipal user)
        {
            try
            {
                if (string.IsNullOrEmpty(wocName))
                    return Roles.IsUserInRole(GeneralAdmins);

                // Der Verzeichnisname ist der erste Partikel in einem WocName
                string[] levels = wocName.Split('.');
                Debug.Assert(levels.Length > 0);

                // Bilden des Rollennamens in Abhängigkeit vom Verzeichnisnamen
                string AdminRoleName = levels[0] + "_Admin";

                Debug.Assert(Roles.RoleExists(AdminRoleName));

                return user.IsInRole(AdminRoleName) || user.IsInRole(GeneralAdmins);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(mko.TraceHlp.FormatErrMsg("mkoIt.Asp.WocSecurity", "IsAdmin", mko.ExceptionHelper.FlattenExceptionMessages(ex)));
                return false;
            }
        }

        public static bool IsReader(string wocName)
        {
            try
            {
                if (string.IsNullOrEmpty(wocName))
                    return Roles.IsUserInRole(GeneralAdmins);

                // Der Verzeichnisname ist der erste Partikel in einem WocName
                string[] levels = wocName.Split('.');
                Debug.Assert(levels.Length > 0);

                // Bilden des Rollennamens in Abhängigkeit vom Verzeichnisnamen
                string ReaderRoleName = levels[0] + "_Reader";

                // Wenn die Rolle nicht existiert, dann haben alle Benutzer lesenden 
                // Zugriff
                if (!Roles.RoleExists(ReaderRoleName))
                    return true;
                else
                    return Roles.IsUserInRole(ReaderRoleName) || IsWriter(wocName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(mko.TraceHlp.FormatErrMsg("mkoIt.Asp.WocSecurity", "IsReader", mko.ExceptionHelper.FlattenExceptionMessages(ex)));
                return false;

            }
        }

        public static bool IsReader(string wocName, System.Security.Principal.IPrincipal user)
        {
            try { 
            if (string.IsNullOrEmpty(wocName))
                return Roles.IsUserInRole(GeneralAdmins);

            // Der Verzeichnisname ist der erste Partikel in einem WocName
            string[] levels = wocName.Split('.');
            Debug.Assert(levels.Length > 0);

            // Bilden des Rollennamens in Abhängigkeit vom Verzeichnisnamen
            string ReaderRoleName = levels[0] + "_Reader";

            // Wenn die Rolle nicht existiert, dann haben alle Benutzer lesenden 
            // Zugriff
            if (!Roles.RoleExists(ReaderRoleName))
                return true;
            else
                return user.IsInRole(ReaderRoleName) || IsWriter(wocName, user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(mko.TraceHlp.FormatErrMsg("mkoIt.Asp.WocSecurity", "IsReader", mko.ExceptionHelper.FlattenExceptionMessages(ex)));
                return false;

            }
        }

        public static bool IsWriter(string wocName)
        {
            try
            {
                if (string.IsNullOrEmpty(wocName))
                    return Roles.IsUserInRole(GeneralAdmins);

                // Der Verzeichnisname ist der erste Partikel in einem WocName
                string[] levels = wocName.Split('.');
                Debug.Assert(levels.Length > 0);

                // Bilden des Rollennamens in Abhängigkeit vom Verzeichnisnamen
                string WriterRoleName = levels[0] + "_Writer";

                Debug.Assert(Roles.RoleExists(WriterRoleName));

                return Roles.IsUserInRole(WriterRoleName) || IsAdmin(wocName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(mko.TraceHlp.FormatErrMsg("mkoIt.Asp.WocSecurity", "IsWriter", mko.ExceptionHelper.FlattenExceptionMessages(ex)));
                return false;
            }
        }

        public static bool IsWriter(string wocName, System.Security.Principal.IPrincipal user)
        {
            try
            {
                if (string.IsNullOrEmpty(wocName))
                    return Roles.IsUserInRole(GeneralAdmins);

                // Der Verzeichnisname ist der erste Partikel in einem WocName
                string[] levels = wocName.Split('.');
                Debug.Assert(levels.Length > 0);

                // Bilden des Rollennamens in Abhängigkeit vom Verzeichnisnamen
                string WriterRoleName = levels[0] + "_Writer";

                Debug.Assert(Roles.RoleExists(WriterRoleName));

                return user.IsInRole(WriterRoleName) || IsAdmin(wocName, user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(mko.TraceHlp.FormatErrMsg("mkoIt.Asp.WocSecurity", "IsWriter", mko.ExceptionHelper.FlattenExceptionMessages(ex)));
                return false;
            }
        }
    }
}
