//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 4.10.2012
//
//  Projekt.......: mkoIt.Db
//  Name..........: BoBaseView
//  Aufgabe/Fkt...: Implementierung eines View, bei der die Aktualisierung auf die tatsächlich geänderten 
//                  Werte beschränkt wird. Dabei werden von den Settern Jobs in eine Queue eingestellt. Die 
//                  Jobs sind als schöngefinkelte Lambdaausdrücke realisert, die die Zuweisung des neuen Wertes an die 
//                  zugrundeliegende Entity- Eigenschaft beschreiben. 
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mko.Algo.FunctionalProgramming;
using System.Diagnostics;

namespace mkoIt.Db
{
    [Serializable]
    public abstract class BoBaseView<TEntity, TEntityId> : IEntityView<TEntity, TEntityId>
        where TEntity : class, new()
    {

        public BoBaseView()
        {
            _Entity = new TEntity();
            State = BoBaseViewDefs.ViewState.Added;
        }

        public BoBaseView(TEntity Entity)
        {
            // 3.8.2014, mko
            Debug.Assert(Entity != null);
            _Entity = Entity;
            State = BoBaseViewDefs.ViewState.Unchanged;
        }


        // Referenz auf das Entity, auf das die View aufgebaut wird (für die Getter)
        public TEntity Entity
        {
            get
            {
                return _Entity;
            }
        }

        TEntity _Entity;



        /// <summary>
        /// Dokumentiert den Bearbeitungszustand einer View.
        /// Z.B. steht Detached für eine mittels Standard- Konstruktor angelegt View
        /// </summary>
        public BoBaseViewDefs.ViewState State { get; set; }

        public BoBaseViewDefs.ViewPropertyState PropertiesState { get; set; }

        /// <summary>
        /// Zugriff auf die ID des Entities
        /// </summary>
        public TEntityId Id
        {
            get
            {
                if (State == BoBaseViewDefs.ViewState.Added)
                    return GetDummyEntityId();
                else
                    return GetEntityId(Entity);
            }
            set
            {
                SetEntityId(value, _Entity);
            }
        }

        /// <summary>
        /// Liefert die ID in einer EF5.0 konformen Form
        /// </summary>
        public virtual object[] Keys
        {
            get { return new object[] { Id }; }
        }


        /// <summary>
        /// Überschreibung in abgeleiteter Klasse liefert den Schlüssel eines Entity
        /// </summary>
        /// <param name="Entity">Entity</param>
        /// <returns>Schlüssel</returns>
        protected abstract TEntityId GetEntityId(TEntity Entity);

        protected abstract TEntityId GetDummyEntityId();


        /// <summary>
        /// Überschreibung in abgeleiteter Klasse setzt den Schlüssel eines Entity
        /// </summary>
        /// <param name="id">neuer Schlüssel</param>
        /// <param name="Entity">Entity</param>
        protected abstract void SetEntityId(TEntityId id, TEntity Entity);


        // Liste aller Updatefunktionen, die beim nächsten update abzuarbeiten sind
        public Queue<Action<TEntity>> QueuedJobsForPropertiesUpdate = new Queue<Action<TEntity>>();


        /// <summary>
        /// Hilfsfunktion, mittels der zuverlässig die Eigenschaft eines Objektes gelesen werden kann, falls das Objekt existiert.
        /// Wenn das Objekt nicht existiert, dann wird ein Nullable zurückgeliefert, dessen HasValue- Eigenschaft den Wert false hat.
        /// Sinn macht die Funktion beim Zugriff auf Objekte, die in einer 0-1 Beziehung zum Masterobjekt stehen. Z.B. steht das 
        /// Objekt Sterne_Planeten_Monde in einer 0-1 Beziehung zum Objekt Himmelskoerper in der KeplerDB:
        /// Himmelskoerper 1 -- 0..1 Sterne_Planeten_Monde
        /// Soll der Durchmesser eines Planeten ausgelesen werden, dann kann erfolgt dies über 
        /// Himmelskoerper.Sterne_Planeten_Monde.Aequatordurchmesser_in_km
        /// Wenn jedoch noch kein Sterne_Planeten_Monde zum Himmelskoerper existiert, dann schlägt der Zugriff fehl.
        /// GetPropertyIfObjectExists soll diesen Zugriff absichern
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="obj"></param>
        /// <param name="PropertySelector"></param>
        /// <returns></returns>
        public Nullable<TProperty> GetPropertyIfObjectExists<TObject, TProperty>(TObject obj, Func<TObject, TProperty> PropertySelector)
        where TProperty : struct
        {
            return obj != null ? PropertySelector(obj) : (Nullable<TProperty>)null;
        }

        /// <summary>
        /// ... Überladung für Strings
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="obj"></param>
        /// <param name="PropertySelector"></param>
        /// <returns></returns>
        public string GetPropertyIfObjectExists<TObject>(TObject obj, Func<TObject, string> PropertySelector)        
        {
            return obj != null ? PropertySelector(obj) : "";
        }



        /// <summary>
        /// Fügt einen Updatejob hinzu. Ein Updatejob besteht aus dem neuen Wert und einem 
        /// Lambda- Ausdruck, der die Zuweisung des Wertes an das Datenbankentity beschreibt.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="Value"></param>
        /// <param name="UpdateFunction"></param>
        protected void SetProperty<TProp>(TProp Value, Action<TProp, TEntity> PropertySetter)
        {
            // View als Modifiziert kennzeichnen
            PropertiesState = BoBaseViewDefs.ViewPropertyState.Modified;

            // Ausgewählte Eigenschaft im lokalen Entity der View setzen
            PropertySetter(Value, Entity);

            // Aktualisierungsauftrag für externes Entity aufgeben
            Action<TEntity> job = PropertySetter.Curry(Value);
            QueuedJobsForPropertiesUpdate.Enqueue(job);
        }


        /// <summary>
        /// Korrektere Bezeichnung: SetPropertyWithNullableValue
        /// </summary>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="Value"></param>
        /// <param name="PropertySetter"></param>
        protected void SetPropertyWithNullableValue<TProp>(Nullable<TProp> Value, Action<TProp, TEntity> PropertySetter)
            where TProp : struct
        {
            if (Value.HasValue)
            {
                // View als Modifiziert kennzeichnen
                PropertiesState = BoBaseViewDefs.ViewPropertyState.Modified;

                // Ausgewählte Eigenschaft im lokalen Entity der View setzen
                PropertySetter(Value.Value, Entity);

                // Aktualisierungsauftrag für externes Entity aufgeben
                Action<TEntity> job = PropertySetter.Curry(Value.Value);
                QueuedJobsForPropertiesUpdate.Enqueue(job);
            }
        }
        
        /// <summary>
        /// Alle aktualisierungsaufträge werden auf einem Entity ausgeführt, das z.B. aus einem 
        /// EF- Datenkontext einer Datenbank stammt
        /// </summary>
        /// <param name="extEntity">Referenz auf externes Entity</param>
        public void UpdateExternalEntity(TEntity extEntity)
        {
            Debug.Assert(extEntity != Entity);
            BoBaseView<TEntity, TEntityId>.UpdateEntity(extEntity, QueuedJobsForPropertiesUpdate);
        }

        /// <summary>
        /// Implementieren des Löschens der Liste mit den Einträgen der Änderungsverfolgung
        /// </summary>
        public void DeleteAllChangeTrackingEntries()
        {
            QueuedJobsForPropertiesUpdate.Clear();
        }



        /// <summary>
        /// Führt alle Aktualisierungsaufträge aus der Queue auf dem Entity aus. 
        /// Wenn erfolgreich, dann ist die UpdateJobs Queue leer
        /// </summary>
        /// <param name="entity">zu aktualisierendes Entity</param>
        /// <param name="QueuedJobsForPropertiesUpdate">Warteschlange mit Aktualsierungsaufträgen</param>
        public static void UpdateEntity(TEntity entity, Queue<Action<TEntity>> QueuedJobsForPropertiesUpdate)
        {
            while (QueuedJobsForPropertiesUpdate.Any())
            {
                // Der schnöngefinkelte Lambdaausdruck wird ausgeführt, wodurch die Aktualisierung der 
                // Entity- Eigenschaft implementiert wird
                QueuedJobsForPropertiesUpdate.Dequeue()(entity);
            }
        }

        /// <summary>
        /// Führt alle Aktualisierungsaufträge aus der UpdateJobs- Queue auf dem 
        /// Entity aus und stellt die Aktualisierungaufträge für eine wiederholte
        /// Ausführung in die Queue wieder ein.
        /// </summary>
        /// <param name="entity">zu aktualisierendes Entity</param>
        /// <param name="QueuedJobsForPropertiesUpdate">Warteschlange mit Aktualisierungsaufträgen</param>
        public static void UpdateEntityAndPreserveUpdateJobs(TEntity entity, Queue<Action<TEntity>> QueuedJobsForPropertiesUpdate)
        {
            int Count = QueuedJobsForPropertiesUpdate.Count;
            for(int i = 0; i < Count; i++)
            {
                // Aktualisierungsauftrag der Warteschlange entnehmen
                var updJob = QueuedJobsForPropertiesUpdate.Dequeue();

                // Der schnöngefinkelte Lambdaausdruck wird ausgeführt, wodurch die Aktualisierung der 
                // Entity- Eigenschaft implementiert wird
                updJob(entity);
                // Ausgeführten Job wieder in Warteschlange einstellen
                QueuedJobsForPropertiesUpdate.Enqueue(updJob);
            }
        }



    }
}
