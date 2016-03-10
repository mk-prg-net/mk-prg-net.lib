//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 23.11.2015
//
//  Projekt.......: DB.Kepler.EF60
//  Name..........: UmlaufbahnenCo.cs
//  Aufgabe/Fkt...: Container mit Umlaufbahnen- Geschäftsobjekten
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
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
using System.Threading.Tasks;

namespace DB.Kepler.EF60.Container
{
    /// <summary>
    /// Container mit Planeten- Geschäftsobjekten
    /// </summary>
    public class UmlaufbahnenCo : global::Kepler.UmlaufbahnenCo<EF60.Umlaufbahn, int>, IDisposable
    {
        KeplerDBEntities ctx;
        bool ctxIntern = true;

        public UmlaufbahnenCo()
        {
            ctx = new KeplerDBEntities();
        }

        public UmlaufbahnenCo(KeplerDBEntities ctx)
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

        public override void AddToCollection(Umlaufbahn entity)
        {
            ctx.UmlaufbahnenTab.Add(entity);
        }

        public override IQueryable<Umlaufbahn> BoCollection
        {
            get { return ctx.UmlaufbahnenTab; }
        }

        public override Umlaufbahn CreateBo()
        {
            var ub = ctx.UmlaufbahnenTab.Create();
            return ub;
        }

        public override Umlaufbahn CreateBoAndAddToCollection()
        {
            var ub = ctx.UmlaufbahnenTab.Create();
            ctx.UmlaufbahnenTab.Add(ub);
            return ub;
        }


        public override Func<Umlaufbahn, bool> GetBoIDTest(int id)
        {
            return r => r.TrabantID == id;
        }

        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public override void RemoveFromCollection(Umlaufbahn entity)
        {
            ctx.UmlaufbahnenTab.Remove(entity);
        }

        public override void SubmitChanges()
        {
            ctx.SaveChanges();
        }

    }
}
