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
    public interface IGeographicalClusterScheme : IKeySet
    {
        IContinent NextContinent(string Name);        
    }

    public interface IContinent : IKeySet
    {
        ICountry NextCountry(string Name);
    }

    public interface ICountry : IKeySet
    {
        IRegion NextRegion(string Name);
    }

    public interface IRegion : IKeySet
    {
        IKeySet NextLocation(string Name);
    }

    

}
