using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace mko.Asp.Mvc.Test.Models
{
    public class StatModelMinMeanMax
    {

        [Display(Name="Minimum")]
        public double Min {get; set;}

        [Display(Name = "Mittelwert")]
        public double Mean { get; set; }

        [Display(Name = "Maximum")]
        public double Max{get; set;}
    }
}