using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace mkoIt.Xhtml
{
    public class HierachischeNummern
    {
        public static bool IstHierarchischeNummer(string kandidat)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(kandidat, @"^\d+(.\d+)*$");
        }

        public static int Level(string hierarchicheNummer)
        {
            int lv = hierarchicheNummer.Split('.').Length;
            if (lv == 0)
                return 1;
            else
                return lv;
        }

        /// <summary>
        /// Liefert den Nummernwert auf der Hierarchiestufe level zurück
        /// </summary>
        /// <param name="hierarchicheNummer"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int ValueAtLevel(string hierarchicheNummer, int level)
        {            
            string[] levels = hierarchicheNummer.Split('.');
            Debug.Assert(level < levels.Length && level >= 0);

            return int.Parse(levels[level]);
        }


    }
}
