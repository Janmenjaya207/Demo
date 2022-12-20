using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyReporting.Models
{
    public class Manageusermodel
    {
        public Manage_User Manage_User { get; set; }
        public List<SelectListItem> Usertype { get; set; }
    }
}