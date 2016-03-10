using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

using mkoIt.Db;

using Kepler = DB.Kepler.EF50;

namespace mko.Db.Test
{
    [TestClass]
    public class BoBase_EF50_Tests
    {

        string MakeCSVFileName(string fname)
        {
            return System.IO.Path.Combine(Properties.Settings.Default.BackUpDir, fname + ".csv");
        }

        const string FNameLaender = "Laender";
        const string FNameRaumschiffAufgaben = "RaumschiffAufgaben";

        [TestMethod]
        public void BackupTest()
        {
            try
            {
                int CountLaender = DB.Kepler.EF50.KeplerBackup.SaveLaenderAsCSV(MakeCSVFileName("Laender"));
                int CountRA = DB.Kepler.EF50.KeplerBackup.SaveRaumschiffAufgabenAsCSV(MakeCSVFileName("RaumschiffAufgaben"));
                int CountHT = DB.Kepler.EF50.KeplerBackup.SaveHimmelskoerperTypenAsCSV(MakeCSVFileName("HimmelskoerperTyp"));

                // alle Daten Löschen

                DB.Kepler.EF50.KeplerBackup.ClearLaender();
                DB.Kepler.EF50.KeplerBackup.ClearRaumschiffAufgaben();
                DB.Kepler.EF50.KeplerBackup.ClearHimmelskoerperTypen();

                // Daten wiederherstellen

                int CountLaenderNeu = DB.Kepler.EF50.KeplerBackup.RestoreLaenderFromCSV(MakeCSVFileName("Laender"));
                int CountRAneu = DB.Kepler.EF50.KeplerBackup.RestoreRaumschiffAufgabenFromCSV(MakeCSVFileName("RaumschiffAufgaben"));
                int CountHTneu = DB.Kepler.EF50.KeplerBackup.RestoreHimmelskoerperTypenFromCSV(MakeCSVFileName("HimmelskoerperTyp"));

                Assert.AreEqual(CountLaender, CountLaenderNeu);
                Assert.AreEqual(CountRA, CountRAneu);
                Assert.AreEqual(CountHT, CountHTneu);

            }
            catch (Exception ex)
            {
                Assert.Fail("Backup mit Exception fehlgeschlagen: " + ex.Message);
            }
        }


        [TestMethod]
        public void BoLaenderTest()
        {
            try
            {
                using (var bo = new DB.Kepler.EF50.BoLaender())
                {
                    Assert.IsTrue(bo.Any());

                    // Anzahl aller Länder bestimmen
                    int LaenderTabLineCount = bo.ORMContext.LaenderTab.Count();

                    var AllEntities = bo.GetEntities();
                    Assert.AreEqual(LaenderTabLineCount, AllEntities.Count());

                    // Einzelzugriff testen
                    var erstesLand = AllEntities.First();
                    Assert.IsNotNull(erstesLand);


                    var erstesLand_2 = bo.GetEntity(erstesLand.ID);
                    Assert.AreEqual(erstesLand.Laenderkennzeichen, erstesLand_2.Laenderkennzeichen);

                    var erstesLandView = bo.GetView(erstesLand.ID);
                    Assert.AreEqual(erstesLand.Laenderkennzeichen, erstesLandView.Laenderkennzeichen);


                    // Alle Länder als Queryable abrufen
                    var ViewsAsIQ = bo.GetViews_AsQueryable();
                    Assert.AreEqual(LaenderTabLineCount, ViewsAsIQ.Count());

                    // Alle Länder als Observable abrufen
                    var ViewsAsObsrv = bo.GetViews_AsObservable();
                    Assert.AreEqual(LaenderTabLineCount, ViewsAsObsrv.Count());

                    // Noch keine Filter gesetzt -> Mächtigkeit der gefilterten Menge == Mächtigkeit der gesamten Menge
                    Assert.AreEqual(LaenderTabLineCount, bo.CountFilteredEntities());

                    var firstEntity = bo.ORMContext.LaenderTab.First();
                    var IdFilterFirstEntity = new DB.Kepler.EF50.BoLaender.View.IdFilter() { RValue = firstEntity.ID };

                    bo.SetFilter(IdFilterFirstEntity);
                    bo.SortColumn = "Name";

                    var filteredEntites = bo.GetEntitiesFilteredAndSorted();
                    Assert.AreEqual(filteredEntites.Count(), 1);

                    var filteredViewsAsIQ = bo.GetViewsFilteredAndSorted_AsQueryable();
                    Assert.AreEqual(filteredViewsAsIQ.Count(), 1);

                    var filteredViewsAsObsrv = bo.GetViewsFilteredAndSorted_AsObservableCollection();
                    Assert.AreEqual(filteredViewsAsObsrv.Count(), 1);

                    // Filter löschen
                    bo.RemoveAllFilters();
                    Assert.AreEqual(LaenderTabLineCount, bo.CountFilteredViews());

                    // Erzeugung von einer leeren Views beim Abrufen einschalten
                    bo.CreateDummyOn = true;
                    Assert.AreEqual(LaenderTabLineCount + 1, bo.CountFilteredViews());

                    filteredViewsAsIQ = bo.GetViewsFilteredAndSorted_AsQueryable();
                    Assert.AreEqual(LaenderTabLineCount + 1, filteredViewsAsIQ.Count());


                    string BackupCsv = MakeCSVFileName("LaenderTest");
                    DB.Kepler.EF50.KeplerBackup.SaveLaenderAsCSV(BackupCsv);

                    var LaenderEntitiesStack = new Stack<DB.Kepler.EF50.Land>();
                    var LaenderViewStack = new Stack<DB.Kepler.EF50.BoLaender.View>();
                    try
                    {
                        // Hinzufügen von Entities
                        var Mongolei = new DB.Kepler.EF50.Land()
                        {
                            Laenderkennzeichen = "MGL",
                            Name = "Mongolei"
                        };
                        LaenderEntitiesStack.Push(Mongolei);

                        bo.Insert(Mongolei);

                        LaenderTabLineCount++;
                        Assert.AreEqual(LaenderTabLineCount, bo.CountAllEntities());

                        var Portugal = new DB.Kepler.EF50.Land()
                        {
                            Laenderkennzeichen = "Pt",
                            Name = "Portugal"
                        };
                        LaenderEntitiesStack.Push(Portugal);

                        bo.Insert(Portugal);

                        LaenderTabLineCount++;
                        Assert.AreEqual(LaenderTabLineCount, bo.CountAllEntities());


                        // Hinzufügen von Views
                        var Polen = bo.CreateEntityView();
                        Polen.Name = "Polen";
                        Polen.Laenderkennzeichen = "PL";
                        LaenderViewStack.Push(Polen);

                        bo.Insert(Polen);

                        LaenderTabLineCount++;
                        Assert.AreEqual(LaenderTabLineCount, bo.CountAllEntities());

                        // Aktualisieren von Views
                        Polen.Name = "Polska";
                        bo.Update(Polen);

                        Assert.AreEqual(LaenderTabLineCount, bo.CountAllEntities());

                        // Observable Collection testen
                        bo.RemoveAllFilters();
                        var NameLikeFilter = new DB.Kepler.EF50.BoLaender.View.NameLikeFilter() { RValue = "P" };
                        bo.SetFilter(NameLikeFilter);

                        Assert.IsTrue(bo.CreateDummyOn);
                        var ObsrvColl = bo.GetViewsFilteredAndSorted_AsObservableCollection();

                        // Leerzeile suchen
                        var dummy = ObsrvColl.Where(v => v.State == mkoIt.Db.BoBaseViewDefs.ViewState.Added).Single();

                        dummy.Name = "Pakistan";
                        dummy.Laenderkennzeichen = "PAK";
                        //LaenderViewStack.Push(dummy);

                        bo.UpdateWithObservableEntityViewCollectionAndSubmit(ObsrvColl);

                        LaenderTabLineCount++;
                        Assert.AreEqual(LaenderTabLineCount, bo.CountAllEntities());

                        // Weitere Zeile hinzufügen
                        ObsrvColl.Add(new DB.Kepler.EF50.BoLaender.View());
                        var newLine = ObsrvColl.Where(v => v.State == mkoIt.Db.BoBaseViewDefs.ViewState.Added).Single();

                        newLine.Name = "Polynesien";
                        newLine.Laenderkennzeichen = "Poly";
                        LaenderViewStack.Push(newLine);

                        // Zeile löschen
                        ObsrvColl.Remove(dummy);

                        // Datenquelle aktualisieren
                        bo.UpdateWithObservableEntityViewCollectionAndSubmit(ObsrvColl);

                        Assert.AreEqual(LaenderTabLineCount, bo.CountAllEntities());

                    }
                    finally
                    {
                        while (LaenderViewStack.Count > 0)
                            bo.Delete(LaenderViewStack.Pop());

                        while (LaenderEntitiesStack.Count > 0)
                            bo.Delete(LaenderEntitiesStack.Pop().ID);
                    }
                }
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void BoBeziehungTest()
        {
            try
            {
                using (var tranScope = new System.Transactions.TransactionScope())
                {
                    using (var ctx = new DB.Kepler.EF50.KeplerDBContainer())
                    {

                        var TypStern = ctx.HimmelskoerperTypenTab.Where(r => r.Name == "Sterne").Single();

                        TypStern.Himmelskoerper.Add(new DB.Kepler.EF50.Himmelskoerper() { Name = "Sonne", Masse_in_kg = mko.Newton.Mass.MassOfSun.Value });
                        TypStern.Himmelskoerper.Add(new DB.Kepler.EF50.Himmelskoerper() { Name = "Beteigeuze", Masse_in_kg = mko.Newton.Mass.MassOfSun.Value * 20.0 });

                        ctx.SaveChanges();

                        Assert.AreEqual(ctx.HimmelskoerperTypenTab.Count(), 2);

                    }

                }
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }


        [TestMethod]
        public void BoSortTest()
        {
            try
            {
                using (var bo = new DB.Kepler.EF50.Bo.BoHimmelskoerper())
                {
                    Assert.IsTrue(bo.Any());

                    // Anzahl aller Länder bestimmen
                    int HKCount = bo.ORMContext.HimmelskoerperTab.Count();

                    Assert.IsTrue(HKCount > 0);

                    // Zugriff auf statische Methode zum Sortieren
                    var HK_sortiert_nach_name = Kepler.Bo.BoHimmelskoerper.SortByEntityAttribute(bo.EntityCollection, true, e => e.Name);

                    var boHkTypen = new Kepler.BoHimmelskoerperTypen();
                    boHkTypen.SortJobDefine(Kepler.BoHimmelskoerperTypen.CreateSortJob("Name"));
                    var PlanetenTyp =  boHkTypen.GetViewsFilteredAndSorted_AsQueryable(0, 1, new Kepler.BoHimmelskoerperTypen.View.Name_Filter() { RValue = "Planet"}).Single();

                    bo.SetFilter(new Kepler.Bo.BoHimmelskoerper.View.TypeName_Filter() { RValue = PlanetenTyp });

                    var res = bo.GetViewsFilteredAndSorted_AsQueryable(0, bo.CountAllEntities(),
                        Kepler.Bo.BoHimmelskoerper.CreateSortJob("Name"),
                        Kepler.Bo.BoHimmelskoerper.CreateSortJob("Umlaufgeschwindigkeit"));


                }
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }

        }




    }
}
