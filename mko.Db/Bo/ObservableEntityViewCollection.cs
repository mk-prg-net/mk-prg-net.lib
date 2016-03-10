//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 13.6.2012
//
//  Projekt.......: mko.Db
//  Name..........: ObservableEntityViewCollection
//  Aufgabe/Fkt...: Verwaltet IEntityViews, wobei hinzugefügte und gelöschte Entities in Listen protokolliert
//                  werden. Mittels dieser Listen können weitere Funktionen die Änderungen z.B. mit Tabellen
//                  einer Datenbank synchronisieren.
//
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
using System.Diagnostics;
using System.Linq;
using System.Text;

using Specialized = System.Collections.Specialized;
using Defs = mkoIt.Db.BoBaseViewDefs;

namespace mkoIt.Db
{
    [Serializable]
    public abstract class ObservableEntityViewCollection<TEntity, TEntityID, TEntityView> : System.Collections.ObjectModel.ObservableCollection<TEntityView>
        where TEntity : class, new()
        where TEntityView : IEntityView<TEntity, TEntityID>
    {
        /// <summary>
        /// Zeichnet alle neu hinzugefügten Views auf
        /// </summary>
        public Dictionary<TEntityView, TEntityView> AddedViews = new Dictionary<TEntityView, TEntityView>();

        /// <summary>
        /// Zeichnet alle zu löschenden Views auf
        /// </summary>
        public Dictionary<TEntityView, TEntityView> DeletedViews = new Dictionary<TEntityView, TEntityView>();

        /// <summary>
        /// Klassenfabrik für EntityViews
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected abstract TEntityView CreateEntityView(TEntity Entity);

        /// <summary>
        /// Zeigt an, das neue Entities mittels LoadEntities hinzugefügt werden
        /// </summary>
        private bool IsLoading = false;


        public void LoadEntity(TEntity Entity)
        {
            try
            {
                IsLoading = true;
                var View = CreateEntityView(Entity);
                View.State = Defs.ViewState.Unchanged;
                Add(View);
            }
            catch (Exception ex)
            {
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "LoadEntity", Entity.ToString(), ex.Message), ex);
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// Entities aus einer Datenquelle in die Collection kopieren
        /// </summary>
        /// <param name="EntitySet"></param>
        /// <remarks></remarks>
        public void LoadEntities(IEnumerable<TEntity> EntitySet)
        {

            try
            {
                IsLoading = true;

                foreach (var MyEntity in EntitySet)
                {
                    var View = CreateEntityView(MyEntity);
                    View.State = Defs.ViewState.Unchanged;
                    Add(View);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "LoadEntities", EntitySet.ToString(), ex.Message), ex);
            }
            finally
            {
                IsLoading = false;
            }

        }

        /// <summary>
        /// Alle Einträge löschen und ebenfalls die internen Verwaltungsstrukturen zurücksetzen
        /// </summary>
        public new void Clear()
        {
            base.Clear();
            AddedViews.Clear();
            DeletedViews.Clear();
        }


        /// <summary>
        /// Reaktion auf Änderungen an der Collection. Neue Einträge werden in AddViews verzeichnet,
        /// gelöschte in DeletedViews.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCollectionChanged(Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
                base.OnCollectionChanged(e);

                // Nur Änderungen behandeln, wenn sie nicht von der LoadEntites- Funktion verursacht wurden
                if (!IsLoading)
                {
                    if ((e.Action) == Specialized.NotifyCollectionChangedAction.Add)
                    {
                        foreach (TEntityView EntityView in e.NewItems)
                        {
                            EntityView.State = Defs.ViewState.Added;
                            AddedViews[EntityView] = EntityView;
                        }
                    }
                    else if ((e.Action) == Specialized.NotifyCollectionChangedAction.Remove)
                    {
                        foreach (TEntityView EntityView in e.OldItems)
                        {
                            EntityView.State = Defs.ViewState.Deleted;
                            if (AddedViews.ContainsKey(EntityView))
                            {
                                AddedViews.Remove(EntityView);
                            }
                            else
                            {
                                DeletedViews[EntityView] = EntityView;
                                EntityView.State = Defs.ViewState.Deleted;
                            }
                        }
                    }
                    else if ((e.Action) == Specialized.NotifyCollectionChangedAction.Replace)
                    {
                        Debug.WriteLine("Replace");
                    }
                    else if ((e.Action) == Specialized.NotifyCollectionChangedAction.Reset)
                    {
                        Debug.WriteLine("Reset");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "OnCollectionChanged", ex.Message), ex);
            }


        }


    }
}