using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Defs = mkoIt.Db.BoBaseViewDefs;


namespace mkoIt.Db
{
    public abstract class  BoBaseEF_50<TORMContext, TEntity, TEntityId, TEntityView>
        : BoBase<TEntity, TEntityId, TEntityView>, IDisposable
        where TORMContext : System.Data.Entity.DbContext, new()
        where TEntity : class, new()
        where TEntityView : class, IEntityView<TEntity, TEntityId>, new()
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="DefaultSortCol"></param>
        /// <param name="otherTEntityViews">Liste der Typen der restlichen Views vom Entity</param>
        public BoBaseEF_50(TORMContext ctx, string DefaultSortCol)
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
            ORMContext.SaveChanges();
            //ORMContext..AcceptAllChanges();
        }

        /// <summary>
        /// Allg. Resourcenfreigabe
        /// </summary>
        public void Dispose()
        {
            ORMContext.Dispose();
        }

        public override void UpdateWithObservableEntityViewCollectionAndSubmit(ObservableEntityViewCollection obsrvEntityViewCollection)
        {
            try
            {
                var EcAsDBSet = (System.Data.Entity.DbSet<TEntity>)EntityCollection;
                EcAsDBSet.UpdateWithObservableEntityViewCollection(obsrvEntityViewCollection);

                EcAsDBSet.SubmitChanges(obsrvEntityViewCollection, ORMContext);
            }
            catch (Exception ex)
            {
                throw (BoBaseException.Create(this, "UpdateWithObservableEntityViewCollection", ex));
            }

            SubmitChanges();
        }
    }
}
