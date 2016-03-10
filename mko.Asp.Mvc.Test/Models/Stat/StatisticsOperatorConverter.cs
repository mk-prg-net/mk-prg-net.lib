using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MVCX = System.Web.Mvc;

namespace mko.Asp.Mvc.Test.Models
{
    public class StatisticsOperatorConverter
    {

        public static StatisticsOperatorConverter Create(StatModelData model)
        {
            return new StatisticsOperatorConverter(model);
        }

        StatModelData _model;
        public StatisticsOperatorConverter(StatModelData model)
        {
            _model = model;
        }

       

        // Edit Operationen in Formen bringen, mit denen die View umgehen kann
        public string EditOperatorAsString
        {
            get
            {
                return _model.EditOperator.ToString();
            }

            set
            {
                _model.EditOperator = (StatModelData.EditOps)Enum.Parse(typeof(StatModelData.EditOps), value);
            }
        }


        public IEnumerable<string> AllEditOperatorsAsString
        {
            get
            {
                yield return StatModelData.EditOps.add.ToString();
                yield return StatModelData.EditOps.remove.ToString();
                yield return StatModelData.EditOps.clear.ToString();
            }
        }

        public IEnumerable<MVCX.SelectListItem> AllEditOperatorsAsSelectListItem
        {
            get
            {
                yield return new MVCX.SelectListItem() { Text = "hinzufügen", Value = StatModelData.EditOps.add.ToString() };
                yield return new MVCX.SelectListItem() { Text = "wegnehmen", Value = StatModelData.EditOps.remove.ToString() };
                yield return new MVCX.SelectListItem() { Text = "alle löschen", Value = StatModelData.EditOps.clear.ToString() };
            }
        }
    }
}