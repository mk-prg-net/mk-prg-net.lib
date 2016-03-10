using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lisp = mko.Algo.Listprocessing;
using N = mko.Newton;


namespace mko.Newton.Test
{
    [TestClass]
    public class Measures
    {
        [TestMethod]
        public void MesswertMitBasiseinheitTest()
        {
            N.Init.Do();

            //string orm = N.OrderOfMagnitude.OrderOfMagnitudeId[OrderOfMagnitude.OrderOfMagnitudeEnum.Atto];
            //N.OrderOfMagnitude.Atto a = new OrderOfMagnitude.Atto();

            var erdumfang = N.Length.DiameterEarth * Math.PI;

            Assert.IsTrue(Math.PI * N.Length.DiameterEarth.Vector[0] == erdumfang.Vector[0]);
            Assert.IsTrue(Math.PI * N.Length.DiameterEarth.Vector[0] == erdumfang.Vector.Length);
            Assert.IsTrue(Math.PI * N.Length.DiameterEarth.Vector[0] * 1000.0 == erdumfang.VectorInBaseUnit.Length);
            
            Assert.IsTrue(N.Length.Decimeter(N.Length.DiameterEarth).Vector[0] == N.Length.DiameterEarth.Vector[0] * 10000.0);
            Assert.IsTrue(N.Length.Decimeter(N.Length.DiameterEarth).Vector.Length == N.Length.DiameterEarth.Vector[0] * 10000.0);
            Assert.IsTrue(N.Length.Inch(N.Length.DiameterEarth).Vector[0] == N.Length.DiameterEarth.Vector[0] * 1000.0 / 0.0254);

            var AU = N.Length.AU(1.0);
            Assert.AreEqual(AU.Vector[0], 1.0);

            var AUm = N.Length.Meter(AU);
            Assert.AreEqual(AUm.Vector[0], 149597870700.0);

            var AUkm = N.Length.Kilometer(AU);
            Assert.AreEqual(Math.Round(AUkm.Vector[0], 3), 149597870.7);

            var Day = N.Time.Days(1.0);
            Assert.AreEqual(Day.Value, 1.0);

            var DayInHours = N.Time.Hours(Day);
            Assert.AreEqual(DayInHours.Value, 24.0);

            var DayInSec = N.Time.Sec(Day);
            Assert.AreEqual(DayInSec.Value, 3600.0 * 24.0);

            var Wh_1 = N.Energy.Wh(1.0);
            var KJ = N.Energy.KiloJoule(Wh_1);
            Assert.AreEqual(Wh_1.Value * 3.6, KJ.Value);

            var Nm = N.Energy.Nm(Wh_1);
            Assert.AreEqual(Wh_1.Value * 3600, Nm.Value);

            var kkw_nw = N.Energy.KKW_Nekarwestheim_Block_II_Produktion_2010_elektr;

            var kkw_nw_in_J = N.Energy.Joule(kkw_nw);

            var kkw_nw_in_KJ = N.Energy.KiloJoule(kkw_nw);

        }

        [TestMethod]
        public void MesswertMitAbgeleiteterEinheitTest()
        {
            var AbstandErdeSonne = N.Length.AU(1.0);
            var LängeSonnenumlauf = 2 * AbstandErdeSonne * Math.PI;

            var vErdeSonnenumlauf = N.Velocity.KilometerPerSec(LängeSonnenumlauf, N.Time.Days(365));

            Assert.IsInstanceOfType(vErdeSonnenumlauf, typeof(N.Velocity));
            // Mittlere Umlaufgeschwindigkeit lt. http://en.wikipedia.org/wiki/Earth
            Assert.AreEqual(Math.Round(vErdeSonnenumlauf.Vector[0], 1), 29.8);           

        }

        [TestMethod]
        public void EnergieUndLeistung()
        {
            var Epot1kg1mhoch = N.Energy.EpotInNm(N.Mass.Kilogram(1.0), N.Acceleration.GravityOnEarth, N.Length.Meter(1.0));
            double Epot1kg1mhochManuell = 9.81;
            Assert.AreEqual(Math.Round(Epot1kg1mhoch.Value, 2), Epot1kg1mhochManuell);

            var Benzin_1_kg_EnergieInKWh = N.Energy.KiloWh(N.Energy.Benzin_1kg);

            // http://de.answers.yahoo.com/question/index?qid=20091213063844AALHxTq
            // ca. 22 km
            var HubVon150lWasserMit1kgBenzin = N.Energy.LiftUpVectorInMeter(N.Energy.Benzin_1kg, N.Acceleration.GravityOnEarth, N.Mass.Kilogram(150));
            Assert.AreEqual(Math.Round(HubVon150lWasserMit1kgBenzin.Vector.Length, 0), 20508.0);
            
            // Wieviel Meter kann ein PKW mit meinem Liter Benzin gehoben werden ?
            var LiftPKW = N.Energy.LiftUpVectorInMeter(N.Energy.Benzin_1kg , N.Acceleration.GravityOnEarth, N.Mass.Tons(1.5));
            Assert.AreEqual(Math.Round(LiftPKW.Vector.Length, 0), 2051);


            // Wie hoch muss der Bodensee gehoben werden, um die Jahresstromproduktion von 
            // Neckarwestheim zu speichern ?

            var HöhenDiff = N.Energy.LiftUpVectorInMeter(N.Energy.KKW_Nekarwestheim_Block_II_Produktion_2010_elektr, 
                                                            N.Acceleration.GravityOnEarth, 
                                                            N.Mass.GesamtmasseBodenseewasser);

            Assert.AreEqual(Math.Round(HöhenDiff.Vector.Length, 0), 83);
        }

    }
}
