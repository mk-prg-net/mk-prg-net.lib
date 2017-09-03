//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.3.2016
//
//  Projekt.......: mko.BI
//  Name..........: IGetBoBuilder
//  Aufgabe/Fkt...: Liefert zu  einem Objekt aus einem Repository einen
//                  Builder, mit dem seine Eigenschaften überschrieben/verändert werden können,
//                  bzw. mit dem ein komplexer Objektgraph aufgebaut werden kann. Die Wurzel
//                  des Objektgraphen ist zuvor mittels ICreate.CreateBoAndAddToCollection(id)
//                  angelegt worden.
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 30.11.2016
//  Änderungen....: Co- und Contravarianz zugelassen
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
    public interface IGetBoBuilder<out TBoBuilder, in TBoId>
    {
        /// <summary>
        /// 25.7.2014, mko    
        /// Zugriff auf einzelnes Entity mit der gegebenen id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TBoBuilder GetBoBuilder(TBoId id);
    }
}
