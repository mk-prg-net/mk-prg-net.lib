using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using E = mko.Euklid;

namespace mko.Newton
{
    /// <summary>
    /// Messwerte der Beschleunigung
    /// </summary>
    public abstract partial class Acceleration : MeasuredVector
    {

        public Acceleration(int Dimension) : base(Dimension) { }
        public Acceleration(params double[] coordinates) : base(coordinates) { }
        public Acceleration(E.Vector V) : base(V.coordinates) { }
        public Acceleration(Acceleration A)
        {
            CreateVector((ConvertInV(A.V).Vector * (1 / ConvertInT(A.T).Value)).coordinates);
        }

        public override string UnitSymbol
        {
            get
            {
                return V.S.UnitSymbol + "." + T.UnitSymbol + "-2";
            }
        }

        public override string SiBaseUnitId
        {
            get { return "m.s-2"; }
        }

        public override string SiBaseUnitDefinition
        {
            get { return "Beschleunigung: Veränderung der Geschwindigkeit pro Zeiteinheit"; }
        }


        public override E.Vector Vector
        {
            get
            {
                return V.Vector;
            }
            set
            {
                V.Vector = value;
            }
        }

        /// <summary>
        /// Zugriff auf den Weganteil eines Geschwindigkeitstupels
        /// </summary>
        /// <typeparam name="TLength"></typeparam>
        /// <typeparam name="TTime"></typeparam>
        /// <param name="A"></param>
        /// <returns></returns>

        public abstract Velocity V
        {
            get;
            
        }

        public abstract Velocity ConvertInV(Velocity V);

        /// <summary>
        /// Zugriff auf den Zeitanteil eines Geschwindigkeitstupels
        /// </summary>
        /// <typeparam name="TLength"></typeparam>
        /// <typeparam name="TTime"></typeparam>
        /// <param name="A"></param>
        /// <returns></returns>

        public abstract Time T
        {
            get;            
        }

        public abstract Time ConvertInT(Time T);


    }

}
