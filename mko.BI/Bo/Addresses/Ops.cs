//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 19.1.2015
//
//  Projekt.......: mko.BI
//  Name..........: Ops.cs
//  Aufgabe/Fkt...: Allgemeine Operationen auf Adressen
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Version.......: 3.2.7
//  Datum.........: 2.8.2017
//  Änderungen....: CopyTo und Equ Erweiterungsmethoden  hinzugefügt
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Bo.Addresses
{
    public static class Ops
    {
        /// <summary>
        /// Kopiert Ortsangaben
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public static void Copy(ILocation src, ILocation dest){
            dest.City = src.City;
            dest.Country = src.Country;
            dest.PostalCode = src.PostalCode;
            dest.Street = src.Street;
        }

        public static void CopyTo(this ILocation src, ILocation dest)
        {
            Copy(src, dest);
        }

        public static bool Equ(this ILocation me, ILocation other)
        {
            bool eq = true;

            eq &= me.City == other.City;
            eq &= me.Country == other.Country;
            eq &= me.PostalCode == other.PostalCode;
            eq &= me.Street == other.Street;

            return eq;
        }

        /// <summary>
        /// Kpoiert Firmenadresse
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public static void Copy(IMailingAddressCompany src, IMailingAddressCompany dest)
        {
            Copy((ILocation)src, dest);
            dest.CompanyName = src.CompanyName;
        }

        public static void CopyTo(this IMailingAddressCompany src, IMailingAddressCompany dest)
        {
            Copy(src, dest);
        }

        public static void Equ(this IMailingAddressCompany src, IMailingAddressCompany dest)
        {

        }



        /// <summary>
        /// Kopiert Postanschrift einer Person
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public static void Copy(IMailingAddressPerson src, IMailingAddressPerson dest)
        {
            Copy((ILocation)src, dest);
            dest.FirstName = src.FirstName;
            dest.Name = src.Name;
        }

        public static void CopyTo(this IMailingAddressPerson src, IMailingAddressPerson dest)
        {
            Copy(src, dest);
        }




    }
}
