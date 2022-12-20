using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyReporting.Models
{
    public class Trainingtypemodel
    {
        public int Traing_Type_Id { get; set; }
        public int Tr_Institute_Id { get; set; }
        public string Traing_Name { get; set; }
        public string Tr_Institute_Name { get; set; }
        public string Tr_Address { get; set; }
        public List<SelectListItem> ListTraingName { get; set; }

    }
}