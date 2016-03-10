using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace mkoIt.Db
{
    public abstract class BoBaseEF<TORMContext, TEntity, TEntityId, TEntityView>
        : BoBase<TEntity, TEntityId, TEntityView>, IDisposable
        where TORMContext : System.Data.Objects.ObjectContext, new()
        where TEntity : class, new()
        where TEntityView : class, IEntityView<TEntity, TEntityId>, new()
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="DefaultSortCol"></param>
        /// <param name="otherTEntityViews">Liste der Typen der restlichen Views vom Entity</param>
        public BoBaseEF(TORMContext ctx, string DefaultSortCol)
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
            ORMContext.AcceptAllChanges();
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
            
        }
    }
}
