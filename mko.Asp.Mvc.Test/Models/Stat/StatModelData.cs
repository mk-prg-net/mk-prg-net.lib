using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Ano = System.ComponentModel.DataAnnotations;

namespace mko.Asp.Mvc.Test.Models
{
    public class StatModelData : ICloneable
    {
        List<double> _values = new List<double>();

        public List<double> Values
        {
            get
            {
                return _values;
            }            
        }

        // Operationen zum editieren der Values- Liste

        public enum EditOps
        {
            add, remove, clear
        }

        public EditOps EditOperator { get; set; }

#if(DEBUG)
        // Edit Operationen in Formen bringen, mit denen die View umgehen kann
        public string EditOperatorAsString
        {
            get
            {
                return EditOperator.ToString();
            }

            set
            {
                EditOperator = (StatModelData.EditOps)Enum.Parse(typeof(StatModelData.EditOps), value);
            }
        }
#endif
        [Ano.Display(Name="Neuer Wert", ShortName="+Wert")]
        [Ano.DisplayFormat(DataFormatString="{0:N3}", ApplyFormatInEditMode=true)]
        [Ano.Range(-1000.0, 1000.0)]
        public double ValueToAdd { get; set; }

        [Ano.Display(Name = "Nr des zu löschenden", ShortName = "-Wert i")]
        [Ano.DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = true)]
        [Ano.Range(0, 1000)]
        public int IxValueToRemove { get; set; }


        public object Clone()
        {
            var clone = new StatModelData()
            {
                EditOperator = EditOperator,
                IxValueToRemove = IxValueToRemove,
                _values = _values.Select(r => r).ToList(),
                ValueToAdd = ValueToAdd
            };

            return clone;

        }
    }
}