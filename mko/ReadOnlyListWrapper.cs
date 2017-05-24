//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 25.1.2017
//
//  Projekt.......: mko
//  Name..........: ReadOnlyListWrapper.cs
//  Aufgabe/Fkt...: Wrapper, der aus les- und schreibbaren Lists
//                  nur lesbare Lists macht.
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

namespace mko
{
    public class ReadOnlyListWrapper<T, TReadOnly> : IReadOnlyList<TReadOnly>
        where T : TReadOnly
    {
        public ReadOnlyListWrapper(List<T> list) 
        {
            _list = list;
        }

        List<T> _list;

        public TReadOnly this[int index]
        {
            get { return _list[index]; }
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public IEnumerator<TReadOnly> GetEnumerator()
        {
            return _list.Select(r => (TReadOnly)r).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
