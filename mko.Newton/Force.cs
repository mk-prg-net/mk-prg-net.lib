using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E = mko.Euklid;
using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    /// <summary>
    /// Gemessene Kraft, die an einem Punkt in einem System angreift
    /// </summary>
    public abstract partial class Force : MeasuredVector
    {
        public class ForceException : Exception
        {
            public ForceException() { }
            public ForceException(string Message) : base(Message) { }
            public ForceException(string Message, Exception InnerException) { }
        }

        public Force(int Dimension) : base(Dimension) { }
        public Force(params double[] coordinates) : base(coordinates) { }
        public Force(E.Vector V) : base(V.coordinates) {}

        public Force(Force F)
        {
            CreateVector((ConvertInA(F.A).Vector * ConvertInM(F.M).Value).coordinates);
        }       


        public virtual Mag.OrderOfMagnitudeEnum OrderOfMagnitude
        {
            get { return Mag.OrderOfMagnitudeEnum.One; }
        } 


        

        public override string SiBaseUnitId
        {
            get { return "kg.m.s-2"; }
        }

        public override string SiBaseUnitDefinition
        {
            get { return "Kraft, wie sie durch Newtons Grundgesetz F = m*a definiert ist."; }
        }


        public override E.Vector Vector
        {
            get
            {
                return A.Vector;
            }
            set
            {
                A.Vector = value;
            }
        }

        public abstract Mass M
        {
            get;            
        }

        public abstract Mass ConvertInM(Mass M);

        public abstract Acceleration A
        {
            get;            
        }

        public abstract Acceleration ConvertInA(Acceleration A);       


    }
}
