using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mko.Asp.Mvc.Test.Models
{
    public class ContactData
    {
        public string Name { get; set;}

        public string FirstName {get; set;}

        public string Phone { get; set;}

        public string EMail { get; set;}

        public static ContactData Instance;

        static ContactData()
        {
            Instance = new ContactData()
            {
                Name = "Korneffel",
                FirstName = "Martin",
                Phone = "(0)49 711 52 83 392",
                EMail = "Martin.Korneffel@t-online.de"
            };
        }
    }
}