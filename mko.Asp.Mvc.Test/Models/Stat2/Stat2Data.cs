using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mko.Asp.Mvc.Test.Models
{
    /// <summary>
    /// Nimmt statistik- Daten auf
    /// </summary>
    public class Stat2Data
    {

        /// <summary>
        /// Speichert alle erfassten Messwerte
        /// </summary>
        public IEnumerable<double> Data
        {
            get
            {
                return _Data;
            }

            set
            {
                // Alte Listeneinträge löschen
                _Data.Clear();
                _Data.AddRange(value);
            }
        }

        List<double> _Data = new List<double>();        

    }
}