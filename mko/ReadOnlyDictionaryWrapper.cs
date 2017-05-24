//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 25.1.2017
//
//  Projekt.......: mko
//  Name..........: ReadOnlyDictionaryWrapper.cs
//  Aufgabe/Fkt...: Wrapper, der aus les- und schreibbaren Dictionaries
//                  nur lesbare Dictionaries macht.
//                  Q: http://stackoverflow.com/questions/13593900/how-to-get-around-lack-of-covariance-with-ireadonlydictionary    
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
    public class ReadOnlyDictionaryWrapper<TKey, TValue, TReadOnlyValue> : IReadOnlyDictionary<TKey, TReadOnlyValue> where TValue : TReadOnlyValue
    {
        private IDictionary<TKey, TValue> _dictionary;

        public ReadOnlyDictionaryWrapper(IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            _dictionary = dictionary;
        }
        public bool ContainsKey(TKey key) { return _dictionary.ContainsKey(key); }

        public IEnumerable<TKey> Keys { get { return _dictionary.Keys; } }

        public bool TryGetValue(TKey key, out TReadOnlyValue value)
        {
            TValue v;
            var result = _dictionary.TryGetValue(key, out v);
            value = v;
            return result;
        }

        public IEnumerable<TReadOnlyValue> Values { get { return _dictionary.Values.Cast<TReadOnlyValue>(); } }

        public TReadOnlyValue this[TKey key] { get { return _dictionary[key]; } }

        public int Count { get { return _dictionary.Count; } }

        public IEnumerator<KeyValuePair<TKey, TReadOnlyValue>> GetEnumerator()
        {
            return _dictionary
                        .Select(x => new KeyValuePair<TKey, TReadOnlyValue>(x.Key, x.Value))
                        .GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
