//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.3.2017
//
//  Projekt.......: mko.Test
//  Name..........: TraceHlpTests.cs
//  Aufgabe/Fkt...: Test der Trace- Helper
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
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.Test
{

    class MyClass
    {
        public void MyMethodWithArgEx()
        {
            mko.TraceHlp.ThrowArgExIfNot(false, "ein Testfehler", new Exception("innere Testexception"));
        }

        public void MyMethodWithEx()
        {
            mko.TraceHlp.ThrowArgExIfNot(false, "ein Testfehler");
        }

        public static void MyStaticMethodWithArgEx()
        {
            mko.TraceHlp.ThrowArgExIfNot(false, "ein Testfehler");
        }

    }


    static class MyStaticClass
    {
        public static void MyStaticMethodWithArgEx()
        {
            mko.TraceHlp.ThrowArgExIfNot(false, "ein Testfehler");
        }

    }



    [TestClass]
    public class TraceHlpTests
    {
        [TestMethod]
        public void TraceHlp_ThrowIf()
        {
            var myObj = new MyClass();

            try
            {
                myObj.MyMethodWithArgEx();
            } catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

            try
            {
                myObj.MyMethodWithEx();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));
            }


            try
            {
                MyStaticClass.MyStaticMethodWithArgEx();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }

        }
    }
}
