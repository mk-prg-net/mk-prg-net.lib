using mkoIt.Xhtml.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace mko.Xhtml.Test
{


    /// <summary>
    ///Dies ist eine Testklasse für "StyleBuilderTest" und soll
    ///alle StyleBuilderTest Komponententests enthalten.
    ///</summary>
    [TestClass()]
    public class StyleBuilderTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Testkontext auf, der Informationen
        ///über und Funktionalität für den aktuellen Testlauf bietet, oder legt diesen fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        #region Zusätzliche Testattribute
        // 
        //Sie können beim Verfassen Ihrer Tests die folgenden zusätzlichen Attribute verwenden:
        //
        //Mit ClassInitialize führen Sie Code aus, bevor Sie den ersten Test in der Klasse ausführen.
        [ClassInitialize()]
        public static void StyleBuilderTestInit(TestContext testContext)
        {
            mko.Newton.Init.Do();
        }
        //
        //Mit ClassCleanup führen Sie Code aus, nachdem alle Tests in einer Klasse ausgeführt wurden.
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen.
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Ein Test für "StyleBuilder-Konstruktor"
        ///</summary>
        [TestMethod()]
        public void StyleBuilderConstructorTest()
        {
            StyleBuilder target = new StyleBuilder();
            Assert.IsNotNull(target);

        }

        /// <summary>
        ///Ein Test für "StyleBuilder-Konstruktor"
        ///</summary>
        [TestMethod()]
        public void StyleBuilderConstructorTest1()
        {
            StyleBuilder bld = new StyleBuilder(); // TODO: Passenden Wert initialisieren

            bld.FontFamiliy = Font.Courier;
            bld.ForeColor = Color.Green;
            bld.FontSize = FontSize.Point(12.0);
            bld.FontWeight = FontWeight.Bold;
            bld.Margin = Length.cm(1.0);


            StyleBuilder target = new StyleBuilder(bld);

            Assert.IsTrue(target.FontFamiliy == bld.FontFamiliy);
            Assert.IsTrue(target.ForeColor == bld.ForeColor);
            Assert.IsTrue(target.FontSize == bld.FontSize);
            Assert.IsTrue(target.FontWeight == bld.FontWeight);
            Assert.IsTrue(target.Margin == bld.Margin);
        }

        /// <summary>
        ///Ein Test für "GetDifferenceOf"
        ///</summary>
        [TestMethod()]
        public void GetDifferenceOfTest()
        {
            StyleBuilder Parent = new StyleBuilder()
            {
                BackgroundColor = Color.Red,
                Width = Length.cm(24.50),
                FontFamiliy = Font.TimesNewRoman,
                Margin = Length.Percent(50),
                MarginTop = Length.cm(2.0)
            };

            StyleBuilder Child = new StyleBuilder()
            {
                BackgroundColor = Color.Red,
                Width = Length.cm(2),
                FontFamiliy = Font.Arial,
                MarginLeft = Length.mm(32.0)
            };

            StyleBuilder expected = new StyleBuilder()
            {
                Width = Length.cm(2),
                FontFamiliy = Font.Arial,
                MarginLeft = Length.mm(32.0)
            };
            StyleBuilder actual;
            actual = StyleBuilder.GetDifferenceOf(Parent, Child);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Ein Test für "ToString"
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            StyleBuilder target = new StyleBuilder()
            { // TODO: Passenden Wert initialisieren
                FontFamiliy = Font.Courier,
                ForeColor = Color.Green,
                FontSize = FontSize.Point(12.0),
                FontWeight = FontWeight.Bold,
                Margin = Length.cm(1.0),
                Width = Length.cm(24.50),                
                MarginTop = Length.cm(2.0)
            };

            target.FontFamiliy = Font.TimesNewRoman;


            string expected = "font-family: Times New Roman, serif; color: #008000; font-size: 12 pt; font-weight: bold; margin-top: 2 cm; width: 24.5 cm; ";

            string actual;
            actual = target.ToString();

            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///Ein Test für "ToString"
        ///</summary>
        [TestMethod()]
        public void ToStringTest1()
        {
            StyleBuilder.StyleKeys Key = StyleBuilder.StyleKeys.background_color; // TODO: Passenden Wert initialisieren
            string expected = "background-color"; // TODO: Passenden Wert initialisieren
            string actual;
            actual = StyleBuilder.ToString(Key);
            Assert.AreEqual(expected, actual);            
        }

        /// <summary>
        ///Ein Test für "BackgroundColor"
        ///</summary>
        [TestMethod()]
        public void BackgroundColorTest()
        {
            StyleBuilder target = new StyleBuilder()
            { // TODO: Passenden Wert initialisieren
                FontFamiliy = Font.Courier,
                ForeColor = Color.Green,
                FontSize = FontSize.Point(12.0),
                FontWeight = FontWeight.Bold,
                Margin = Length.cm(1.0),
                Width = Length.cm(24.50),                
                MarginTop = Length.cm(2.0)
            };            

            Color expected = Color.Gray; // TODO: Passenden Wert initialisieren
            Color actual;
            target.BackgroundColor = expected;
            actual = target.BackgroundColor;
            Assert.AreEqual(expected, actual);            
        }

    }
}
