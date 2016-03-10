//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 13.6.2012
//
//  Projekt.......: mko.Db
//  Name..........: LinqTableExtensions.cs
//  Aufgabe/Fkt...: Erweiterungsmethoden für Linq- Tabellen
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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

using Defs = mkoIt.Db.BoBaseViewDefs;

namespace mkoIt.Db
{
    public static class LinqTableExtensions
    {
        public static void UpdateWithObservableEntityViewCollection<TEntity, TEntityID, TEntityView>(this System.Data.Linq.Table<TEntity> Entities, ObservableEntityViewCollection<TEntity, TEntityID, TEntityView> EntityViewCollection, Func<TEntity, TEntityID> GetEntityID)
            where TEntity : class, new()
            where TEntityView : IEntityView<TEntity, TEntityID>
        {

            try
            {
                //
                foreach (var AddedView in EntityViewCollection.AddedViews)
                {
                    Entities.InsertOnSubmit(AddedView.Value.Entity);
                }

                foreach (var RemovedView in EntityViewCollection.DeletedViews)
                {
                    TEntity Entity = Entities.Single(r => GetEntityID(r).Equals(RemovedView.Value.Id));
                    Entities.DeleteOnSubmit(Entity);
                }

                // Modifizieren
                foreach (var View in EntityViewCollection.Where(v => v.PropertiesState == Defs.ViewPropertyState.Modified))
                {
                    TEntity Entity = Entities.Single(r => GetEntityID(r).Equals(View.Id));
                    if (!View.Equals(Entity))
                        // Nur dann ein externes Entity aktuelisieren, wenn Inhalt von Entityview geändert wurde und Entity der EntityView
                        // ein vom ORMContext- verwalteten Entity verschiedenes ist
                        View.UpdateExternalEntity(Entity);
                    else
                        View.DeleteAllChangeTrackingEntries();
                }

                //Context.SaveChanges()

                foreach (var View in EntityViewCollection.Where(v => v.State != Defs.ViewState.Unchanged))
                {
                    View.State = Defs.ViewState.Unchanged;
                }

                EntityViewCollection.AddedViews.Clear();
                EntityViewCollection.DeletedViews.Clear();

            }
            catch (Exception ex)
            {
                throw (new Exception("mkoIt.Db.DBSetExtensions.UpdateWithObservableViewCollection", ex));
            }
        }

    }
}
