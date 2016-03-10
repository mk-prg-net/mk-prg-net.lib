//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: mkoItAsp
//  Name..........: GridViewDecorator.cs
//  Aufgabe/Fkt...: Erweitert GridView um Empty- Template und Sortierfunktion
//                  
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
using mkx = mkoIt.Xhtml.Xhtml;


namespace mkoIt.Asp.GridView
{
    public class GridViewDecorator<TState, TEntity>
        where TEntity : class, new()
        where TState : SessionStateFilterAndSortEntities<TEntity>
    {
        System.Web.UI.WebControls.GridView grd;
        SessionStateFilterAndSortEntities<TEntity> state;
        mko.Log.LogServer log;

        public GridViewDecorator(System.Web.UI.WebControls.GridView grd, TState state, HttpResponse response,  mko.Log.LogServer log)
        {
            Debug.Assert(grd != null && state != null && response != null && log != null, "GridViewDecorator: Konstruktorparameter unvollständig");
            this.grd = grd;
            this.log = log;
            this.state = state;

            grd.Sorting += new System.Web.UI.WebControls.GridViewSortEventHandler(grd_Sorting);
            grd.EmptyDataTemplate = new mkoIt.Asp.GridView.EmptyDataTemplate<TEntity>(state, response);            

        }

        void grd_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            state.SortDirection = e.SortDirection;
            state.SortExpression = e.SortExpression;
        }


    }
}
