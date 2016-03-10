using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.BI.StateMachine
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var sm = new mko.BI.TestMockUps.AutomatDrehkreuz.Controller();

            sm.ActiveState = sm.StateFactory.CreateZu();

            var zustand = sm.StateFactory.CreateZu();

            var newState = zustand.Transition(new TestMockUps.AutomatDrehkreuz.Zu.Input() { Tag = TestMockUps.AutomatDrehkreuz.Zu.Input.Tags.tueNix });
            Assert.AreEqual("Zu", newState.Name, "Zu wurde erwartet");
            Assert.IsInstanceOfType(newState, typeof(mko.BI.TestMockUps.AutomatDrehkreuz.Zu.Context), "Zu Zustand erwartet");

            var newState2 = zustand.Transition(new TestMockUps.AutomatDrehkreuz.Zu.Input() { Tag = TestMockUps.AutomatDrehkreuz.Zu.Input.Tags.EinEuro });

            Assert.AreEqual("Auf", newState2.Name, "Zu wurde erwartet");
            Assert.IsInstanceOfType(newState, typeof(mko.BI.TestMockUps.AutomatDrehkreuz.Zu.Context), "Zu Zustand erwartet");
            

        }
    }
}
