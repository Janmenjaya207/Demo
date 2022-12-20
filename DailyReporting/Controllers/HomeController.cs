using DailyReporting.Abstract;
using DailyReporting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DailyReporting.Controllers
{
    public class HomeController : Controller
    {

        TASEntities db = new TASEntities();

        private readonly IApplicationRepository applicationRepository;

        public HomeController(IApplicationRepository appRepository)
        {
            applicationRepository = appRepository;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            var data1 = db.Manage_User.ToList();
            var data = db.Manage_User.Where(x => x.UserName == username && x.Password == password && x.IsActive == true).FirstOrDefault();
            if (data != null)
            {
                if (data.UserTypeId == 1 && data.IsActive == true)
                {

                    Session["userid"] = data.UserId;
                    Session["username"] = data.UserName;
                    Session["usertype"] = data.UserTypeId;
                    //Session["empname"] = data.EmployeeName;
                    //Session["Sectionid"] = data.Section_id;
                    return RedirectToAction("Dashboard", "Home");
                }
                else if (data.IsActive == true && data.UserTypeId != 1)
                {
                    Session["userid"] = data.UserId;
                    Session["username"] = data.UserName;
                    Session["usertype"] = data.UserTypeId;
                    //Session["empname"] = data.EmployeeName;
                    //Session["Sectionid"] = data.Section_id;
                    return RedirectToAction("Dashboard", "Home");
                }

                else
                {
                    TempData["WarningMessage"] = "User not yet registered";
                    return View();
                }
            }
            else
            {
                Session.Abandon();
                TempData["WarningMessage"] = "Username or password is incorrect";
                return View();
            }
        }

        public ActionResult Dashboard()
        {

            int id = Convert.ToInt32(Session["userid"]);
            ViewBag.count = db.Vw_Training_Assign.Where(x => x.Userid == id).ToList().Count;
            ViewBag.data = Session["usertype"];
            return View();
        }

        public ActionResult signup()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult resetpassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult resetpassword(string username, string confpassword)
        {
            try
            {
                var data = db.Manage_User.Where(x => x.UserName == username).FirstOrDefault();
                if (data != null)
                {
                    data.Password = confpassword;
                    db.SaveChanges();
                    TempData["Message"] = "Password Reset Successfully";
                    return View();

                }
                else
                {
                    return RedirectToAction("resetpassword", "Home");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult AdminReport()
        {
            if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 1)
            {
                try
                {
                    ViewBag.trainingType = applicationRepository.trainingType();
                    ViewBag.traininginstitute = applicationRepository.trainingInstitution();
                    ViewBag.report = applicationRepository.Vw_Employee_DetailsReport();
                    return View();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        [HttpPost]
        public ActionResult AdminReport(string traininginstitute,string trainingType, string fdate, string tdate)
        {
            if (Session["username"] != null)
            {
                try
                {
                    ViewBag.trainingType = applicationRepository.trainingType();
                    ViewBag.traininginstitute = applicationRepository.trainingInstitution();
                    var data = applicationRepository.Vw_Employee_DetailsReport().ToList();
                    if (trainingType == "" && traininginstitute == "")
                    {
                        data = applicationRepository.Vw_Employee_DetailsReport().ToList();

                    }
                    if (trainingType != "" && traininginstitute == "")
                    {
                      
                        int typeid = Convert.ToInt32(trainingType);
                        data = data.Where(x => x.Traing_Type_Id == typeid).ToList();

                    }
                    else if (traininginstitute != "" && trainingType == "")
                    {
                        int trid = Convert.ToInt32(traininginstitute);
                       
                        data = data.Where(x => x.Tr_Institute_Id == trid).ToList();
                    }
                    else if (traininginstitute != "" && trainingType != "")
                    {
                        int trid = Convert.ToInt32(traininginstitute);
                        int typeid = Convert.ToInt32(trainingType);
                        data = data.Where(x => x.Tr_Institute_Id == trid & x.Traing_Type_Id == typeid).ToList();
                    }

                    else
                    {
                        data = data.ToList();
                    }
                    ViewBag.report = data;
                    return View();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        public ActionResult ManageUser()
        {
            if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 1)
            {
                Manageusermodel mj = new Manageusermodel();
                try
                {
                    mj.Usertype = applicationRepository.Usertype();
                    ViewBag.mngdtl = applicationRepository.Vw_Manage_Users().ToList();
                    return View(mj);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                return RedirectToAction("Home", "Login");
            }

        }

        [HttpPost]
        public ActionResult ManageUser(Manageusermodel gt)
        {
            int count = applicationRepository.SaveManageUser(gt);
            if (count == 1)
            {
                TempData["Message"] = "User Created Successfully";
            }
            else
            {
                TempData["Message"] = "User Updated Successfully";
            }
            return RedirectToAction("ManageUser", "Home");
        }

        [HttpPost]
        public ActionResult EditManageUser(int id)
        {
            var data = applicationRepository.GetManage_User(id);
            return Json(data);
        }

        public ActionResult DeleteManageUser(int id)
        {
            var data = applicationRepository.DeleteManageUser(id);
            TempData["warningMessage"] = "User Deleted Successfully";
            return RedirectToAction("ManageUser", "Home");
        }


        //Designation
        [HttpGet]
        public ActionResult Designation()
        {
            ViewBag.data = applicationRepository.Designations();
            return View();
        }

        [HttpPost]
        public ActionResult Designation(Designation designation)
        {
            int count = applicationRepository.SaveDesignation(designation);
            if (count == 1)
            {
                TempData["Message"] = "Desingnation Created Successfully";
                return RedirectToAction("Designation", "Home");
            }
            else
            {
                TempData["Message"] = "Desingnation Updated Successfully";
                return RedirectToAction("Designation", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditDesignation(int id)
        {
            var data = applicationRepository.GetDesignation(id);
            return Json(data);
        }

        public ActionResult DeleteDesignation(int id)
        {
            var data = applicationRepository.DeleteDesignation(id);
            TempData["warningMessage"] = "Designation Deleted Successfully";
            return RedirectToAction("Designation", "Home");
        }



        //User_Type
        [HttpGet]
        public ActionResult User_Type()
        {
            ViewBag.data = applicationRepository.User_Types();
            return View();
        }

        [HttpPost]
        public ActionResult User_Type(User_Type user_Type)
        {
            int count = applicationRepository.SaveUser_Type(user_Type);
            if (count == 1)
            {
                TempData["Message"] = "User Type Created Successfully";
                return RedirectToAction("User_Type", "Home");
            }
            else
            {
                TempData["Message"] = "User_Type Updated Successfully";
                return RedirectToAction("User_Type", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditUser_Type(int id)
        {
            var data = applicationRepository.GetUser_Type(id);
            return Json(data);
        }

        public ActionResult DeleteUser_Type(int id)
        {
            var data = applicationRepository.DeleteUser_Type(id);
            TempData["warningMessage"] = "User Type Deleted Successfully";
            return RedirectToAction("User_Type", "Home");
        }



        //Training_Institute
        [HttpGet]
        public ActionResult Training_Institute()
        {
            ViewBag.data = applicationRepository.Training_Institutes();
            return View();
        }

        [HttpPost]
        public ActionResult Training_Institute(Training_Institute training_Institute, string Type)
        {
            training_Institute.Insti_Type = Type;
            int count = applicationRepository.SaveTraining_Institute(training_Institute);
            if (count == 1)
            {
                TempData["Message"] = "Training Institute Created Successfully";
                return RedirectToAction("Training_Institute", "Home");
            }
            else
            {
                TempData["Message"] = "Training_Institute Updated Successfully";
                return RedirectToAction("Training_Institute", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditTraining_Institute(int id)
        {
            var data = applicationRepository.GetTraining_Institute(id);
            return Json(data);
        }

        public ActionResult DeleteTraining_Institute(int id)
        {
            var data = applicationRepository.DeleteTraining_Institute(id);
            TempData["warningMessage"] = "Training Institute Deleted Successfully";
            return RedirectToAction("Training_Institute", "Home");
        }



        //Training_Intended
        [HttpGet]
        public ActionResult Training_Intended()
        {
            ViewBag.data = applicationRepository.Training_Intendeds();
            return View();
        }

        [HttpPost]
        public ActionResult Training_Intended(Training_Intended training_Intended)
        {
            int count = applicationRepository.SaveTraining_Intendeds(training_Intended);
            if (count == 1)
            {
                TempData["Message"] = "Training Intended Created Successfully";
                return RedirectToAction("Training_Intended", "Home");
            }
            else
            {
                TempData["Message"] = "Training Intended Updated Successfully";
                return RedirectToAction("Training_Intended", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditTraining_Intended(int id)
        {
            var data = applicationRepository.GetTraining_Intended(id);
            return Json(data);
        }

        public ActionResult DeleteTraining_Intended(int id)
        {
            var data = applicationRepository.DeleteTraining_Intended(id);
            TempData["warningMessage"] = "Training Intended Deleted Successfully";
            return RedirectToAction("Training_Intended", "Home");
        }


        //Training Type
        [HttpGet]
        public ActionResult Trainingtype(int? id = 0)
        {
            Trainingtypemodel Tp = new Trainingtypemodel();
            try
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 1)
                {
                    //                    if (id != 0)
                    //                    {
                    //                        int sid = Convert.ToInt32(id)
                    //;
                    //                        var data = applicationRepository.GetTraining_Type(sid);
                    //                        Tp.Tr_Institute_Name
                    //                    }
                    ViewBag.trtyp = applicationRepository.vw_Training_Types().ToList();
                    Tp.ListTraingName = applicationRepository.Traing_Institutelist();
                    return View(Tp);
                }
                else
                {

                    return RedirectToAction("Login", "Home");
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Trainingtype(Trainingtypemodel nh)
        {
            int count = applicationRepository.SaveTraingType(nh);
            if (count == 1)
            {
                TempData["Message"] = "Training Type Created Successfully";
                return RedirectToAction("Trainingtype", "Home");
            }
            else
            {
                TempData["Message"] = "Training Type Updated Successfully";
                return RedirectToAction("Trainingtype", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditTrainingtype(int id)
        {
            var data = applicationRepository.GetTraining_Type(id);
            return Json(data);
        }

        public ActionResult DeleteTrainingtype(int id)
        {
            var data = applicationRepository.DeleteTraining_Type(id);
            TempData["warningMessage"] = "Training Type Deleted Successfully";
            return RedirectToAction("TrainingType", "Home");
        }


        public ActionResult ViewAssignedTraining()
        {
            ViewBag.AdminTrAssgn = applicationRepository.vw_Admin_Training_Assigns();
            return View();
        }
        
        //[HttpPost]
        //   public ActionResult ViewAssignedTrainingDeatils(int id)
        //   {
        //       AdmintrainingAssignmodel nh = new AdmintrainingAssignmodel();

        //       nh.vw_Admin_Training_Assign_Details = applicationRepository.Vw_Admin_Training_Assign_Details(id);

        //       return PartialView("_ViewAssigntrainingDetailsPartial", nh);
        //   }

        public bool SendMail(string to, string subject, string body)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress("transactiondomain@gmail.com");
            mail.Subject = subject;
            string Body = body;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("transactiondomain@gmail.com", "zaienkklwnmholof"); // Enter seders User name and password       
            smtp.EnableSsl = true;
            smtp.Send(mail)
;

            return true;
        }

        
        [HttpGet]
        public ActionResult ViewAssignedTrainingDeatilss(int id)
         {
         
            var data = db.TraingAssigns.Where(x => x.Tr_AssignID == id).ToList();
          var dta1=  (from a in data

             join c in db.Training_Institute on a.Tr_Institute_Id equals c.Tr_Institute_Id
             join n in db.Training_Type on a.Traing_Type_Id equals n.Traing_Type_Id
             select new sdasda
             {
                 Instname = c.Tr_Institute_Name,
                 instype = n.Traing_Name,
                 fdate = Convert.ToDateTime(a.Trfdate),
                tdate=Convert.ToDateTime(a.Trtdate),

             }).FirstOrDefault();


            AdmintrainingAssignmodel nh = new AdmintrainingAssignmodel();
            nh.sfaa = dta1;
            nh.Vw_Admin_Training_Assign_Details = applicationRepository.Vw_Admin_Training_Assign_Details(id);
            ViewBag.data = db.TrainingAssignDetails.Where(x => x.Tr_AssignID == id && x.IsEmail == false).FirstOrDefault();
            return View(nh);
        }

        [HttpPost]
        public ActionResult ViewAssignedTrainingDeatilss(string [] email,string [] Tr_AssignID, string Userid)
        {

            if (email[0]!= "")
            {
                foreach(var item in email)
                {
                    string sub = "Training Proposal";
                    var body = new StringBuilder();
                    body.AppendFormat("Dear {0}\n", item + ",");
                    //body.AppendFormat("Dear Candidate,");
                    body.AppendLine("<br/>");

                    body.AppendLine(@"You are Successfully invited for training.");
                    body.AppendLine("<br/>");
                    //body.AppendLine(@"Use This User Name & Password For LogIn Purpose");
                    //body.AppendLine("<br/>");

                    body.AppendLine("<br/>");
                    body.AppendLine("<br/>");
                    body.AppendLine("<br/>");
                    body.Append("<b>");
                    body.Append("Regards,");
                    body.AppendLine("<br/>");
                    body.Append("Co-Operative Society");
                    body.Append("</b>");

                    body.AppendLine("<br/>");


                    string mailbodyy = body.ToString();
                    SendMail(item, sub, body.ToString());
                    foreach (var item1 in Tr_AssignID)
                    {
                        int id = Convert.ToInt32(item1);

                        var data = db.TrainingAssignDetails.Where(x => x.Tr_AssignID == id && x.IsEmail == false).FirstOrDefault();
                        if (data != null)
                        {
                            data.IsEmail = true;
                            db.SaveChanges();
                        }
                       

                    }
                    
                }
                
            }
           
            return RedirectToAction("ViewAssignedTraining", "Home");
        }
        
        //Feedback
        public ActionResult ViewFeedback()
        {
            ViewBag.traininginstitute = applicationRepository.trainingInstitution();
            var data = applicationRepository.vw_Feedbacks();
            ViewBag.FeedList = data;
            return View();
        }

        [HttpPost]
        public ActionResult ViewFeedback(FeedbackReport nh)
        {
            ViewBag.traininginstitute = applicationRepository.trainingInstitution();
            var data = applicationRepository.vw_Feedbacks();
            if (nh.instiid != 0)
            {
                data = data.Where(x => x.Tr_Institute_Id == nh.instiid).ToList();
            }
            else
            {
                data = data.ToList();
            }
            ViewBag.FeedList = data;
            return View();
        }
    }
}

