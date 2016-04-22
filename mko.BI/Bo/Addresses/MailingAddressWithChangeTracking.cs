//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 17.1.2015
//
//  Projekt.......: mko.BI
//  Name..........: MailingAddressCompanyWithChangeTracking
//  Aufgabe/Fkt...: Implementiert eine Firmenanschrift mit Funktionalität für Änderungsverfolgung
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
//  Datum.........: 20.3.2016
//  Änderungen....: Schnittstellenabhängigkeit ICoreData eliminiert
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
    public class MailingAddressCompanyWithChangeTracking : mko.BI.ChangeTracking.BoWithChangeTracking<mko.BI.Bo.Addresses.MailingAddressCompany>, mko.BI.Bo.Addresses.IMailingAddressCompany
    {
        mko.BI.Bo.Addresses.MailingAddressCompany _Adr = new Bo.Addresses.MailingAddressCompany();
        public override Bo.Addresses.MailingAddressCompany Bo
        {
            get
            {
                return _Adr;
            }

        }

        public string CompanyName
        {
            get
            {
                return Bo.CompanyName;
            }
            set
            {
                SetProperty(value, (v, e) => e.CompanyName = v);
            }
        }

        public string Street
        {
            get
            {
                return Bo.Street;
            }
            set
            {
                SetProperty(value, (v, e) => e.Street = v);
            }
        }

        public string City
        {
            get
            {
                return Bo.City;
            }
            set
            {
                SetProperty(value, (v, e) => e.City = v);
            }
        }

        public string Country
        {
            get
            {
                return Bo.Country;
            }
            set
            {
                SetProperty(value, (v, e) => e.Country = v);
            }
        }

        public string PostalCode
        {
            get
            {
                return Bo.PostalCode;
            }
            set
            {
                SetProperty(value, (v, e) => e.PostalCode = v);
            }
        }
    }
}
