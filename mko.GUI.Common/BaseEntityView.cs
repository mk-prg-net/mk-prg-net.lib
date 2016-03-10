using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using mko.Algo.FunctionalProgramming;
using Defs = mko.GUI.Common.BaseEntityViewDefs;
using System.Diagnostics;


namespace mko.GUI.Common
{   
    public class BaseEntityView<TEntity> where TEntity : new()
    {

        /// <summary>
        /// Lokales Entity, das die darzustellenden und zu verarbeitenden Eigenschaften anbietet
        /// </summary>
        /// <remarks></remarks>
        public TEntity Entity;

        /// <summary>
        /// Bearbeitungszustand einer View
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public Defs.ViewState State { get; set; }

        protected BaseEntityView()
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            State = Defs.ViewState.Detached;

            Entity = new TEntity();
        }

        protected BaseEntityView(TEntity Entity)
        {
            // VBConversions Note: Non-static class variable initialization is below.  Class variables cannot be initially assigned non-static values in C#.
            State = Defs.ViewState.Detached;

            this.Entity = Entity;
        }


        /// <summary>
        /// Alle Aktualisierungen, die am Entity vorzunehen sind, werden hier zwischengespeichert
        /// </summary>
        /// <remarks></remarks>
        protected Queue<Action<TEntity>> QueuedJobsForPropertiesUpdate = new Queue<Action<TEntity>>();


        protected void SetProperty<TProp>(TProp Value, Action<TProp, TEntity> PropertySetter)
        {
            // View als Modifiziert kennzeichnen
            State = Defs.ViewState.Modified;

            // Ausgewählte Eigenschaft im lokalen Entity der View setzen
            PropertySetter(Value, Entity);

            // Aktualisierungsauftrag für externes Entity aufgeben
            Action<TEntity> job = PropertySetter.Curry(Value);
            QueuedJobsForPropertiesUpdate.Enqueue(job);
        }

        /// <summary>
        /// Alle auf am lokalen Entity durchgeführten Änderungen an einem externen Entity ebenfalls durchfürhen
        /// Das externe Entity kann z.B. aus Teil einer Datenbank sein.
        /// </summary>
        /// <param name="Entity">externes Entity</param>
        /// <remarks></remarks>
        public void UpdateExternalEntity(TEntity Entity)
        {

            foreach (var JobForPropertyUpdate in QueuedJobsForPropertiesUpdate)
            {
                // Aktualisierungen einer Eigenschaft auf dem externen Entity ausführen
                JobForPropertyUpdate(Entity);
            }

        }


    }
}
