using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using E= mko.Euklid;
using Mag = mko.Newton.OrderOfMagnitude;

namespace mko.Newton
{
    public static class OrderOfMagnitude
    {
        public enum OrderOfMagnitudeEnum
        {
            Atto,
            Femto,
            Pico,
            Nano,
            Micro,
            Milli,
            Centi,
            Deci,
            One,
            Deca,
            Hecto,
            Kilo,
            Mega,
            Giga,
            Terra,
            Peta,
            Exa
        }

        public static Dictionary<OrderOfMagnitudeEnum, double> OrderOfMagnitudeFactor;

        public static Dictionary<OrderOfMagnitudeEnum, string> OrderOfMagnitudeId = new Dictionary<OrderOfMagnitudeEnum, string>()
        {
            {OrderOfMagnitudeEnum.Atto, "a"},
            {OrderOfMagnitudeEnum.Centi, "c"},
            {OrderOfMagnitudeEnum.Deca, "da"},
            {OrderOfMagnitudeEnum.Deci, "d"},
            {OrderOfMagnitudeEnum.Exa, "E"},
            {OrderOfMagnitudeEnum.Femto, "f"},
            {OrderOfMagnitudeEnum.Giga, "G"},
            {OrderOfMagnitudeEnum.Hecto, "h"},
            {OrderOfMagnitudeEnum.Kilo, "K"},
            {OrderOfMagnitudeEnum.Mega, "M"},
            {OrderOfMagnitudeEnum.Micro, "µ"},
            {OrderOfMagnitudeEnum.Milli, "m"},
            {OrderOfMagnitudeEnum.Nano, "n"},
            {OrderOfMagnitudeEnum.One, ""},
            {OrderOfMagnitudeEnum.Peta, "P"},
            {OrderOfMagnitudeEnum.Pico, "p"},
            {OrderOfMagnitudeEnum.Terra, "T"}
        };

        /// <summary>
        /// Statischer Konstruktor der Magnitude Klasse. Baut die Magnitude- Factor- Dictionary auf
        /// </summary>
        static OrderOfMagnitude()
        {
            OrderOfMagnitudeFactor = new Dictionary<OrderOfMagnitudeEnum, double>();
            //double MagnitudeValue = 1e-18;
            //mko.algorithm.ForEachEnumMember<OrderOfMagnitudeEnum>.execute((name, e) => { OrderOfMagnitudeFactor[(OrderOfMagnitudeEnum)e] = MagnitudeValue; MagnitudeValue *= 10.0; });            
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Atto] = 1e-18;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Femto] = 1e-15;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Pico] = 1e-12;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Nano] = 1e-9;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Micro] = 1e-6;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Milli] = 1e-3;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Centi] = 1e-2;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Deci] = 1e-1;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.One] = 1.0;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Deca] = 10.0;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Hecto] = 100.0;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Kilo] = 1000.0;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Mega] = 1e6;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Giga] = 1e9;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Terra] = 1e12;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Peta] = 1e15;
            OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Exa] = 1e18;
        }

        public static void Init() {            
            new Atto();
            new Femto();
            new Pico();
            new Nano();
            new Micro();
            new Milli();
            new Centi();
            new Deci();
            new One();
            new Deci();
            new Hecto();
            new Kilo();
            new Mega();
            new Giga();
            new Terra();
            new Peta();
        }


        public static double ToOne(double Value, OrderOfMagnitudeEnum FromOofM)
        {
            return Value * OrderOfMagnitudeFactor[FromOofM];
        }

        public static E.Vector ToOne(E.Vector Vector, OrderOfMagnitudeEnum FromOofM)
        {
            double f = OrderOfMagnitudeFactor[FromOofM];
            return new E.Vector(Vector.coordinates.Select(c => c * f).ToArray());
        }

        public static double FromTo(double Value, OrderOfMagnitudeEnum From, OrderOfMagnitudeEnum To)
        {
            return Value * OrderOfMagnitudeFactor[From] / OrderOfMagnitudeFactor[To];
        }

        public static E.Vector FromTo(E.Vector Vector, OrderOfMagnitudeEnum From, OrderOfMagnitudeEnum To)
        {
            double f = OrderOfMagnitudeFactor[From] / OrderOfMagnitudeFactor[To];
            return new E.Vector(Vector.coordinates.Select(c => c * f).ToArray());
        }



        /// <summary>
        /// Atto
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToAtto(double value)
        {
            return value / AttoFactor;
        }

        public static double AttoFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Atto];
            }
        }

        public static double FromAtto(double value)
        {
            return value * AttoFactor;
        }

        public class Atto : OrderOfMagnitudeBase
        {
            static Atto()
            {
                OrderOfMagnitudeBase.Instance[typeof(Atto)] = new Atto();                
            }
         
            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Atto; }
            }           
        }

        /// <summary>
        /// Femto
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToFemto(double value)
        {
            return value /FemtoFactor;
        }

        public static double FemtoFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Femto];
            }
        }
        public static double FromFemto(double value)
        {
            return value * FemtoFactor;
        }

        public class Femto : OrderOfMagnitudeBase
        {
            static Femto()
            {
                OrderOfMagnitudeBase.Instance[typeof(Femto)] = new Femto();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Femto; }
            }
        }


        /// <summary>
        /// Pico
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToPico(double value)
        {
            return value / PicoFactor;
        }

        public static double PicoFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Pico];
            }
        }

        public static double FromPico(double value)
        {
            return value * PicoFactor;
        }

        public class Pico : OrderOfMagnitudeBase
        {
            static Pico()
            {
                OrderOfMagnitudeBase.Instance[typeof(Pico)] = new Pico();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Pico; }
            }
        }


        /// <summary>
        /// Nano
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToNano(double value)
        {
            return value / NanoFactor;
        }

        public static double NanoFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Nano];
            }
        }

        public static double FromNano(double value)
        {
            return value * NanoFactor;
        }

        public class Nano : OrderOfMagnitudeBase
        {
            static Nano()
            {
                OrderOfMagnitudeBase.Instance[typeof(Nano)] = new Nano();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Nano; }
            }
        }


        /// <summary>
        /// Micro
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToMicro(double value)
        {
            return value / MicroFactor;
        }

        public static double MicroFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Micro];
            }
        }

        public static double FromMicro(double value)
        {
            return value * MicroFactor;
        }

        public class Micro : OrderOfMagnitudeBase
        {
            static Micro()
            {
                OrderOfMagnitudeBase.Instance[typeof(Micro)] = new Micro();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Micro; }
            }
        }


        /// <summary>
        /// Milli
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToMilli(double value)
        {
            return value / MilliFactor;
        }

        public static double MilliFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Milli];
            }
        }

        public static double FromMilli(double value)
        {
            return value * MilliFactor;
        }

        public class Milli : OrderOfMagnitudeBase
        {
            static Milli()
            {
                OrderOfMagnitudeBase.Instance[typeof(Milli)] = new Milli();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Milli; }
            }
        }


        /// <summary>
        /// Centi
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToCenti(double value){
            return value / CentiFactor;
        }

        public static double CentiFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Centi];
            }
        }

        public static double FromCenti(double value)
        {
            return value * CentiFactor;
        }

        public class Centi : OrderOfMagnitudeBase
        {
            static Centi()
            {
                OrderOfMagnitudeBase.Instance[typeof(Centi)] = new Centi();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Centi; }
            }
        }

        /// <summary>
        /// deci
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDeci(double value)
        {
            return value / DeciFactor;
        }

        public static double DeciFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Deci];
            }
        }

        public static double FromDeci(double value)
        {
            return value * DeciFactor;
        }

        public class Deci : OrderOfMagnitudeBase
        {
            static Deci()
            {
                OrderOfMagnitudeBase.Instance[typeof(Deci)] = new Deci();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Deci; }
            }
        }


        public static double OneFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.One];
            }
        }

        public class One : OrderOfMagnitudeBase
        {
            static One()
            {
                OrderOfMagnitudeBase.Instance[typeof(One)] = new One();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.One; }
            }
        }



        /// <summary>
        /// Deca
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDeca(double value)
        {
            return value / DecaFactor;
        }

        public static double DecaFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Deca];
            }
        }

        public static double FromDeca(double value)
        {
            return value * DecaFactor;
        }

        public class Deca : OrderOfMagnitudeBase
        {
            static Deca()
            {
                OrderOfMagnitudeBase.Instance[typeof(Deca)] = new Deca();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Deca; }
            }
        }


        /// <summary>
        /// Hecto
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToHecto(double value)
        {
            return value / HectoFactor;
        }

        public static double HectoFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Hecto];
            }
        }

        public static double FromHecto(double value)
        {
            return value * HectoFactor;
        }

        public class Hecto : OrderOfMagnitudeBase
        {
            static Hecto()
            {
                OrderOfMagnitudeBase.Instance[typeof(Hecto)] = new Hecto();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Hecto; }
            }
        }


        /// <summary>
        /// Kilo
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToKilo(double value)
        {
            return value / KiloFactor;
        }

        public static double KiloFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Kilo];
            }
        }

        public static double FromKilo(double value)
        {
            return value * KiloFactor;
        }

        public class Kilo : OrderOfMagnitudeBase
        {
            static Kilo()
            {
                OrderOfMagnitudeBase.Instance[typeof(Kilo)] = new Kilo();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Kilo; }
            }
        }


        /// <summary>
        /// Mega
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToMega(double value)
        {
            return value / MegaFactor;
        }

        public static double MegaFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Mega];
            }
        }

        public static double FromMega(double value)
        {
            return value * MegaFactor;
        }

        public class Mega : OrderOfMagnitudeBase
        {
            static Mega()
            {
                OrderOfMagnitudeBase.Instance[typeof(Mega)] = new Mega();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Mega; }
            }
        }


        /// <summary>
        /// Giga
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToGiga(double value)
        {
            return value / GigaFactor;
        }

        public static double GigaFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Giga];
            }
        }

        public static double FromGiga(double value)
        {
            return value * GigaFactor;
        }

        public class Giga : OrderOfMagnitudeBase
        {
            static Giga()
            {
                OrderOfMagnitudeBase.Instance[typeof(Giga)] = new Giga();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Giga; }
            }
        }


        /// <summary>
        /// Terra
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToTerra(double value)
        {
            return value / TerraFactor;
        }

        public static double TerraFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Terra];
            }
        }

        public static double FromTerra(double value)
        {
            return value * TerraFactor;
        }

        public class Terra : OrderOfMagnitudeBase
        {
            static Terra()
            {
                OrderOfMagnitudeBase.Instance[typeof(Terra)] = new Terra();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Terra; }
            }
        }


        /// <summary>
        /// Peta
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToPeta(double value)
        {
            return value / PetaFactor;
        }

        public static double PetaFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Peta];
            }
        }

        public static double FromPeta(double value)
        {
            return value * PetaFactor;
        }

        public class Peta : OrderOfMagnitudeBase
        {
            static Peta()
            {
                OrderOfMagnitudeBase.Instance[typeof(Peta)] = new Peta();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Peta; }
            }
        }


        /// <summary>
        /// Terra
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToExa(double value)
        {
            return value / ExaFactor;
        }

        public static double ExaFactor
        {
            get
            {
                return OrderOfMagnitudeFactor[OrderOfMagnitudeEnum.Exa];
            }
        }

        public static double FromExa(double value)
        {
            return value * ExaFactor;
        }

        public class Exa : OrderOfMagnitudeBase
        {
            static Exa()
            {
                OrderOfMagnitudeBase.Instance[typeof(Peta)] = new Exa();
            }

            public override OrderOfMagnitudeEnum OrderOfMagnitude
            {
                get { return OrderOfMagnitudeEnum.Exa; }
            }
        }



    }
}
