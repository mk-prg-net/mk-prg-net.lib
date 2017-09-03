using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MkPrgNet.Pattern.StateMachine.Test
{
    [TestClass]
    public class DrehkreuzTests
    {
        [TestMethod]
        public void StateMachine_Drehkreuz_Create()
        {
            var dk = new Drehkreuz.Automat();

            dk.Start();

            // Startzustand sei Zu
            Assert.IsInstanceOfType(dk.CurrentState, typeof(Drehkreuz.Zu.State));
        }


        [TestMethod]
        public void StateMachine_Drehkreuz_Zu_Muenzeinwurf()
        {
            var dk = new Drehkreuz.Automat();

            dk.Start();

            // Startzustand sei Zu
            Assert.IsInstanceOfType(dk.CurrentState, typeof(Drehkreuz.Zu.State));

            // 1 Euro reicht nicht 
            dk.Muenzeinwurf.Einwurf(1, 0);
            dk.Transistion();
            Assert.IsInstanceOfType(dk.CurrentState, typeof(Drehkreuz.Zu.State));

            // Drehen am drehkreuz nützt nichts, das Tor bleibt geschlossen
            dk.Drehkreuz.drehen();
            dk.Transistion();
            Assert.IsInstanceOfType(dk.CurrentState, typeof(Drehkreuz.Zu.State));

            // Mit 1,50 Euro öffnet sich das Tor
            dk.Muenzeinwurf.Einwurf(1, 50);
            dk.Transistion();
            Assert.IsInstanceOfType(dk.CurrentState, typeof(Drehkreuz.Auf.State));

            // Weitere Münzeinwürfe lassen das tor geöffnet
            dk.Muenzeinwurf.Einwurf(1, 0);
            dk.Transistion();
            Assert.IsInstanceOfType(dk.CurrentState, typeof(Drehkreuz.Auf.State));

            // Nach dem passieren schließt sich das Drehkreuz wieder
            dk.Drehkreuz.drehen();
            dk.Transistion();
            Assert.IsInstanceOfType(dk.CurrentState, typeof(Drehkreuz.Zu.State));

            // Das Drehkreuz bleibt zu
            dk.Drehkreuz.drehen();
            dk.Transistion();
            Assert.IsInstanceOfType(dk.CurrentState, typeof(Drehkreuz.Zu.State));


        }


    }
}
