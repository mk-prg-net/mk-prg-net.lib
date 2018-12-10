using System;

namespace MkPrgNet.Woc
{
    /// <summary>
    /// mko, 5.12.2018
    /// Struktur einer WocID.
    /// Ein Woc ist ein "Web Document Container". Er dient zur Identifizierung von Inhalten und dem
    /// organisieren der Verteilung dieser im Netz.
    /// Ein Inhalt, auf den ein Woc Wi verweist, gilt als unveränderlich. Dieser Zustand wird auch als 
    /// "versiegelt" bezeichnet.
    /// Wird ein Inhalt überarbeitet und als modifizierte Form im Netz bereitgestellt, dann kann auf 
    /// ihm wiederum mit einem Woc Wi+1 verwiesen werden. Wi+1 wird als "Thronfolger" von Wi in einer 
    /// Woc- Dynastie bezeichnet. Wi+1 muss dazu über einen speziellen Link auf seinen Vorgänger verweisen.
    /// </summary>
    public interface IWocId : IWocDynasty
    {
        /// <summary>
        /// Thronfolger
        /// Wocs der gleichen Dynastie (== alle Wocs mit der gleichen WocDynasty: NodeId, AuthorId und Dynasty)
        /// können bezüglich Successor geordnet werden und ergeben so die Dynastie.
        /// Es gilt die Regel: Auf einem Node kann zu einem bestimmten Zeitpunkt
        /// ein Autor nur genau ein Woc erzeugen (== sequentielle Arbeitsweise der Autoren)

        /// </summary>
        long Successor { get; }


    }
}
