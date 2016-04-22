using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Repositories.Addresses
{
    partial class CompanyAddresses 
    {
        /// <summary>
        /// Struktur eines Builders, mit dem Filter und Sortierkriterien für die Menge der Firmenanschriften definiert wird.
        /// Anschließend kann ein Objekt mittels GetSet() abgerufen werden, über welches auf die 
        /// gefilterten und sortierten Firmenanschriften zugegriffen werden kann.
        /// </summary>
        public interface IFilteredSortedSetBuilder : Interfaces.IFilteredSortedSetBuilder<Bo.Addresses.IMailingAddressCompany>
        {
          
            // Filterkriterien definieren

            void defCompanyName(string Name);

            void defCityLike(string pattern);

            void defPostalCode(string PostalCode);

            void defStreetLike(string pattern);

            void defCountryLike(string pattern);

            // Sortierkriterien definieren

            void sortByCompanyName(bool descending);

            void sortByCity(bool descending);

            void sortByPostalCode(bool descending);

            void sortByStreetName(bool descending);
              
        }

        /// <summary>
        /// Liefert einen neuen Builder, über den Filter- und Sortierkriterien eingestellt werden können, um mit ihm
        /// anschließend ein IFilteredSortedSet- Objekt zu erzeugen.
        /// </summary>
        /// <returns></returns>
        public abstract IFilteredSortedSetBuilder getFilteredSortedSetBuilder();

    }
}
