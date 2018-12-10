using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Woc
{
    public interface IWoc : IWocId
    {
        /// <summary>
        /// Zeitpunkt, zu dem Woc erstellt wurde.
        /// </summary>
        DateTime Created { get; }


        // verweist auf eine synonyme Wocdynastie
        IWocDynasty Equal { get; }

        /// <summary>
        /// VErweist auf ein Woc, welches einen Oberbegriff zu diesem Woc darstellt
        /// </summary>
        IWocDynasty Abstract { get; }

        /// <summary>
        /// Liefert alle Wocs, die Detailbegriffe zum Woc darstellen
        /// </summary>
        IEnumerable<IWocDynasty> Details { get; }

        /// <summary>
        /// Verweist/steht symbolisch für einen Inhalt
        /// </summary>
        Guid ContentPointer { get; }
    }
}
