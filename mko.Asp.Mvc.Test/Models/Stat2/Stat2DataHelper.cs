using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace mko.Asp.Mvc.Test.Models
{
    public class Stat2DataHelper
    {
        /// <summary>
        /// Erzeugt eine Liste von SelectListItems, aus denen auf dem Server eine Drop-Down Liste aufgebaut wird
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> CreateDataAsSelectedListItems(Models.Stat2Data model)
        {

            List<SelectListItem> sList = new List<SelectListItem>(model.Data.Count());

            foreach(double elem in model.Data) {
                sList.Add(new SelectListItem(){ Text = elem.ToString(), Value= elem.ToString()});
            }

            return sList;
        }
    }
}