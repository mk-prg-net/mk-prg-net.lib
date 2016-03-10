using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using mko.Newton;
using Mag = mko.Newton.OrderOfMagnitude;
using System.Diagnostics;


namespace mko.Ballistik
{
    public class Flugbahn
    {
        /// <summary>
        /// Berechnet die Flugbahn eines Flugkörpers nach Newton
        /// </summary>
        /// <param name="v0">Startgeschwindigkeit</param>
        /// <param name="Auftrieb">Auftriebskraft senkrecht zur Flugrichtung</param>
        /// <param name="Widerstand">Widerstandskraft entgegen der Flugrichtung, bedingt durch den Luftwiderstand</param>
        /// <param name="Fallbeschleunigung">Fallbeschleunigung</param>
        /// <param name="dt">Zeitdifferenz zwischen den Einzelmessungen</param>
        /// <param name="Flugzeit">gesamte betrachtete Flugzeit</param>
        /// <returns></returns>
        public static List<Length> berechne2D(
            Velocity v0,
            Mass MasseFlugkörper,
            Func<Velocity, Force> Auftrieb,
            Func<Velocity, Force> Widerstand,
            Acceleration Fallbeschleunigung,
            Time dt,
            Time Flugzeit)
        {

            Debug.Assert(v0.Dimension == 2, "Die Funktion Flugbahn2D ist auf zweidimensionale Vektoren beschränkt. Parameter v0 weicht davon ab !");
            Debug.Assert(Fallbeschleunigung.Dimension == 2, "Die Funktion Flugbahn2D ist auf zweidimensionale Vektoren beschränkt. Parameter fallbeschleunigung weicht davon ab !");

            var vi = Velocity.MeterPerSec(v0);

            int stepCount = (int)Math.Ceiling((Flugzeit.ValueInBaseUnit / dt.ValueInBaseUnit));

            var pi = Length.Meter(0, 0);

            var bahn = new List<Length>(stepCount);

            // Newtonsches Näherungsverfahren für die Flugbahn
            for (int i = 0; i < stepCount; i++)
            {
                // nach F = m*a bestimmt sich aAuftrieb = Fauftrieb/mFlugkörper. vAuftrieb wird durch integration über t ermittelt
                var vAuftrieb = Velocity.MeterPerSec(Acceleration.MeterPerSec2(Auftrieb(vi), MasseFlugkörper), dt);
                var vWiderstand = Velocity.MeterPerSec(Acceleration.MeterPerSec2(Widerstand(vi), MasseFlugkörper), dt);

                vi = vi + Velocity.MeterPerSec(Fallbeschleunigung, Time.SCALE(i, dt));

                vi = vi + Velocity.MeterPerSec(vAuftrieb);
                vi = vi + vWiderstand;

                // Integration der Wege
                pi = pi + Length.Meter(vi, dt);

                // Bahn aufzeichnen
                bahn.Add(pi);
            }

            return bahn;
        }

    }
}
