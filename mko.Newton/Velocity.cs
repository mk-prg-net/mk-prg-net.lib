//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 04.12.2012
//
//  Projekt.......: mko.Newton
//  Name..........: Velocity.cs
//  Aufgabe/Fkt...: Abbildung der Geschwindigkeitswerte auf Tupel
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.0
//  Werkzeuge.....: Visual Studio 2010
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;
using Mag= mko.Newton.OrderOfMagnitude;
using System.Diagnostics;

namespace mko.Newton
{
    /// <summary>
    /// Messwerte der Geschwindigkeit
    /// </summary>
    public abstract partial class Velocity : MeasuredVector
    {
        public class VelocityException : Exception
        {
            public VelocityException(string msg) : base(msg) { }
            public VelocityException(string msg, Exception InnerException) : base(msg, InnerException) { }
        }
        
        public Velocity() { }
        public Velocity(int Dimension) : base(Dimension) { }
        public Velocity(params double[] coordinates) : base(coordinates) {}
        public Velocity(E.Vector V) : base(V.coordinates) { }

        public Velocity(Length S, Time t)
        {
            CreateVector((ConvertInS(S).Vector * (1 / ConvertInT(t).Value)).coordinates);
        }

        public Velocity(Velocity V)
        {            
            CreateVector((ConvertInS(V.S).Vector * (1 / ConvertInT(V.T).Value)).coordinates);
        }


        //public abstract Velocity Create(Length S, Time T);

        public override string UnitSymbol {
            get
            {
                return S.UnitSymbol + "." + T.UnitSymbol + "-1";
            }
        }

        public override string SiBaseUnitId
        {
            get { return "m.s-1"; }
        }

        public override string SiBaseUnitDefinition
        {
            get { return "Weg in Metern, der pro Sekunde zurückgelegt wird"; }
        }


        /// <summary>
        /// Zugriff auf den Weganteil eines Geschwindigkeitstupels
        /// </summary>
        /// <typeparam name="TLength"></typeparam>
        /// <typeparam name="TTime"></typeparam>
        /// <param name="V"></param>
        /// <returns></returns>       

        public abstract Length S
        {
            get;
        }

        public abstract Length ConvertInS(Length S);

        public override E.Vector Vector
        {
            get
            {
                return S.Vector;
            }
            set
            {
                S.Vector = value;
            }
        }


        /// <summary>
        /// Zugriff auf den Zeiteinheit eines Geschwindigkeitstupels
        /// </summary>
        /// <typeparam name="TLength"></typeparam>
        /// <typeparam name="TTime"></typeparam>
        /// <param name="V"></param>
        /// <returns></returns>       

        public abstract Time T
        {
            get;
        }

        public abstract Time ConvertInT(Time t);


    }
}
