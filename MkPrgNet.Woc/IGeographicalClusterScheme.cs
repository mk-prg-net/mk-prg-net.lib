using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Woc
{
    /// <summary>
    /// mko, 10.12.2018
    /// Z.B. wird die Menge der NodeIds als 64 Bit Zahlen dargestellt, 
    /// welche hierarchisch in Intervalle aufgeteilt werden z.B. auf oberster Ebene
    /// in Kontinente (z.B. 16 = 7 Reale und 9 hypothetische => 2^64 / 2^4 = 2^60
    /// ID's pro Kontinent.
    /// Jeder Kontinent ist ein Interval von z.B. [0, 2^60-1], [2^60, 2^61], ...
    /// Ein Kontinent wird auf Länder aufgeteilt usw..
    /// </summary>
    public interface IGeographicalClusterScheme
    {
        ICluster L0_Continents(Math.Sets.Interval<long> ival);

        ICluster L1_Countrys(Math.Sets.Interval<long> ival);

        ICluster L2_Regions(Math.Sets.Interval<long> ival);

        ICluster L3_Locations(Math.Sets.Interval<long> ival);
        
    }
}
