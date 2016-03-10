using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using G = mko.Algo.GraphTheory;
using System.Xml.Linq;

namespace mko.Algo.Test
{
    [TestClass]
    public class Graphentheorie
    {
        [TestMethod]
        public void PathfinderTest()
        {
            var pf = new G.Pathfinder();

            pf.MaxPathLength = 4;
            pf.MaxCountSolutions = 1;
            pf.Solve();

            //pf.MaxPathLength = 5;
            //pf.SolveSerial();
        }


        class GestirnSatz
        {
            public int ID { get; set; }
            public int ID_Zentrum { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// testdaten
        /// </summary>
        GestirnSatz[] Sonnensystem = {
                                        new GestirnSatz() { ID = 0, ID_Zentrum = 0, Name="Sonne" },
                                        new GestirnSatz() { ID = 1, ID_Zentrum = 0, Name = "Merkur"},
                                        new GestirnSatz() { ID = 2, ID_Zentrum = 0, Name = "Venus"},
                                        new GestirnSatz() { ID = 3, ID_Zentrum = 0, Name = "Erde"},
                                        new GestirnSatz() { ID = 4, ID_Zentrum = 0, Name = "Mars"},
                                        new GestirnSatz() { ID = 5, ID_Zentrum = 0, Name = "Jupiter"},
                                        new GestirnSatz() { ID = 6, ID_Zentrum = 0, Name = "Saturn"},
                                        new GestirnSatz() { ID = 7, ID_Zentrum = 0, Name = "Uranus"},
                                        new GestirnSatz() { ID = 8, ID_Zentrum = 0, Name = "Neptun"},
                                        new GestirnSatz() { ID = 9, ID_Zentrum = 3, Name = "Mond"},
                                        new GestirnSatz() { ID = 10, ID_Zentrum = 4, Name = "Phobos"},
                                        new GestirnSatz() { ID = 11, ID_Zentrum = 4, Name = "Deimos"},
                                        new GestirnSatz() { ID = 12, ID_Zentrum = 5, Name = "Europa"},
                                        new GestirnSatz() { ID = 13, ID_Zentrum = 5, Name = "Io"},
                                        new GestirnSatz() { ID = 14, ID_Zentrum = 5, Name = "Kallisto"},
                                        new GestirnSatz() { ID = 15, ID_Zentrum = 5, Name = "Ganymed"},
                                        new GestirnSatz() { ID = 16, ID_Zentrum = 5, Name = "Kallisto"},
                                        new GestirnSatz() { ID = 17, ID_Zentrum = 6, Name = "Iapetus"},
                                        new GestirnSatz() { ID = 18, ID_Zentrum = 6, Name = "Titan"},
                                        new GestirnSatz() { ID = 19, ID_Zentrum = 6, Name = "Reha"},
                                        new GestirnSatz() { ID = 20, ID_Zentrum = 6, Name = "Dione"},
                                        new GestirnSatz() { ID = 21, ID_Zentrum = 6, Name = "Tethys"},
                                        new GestirnSatz() { ID = 22, ID_Zentrum = 6, Name = "Dione"},
                                        new GestirnSatz() { ID = 23, ID_Zentrum = 6, Name = "Mimas"},
                                        new GestirnSatz() { ID = 24, ID_Zentrum = 6, Name = "Enceladus"},

                                    };

        GestirnSatz GetZentralkoerper(GestirnSatz Trabant)
        {
            return Sonnensystem.Where(gs => gs.ID == Trabant.ID_Zentrum).Single();
        }

        bool IstDieSonne(GestirnSatz Gestirn)
        {
            return Gestirn.ID == 0;
        }

        [TestMethod]
        public void GetPathTest()
        {
            var pfadEnceladus = mko.Algo.GraphTheory.Tree.GetPath(Sonnensystem.Last(), GetZentralkoerper, IstDieSonne).ToArray();

            Assert.AreEqual(pfadEnceladus.Count(), 3);
            Assert.AreEqual(pfadEnceladus[0].Name, "Sonne");
            Assert.AreEqual(pfadEnceladus[1].Name, "Saturn");
            Assert.AreEqual(pfadEnceladus[2].Name, "Enceladus");


            Assert.AreEqual(G.Tree.GetPath(Sonnensystem.Where(gs => gs.Name == "Erde").Single(), GetZentralkoerper, IstDieSonne).Count(), 2);

            Assert.AreEqual(G.Tree.GetPath(Sonnensystem.Where(gs => gs.Name == "Phobos").Single(), GetZentralkoerper, IstDieSonne).Count(), 3);

            Assert.AreEqual(G.Tree.GetPath(Sonnensystem.Where(gs => gs.Name == "Sonne").Single(), GetZentralkoerper, IstDieSonne).Count(), 1);

        }

        [TestMethod]
        public void InsertPathTest()
        {

            var Root = new XElement(XName.Get("Sonnensystem"));

            foreach (var Gestirn in Sonnensystem)
            {
                var pfad = G.Tree.GetPath(Gestirn, GetZentralkoerper, IstDieSonne).Select(gs => new XElement(XName.Get(gs.Name)));

                G.Tree.InsertPath(pfad, Root, xe => xe.Elements(), (tn, pn) => tn.Name == pn.Name, (tn, pn) => tn.Add(new XElement[]{pn}));

            }

            var AlleMx = Root.Descendants().Where(d => d.Name.LocalName.StartsWith("M"));

            Assert.AreEqual(AlleMx.Count(), 4);

            Assert.AreEqual(G.Tree.GetDepth(Root, xe => xe.Elements()), 4);


            var Sonne = G.Tree.GetLevel(Root, xe => xe.Elements(), 2);
            var AllePlaneten = G.Tree.GetLevel(Root, xe => xe.Elements(), 3);
            var AlleMonde = G.Tree.GetLevel(Root, xe => xe.Elements(), 4);

        }
    }
}
