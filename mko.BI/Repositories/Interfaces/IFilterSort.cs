using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Repositories.Interfaces
{
    public interface IFilterSort<TBo>
        where TBo : class
    {
        void DefSortOrders(params DefSortOrder<TBo>[] DefSortOrder);


        //------------------------------------------------------------------------------------------
        // Filtern

        /// <summary>
        /// Liefert true, wenn das Filter Filter auf die Menge angewendet wird
        /// </summary>
        /// <param name="srcFilter"></param>
        /// <returns></returns>
        bool IsFilteredWith(Filter<TBo> Filter);


        /// <summary>
        /// Zuweisen eines Filter zur Liste der Filter
        /// </summary>
        /// <param name="srcFilter"></param>
        void SetFilter(Filter<TBo> srcFilter);


        /// <summary>
        /// Entfernt ein Filter aus dem Bo
        /// </summary>
        /// <param name="flt"></param>
        void RemoveFilter(Filter<TBo> flt);

        void RemoveAllFilters();

        /// <summary>
        /// Beschreibung der Filter zurückgeben
        /// </summary>
        /// <returns></returns>
        string FilterDescription();

        /// <summary>
        /// Hier können vom Anwender Anmerkungen hinzugefügt werden. Diese werden 
        /// im EntitySetDescriptor aufgenommen
        /// </summary>
        /// 

        string UserAnnotations
        {
            get;
            set;
        }


        /// <summary>
        /// Gibt zusätzliche Informationen über die aktuell durch Filter definierte 
        /// Menge von Geschäftsobjekten zurück. Dazu gehören z.B. die Anzahl der Datensätze,
        /// Anmerkungen vom Benutzer und eine informelle Beschreibung der Filter.
        /// Diese Informationen können z.B. in Berichten im Kopf ausgegeben werden.
        /// </summary>
        /// <returns></returns>
        BoSetDescriptorEntry[] GetBoSetDescriptor();

        /// <summary>
        /// Muss in abgeleiteten Klassen mit der gewünschten Funktionalität zur Konstruktion eines Filters aus einer Auswahl
        /// und der Filterung überschrieben werden
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        IQueryable<TBo> FilterBoCollection(IQueryable<TBo> tab);

        /// <summary>
        /// Erzeugt ein Filter, welches nur Datensätze passieren lässt mit der übergebenen Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        //public abstract Db.FilterFunctor<TEntity, TEntityId> CreateIdFilter(TEntityId Id);

        //----------------------------------------------------------------------------------------------------------------
        // Existenztests

        /// <summary>
        /// Prüft, ob überhautp Elemente vorhanden sind
        /// </summary>
        /// <returns></returns>
        bool Any();

        /// <summary>
        /// Prüft, ob die gefilterte Menge überhaupt ein Element enthält
        /// </summary>
        /// <returns></returns>
        bool IsFilterdListNotEmpty();

        //----------------------------------------------------------------------------------------------------------------
        // Teilmengen von Entities oder EntityViews berechnen

        /// <summary>
        /// Zugriff auf Liste aller Entities
        /// </summary>
        /// <returns></returns>
        IQueryable<TBo> GetAllBo();

        /// <summary>
        /// Wendet auf die Menge der Entities alle definierten Filter an. Das Ergebnis ist die 
        /// gefilterte Menge
        /// </summary>
        /// <returns></returns>
        IQueryable<TBo> GetFilteredListOfBo();

        /// <summary>
        /// Wendet auf die Menge der Entites alle definierten Filter an. Das Ergebnis ist die 
        /// gefilterte Menge. Diese wird anschließend sortiert
        /// </summary>
        /// <returns></returns>
        IQueryable<TBo> GetFilteredAndSortedListOfBo();

        /// <summary>
        /// Zählt alle Entities nach der Filterung durch.
        /// </summary>
        /// <param name="sortType"></param>
        /// <returns></returns>
        long CountFilteredBo();

        /// <summary>
        /// Zählt alle Entities durch
        /// </summary>
        /// <returns></returns>
        long CountAllBo();

    }
}
