using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace mko.BI.Test
{
    [TestClass]
    public class RepositoriesTest
    {

        mko.BI.Repositories.Addresses.CompanyAddresses repo;

        [TestInitialize]
        public void Init()
        {
            repo = new Repositories.Addresses.InMem.CompanyAddresses();

            repo.CreateBoAndAddToCollection("mk-prg-net");
            repo.SubmitChanges();
            var adr = repo.Get("mk-prg-net");
            adr.City = "Stuttgart";
            adr.Country = "de";
            adr.PostalCode = "70599";
            adr.Street = "Hans-Kächele-Str. 11";

            repo.CreateBoAndAddToCollection("DagobertBank");
            repo.SubmitChanges();
            adr = repo.Get("DagobertBank");
            adr.City = "Entenhausen";
            adr.Country = "de";
            adr.PostalCode = "12345";
            adr.Street = "Goldtalerallee 1";

            repo.CreateBoAndAddToCollection("Tante Emma Laden");
            repo.SubmitChanges();
            adr = repo.Get("Tante Emma Laden");
            adr.City = "Entenhausen";
            adr.Country = "de";
            adr.PostalCode = "12345";
            adr.Street = "Goldtalerallee 2";

            repo.CreateBoAndAddToCollection("Donald - Allrounder");
            repo.SubmitChanges();
            adr = repo.Get("Donald - Allrounder");
            adr.City = "Entenhausen";
            adr.Country = "de";
            adr.PostalCode = "12346";
            adr.Street = "Wurschtelgasse 3";

            repo.CreateBoAndAddToCollection("Mineralbad Leuze");
            repo.SubmitChanges();
            adr = repo.Get("Mineralbad Leuze");
            adr.City = "Stuttgart";
            adr.Country = "de";
            adr.PostalCode = "70190";
            adr.Street = "Am Leuzebad 2";

            repo.CreateBoAndAddToCollection("Claas Clever - Immobilien");
            repo.SubmitChanges();
            adr = repo.Get("Claas Clever - Immobilien");
            adr.City = "Entenhausen";
            adr.Country = "de";
            adr.PostalCode = "12344";
            adr.Street = "Schlauer Weg 1";

            repo.CreateBoAndAddToCollection("Staatsgalerie Stuttgart");
            repo.SubmitChanges();
            adr = repo.Get("Staatsgalerie Stuttgart");
            adr.City = "Stuttgart";
            adr.Country = "de";
            adr.PostalCode = "70173";
            adr.Street = "Konrad-Adenauer-Str. 30-32";

            repo.CreateBoAndAddToCollection("Reichstag");
            repo.SubmitChanges();
            adr = repo.Get("Reichstag");
            adr.City = "Berlin";
            adr.Country = "de";
            adr.PostalCode = "10557";
            adr.Street = "Scheidemannstraße 2";

            repo.CreateBoAndAddToCollection("Fernsehturm Berlin");
            repo.SubmitChanges();
            adr = repo.Get("Fernsehturm Berlin");
            adr.City = "Berlin";
            adr.Country = "de";
            adr.PostalCode = "10178";
            adr.Street = "Panoramastraße 1A";

            repo.CreateBoAndAddToCollection("Fernsehturm Stuttgart");
            repo.SubmitChanges();
            adr = repo.Get("Fernsehturm Stuttgart");
            adr.City = "Stuttgart";
            adr.Country = "de";
            adr.PostalCode = "70597";
            adr.Street = "Jahnstraße 120";


            repo.CreateBoAndAddToCollection("Eiffelturm");
            repo.SubmitChanges();
            adr = repo.Get("Eiffelturm");
            adr.City = "Paris";
            adr.Country = "fr";
            adr.PostalCode = "75007";
            adr.Street = "Champ de Mars, 5 Avenue Anatole";

            repo.CreateBoAndAddToCollection("Siegessäule");
            repo.SubmitChanges();
            adr = repo.Get("Siegessäule");
            adr.City = "Berlin";
            adr.Country = "de";
            adr.PostalCode = "10557";
            adr.Street = "Großer Stern 1";

            repo.SubmitChanges();
        }

        [TestCleanup]
        public void Cleanup()
        {
            repo.RemoveAll();
            repo.SubmitChanges();
        }


        [TestMethod]
        public void Repositories_GetBo()
        {           

            var bo = repo.Get("Siegessäule");
            Assert.IsNotNull(bo, "Die Siegessäule sollte als Adresse erfasst sein");
            Assert.AreEqual("Großer Stern 1", bo.Street, "Die Siegesäule sollte auf dem Großen Stern stehen");

            Assert.IsTrue(repo.Any("Fernsehturm Stuttgart"));
            bo = repo.Get("Fernsehturm Stuttgart");
            Assert.IsNotNull(bo, "Der Stuttgarter Fernsehtumr sollte als Adresse erfasst sein");
            Assert.AreEqual("Jahnstraße 120", bo.Street, "Der Fernsehturm in Stuttgart sollte in der Jahnstrasse stehen.");
        }


        [TestMethod]
        public void Repositories_FilterAndSort()
        {
            var builder = repo.getFilteredSortedSetBuilder();
            builder.defCityLike("Stuttgart");            
            builder.sortByCompanyName(false);

            var StuttgarterFirmen = builder.GetSet();

            Assert.IsTrue(StuttgarterFirmen.Any(), "Für Stuttgart sollten Ergebnisse gefunden werden");
            var res = StuttgarterFirmen.Get();

            Assert.IsTrue(res.First().CompanyName[0] <= res.Last().CompanyName[0], "Die Menge sollte bezüglich des Firmennamens austeigend sortiert sein");


            var builder2 = repo.getFilteredSortedSetBuilder();
            builder2.sortByCity(false);
            builder2.sortByStreetName(false);
            builder2.defCountryLike("de");

            var DeutscheFirmen = builder2.GetSet();


            Assert.IsTrue(DeutscheFirmen.Any(), "Es sollten Adressen aus Deutschland gefunden werden");
            res = DeutscheFirmen.Get();

            Assert.IsTrue(res.First().City[0] <= res.Last().City[0], "Die Menge sollte bezüglich des Firmennamens austeigend sortiert sein");


        }



    }
}
