using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kepler;

namespace DB.Kepler.EF60
{
    /// <summary>
    /// Festlegen, das automatisch generierte Klasse aus edmx, Spektralklasse, die 
    /// Schnittstelle ISpektralklasse definiert
    /// </summary>
    partial class Spektralklasse : ISpektralklasse
    {

        public SpektralklasseID SpektralklasseId
        {
            get
            {
                return (SpektralklasseID)ID;
            }
            set
            {
                ID = (int)value;
                Name = value.ToString();
            }
        }

        Spektralklasse_Farbe ISpektralklasse.Farbe
        {
            get
            {
                return global::Kepler.Defs.Spektralklasse.ListeSpektralklassen[SpektralklasseId].Farbe;
            }
            set
            {
                Farbe = value.ToString().Replace('_', ' ');
            }
        }

        public string FarbeHtml
        {
            get
            {
                return global::Kepler.Defs.Spektralklasse.ListeSpektralklassen[SpektralklasseId].FarbeHtml;
            }

            set{
                throw new NotImplementedException();
            }
        }

    }
}
