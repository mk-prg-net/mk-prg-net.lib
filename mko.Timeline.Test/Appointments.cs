﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using mko.Timeline;

namespace mko.Timeline.Test
{
    [TestClass]
    public class Appointments
    {
        [TestMethod]
        public void Appointments_Test()
        {
            var now = DateTime.Now;

            var weihnachtenDef = new mko.Timeline.AppointmentBuilder();

            weihnachtenDef.Owner = "Familie";

            weihnachtenDef.Category = AppointmentCategory.@private;
            weihnachtenDef.BeginDate = new mko.Timeline.Date(now.Year + 1, 12, 24);
            weihnachtenDef.BeginTime = new mko.Timeline.Time(16, 0, 0);

            Assert.IsFalse(weihnachtenDef.IsValid);

            weihnachtenDef.EndDate = new mko.Timeline.Date(now.Year + 1, 12, 25);
            weihnachtenDef.EndTime = new mko.Timeline.Time(0, 0, 0);

            mko.Timeline.IAppointment w1 = weihnachtenDef.Create();

            Assert.AreEqual("DE", w1.Location.Country);
            Assert.IsTrue(w1.BeginDate.Equ(new mko.Timeline.Date(now.Year + 1, 12, 24)));

        }
    }
}
