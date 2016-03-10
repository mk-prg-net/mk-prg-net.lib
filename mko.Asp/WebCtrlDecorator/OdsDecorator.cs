//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 19.3.2012
//
//  Projekt.......: mkoItAsp
//  Name..........: OdsDecorator.cs
//  Aufgabe/Fkt...: Erweitert eine Objectdatasource um Funktionen zum Initialisieren
//                  eines Business- Objektes in der Code- Behind- Datei
//
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
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;


namespace mkoIt.Asp.ObjectDataSource
{
    public class OdsDecorator<TEntity, TKey, TEntityView, TBo, TState>
        //where TORMContext : System.Data.Linq.DataContext, new()
        where TEntity:  class, new()
        //where TKey: struct
        where TEntityView : class, mkoIt.Db.IEntityView<TEntity, TKey>,  new()
        where TState : SessionStateFilterAndSortEntities<TEntity>
        where TBo : mkoIt.Db.BoBase<TEntity, TKey, TEntityView>
    {

        TState _state;
        public TState state
        {
            get
            {
                return _state;
            }
        }

        mko.Log.LogServer _log;
        public mko.Log.LogServer log
        {
            get
            {
                return _log;
            }
        }

        public bool HandledUpdateExeptions {get; set;}

        public OdsDecorator(System.Web.UI.WebControls.ObjectDataSource ods, TState state, mko.Log.LogServer log)
        {
            _log = log;
            _state = state;
            HandledUpdateExeptions = true;
            ods.ObjectCreated +=new ObjectDataSourceObjectEventHandler(ods_ObjectCreated);
            ods.Updated += new ObjectDataSourceStatusEventHandler(ods_Updated);
        }

        /// <summary>
        /// Eventhandler, der nach dem Erzeugen einer Objektdatasource eingerichet wird
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ods_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
        {
            var bo = e.ObjectInstance as TBo;
            bo_Init(bo);
            state.SetFilterAndSort(bo);
            bo_modify_after_Filter_and_Sort_set(bo);
        }

        /// <summary>
        /// Kann in abgeleiteten Klassen überschrieben werden, um das Geschäftsobjekt
        /// nach seiner Erzeugung im Kontext einer Objektdatasource zu initialisieren
        /// </summary>
        /// <param name="bo"></param>
        protected virtual void bo_Init(TBo bo) {

        }

        /// <summary>
        /// Kann in abgeleiteten Klassen überschrieben werden, um das Geschäftsobjekt
        /// nach der Definition der anzuwendenen Filter noch weiter zu modifizieren
        /// </summary>
        /// <param name="bo"></param>
        protected virtual void bo_modify_after_Filter_and_Sort_set(TBo bo)
        {
        }

        /// <summary>
        /// Reaktion auf Aktualisierungen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ods_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            var ods = sender as System.Web.UI.WebControls.ObjectDataSource;
            if (HandledUpdateExeptions && e.Exception != null)
            {
                log.Log(mko.Log.RC.CreateError(
                    string.Format("Beim Aktualisieren mit {0}: {1}",
                    ods.TypeName,
                    mko.ExceptionHelper.FlattenExceptionMessages(e.Exception))));

                // Ausnahme als behandelt markieren
                e.ExceptionHandled = true;
            }
        }


    }
}
