using System;
using System.Collections.Generic;
using System.Text;

namespace MkPrgNet.Woc
{
    /// <summary>
    /// mko, 5.12.2018
    /// Definiert eine Menge von Dokumenten, die in einer historischen Beziehung stehen.
    /// Dabei  kann jedes auf null oder ein früher erstelltes verweise. Die historische Reihe von 
    /// Dokumenten wird Dynastie genannt.
    /// Ein Woc ist ein "Web Document Container". Er dient zur Identifizierung von Inhalten und dem
    /// organisieren der Verteilung dieser im Netz.
    /// Ein Inhalt, auf den ein Woc Wi verweist, gilt als unveränderlich. Dieser Zustand wird auch als 
    /// "versiegelt" bezeichnet.
    /// Wird ein Inhalt überarbeitet und als modifizierte Form im Netz bereitgestellt, dann kann auf 
    /// ihm wiederum mit einem Woc Wi+1 verwiesen werden. Wi+1 wird als "Thronfolger" von Wi in einer 
    /// Woc- Dynastie bezeichnet. Wi+1 muss dazu über einen speziellen Link auf seinen Vorgänger verweisen.
    /// </summary>
    public interface IWocDynasty
    {
        string NodeId { get; }

        string AuthorId { get; }

        /// <summary>
        /// Eindeutige Bezeichnung der Dynastie bezüglich eines Node und Autors.
        /// Die Dynastie ist vergleichbar mit einem eindeutigen Titel, der die Inhalte beschreibt,
        /// auf den die Wocs dieser Dynastie verweisen.
        /// </summary>
        string Dynasty { get; }

    }
}
