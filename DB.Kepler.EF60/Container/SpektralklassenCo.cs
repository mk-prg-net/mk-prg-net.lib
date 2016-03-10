using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60.Container
{
    public class SpektralklassenCo : global::Kepler.SpektralklassenCo<Spektralklasse>, IDisposable
    {

        KeplerDBEntities ctx;
        bool ctxIntern = true;

        public SpektralklassenCo()
        {
            ctx = new KeplerDBEntities();
        }

        public SpektralklassenCo(KeplerDBEntities ctx)
        {
            ctxIntern = false;
            this.ctx = ctx;
        }

        public void Dispose()
        {
            if (ctxIntern)
            {
                ctx.Dispose();
            }
        }

        public override Spektralklasse GetSpektralklasse(global::Kepler.SpektralklasseID SKlasseID)
        {
            return BoCollection.Single(r => r.ID == (int)SKlasseID);
        }


        public override void AddToCollection(Spektralklasse entity)
        {
            ctx.SpektralklasseTab.Add(entity);
        }

        public override IQueryable<Spektralklasse> BoCollection
        {
            get {
                return ctx.SpektralklasseTab;
            }
        }

        public override Spektralklasse CreateBo()
        {
            return ctx.SpektralklasseTab.Create();
        }

        public override Spektralklasse CreateBoAndAddToCollection()
        {
            var spekt = ctx.SpektralklasseTab.Create();
            ctx.SpektralklasseTab.Add(spekt);
            return spekt;
        }


        public override Func<Spektralklasse, bool> GetBoIDTest(int id)
        {
            return r => r.ID == id;
        }

        public override void RemoveFromCollection(Spektralklasse entity)
        {
            ctx.SpektralklasseTab.Remove(entity);
        }

        public override void SubmitChanges()
        {
            ctx.SaveChanges();
        }

        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }


    }
}
