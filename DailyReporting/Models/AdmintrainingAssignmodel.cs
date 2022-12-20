using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyReporting.Models
{
    public class AdmintrainingAssignmodel
    {

        public List<Vw_Admin_Training_Assign_Details> Vw_Admin_Training_Assign_Details { get; set; }
       public sdasda sfaa { get; set; }
    }
    public class sdasda
    {
        public string Instname { get; set; }
        public string instype { get; set; }
        public DateTime fdate { get; set; }
        public DateTime tdate { get; set; }

    }
}