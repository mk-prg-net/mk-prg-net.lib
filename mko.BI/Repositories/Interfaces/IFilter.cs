//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, 2011
//
//  Projekt.......: mko.BI
//  Name..........: IFilter.cs
//  Aufgabe/Fkt...: Entstanden aus der Basisklasse Basisklassen Filter<TEntity>.
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 20.4.2016
//  Änderungen....: In die Schnittstelle IFilter umgewandelt
//
//</unit_history>
//</unit_header>        


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Repositories.Interfaces
{
    public interface IFilter<TBo>
    {
        IQueryable<TBo> filter(IQueryable<TBo> BoList);


        Func<IQueryable<TBoInstance>, IQueryable<TBoInstance>> GetFilter<TBoInstance>() where TBoInstance: TBo;
    }
}
