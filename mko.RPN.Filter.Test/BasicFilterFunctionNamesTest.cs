//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 11.4.2017
//
//  Projekt.......: mko.RPN.Filter.Test
//  Name..........: BasicFilterFunctionNamesTest.cs
//  Aufgabe/Fkt...: Test der Implementierung der systematischen Namensbildung für
//                  Filterfunktionen
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.RPN.Filter.Test
{
    [TestClass]
    public class BasicFilterFunctionNamesTest
    {

        IFilterFunctionNamePrefixes fnPrefix;
        IFilterFunctionNameFactories fnFactories;

        public BasicFilterFunctionNamesTest()
        {
            fnPrefix = new BasicFilterFunctionNamePrefixes();
            fnFactories = new BasicFilterFunctionNameFactories(fnPrefix);
        }


        [TestMethod]
        public void BasicFilterFunctionNamePrefix()
        {
            Assert.AreEqual(".key", fnPrefix.keyFltPrefix);
            Assert.AreEqual(".like", fnPrefix.likeFltPrefix);            
            Assert.AreEqual(".rng", fnPrefix.rngFltPrefix);
            Assert.AreEqual(".set", fnPrefix.setFltPrefix);
            Assert.AreEqual(".sort", fnPrefix.sortFltPrefix);
        }


        [TestMethod]
        public void BasicFilterFunctionNameFactories()
        {
            const string TestName = "test";

            var n = fnFactories.createKeyFltName(TestName);
            Assert.AreEqual(".key.test", n);
            Assert.IsTrue(fnFactories.IsKeyFilter(n));
            Assert.IsFalse(fnFactories.IsLikeFilter(n));
            Assert.AreEqual(TestName, fnFactories.reduceKeyFilterName(n));

            n = fnFactories.createLikeFltName(TestName);
            Assert.AreEqual(".like.test", n);
            Assert.IsTrue(fnFactories.IsLikeFilter(n));            
            Assert.AreEqual(TestName, fnFactories.reduceLikeFilterName(n));

            n = fnFactories.createRngFltName(TestName);
            Assert.AreEqual(".rng.test", n);
            Assert.IsTrue(fnFactories.IsRngFilter(n));
            Assert.AreEqual(TestName, fnFactories.reduceRngFilterName(n));

            n = fnFactories.createSetFltName(TestName);
            Assert.AreEqual(".set.test", n);
            Assert.IsTrue(fnFactories.IsSetFilter(n));
            Assert.AreEqual(TestName, fnFactories.reduceSetFilterName(n));

            n = fnFactories.createSortFltName(TestName);
            Assert.AreEqual(".sort.test", n);
            Assert.IsTrue(fnFactories.IsSortFilter(n));
            Assert.AreEqual(TestName, fnFactories.reduceSortFilterName(n));

        }



    }
}
