using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60.Container
{
    partial class HimmelskoerperCo
    {
        /// <summary>
        /// Filtert nach einem bestimmten Himmelskörpertyp
        /// </summary>
        public class _TypeFlt : mko.BI.Repositories.FilterFunctor<Himmelskoerper, global::Kepler.Bo.HimmelskoerperTypen>
        {

            public override IQueryable<Himmelskoerper> filterImpl(IQueryable<Himmelskoerper> srcTab)
            {
                return srcTab.Where(r => r.HimmelskoerperTyp_ID == (int)RValue);
            }
        }

        /// <summary>
        /// Mittels des FilterControllers wird das Filter ein/ und ausgeschaltet
        /// </summary>
        public class TypFltCtrl : mko.BI.Repositories.FilterController<_TypeFlt, Himmelskoerper, int, global::Kepler.Bo.HimmelskoerperTypen, HimmelskoerperCo>
        {
            public TypFltCtrl(HimmelskoerperCo boCo) : base(boCo, global::Kepler.Bo.HimmelskoerperTypen.Planet) { }

            protected override _TypeFlt CreateFilter(global::Kepler.Bo.HimmelskoerperTypen RValue)
            {
                return new _TypeFlt() { RValue = RValue, Description = "Typ == " + RValue.ToString() };
            }
        }

        /// <summary>
        /// Filtern nach Typ des Himmeslkörpers
        /// </summary>
        public TypFltCtrl TypFlt;


        /// <summary>
        /// Schränkt auf Himmelskörper ein, deren Masse in einem Bereich [begin, end] liegt
        /// </summary>
        public class _MasseFlt : mko.BI.Repositories.FilterFunctor<Himmelskoerper, mko.BI.Bo.Interval<double>>
        {
            public override IQueryable<Himmelskoerper> filterImpl(IQueryable<Himmelskoerper> srcTab)
            {
                return srcTab.Where(r => r.Masse_in_kg >= RValue.Begin && r.Masse_in_kg <= RValue.End);
            }
        }


        public class MasseFltCtrl : mko.BI.Repositories.FilterController<_MasseFlt, Himmelskoerper, int, mko.BI.Bo.Interval<double>, HimmelskoerperCo>
        {
            public MasseFltCtrl(HimmelskoerperCo boCo) : base(boCo, new mko.BI.Bo.Interval<double>(0, double.MaxValue)) { }

            protected override _MasseFlt CreateFilter(mko.BI.Bo.Interval<double> RValue)
            {
                return new _MasseFlt() { RValue = RValue, Description = "" + RValue.Begin + " <= Masse <= " + RValue.End };
            }
        }

        /// <summary>
        /// Filtern nach Himmelskörpern, deren Masse in einem Bereich liegt
        /// </summary>
        public MasseFltCtrl MasseFlt;



    }
}
