//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 17.11.2011
//
//  Projekt.......: mkoDb
//  Name..........: BoBaseSqlToLinq.cs
//  Aufgabe/Fkt...: Basisklasse für die Verarbeitung von Geschäftsobjektlisten
//                  Aus der Klasse GblDbLayer.OdsToLinq abgeleitet, die am 13.11.2010
//                  entwickelt wurde.
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: Herbst 2009
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.8.2011
//  Änderungen....: Integration von DynamicQuery, EntityCollection und Implementierung von 
//                  select in der Basisklasse
//
//  Version.......: 1.2
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.11.2011
//  Änderungen....: Erweitern des Sortieren bzgl. meherer Spalten 
//
//  Version.......: 1.3
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 13.3.2012
//  Änderungen....: Verschoben in Projekt mkoDb. Ziel ist die Verallgemeinerung auf 
//                  alle Projekte durch Loslösung von Web- Bibliothek.
//                  Eigenschaft SortDirection von Typ System.Web.UI.SortDirection auf 
//                  EnumSortDirection umgestellt.
//
//  Version.......: 1.4
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 17.5.2012
//  Änderungen....: Aufteilung in BoBase und BoBaseSqlToLinq zwecks nutzten der Basisklassenfunktionalität
//                  in Datasets <> SqlToLinq DataContext
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

using System.Linq.Dynamic;


namespace mkoIt.Db
{
    public abstract class BoBaseSqlToLinq<TORMContext, TEntity, TEntityId, TEntityView>
        : BoBase<TEntity, TEntityId, TEntityView>, IDisposable
        where TORMContext : System.Data.Linq.DataContext, new()
        where TEntity : class, new()
        where TEntityView : class,  IEntityView<TEntity, TEntityId>, new()
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="DefaultSortCol"></param>
        /// <param name="otherTEntityViews">Liste der Typen der restlichen Views vom Entity</param>
        public BoBaseSqlToLinq(TORMContext ctx, string DefaultSortCol)
            : base(DefaultSortCol)
        {
            _ctx = ctx;            
        }

        /// <summary>
        /// Gibt den Objektkontext zurück, über dem alle EF- Entityauflistungen erreichbar sind
        /// </summary>
        TORMContext _ctx;
        public TORMContext ORMContext
        {
            get
            {
                return _ctx;
            }
        }

        public override void SubmitChanges()
        {
            ORMContext.SubmitChanges();
        }

        /// <summary>
        /// Allg. Resourcenfreigabe
        /// </summary>
        public void Dispose()
        {
            ORMContext.Dispose();
        }

        public override void UpdateWithObservableEntityViewCollectionAndSubmit(ObservableEntityViewCollection EntityViewCollection)
        {
            var EcAsTable = (System.Data.Linq.Table<TEntity>)EntityCollection;
            //EcAsTable.UpdateWithObservableEntityViewCollection(EntityViewCollection, GetEntityIDTest());
        }

        public override void AddToEntityCollection(TEntity entity)
        {
            var EcAsTable = (System.Data.Linq.Table<TEntity>)EntityCollection;
            EcAsTable.InsertOnSubmit(entity);
        }

        public override void RemoveFromEntityCollection(TEntity entity)
        {
            var EcAsTable = (System.Data.Linq.Table<TEntity>)EntityCollection;
            EcAsTable.DeleteOnSubmit(entity);
        }
    }
}

