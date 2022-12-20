using DailyReporting.Abstract;
using DailyReporting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyReporting.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        TASEntities db = new TASEntities();
        private readonly IApplicationRepository applicationRepository;

        public UserController(IApplicationRepository appRepository)
        {
            applicationRepository = appRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeDetails(RegstrationModel model, string[] Institution, string[] trainingType, string[] fdate, string[] tdate)
        {

            model.tasDetails.CreatedBy = Session["userid"].ToString();
            if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 1)
            {
                try
                {

                    List<TraingDetaisSS> pdetail = new List<TraingDetaisSS>();

                    if (Institution != null)
                    {
                        for (int i = 0; i < Institution.Length; i++)
                        {
                            TraingDetaisSS q = new TraingDetaisSS();
                            q.Institutionn = Convert.ToInt32(Institution[i]);
                            q.trainingTypee = Convert.ToInt32(trainingType[i]);
                            if (tdate[i] != "")
                            {
                                q.tdatee = tdate[i];
                            }
                            else
                            {
                                q.tdatee = null;
                            }
                            q.fdatee = fdate[i];



                            pdetail.Add(q);
                            q = new TraingDetaisSS();
                        }
                    }
                    model.TraingDetaisssss = pdetail.ToList();

                    int count = applicationRepository.SaveEmployeeDetails(model);
                    if (count == 1)
                    {
                        TempData["Message"] = "Registration Completed";
                    }

                    else
                    {
                        TempData["Message"] = "Registration Details Updated";
                    }
                    return RedirectToAction("EmployeeDetails", "User");
                }
                catch (Exception ex)
                {
                    TempData["WarningMessage"] = "Something Went Wrong";
                    return RedirectToAction("EmployeeDetails", "User");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");

            }

        }

        [HttpPost]
        public ActionResult Traintype(int cccc)
        {

            var list = db.Training_Type.Where(x => x.Tr_Institute_Id == cccc).ToList();


            List<SelectListItem> districtNames = new List<SelectListItem>();
            if (cccc != 0)
            {




                //List<Mst_MILLER> districts = con.Mst_MILLER.Where(x => x.doid == Distid).ToList();
                list.ForEach(x =>
                {
                    districtNames.Add(new SelectListItem { Text = x.Traing_Name, Value = x.Traing_Type_Id.ToString() });
                });
            }
            return Json(districtNames, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EmployeeDetails(int? idd = 0)
        {
            if (idd != 0)
            {
                //RegstrationModel dataa = new RegstrationModel();
                ViewBag.designation = applicationRepository.designation();
                ViewBag.trainingType = applicationRepository.trainingType();
                ViewBag.traininginstitute = applicationRepository.trainingInstitution();
                int startYear = 1990;
                var data = Enumerable.Range(startYear, DateTime.Now.Year - startYear + 1);
                List<SelectListItem> iteams = new List<SelectListItem>();
                iteams.Add(new SelectListItem { Text = "Select", Value = "0", });

                foreach (var item in data)
                {
                    iteams.Add(new SelectListItem
                    {
                        Text = item.ToString(),
                        Value = item.ToString()
                    });
                }
                ViewBag.year = iteams;

                //int startYear = 1990;
                ////var years = Enumerable.Range(DateTime.Now.Year, 10);
                //var years = Enumerable.Range(startYear, DateTime.Now.Year - startYear + 1);

                ////var select = new SelectList(years.Select(y => new SelectListItem()
                ////{
                ////    Text = y.ToString(),
                ////    Value = y.ToString()
                ////})).ToList();
                //ViewBag.year = years;

                //dataa.TraingIntend = applicationRepository.TrainingIntend();
                int id = Convert.ToInt32(Session["userid"]);
                ViewBag.EmpDtl = applicationRepository.Vw_Employee_Details().ToList();


                var Tasdetails = db.TAS_Details.ToList();
                var Manuser = db.Manage_User.ToList();
                var designation = db.Designations.ToList();
                var institute = db.Training_Institute.ToList();
                var Traingty = db.Training_Type.ToList();
                var traingdetails = db.TrainingDetails.ToList();

                var dataa = (from c in Tasdetails.Where(x => x.Tas_Id == idd)
                             select new RegstrationModel
                             {
                                 tasDetails = c,
                                 //Emp_Name = c.Emp_Name,
                                 Email = Manuser.Where(x => x.Tas_Id == idd).Select(y => y.Emaild).FirstOrDefault(),
                                 //DESIGNATION = (int)c.DesgId,
                                 //designationname = designation.Where(x => x.DesgId == c.DesgId).Select(y => y.DesgName).FirstOrDefault(),
                                 //Present_Post = c.Present_Post,
                                 //Edu_Qualification = c.Edu_Qualification,
                                 //Retirement_Date = c.Retirement_Date.ToString(),
                                 //Prv_Assignment = c.Prv_Assignment,
                                 //Agreed_For_Training = c.Agreed_For_Training,
                                 //TraingIntenddd = c.Tr_Intended_details,
                                 //Remarks = c.Remarks,


                             }).FirstOrDefault();

                return View(dataa);
            }


            else
            {
                if (Session["username"] != null && Convert.ToInt32(Session["usertype"]) == 1)
                {
                    try
                    {
                        RegstrationModel rgstmdl = new RegstrationModel();
                        ViewBag.designation = applicationRepository.designation();
                        ViewBag.trainingType = applicationRepository.trainingType();
                        ViewBag.traininginstitute = applicationRepository.trainingInstitution();
                        int startYear = 1990;
                        var data = Enumerable.Range(startYear, DateTime.Now.Year - startYear + 1);
                        List<SelectListItem> iteams = new List<SelectListItem>();
                        iteams.Add(new SelectListItem { Text = "Select", Value = "0", });

                        foreach (var item in data)
                        {
                            iteams.Add(new SelectListItem
                            {
                                Text = item.ToString(),
                                Value = item.ToString()
                            });
                        }
                        ViewBag.year = iteams;

                        //int startYear = 1990;
                        ////var years = Enumerable.Range(DateTime.Now.Year, 10);
                        //var years = Enumerable.Range(startYear, DateTime.Now.Year - startYear + 1);

                        ////var select = new SelectList(years.Select(y => new SelectListItem()
                        ////{
                        ////    Text = y.ToString(),
                        ////    Value = y.ToString()
                        ////})).ToList();
                        //ViewBag.year = years;

                        //rgstmdl.TraingIntend = applicationRepository.TrainingIntend();
                        int id = Convert.ToInt32(Session["userid"]);
                        ViewBag.EmpDtl = applicationRepository.Vw_Employee_Details().ToList();
                        return View(rgstmdl);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                }
            }

            //else
            //{
            //    return RedirectToAction("Login", "Home");
            //}
            return View();
        }

        public ActionResult viewTraing()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult EditEmployee(int id)
        //{
        //    var Tasdetails = db.TAS_Details.ToList();
        //    var Manuser = db.Manage_User.ToList();
        //    var designation = db.Designations.ToList();
        //    var institute = db.Training_Institute.ToList();
        //    var Traingty = db.Training_Type.ToList();
        //    var traingdetails = db.TrainingDetails.ToList();

        //    var data = (from c in Tasdetails.Where(x => x.Tas_Id == id)
        //                select new RegstrationModel
        //                {
        //                    Emp_Name = c.Emp_Name,
        //                    Email = Manuser.Where(x => x.Tas_Id == id).Select(y => y.Emaild).FirstOrDefault(),
        //                    DESIGNATION = (int)c.DesgId,
        //                    designationname= designation.Where(x=>x.DesgId==c.DesgId).Select(y=>y.DesgName).FirstOrDefault(),
        //                    Present_Post = c.Present_Post,
        //                    Edu_Qualification = c.Edu_Qualification,
        //                    Retirement_Date = c.Retirement_Date.ToString(),
        //                    Prv_Assignment = c.Prv_Assignment,
        //                    Agreed_For_Training = c.Agreed_For_Training,
        //                    TraingIntenddd = c.Tr_Intended_details,
        //                    Remarks = c.Remarks,


        //                }).FirstOrDefault();
        //    return Json(data);
        //}



        //Assign Training
        public ActionResult AssignTrainingDetails()
        {
            int id = Convert.ToInt32(Session["userid"]);
            ViewBag.TrAssgn = applicationRepository.Vw_User_Training_Assign(id)
;
            return View();
        }

        [HttpPost]
        public List<Assigntrainingmodel> AssignTrainingsearch(Assigntrainingmodel tm, string year, int designation, string TRTaken)
        {

            DateTime thisDate = DateTime.Today;


            DateTime thisDateNextYear;
            thisDateNextYear = thisDate.AddYears(1);


            int desg = Convert.ToInt32(designation);
            var data = db.TAS_Details.Where(c => c.Agreed_For_Training == "Yes" && c.Retirement_Date >= thisDateNextYear).ToList();

            if (desg == 0 && TRTaken == "No")
            {

                var data4 = db.TAS_Details.Where(c => c.Agreed_For_Training == "Yes" && c.Retirement_Date >= thisDateNextYear).ToList();
                var dsdsa = db.TrainingDetails.ToList();
                var datad = (from user in data4
                             join c in db.TrainingDetails on user.Tas_Id equals c.Tas_Id
                             where c.Traing_Type_Id == 0 && c.Tr_Institute_Id == 0
                             select new TAS_Details()
                             {
                                 Tas_Id = user.Tas_Id,
                                 Emp_Name = user.Emp_Name,
                                 DesgId = user.DesgId,
                                 Retirement_Date = user.Retirement_Date,
                                 Present_Post = user.Present_Post,
                             }).ToList();

                data = datad;

            }
            else if (desg != 0 && TRTaken == "Yes")
            {
                 data = db.TAS_Details.Where(c => c.Agreed_For_Training == "Yes" && c.DesgId == desg && c.Retirement_Date >= thisDateNextYear).ToList();
            }
            else if (desg != 0 && TRTaken == "No")
            {

                var data4 = db.TAS_Details.Where(c => c.Agreed_For_Training == "Yes" && c.DesgId == desg && c.Retirement_Date >= thisDateNextYear).ToList();
                var dsdsa = db.TrainingDetails.ToList();
                var datad = (from user in data4
                             join c in  db.TrainingDetails on user.Tas_Id equals c.Tas_Id where c.Traing_Type_Id==0 && c.Tr_Institute_Id==0
                             select new TAS_Details()
                             {
                                 Tas_Id = user.Tas_Id,
                                 Emp_Name = user.Emp_Name,
                                 DesgId = user.DesgId,
                                 Retirement_Date = user.Retirement_Date,
                                 Present_Post = user.Present_Post,
                             }).ToList();
                //var datad = (from user in data4
                //             where !dsdsa.Any(f => f.Tas_Id == user.Tas_Id)
                //             select new TAS_Details()
                //             {
                //                 Tas_Id = user.Tas_Id,
                //                 Emp_Name = user.Emp_Name,
                //                 DesgId = user.DesgId,
                //                 Retirement_Date = user.Retirement_Date,
                //                 Present_Post = user.Present_Post,
                //             }).ToList();
                data = datad;
            }
            else
            {
                data = db.TAS_Details.Where(c => c.Agreed_For_Training == "Yes" && c.Retirement_Date >= thisDateNextYear).ToList();
            }


            int date = DateTime.Now.Year;
            int date1 = DateTime.Now.Year - 5;
            int date2 = DateTime.Now.Year - 10;
            var gtt = Convert.ToInt32(year);

            var data1 = db.TrainingDetails.Where(c => c.Traing_Type_Id == tm.TraingAssign.Traing_Type_Id).GroupBy(x => x.Tas_Id).Select(grp => grp.FirstOrDefault().Tas_Id).ToList();
            if (TRTaken != "No")
            {
                if (gtt == 1)
                {
                    data1 = db.TrainingDetails.Where(c => c.Traing_Type_Id == tm.TraingAssign.Traing_Type_Id && c.Year >= date1 && c.Year <= date).GroupBy(x => x.Tas_Id).Select(grp => grp.FirstOrDefault().Tas_Id).ToList();

                }
                else if (gtt == 2)
                {
                    data1 = db.TrainingDetails.Where(c => c.Traing_Type_Id == tm.TraingAssign.Traing_Type_Id && c.Year >= date2 && c.Year <= date).GroupBy(x => x.Tas_Id).Select(grp => grp.FirstOrDefault().Tas_Id).ToList();

                }
                else
                {
                    data1 = db.TrainingDetails.Where(c => c.Traing_Type_Id == tm.TraingAssign.Traing_Type_Id).GroupBy(x => x.Tas_Id).Select(grp => grp.FirstOrDefault().Tas_Id).ToList();

                }
            }
            else
            {
                var dtaa3 = (from a in data

                             join c in db.Manage_User on a.Tas_Id equals c.Tas_Id
                             select new Assigntrainingmodel
                             {
                                 Tas_Id = a.Tas_Id,
                                 UserId = c.UserId,
                                 Emp_Name = a.Emp_Name,
                                 Retirement_Date = Convert.ToDateTime(a.Retirement_Date),
                                 desgnation = a.Present_Post,
                                 email = c.Emaild
                             }).ToList();
                return dtaa3;
            }



            if (data1.Count == 0)
            {
                var dtaa3 = (from a in data

                             join c in db.Manage_User on a.Tas_Id equals c.Tas_Id
                             select new Assigntrainingmodel
                             {
                                 Tas_Id = a.Tas_Id,
                                 UserId = c.UserId,
                                 Emp_Name = a.Emp_Name,
                                 Retirement_Date = Convert.ToDateTime(a.Retirement_Date),
                                 desgnation = a.Present_Post,
                                 email = c.Emaild
                             }).ToList();
                return dtaa3;
            }
            else
            {
                var dtaa3 = (from a in data
                             join b in db.TrainingDetails.Where(x => !data1.Contains(x.Tas_Id)) on a.Tas_Id equals b.Tas_Id
                             join c in db.Manage_User on a.Tas_Id equals c.Tas_Id
                             select new Assigntrainingmodel
                             {
                                 Tas_Id = a.Tas_Id,
                                 UserId = c.UserId,
                                 Emp_Name = a.Emp_Name,
                                 Retirement_Date = Convert.ToDateTime(a.Retirement_Date),
                                 desgnation = a.Present_Post,
                                 email = c.Emaild
                             }).OrderByDescending(x => x.Tas_Id).ToList();
                return dtaa3;

            }



        }

        public ActionResult AssignTraining(Assigntrainingmodel tm, string btn)
        {

            ViewBag.traininginstitute = applicationRepository.trainingInstitution();
            ViewBag.trainingType = applicationRepository.trainingType();
            ViewBag.designation = applicationRepository.designation();
            var data = db.TAS_Details.Where(c => c.Agreed_For_Training == "Yes").ToList();
            //var data1 = (from a in data
            //             join b in db.Manage_User on a.Tas_Id equals b.Tas_Id
            //             join c in db.Designations on a.DesgId equals c.DesgId
            //             select new Assigntrainingmodel
            //             {
            //                 Tas_Id = a.Tas_Id,
            //                 UserId = b.UserId,
            //                 Emp_Name = a.Emp_Name,
            //                 Retirement_Date = Convert.ToDateTime(a.Retirement_Date),
            //                 desgnation = c.DesgName,
            //                 email = b.Emaild
            //             }).ToList();
            ViewBag.Empname = null;

            int startYear = 1990;
            var data1 = Enumerable.Range(startYear, DateTime.Now.Year - startYear + 1);
            List<SelectListItem> iteams = new List<SelectListItem>();
            iteams.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data1)
            {
                iteams.Add(new SelectListItem
                {
                    Text = item.ToString(),
                    Value = item.ToString()
                });
            }
            ViewBag.year = iteams;

            return View();
        }

        [HttpPost]
        public ActionResult AssignTraining(Assigntrainingmodel tm, string btn, string[] Applicationid, string[] Tasid, string year, string TRTaken)
        {

            ViewBag.traininginstitute = applicationRepository.trainingInstitution();
            ViewBag.trainingType = applicationRepository.trainingType();
            ViewBag.designation = applicationRepository.designation();
            int designation = tm.desgiddddd;
            if (btn == "search")
            {
                var data = AssignTrainingsearch(tm, year, designation, TRTaken);
                ViewBag.Empname = data;
                ViewBag.xxx = tm.TraingAssign.Traing_Type_Id;
                ViewBag.fiveyears = year;
                ViewBag.trtaken = TRTaken;
                return View(tm)
;
            }
            else if (btn == "submit")
            {
                int data = savedata(tm, Applicationid, Tasid);
                if (data == 1)
                {
                    TempData["Message"] = "Training Assigned Successfully";
                    return RedirectToAction("AssignTraining");
                }
                else
                {
                    TempData["Message"] = "Something went wrong";
                    return RedirectToAction("AssignTraining");
                }
            }
            else
            {
                var data = db.TAS_Details.Where(c => c.Agreed_For_Training == "Yes").ToList();
                var data1 = (from a in data
                             join b in db.Manage_User on a.Tas_Id equals b.Tas_Id
                             join c in db.Designations on a.DesgId equals c.DesgId
                             select new Assigntrainingmodel
                             {
                                 Tas_Id = a.Tas_Id,
                                 UserId = b.UserId,
                                 Emp_Name = a.Emp_Name,
                                 Retirement_Date = Convert.ToDateTime(a.Retirement_Date),
                                 desgnation = c.DesgName,
                                 email = b.Emaild,

                             }).ToList();
                ViewBag.Empname = data1;
                return View();
            }
        }

        public int savedata(Assigntrainingmodel tm, string[] Applicationid, string[] Tasid)
        {
            List<ListAssign> pdetail = new List<ListAssign>();

            if (Applicationid != null)
            {
                for (int i = 0; i < Applicationid.Length; i++)
                {
                    ListAssign q = new ListAssign();
                    q.Tas_Id = Convert.ToInt32(Tasid[i]);
                    q.UserId = Convert.ToInt32(Applicationid[i]);
                    pdetail.Add(q);
                    q = new ListAssign();
                }
                tm.ListAssign = pdetail.ToList();
                int count = applicationRepository.SaveAssignTraining(tm)
;
                return count;
            }
            else
            {
                return 2;
            }
        }


        //Feedback
        [HttpPost]
        public ActionResult Feedback(Feedback feedback)
        {
            int id = Convert.ToInt32(Session["userid"]);
            feedback.CreatedBy = id.ToString();
            int count = applicationRepository.SaveFeedback(feedback);
            if (count == 1)
            {
                TempData["Message"] = "Feedback Submitted Successfully";
                return RedirectToAction("Feedback", "User");
            }
            else
            {
                TempData["Message"] = "Feedback Updated Successfully";
                return RedirectToAction("Feedback", "User");
            }
        }

        [HttpGet]
        public ActionResult Feedback()
        {
            ViewBag.trainingType = applicationRepository.trainingType();
            ViewBag.traininginstitute = applicationRepository.trainingInstitution();
            return View();
        }


    }
}