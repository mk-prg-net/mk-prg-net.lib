using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60.Container
{
    public partial class HimmelskoerperCo : global::Kepler.HimmelskoerperCo<Himmelskoerper, int>
    {
        KeplerDBEntities ctx;
        bool ctxIntern = true;

        public HimmelskoerperCo()
        {
            ctx = new KeplerDBEntities();
            InitFlt();
        }

        public HimmelskoerperCo(KeplerDBEntities ctx)
        {
            ctxIntern = false;
            this.ctx = ctx;
            InitFlt();
        }


        void InitFlt()
        {
            TypFlt = new TypFltCtrl(this);
            MasseFlt = new MasseFltCtrl(this);
        }

        public void Dispose()
        {
            if (ctxIntern)
            {
                ctx.Dispose();
            }
        }

        public override void AddToCollection(EF60.Himmelskoerper entity)
        {
            throw new NotImplementedException();
        }

        public override void RemoveFromCollection(EF60.Himmelskoerper entity)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<EF60.Himmelskoerper> BoCollection
        {
            get
            {
                return ctx.HimmelskoerperTab;
            }
        }

        public override Himmelskoerper CreateBo()
        {
            throw new NotImplementedException();
        }

        public override Himmelskoerper CreateBoAndAddToCollection()
        {
            throw new NotImplementedException();
        }

        public override Func<Himmelskoerper, bool> GetBoIDTest(int id)
        {
            return r => r.ID == id;
        }

        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public override void SubmitChanges()
        {
            throw new NotImplementedException();
        }
    }
}
