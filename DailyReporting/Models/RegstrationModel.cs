using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyReporting.Models
{
    public class RegstrationModel
    {
        public TAS_Details tasDetails { get; set; }
        public Designation DesignationSSSSS { get; set; }

        public string Emp_Name { get; set; }
        public string Present_Post { get; set; }
        public string Edu_Qualification { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string Retirement_Date { get; set; }
        public string Prv_Assignment { get; set; }
        public string Tr_Period_From { get; set; }
        public string Tr_Period_To { get; set; }
        public string Remarks { get; set; }
        public int Traing_Type_Id { get; set; }
        public int Tr_Institute_Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int DESIGNATIONnnnn { get; set; }

        public string designationname { get; set; }
        public List<SelectListItem> designation { get; set; }
        public List<SelectListItem> TraingIntend { get; set; }


        public List<Vw_Employee_Details> Vw_Employee_Details { get; set; }
        public List<TraingDetaisSS> TraingDetaisssss { get; set; }
       
        public string Agreed_For_Training { get; set; }

        public string TraingIntenddd { get; set; }
    }
    public class TraingDetaisSS
    {
        public int Institutionn { get; set; }
        public int trainingTypee { get; set; }

        public string fdatee { get; set; }
        public string tdatee { get; set; }
    }
}