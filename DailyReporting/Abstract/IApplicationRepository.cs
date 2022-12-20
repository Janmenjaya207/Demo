using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DailyReporting.Models;

namespace DailyReporting.Abstract
{
    public interface IApplicationRepository
    {
        int SaveEmployeeDetails(RegstrationModel regstrationModel);
        List<Vw_Employee_Details> Vw_Employee_Details();
        List<SelectListItem> designation();
        List<SelectListItem> trainingType();
        List<SelectListItem> trainingInstitution();
        List<SelectListItem> TrainingIntend();
        List<Vw_Employee_Details_Report> Vw_Employee_DetailsReport();
        List<SelectListItem> Usertype();
        TAS_Details GetTAS_Details(int id);

        int SaveManageUser(Manageusermodel mng);
        List<Vw_Manage_User> Vw_Manage_Users();
        Manage_User GetManage_User(int id);
        int DeleteManageUser(int id);

        List<Designation> Designations();
        int SaveDesignation(Designation designation);
        Designation GetDesignation(int id);
        int DeleteDesignation(int id);

        List<User_Type> User_Types();
        int SaveUser_Type(User_Type user_Type);
        User_Type GetUser_Type(int id);
        int DeleteUser_Type(int id);

        List<Training_Institute> Training_Institutes();
        int SaveTraining_Institute(Training_Institute training_Institute);
        Training_Institute GetTraining_Institute(int id);
        int DeleteTraining_Institute(int id);

        List<Training_Intended> Training_Intendeds();
        int SaveTraining_Intendeds(Training_Intended training_Intended);
        Training_Intended GetTraining_Intended(int id);
        int DeleteTraining_Intended(int id);


        List<Training_Type> Training_Types();
        List<SelectListItem> Traing_Institutelist();

        int SaveTraingType(Trainingtypemodel nu);
        List<Vw_Training_Type> vw_Training_Types();
        Training_Type GetTraining_Type(int id);
        int DeleteTraining_Type(int id);
        List<vw_employee_institute> vw_employee_institute(int id);
        int SaveAssignTraining(Assigntrainingmodel nu);
        List<Vw_Training_Assign> Vw_Training_Assigns(int id);
        List<Vw_Admin_Training_Assign> vw_Admin_Training_Assigns();
        List<Vw_Admin_Training_Assign_Details> Vw_Admin_Training_Assign_Details(int id);
        List<Vw_User_Training_Assign> Vw_User_Training_Assign(int id);
        int SaveFeedback(Feedback fed);
        List<Vw_Feedback> vw_Feedbacks();
    }
}
