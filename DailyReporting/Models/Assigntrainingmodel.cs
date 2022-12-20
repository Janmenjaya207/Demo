using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyReporting.Models
{
    public class Assigntrainingmodel
    {
        public TraingAssign TraingAssign { get; set; }
        public TrainingAssignDetail TrainingAssignDetail { get; set; }
        public int Tas_Id { get; set; }
        public int UserId { get; set; }
        public string Emp_Name { get; set; }
        public DateTime Retirement_Date { get; set; }
        public string desgnation { get; set; }
        public string email { get; set; }
        public int Instiid { get; set; }
        public int typeid { get; set; }
        public int desgiddddd { get; set; }


        public List<ListAssign> ListAssign { get; set; }
    }
    public class ListAssign
    {
        public int Tas_Id { get; set; }
        public int UserId { get; set; }
        public string Emp_Name { get; set; }
    }
    
}