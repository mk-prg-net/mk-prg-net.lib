using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MVCX = System.Web.Mvc;

using System.ComponentModel.DataAnnotations;

namespace mko.Asp.Mvc.Test.Models
{
    public class BinOpModel
    {
        private int myPrivate = 0;
        protected int myProtected = 0;

        public BinOpModel()
        {
            Operator = Operators.add;
        }

        public enum Operators
        {
            add, sub, mul, div
        }

       
        [Required]
        [Range(typeof(double), "-1e100", "1e100")]
        [Display(Name = "A:", Description = "1. Operand", Prompt = "Geben Sie hier den 1. Operanden ein")]
        public double A { get; set; }

        [Required]
        [Range(typeof(double), "-1e100", "1e100")]
        [Display(Name = "B:", Description = "2. Operand", Prompt = "Geben Sie hier den 2. Operanden ein")]
        public double B { get; set; }

        public Operators Operator { get; set; }
        public string OperatorTxt
        {
            get
            {
                return Operator.ToString();
            }

            set
            {
                Operator =  (Operators)Enum.Parse(typeof(Operators), value);
            }
        }

        public IEnumerable<MVCX.SelectListItem> OperatorSelection
        {
            get
            {
                yield return new MVCX.SelectListItem() { Text = "+", Value = Operators.add.ToString() };
                yield return new MVCX.SelectListItem() { Text = "-", Value = Operators.sub.ToString() };
                yield return new MVCX.SelectListItem() { Text = "*", Value = Operators.mul.ToString() };
                yield return new MVCX.SelectListItem() { Text = "/", Value = Operators.div.ToString() };
                
            }
        }

        public double Result { get; set; }
    }
}