using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace mko.BI.Repositories.Addresses.InMem
{
    public partial class CompanyAddresses
    {
        public class FilteredSortedSetBuilder : Addresses.CompanyAddresses.IFilteredSortedSetBuilder
        {
            IQueryable<Bo.Addresses.MailingAddressCompany> query;
            List<Bo.Addresses.MailingAddressCompany> _all;

            //IOrderedQueryable<Bo.Addresses.MailingAddressCompany> sorter = 

            internal FilteredSortedSetBuilder(List<Bo.Addresses.MailingAddressCompany> _all)
            {
                this._all = _all;
                query = _all.AsQueryable<Bo.Addresses.MailingAddressCompany>();
            }

            public void defCompanyName(string Name)
            {
                query = query.Where(r => r.CompanyName == Name);
            }

            public void defCityLike(string pattern)
            {                
                query = query.Where(r => r.City != null && r.City.Contains(pattern)); //, StringComparison.CurrentCultureIgnoreCase));
            }

            public void defPostalCode(string PostalCode)
            {
                query = query.Where(r => r.PostalCode == PostalCode);
            }

            public void defStreetLike(string pattern)
            {
                query = query.Where(r => r.Street != null && r.Street.StartsWith(pattern));
            }

            public void defCountryLike(string pattern)
            {
                query = query.Where(r => r.Country != null && r.Country.StartsWith(pattern));
            }


            //---------------------------------------------------------------------------------------------------------------------------------------
            // Sortierung definieren

            List<DefSortOrder<Bo.Addresses.MailingAddressCompany>> _DefSortOrders = new List<DefSortOrder<Bo.Addresses.MailingAddressCompany>>();

            public void sortByCompanyName(bool descending)
            {
                _DefSortOrders.Add(new DefSortByCompanyName(descending));
            }

            internal class DefSortByCompanyName : DefSortOrderCol<Bo.Addresses.MailingAddressCompany, string>
            {
                 public DefSortByCompanyName(bool descending) : base(r => r.CompanyName, descending){}
            }


            public void sortByCity(bool descending)
            {
                _DefSortOrders.Add(new DefSortByCity(descending));
            }

            internal class DefSortByCity : DefSortOrderCol<Bo.Addresses.MailingAddressCompany, string>
            {
                public DefSortByCity(bool descending) : base(r => r.City, descending) { }
            }


            public void sortByPostalCode(bool descending)
            {
                _DefSortOrders.Add(new DefSortByPostalCode(descending));
            }

            internal class DefSortByPostalCode : DefSortOrderCol<Bo.Addresses.MailingAddressCompany, string>
            {
                public DefSortByPostalCode(bool descending) : base(r => r.PostalCode, descending) { }
            }


            public void sortByStreetName(bool descending)
            {
                _DefSortOrders.Add(new DefSortByStreetName(descending));
            }

            internal class DefSortByStreetName : DefSortOrderCol<Bo.Addresses.MailingAddressCompany, string>
            {
                public DefSortByStreetName(bool descending) : base(r => r.Street, descending) { }
            }



            public Interfaces.IFilteredSortedSet<Bo.Addresses.IMailingAddressCompany> GetSet()
            {
                return new FilteredSortedSet<Bo.Addresses.MailingAddressCompany>(query, _DefSortOrders);
            }
        }
    }
}
