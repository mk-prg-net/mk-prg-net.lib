using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DB.Kepler.EF60.Test
{
    [TestClass]
    public class KeplerDBInstallerTest
    {
        #region Zusätzliche Testattribute
        //
        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
        //
        // Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
        [ClassInitialize()]
        public static void CreateDB(TestContext testContext)
        {
            try
            {
                KeplerDBInstaller.CreateDB();

                KeplerDBInstaller.InitBaseTables();

            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        //
        // Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen. 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestMethod]
        public void FillDB()
        {
            try
            {
                KeplerDBInstaller.FillDBWithStarsPlanetsAndMoons();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
