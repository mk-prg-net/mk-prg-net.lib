using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mko.Asp.Mvc.Test.Controllers
{
    public class StatController : Controller
    {


        /// <summary>
        /// ID im Sitzungszustand, unter der das für diese Sitzung gültige Statistikmodell abrufbar ist
        /// </summary>
        const string IdServermodel = "StatModell";

        protected Models.StatModelData Servermodell
        {
            get
            {
                var model = (Models.StatModelData)Session[IdServermodel];

                if (model == null)
                {
                    model = new Models.StatModelData()
                    {
                        EditOperator = Models.StatModelData.EditOps.add,
                    };
                    Session[IdServermodel] = model;

                }
                return model;
            }

            set
            {
                Session[IdServermodel] = value;
            }
        }


        //
        // GET: /Statistics/

        public ActionResult Index()
        {
            // Per Konvention wird beim Parameterlosen Aufruf von View eine View mit Namen = Actionname 
            // aufgerufen
            return View("EditView", Servermodell);
        }

        // Edit nimmt die geänderten Eigenschaften als FromCollection entgegen,
        // aktualisiert mit Ihnen das Servermodell und sendet dieses an die Editview zurück
        public ActionResult Edit(FormCollection fc)
        {
            if (!ModelState.IsValid)
            {
                var ServermodellClon = Servermodell.Clone() as Models.StatModelData;
                return View("EditView", ServermodellClon);
            }
           
            TryUpdateModel(Servermodell, new string[] { "ValueToAdd", "IxValueToRemove", "Operator", "EditOperatorAsString" }, fc);

            switch (Servermodell.EditOperator)
            {
                case Models.StatModelData.EditOps.add:
                    Servermodell.Values.Add(Servermodell.ValueToAdd);
                    break;
                case Models.StatModelData.EditOps.clear:
                    Servermodell.Values.Clear();
                    break;
                case Models.StatModelData.EditOps.remove:
                    Servermodell.Values.RemoveAt(Servermodell.IxValueToRemove);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return View("EditView", Servermodell);
        }

        public ActionResult Delete(int id)
        {
            if (id < 0 || id >= Servermodell.Values.Count)
                throw new Exception("id ungültig");

            Servermodell.Values.RemoveAt(id);

            return View("EditView", Servermodell);
        }


        /// <summary>
        /// BasicsCalc fürht eine statistiche Opeartion aus und leitet das Ergebnis 
        /// eine View zur Darstellung des Ergebnisses weiter
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult StatCalc1()
        {

            var result = new Models.StatModelMinMeanMax();

            result.Max = Servermodell.Values.Max();
            result.Mean = Servermodell.Values.Average();
            result.Min = Servermodell.Values.Min();

            return View("StatCalcView", result);
        }
    }
}
