using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.CompilerServices;
using Defs = mkoIt.Db.BoBaseViewDefs;
using System.Diagnostics;


namespace mkoIt.Db
{
    public static class EF5DBSetExtensions
    {
        /// <summary>
        /// Aktualisiert ein EF5.0 DBSet mit den in einer ObservableEntityView geänderten Einträgen
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TEntityID"></typeparam>
        /// <typeparam name="TEntityView"></typeparam>
        /// <param name="Entities"></param>
        /// <param name="EntityViewCollection"></param>
        public static void UpdateWithObservableEntityViewCollection<TEntity, TEntityID, TEntityView>(this System.Data.Entity.DbSet<TEntity> Entities, ObservableEntityViewCollection<TEntity, TEntityID, TEntityView> EntityViewCollection)
            where TEntity : class, new()
            where TEntityView : class, IEntityView<TEntity, TEntityID>, new()
        {
            try
            {
                //
                foreach (var AddedView in EntityViewCollection.AddedViews)
                {
                    Debug.Assert(AddedView.Value.State == Defs.ViewState.Added);
                    Entities.Add(AddedView.Value.Entity);
                }

                foreach (var RemovedView in EntityViewCollection.DeletedViews)
                {
                    Debug.Assert(RemovedView.Value.State == Defs.ViewState.Deleted);
                    TEntity Entity = Entities.Find(RemovedView.Value.Keys);
                    Entities.Remove(Entity);
                }

                // Modifizieren
                foreach (var View in EntityViewCollection.Where(v => v.PropertiesState == Defs.ViewPropertyState.Modified))
                {
                    TEntity Entity = Entities.Find(View.Keys);
                    if (Entity != null && !View.Entity.Equals(Entity))
                        // Nur dann ein externes Entity aktualisieren, wenn Inhalt von Entityview geändert wurde und Entity der EntityView
                        // ein vom ORMContext- verwalteten Entity verschiedenes ist
                        View.UpdateExternalEntity(Entity);
                    else
                        View.DeleteAllChangeTrackingEntries();
                }


            }
            catch (Exception ex)
            {
                throw (BoBase<TEntity, TEntityID, TEntityView>.BoBaseException.Create("UpdateWithObservableEntityViewCollection", ex));
            }
        }

        /// <summary>
        /// Überträgt alle am DBset mit UpdateWithObservableEntityViewCollection vorgenommenen Änderungen in die Datenbank
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TEntityID"></typeparam>
        /// <typeparam name="TEntityView"></typeparam>
        /// <param name="Entities"></param>
        /// <param name="EntityViewCollection"></param>
        /// <param name="Context"></param>
        public static void SubmitChanges<TEntity, TEntityID, TEntityView>(this System.Data.Entity.DbSet<TEntity> Entities, ObservableEntityViewCollection<TEntity, TEntityID, TEntityView> EntityViewCollection, System.Data.Entity.DbContext Context)
            where TEntity : class, new()
            where TEntityView : class, IEntityView<TEntity, TEntityID>, new()
        {
            try
            {
                Context.SaveChanges();

                foreach (var View in EntityViewCollection.Where(v => v.State != Defs.ViewState.Unchanged))
                {
                    View.State = Defs.ViewState.Unchanged;
                }

                EntityViewCollection.AddedViews.Clear();
                EntityViewCollection.DeletedViews.Clear();

            }
            catch (Exception ex)
            {
                throw (BoBase<TEntity, TEntityID, TEntityView>.BoBaseException.Create("SubmitChanges", ex));
            }
        }
    }
}
