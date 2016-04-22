//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 21.4.2016
//
//  Projekt.......: mko.BI
//  Name..........: CompanyAddresses.cs
//  Aufgabe/Fkt...: Allegemeine Struktur  eines Repository für Firmenanschriften
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
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Repositories.Addresses
{
    public abstract partial class CompanyAddresses : 
        Interfaces.ICreateUpdate<string>,
        Interfaces.IGet<Bo.Addresses.IMailingAddressCompany, string>,
        Interfaces.IRemove<string>,
        Interfaces.ISubmitChanges
    {

        public abstract void CreateBoAndAddToCollection(string id);

        public abstract Bo.Addresses.IMailingAddressCompany Get(string id);

        public abstract bool Any(string id);

        public abstract void RemoveFromCollection(string id);

        public abstract void RemoveAll();

        public abstract void SubmitChanges();

        
    }
}
