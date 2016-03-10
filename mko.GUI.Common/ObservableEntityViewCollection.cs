using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

using Defs = mko.GUI.Common.BaseEntityViewDefs;
using System.Diagnostics;

namespace mko.GUI.Common
{
    public abstract class ObservableEntityViewCollection<TEntity, TEntityView> : System.Collections.ObjectModel.ObservableCollection<TEntityView>
        where TEntity : new()
        where TEntityView : BaseEntityView<TEntity>
    {

        public Dictionary<TEntityView, TEntityView> AddedViews = new Dictionary<TEntityView, TEntityView>();
        public Dictionary<TEntityView, TEntityView> DeletedViews = new Dictionary<TEntityView, TEntityView>();

        /// <summary>
        /// Klassenfabrik für EntityViews
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected abstract TEntityView CreateEntityView(TEntity Entity);

        private bool IsLoading = false;

        /// <summary>
        /// Entites aus einer Datenquelle in die Collection kopieren
        /// </summary>
        /// <param name="EntitySet"></param>
        /// <remarks></remarks>
        public void LoadEntites(IEnumerable<TEntity> EntitySet)
        {

            try
            {
                IsLoading = true;

                foreach (var Entity in EntitySet)
                {
                    Add(CreateEntityView(Entity));
                }

                foreach (var EntityView in this)
                {
                    EntityView.State = Defs.ViewState.Unchanged;
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                IsLoading = false;
            }

        }


        public new void Clear()
        {
            base.Clear();
            AddedViews.Clear();
            DeletedViews.Clear();
        }



        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            // Nur Änderungen behandeln, wenn sie nicht von der LoadEntites- Funktion verursacht wurden
            if (!IsLoading)
            {
                if ((e.Action) == NotifyCollectionChangedAction.Add)
                {
                    foreach (TEntityView EntityView in e.NewItems)
                    {
                        EntityView.State = Defs.ViewState.Added;
                        AddedViews[EntityView] = EntityView;
                    }
                }
                else if ((e.Action) == NotifyCollectionChangedAction.Remove)
                {
                    foreach (TEntityView EntityView in e.OldItems)
                    {
                        EntityView.State = Defs.ViewState.Deleted;
                        if (AddedViews.ContainsKey(EntityView))
                        {
                            AddedViews.Remove(EntityView);
                        }
                    }
                }
                else if ((e.Action) == NotifyCollectionChangedAction.Replace)
                {
                    Debug.WriteLine("Replace");
                }
                else if ((e.Action) == NotifyCollectionChangedAction.Reset)
                {
                    Debug.WriteLine("Reset");
                }
            }
        }
    }
}
