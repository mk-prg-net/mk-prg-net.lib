using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Db
{
    public class BoEventLog : mkoIt.Db.BoBaseSqlToLinq<EventLogDb.DtxEventLogDataContext, EventLogDb.EventLog, int, BoEventLog.View>
    {
        public BoEventLog() : base(new EventLogDb.DtxEventLogDataContext(), "created") { }

        public class View : mkoIt.Db.BoBaseView<EventLogDb.EventLog, int>
        {
            public View() { }

            public View(EventLogDb.EventLog entity)
                : base(entity)
            {                
            }   

            public class IdFilter : mkoIt.Db.FilterFunctor<EventLogDb.EventLog, int>{
                public IdFilter()
                {
                }

                public override IQueryable<EventLogDb.EventLog> filterImpl(IQueryable<EventLogDb.EventLog> srcTab)
                {
                    return srcTab.Where(r => r.id == RValue);
                }
            }

            

            [mkoIt.Db.MapPropertyToColName("author")]
            public string Autor {
                get
                {
                    return Entity.author;
                }
                set
                {
                    SetProperty(value, (val, entity) => entity.author = val);
                }
            }

            [mkoIt.Db.MapPropertyToColName("created")]
            public DateTime? VerfasstAm { 
                get {
                    return Entity.created;
                }
                set
                {
                    SetPropertyWithNullableValue(value, (val, entity) => entity.created = val);
                }
            }

            [mkoIt.Db.MapPropertyToColName("DerivatTyp")]
            public string Typ
            {
                get
                {
                    return Entity.EventLogTypes.name;
                }
                set { }
            }


            public string Log {
                get
                {
                    return Entity.log.ToString();
                }
                set {
                } 
            }
           

            protected override int GetEntityId(EventLogDb.EventLog Entity)
            {
                return Entity.id;
            }

            protected override void SetEntityId(int id, EventLogDb.EventLog Entity)
            {
                Entity.id = id;
            }

            protected override int GetDummyEntityId()
            {
                return -1;
            }
        }

        protected override Type[] AllEntityViewTypes()
        {
            return new Type[] { typeof(View) };
        }

        protected override Type GetBoThatInclude(Type typeTEntityView)
        {
            return typeof(BoEventLog);
        }

        protected override bool SortByDerivat(IQueryable<EventLogDb.EventLog> tab, string ColName, bool desc, out IQueryable<EventLogDb.EventLog> tabOrdered)
        {
            tabOrdered = null;

            switch (ColName)
            {
                case "DerivatTyp":
                    tabOrdered = sortHlp(tab, desc, r => r.EventLogTypes.name);
                    return true;
                default: ;
                    break;
            }

            return false;
        }

        public override void DeletAll()
        {          
        
            try
            {
                var all = ORMContext.EventLog;

                ORMContext.EventLog.DeleteAllOnSubmit(all);

                ORMContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw BoBaseException.Create("deleteAll", ex);
            }

        }

        public override IQueryable<EventLogDb.EventLog> EntityCollection
        {
            get { return ORMContext.EventLog; }
        }


        public override EventLogDb.EventLog CreateEntity()
        {
            return new EventLogDb.EventLog();
        }


        public override BoEventLog.View CreateEntityView()
        {
            return new View();
        }

        protected override BoEventLog.View CreateEntityView(EventLogDb.EventLog entity)
        {
            return new View(entity);
        }

        //public override mkoIt.Db.FilterFunctor<EventLogDb.EventLog, int> CreateIdFilter(int Id)
        //{
        //    return new View.IdFilter() { RValue = Id };
        //}

        public override Func<EventLogDb.EventLog, bool> GetEntityIDTest(int Id)
        {
            return new Func<EventLogDb.EventLog, bool>(r => r.id == Id);
        }
    }
}
