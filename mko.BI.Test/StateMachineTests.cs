using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using mko.BI.StateMachine;
using Mocks= mko.BI.TestMockUps;

namespace mko.BI.Test
{
    [TestClass]
    public class StateMachineTests
    {
        [TestMethod]
        public void StateMachine_Drehkreutz()
        {
            var sm = new Mocks.AutomatDrehkreuz.Controller();

            sm.ActiveState = sm.StateFactory.CreateZu();

            var zustand = sm.StateFactory.CreateZu();

            sm.ActiveState = zustand.Transition(new Mocks.AutomatDrehkreuz.Zu.Input() { Tag = Mocks.AutomatDrehkreuz.Zu.Input.Tags.tueNix });
            
            Assert.AreEqual("Zu", sm.ActiveState.Name, "Zu wurde erwartet");
            Assert.IsInstanceOfType(sm.ActiveState, typeof(Mocks.AutomatDrehkreuz.Zu.Context), "Zu Zustand hat den Typ Zu.Context wurde erwartet");

            sm.ActiveState = zustand.Transition(new Mocks.AutomatDrehkreuz.Zu.Input() { Tag = Mocks.AutomatDrehkreuz.Zu.Input.Tags.EinEuro });

            Assert.AreEqual("Auf", sm.ActiveState.Name, "Auf wurde erwartet");
            Assert.IsInstanceOfType(sm.ActiveState, typeof(Mocks.AutomatDrehkreuz.Auf.Context), "Auf Zustand hat den Typ Auf.Context wurde erwartet");

            // Typisierter zugriff auf den neuen Zustand. Sinvoll, wenn das Zustandsobjekt Anwendungsspezifische Informationen speichert.
            Assert.AreEqual("Tor ist auf", sm.GetActiveState<Mocks.AutomatDrehkreuz.Auf.Context>().Meldung);

        }
    }
}
