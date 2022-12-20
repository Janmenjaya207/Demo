using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyReporting.Abstract;
using DailyReporting.Models;

namespace DailyReporting.Concrete
{
    public class ApplicationRepository : IApplicationRepository
    {
        TASEntities db = new TASEntities();
        public int SaveEmployeeDetails(RegstrationModel regstrationModel)
        {
            try
            {
                int count = 0;
                if (regstrationModel.tasDetails.Tas_Id != 0 && regstrationModel.tasDetails.Tas_Id != null)
                {
                    var obj = db.TAS_Details.Where(x => x.Tas_Id == regstrationModel.tasDetails.Tas_Id).FirstOrDefault();
                    obj.Emp_Name = regstrationModel.tasDetails.Emp_Name;
                    obj.Present_Post = regstrationModel.tasDetails.Present_Post;
                    obj.Edu_Qualification = regstrationModel.tasDetails.Edu_Qualification;
                    obj.Retirement_Date = regstrationModel.tasDetails.Retirement_Date;
                    obj.Prv_Assignment = regstrationModel.tasDetails.Prv_Assignment;
                    //obj.Tr_Period_From = regstrationModel.tasDetails.Tr_Period_From;
                    //obj.Tr_Period_To = regstrationModel.tasDetails.Tr_Period_To;
                    obj.Agreed_For_Training = regstrationModel.tasDetails.Agreed_For_Training;
                    obj.Remarks = regstrationModel.tasDetails.Remarks;
                    obj.IsActive = true;
                    obj.IsDelete = false;
                    obj.ModifiedBy = regstrationModel.tasDetails.ModifiedBy;
                    obj.ModifiedOn = DateTime.Now;
                    obj.DesgId = regstrationModel.tasDetails.DesgId;
                    db.SaveChanges();
                    var data = db.Manage_User.Where(x => x.Tas_Id == regstrationModel.tasDetails.Tas_Id).FirstOrDefault();
                    data.Emaild = regstrationModel.Email;
                    data.UserName = regstrationModel.Email;
                    data.FirstName = regstrationModel.tasDetails.Emp_Name;
                    db.SaveChanges();

                  
                    count = 2;
                }
                else
                {
                    TAS_Details obj = new TAS_Details();
                    obj.Emp_Name = regstrationModel.tasDetails.Emp_Name;
                    obj.Present_Post = regstrationModel.tasDetails.Present_Post;
                    obj.Edu_Qualification = regstrationModel.tasDetails.Edu_Qualification;
                    obj.Retirement_Date = regstrationModel.tasDetails.Retirement_Date;
                    obj.Prv_Assignment = regstrationModel.tasDetails.Prv_Assignment;
                    obj.DesgId = regstrationModel.tasDetails.DesgId;
                    obj.Tr_Intended_Id = regstrationModel.tasDetails.Tr_Intended_Id;
                    //obj.Tr_Period_From = regstrationModel.tasDetails.Tr_Period_From;
                    //obj.Tr_Period_To = regstrationModel.tasDetails.Tr_Period_To;
                    obj.Agreed_For_Training = regstrationModel.tasDetails.Agreed_For_Training;
                    obj.Remarks = regstrationModel.tasDetails.Remarks;
                    obj.Tr_Intended_details = regstrationModel.tasDetails.Tr_Intended_details;
                    obj.IsActive = true;
                    obj.IsDelete = false;
                    obj.CreatedBy = regstrationModel.tasDetails.CreatedBy;
                    obj.CreatedOn = DateTime.Now;
                    db.TAS_Details.Add(obj);
                    db.SaveChanges();
                    if (regstrationModel.TraingDetaisssss != null)
                    {
                        decimal sum = 0;
                        foreach (var item in regstrationModel.TraingDetaisssss)
                        {
                            TrainingDetail r = new TrainingDetail();
                            r.Tas_Id = obj.Tas_Id;
                            r.Traing_Type_Id = item.trainingTypee;
                            r.Tr_Institute_Id = item.Institutionn;
                            r.Year =Convert.ToInt32( item.fdatee);
                            r.Days = Convert.ToInt32(item.tdatee);
                            r.IsActive = true;
                            r.IsDelete = false;
                            db.TrainingDetails.Add(r);
                            db.SaveChanges();

                        }
                        count = 1;
                    }

                    Manage_User mn = new Manage_User();
                    mn.FirstName = obj.Emp_Name;
                    mn.Emaild = regstrationModel.Email;
                    mn.UserTypeId = 2;
                    mn.UserName = regstrationModel.Email;
                    mn.Password = "12345";
                    mn.Tas_Id = obj.Tas_Id;
                    mn.IsActive = true;
                    mn.IsDelete = false;
                    db.Manage_User.Add(mn);
                    db.SaveChanges();

                    //var data = db.TAS_Details.Where(x => x.Tas_Id == mn.Tas_Id).FirstOrDefault();
                    //data.Tas_User_Id = mn.UserId;
                    //db.SaveChanges();
              
                   
                }
                return count;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TAS_Details GetTAS_Details(int id)
        {
            var data = db.TAS_Details.Where(x => x.Tas_Id == id).FirstOrDefault();
            //var data1 = db.Manage_User.Where(x => x.UserId == id).FirstOrDefault();
            return data;
        }

        public List<Vw_Employee_Details> Vw_Employee_Details()
        {
            var data = db.Vw_Employee_Details.ToList();
            return data;
        }

        public List<Vw_Employee_Details_Report> Vw_Employee_DetailsReport()
        {
            var data = db.Vw_Employee_Details_Report.ToList();
            return data;
        }

        public List<SelectListItem> designation()
        {
            var data = db.Designations.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> iteams = new List<SelectListItem>();
            iteams.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                iteams.Add(new SelectListItem
                {
                    Text = item.DesgName,
                    Value = item.DesgId.ToString(),
                });
            }
            return iteams;
        }

        public List<SelectListItem> trainingType()
        {
            var data = db.Training_Type.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> iteams = new List<SelectListItem>();
            iteams.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                iteams.Add(new SelectListItem
                {
                    Text = item.Traing_Name,
                    Value = item.Traing_Type_Id.ToString(),
                });
            }
            return iteams;
        }

        public List<SelectListItem> trainingInstitution()
        {
            var data = db.Training_Institute.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> iteams = new List<SelectListItem>();
            iteams.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                iteams.Add(new SelectListItem
                {
                    Text = item.Tr_Institute_Name,
                    Value = item.Tr_Institute_Id.ToString(),
                });
            }
            return iteams;
        }

        public List<SelectListItem> TrainingIntend()
        {
            var data = db.Training_Intended.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> iteams = new List<SelectListItem>();
            iteams.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                iteams.Add(new SelectListItem
                {
                    Text = item.Tr_Intended_Name,
                    Value = item.Tr_Intended_Id.ToString(),
                });
            }
            return iteams;
        }

        public List<SelectListItem> Usertype()
        {
            var data = db.User_Type.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> iteams = new List<SelectListItem>();
            iteams.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                iteams.Add(new SelectListItem
                {
                    Text = item.UserTypeName,
                    Value = item.UserTypeId.ToString(),
                });
            }
            return iteams;
        }

        public int SaveManageUser(Manageusermodel mng)
        {
            
            try
            {
                int count = 0;
                if (mng.Manage_User.UserId !=0 && mng.Manage_User.UserId != null)
                {
                    var obj= db.Manage_User.Where(x => x.UserId == mng.Manage_User.UserId).FirstOrDefault();
                    obj.FirstName = mng.Manage_User.FirstName;
                    obj.LastName = mng.Manage_User.LastName;
                    obj.UserTypeId = mng.Manage_User.UserTypeId;
                    obj.Address = mng.Manage_User.Address;
                    obj.MobileNo = mng.Manage_User.MobileNo;
                    obj.Emaild = mng.Manage_User.Emaild;
                    obj.UserName = mng.Manage_User.UserName;
                    obj.Password = mng.Manage_User.Password;
                    obj.IsActive = true;
                    obj.IsDelete = false;
                    obj.ModifiedOn = DateTime.Now;
                    db.SaveChanges();
                    count = 2;
                }
                else
                {
                    Manage_User obj = new Manage_User();
                    obj.FirstName = mng.Manage_User.FirstName;
                    obj.LastName = mng.Manage_User.LastName;
                    obj.UserTypeId = mng.Manage_User.UserTypeId;
                    obj.Address = mng.Manage_User.Address;
                    obj.MobileNo = mng.Manage_User.MobileNo;
                    obj.Emaild = mng.Manage_User.Emaild;
                    obj.UserName = mng.Manage_User.UserName;
                    obj.Password = mng.Manage_User.Password;
                    obj.IsActive = true;
                    obj.IsDelete = false;
                    obj.CreatedOn = DateTime.Now;
                    db.Manage_User.Add(obj);
                    db.SaveChanges();
                    count = 1;
                }
                return count;
            }

            catch (Exception ex)
            {
                throw;
            }
           
        }

        public List<Vw_Manage_User> Vw_Manage_Users()
        {
            var data = db.Vw_Manage_User.ToList();
            return data;
        }

        public Manage_User GetManage_User(int id)
        {
            var data = db.Manage_User.Where(x => x.UserId == id).FirstOrDefault();
            return data;
        }

        public int DeleteManageUser(int id)
        {
            var data = db.Manage_User.Where(x => x.UserId == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDelete = true;
            db.SaveChanges();
            return 1;
        }


        public List<Designation> Designations()
        {
            var data = db.Designations.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            return data;
        }

        public int SaveDesignation(Designation designation)
        {
            int count = 0;
            if (designation.DesgId != 0)
            {

                var data = db.Designations.Where(x => x.DesgId == designation.DesgId).FirstOrDefault();

                data.DesgName = designation.DesgName;
                db.SaveChanges();
                count = 2;

            }
            else
            {
                Designation obj1 = new Designation();
                obj1.DesgName = designation.DesgName;
                obj1.IsActive = true;
                obj1.IsDelete = false;
                db.Designations.Add(obj1);
                db.SaveChanges();
                count = 1;

            }
            return count;

        }

        public Designation GetDesignation(int id)
        {
            var data = db.Designations.Where(x => x.DesgId == id).FirstOrDefault();
            return data;
        }

        public int DeleteDesignation(int id)
        {
            var data = db.Designations.Where(x => x.DesgId == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDelete = true;
            db.SaveChanges();
            return 1;
        }


        public List<User_Type> User_Types()
        {
            var data = db.User_Type.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            return data;
        }

        public int SaveUser_Type(User_Type user_Type)
        {
            int count = 0;
            if (user_Type.UserTypeId != 0)
            {
                var data = db.User_Type.Where(x => x.UserTypeId == user_Type.UserTypeId).FirstOrDefault();

                data.UserTypeName = user_Type.UserTypeName;
                db.SaveChanges();
                count = 2;
            }
            else
            {
                User_Type obj = new User_Type();
                obj.UserTypeName = user_Type.UserTypeName;
                obj.IsActive = true;
                obj.IsDelete = false;
                db.User_Type.Add(obj);
                db.SaveChanges();
                count = 1;

            }
            return count;


        }

        public User_Type GetUser_Type(int id)
        {
            var data = db.User_Type.Where(x => x.UserTypeId == id).FirstOrDefault();
            return data;
        }

        public int DeleteUser_Type(int id)
        {
            var data = db.User_Type.Where(x => x.UserTypeId == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDelete = true;
            db.SaveChanges();
            return 1;
        }


        public List<Training_Institute> Training_Institutes()
        {
            var data = db.Training_Institute.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            return data;
        }

        public int SaveTraining_Institute(Training_Institute training_Institute)
        {
            int count = 0;
            if (training_Institute.Tr_Institute_Id != 0)
            {
                var data = db.Training_Institute.Where(x => x.Tr_Institute_Id == training_Institute.Tr_Institute_Id).FirstOrDefault();

                data.Tr_Institute_Name = training_Institute.Tr_Institute_Name;
                data.Address = training_Institute.Address;
                data.Insti_Type = training_Institute.Insti_Type;
                db.SaveChanges();
                count = 2;
            }
            else
            {
                Training_Institute obj2 = new Training_Institute();
                obj2.Tr_Institute_Name = training_Institute.Tr_Institute_Name;
                obj2.Address = training_Institute.Address;
                obj2.Insti_Type = training_Institute.Insti_Type;
                obj2.IsActive = true;
                obj2.IsDelete = false;
                db.Training_Institute.Add(obj2);
                db.SaveChanges();
                count = 1;
            }
            return count;
        }

        public Training_Institute GetTraining_Institute(int id)
        {
            var data = db.Training_Institute.Where(x => x.Tr_Institute_Id == id).FirstOrDefault();
            return data;
        }

        public int DeleteTraining_Institute(int id)
        {
            var data = db.Training_Institute.Where(x => x.Tr_Institute_Id == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDelete = true;
            db.SaveChanges();
            return 1;
        }


        public List<Training_Intended> Training_Intendeds()
        {
            var data = db.Training_Intended.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            return data;
        }

        public int SaveTraining_Intendeds(Training_Intended training_Intended)
        {
            int count = 0;
            if (training_Intended.Tr_Intended_Id != 0)
            {
                var data = db.Training_Intended.Where(x => x.Tr_Intended_Id == training_Intended.Tr_Intended_Id).FirstOrDefault();

                data.Tr_Intended_Name = training_Intended.Tr_Intended_Name;
                db.SaveChanges();
                count = 2;
            }
            else
            {
                Training_Intended obj3 = new Training_Intended();
                obj3.Tr_Intended_Name = training_Intended.Tr_Intended_Name;
                obj3.IsActive = true;
                obj3.IsDelete = false;
                db.Training_Intended.Add(obj3);
                db.SaveChanges();
                count = 1;

            }
            return count;


        }

        public Training_Intended GetTraining_Intended(int id)
        {

            var data = db.Training_Intended.Where(x => x.Tr_Intended_Id == id).FirstOrDefault();
            return data;

        }

        public int DeleteTraining_Intended(int id)
        {
            var data = db.Training_Intended.Where(x => x.Tr_Intended_Id == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDelete = true;
            db.SaveChanges();
            return 1;
        }


        public List<Training_Type> Training_Types()
        {
            var data = db.Training_Type.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            return data;
        }

        public List<SelectListItem> Traing_Institutelist()
        {
            var data = db.Training_Institute.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            List<SelectListItem> iteams = new List<SelectListItem>();
            iteams.Add(new SelectListItem { Text = "Select", Value = "0", });

            foreach (var item in data)
            {
                iteams.Add(new SelectListItem
                {
                    Text = item.Tr_Institute_Name,
                    Value = item.Tr_Institute_Id.ToString(),
                });
            }
            return iteams;
        }

        public int SaveTraingType(Trainingtypemodel nu)
        {
            try
            {
                int count = 0;
                if (nu.Traing_Type_Id != 0 && nu.Traing_Type_Id != null)
                {
                    var data = db.Training_Type.Where(x => x.Traing_Type_Id == nu.Traing_Type_Id).FirstOrDefault();

                    data.Tr_Institute_Id = nu.Tr_Institute_Id;
                    data.Traing_Name = nu.Traing_Name;
                    data.Tr_Address = nu.Tr_Address;
                    db.SaveChanges();
                    count = 2;
                }
                else
                {
                    Training_Type obj3 = new Training_Type();
                    obj3.Traing_Name = nu.Traing_Name;
                    obj3.Tr_Institute_Id = nu.Tr_Institute_Id;
                    obj3.Tr_Address = nu.Tr_Address;
                    obj3.IsActive = true;
                    obj3.IsDelete = false;
                    db.Training_Type.Add(obj3);
                    db.SaveChanges();
                    count = 1;

                }
                return count;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Vw_Training_Type> vw_Training_Types()
        {
            var data = db.Vw_Training_Type.ToList();
            return data;
        }

        public Training_Type GetTraining_Type(int id)
        {
            var data = db.Training_Type.Where(x => x.Traing_Type_Id == id).FirstOrDefault();
            return data;
        }

        public int DeleteTraining_Type(int id)
        {
            var data = db.Training_Type.Where(x => x.Traing_Type_Id == id).FirstOrDefault();
            data.IsActive = false;
            data.IsDelete = true;
            db.SaveChanges();
            return 1;
        }

        public int SaveAssignTraining(Assigntrainingmodel nu)
        {
            try
            {
                int count = 0;
                DateTime fd = Convert.ToDateTime(nu.TraingAssign.Trfdate);
                DateTime td = Convert.ToDateTime(nu.TraingAssign.Trtdate);
                var updtr = db.TraingAssigns.Where(x => x.Tr_Institute_Id == nu.TraingAssign.Tr_Institute_Id && x.Traing_Type_Id == nu.TraingAssign.Traing_Type_Id &&
                  x.Trfdate == fd && x.Trtdate == td).FirstOrDefault();
                if (updtr != null)
                {
                    if (nu.ListAssign != null)
                    {
                        decimal sum = 0;
                        foreach (var item in nu.ListAssign)
                        {
                            var sdsd = db.TrainingAssignDetails.Where(x=>x.Tr_AssignID==updtr.Tr_AssignID && x.Userid==item.UserId).ToList();
                            if (sdsd.Count!= 0){
                                foreach (var item1 in sdsd)
                                {
                                    var ff = db.TrainingAssignDetails.Where(x => x.Tr_Ass_DetailsID == item1.Tr_Ass_DetailsID).FirstOrDefault();
                                    ff.Tr_Ass_DetailsID = item1.Tr_Ass_DetailsID;
                                    ff.Userid = item1.Userid;
                                    ff.IsVisible = false;
                                    ff.IsActive = true;
                                    ff.IsDelete = false;
                                    ff.IsEmail = false;
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                TrainingAssignDetail r = new TrainingAssignDetail();

                                r.Tr_AssignID = updtr.Tr_AssignID;
                                r.Userid = item.UserId;
                                r.IsVisible = false;
                                r.IsActive = true;
                                r.IsDelete = false;
                                r.IsEmail = false;
                                db.TrainingAssignDetails.Add(r);
                                db.SaveChanges();
                            }
                        }
                        count = 1;
                    }
                }
                //if (nu.TraingAssign.Tr_AssignID != 0 && nu.TraingAssign.Tr_AssignID != null)
                //{
                //    var data = db.TraingAssigns.Where(x => x.Tr_AssignID == nu.TraingAssign.Tr_AssignID).FirstOrDefault();

                //    data.Tr_Institute_Id = nu.TraingAssign.Tr_Institute_Id;
                //    data.Traing_Type_Id = nu.TraingAssign.Traing_Type_Id;
                //    data.Trfdate = nu.TraingAssign.Trfdate;
                //    data.Trtdate = nu.TraingAssign.Trtdate;
                //    data.Remarks = nu.TraingAssign.Remarks;
                //    db.SaveChanges();
                //    count = 2;
                //}
                else
                {
                    TraingAssign obj3 = new TraingAssign();
                    obj3.Tr_Institute_Id = nu.TraingAssign.Tr_Institute_Id;
                    obj3.Traing_Type_Id = nu.TraingAssign.Traing_Type_Id;
                    obj3.Trfdate = nu.TraingAssign.Trfdate;
                    obj3.Trtdate = nu.TraingAssign.Trtdate;
                    obj3.Remarks = nu.TraingAssign.Remarks;
                    obj3.IsActive = true;
                    obj3.IsDelete = false;
                    obj3.Is_Status = false;
                    obj3.CreatedOn = DateTime.Now;
                    db.TraingAssigns.Add(obj3);
                    db.SaveChanges();
                    if (nu.ListAssign != null)
                    {
                        decimal sum = 0;
                        foreach (var item in nu.ListAssign)
                        {
                            TrainingAssignDetail r = new TrainingAssignDetail();
                            r.Tr_AssignID = obj3.Tr_AssignID;
                            r.Userid = item.UserId;
                            r.IsVisible = false;
                            r.IsActive = true;
                            r.IsDelete = false;
                            r.IsEmail = false;
                            db.TrainingAssignDetails.Add(r);
                            db.SaveChanges();

                        }
                        count = 1;
                    }


                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Vw_User_Training_Assign> Vw_User_Training_Assign(int id)
        {
            var data = db.Vw_User_Training_Assign.Where(x => x.Userid == id).ToList();
            return data;
        }

        public List<Vw_Training_Assign> Vw_Training_Assigns(int id)
        {
            var data = db.Vw_Training_Assign.Where(x=>x.Userid==id).ToList();
            return data;
        }

        public List<Vw_Admin_Training_Assign> vw_Admin_Training_Assigns()
        {
            
            var data = db.Vw_Admin_Training_Assign.ToList();
           
            return data;
        }

        public List<Vw_Admin_Training_Assign_Details> Vw_Admin_Training_Assign_Details(int id)
        {

            var data = db.Vw_Admin_Training_Assign_Details.Where(x=>x.Tr_AssignID==id).ToList();

            return data;
        }

        public int SaveFeedback(Feedback fed)
        {
            try
            {
                int count = 0;
                if (fed.Feedback_Id != 0 && fed.Feedback_Id != null)
                {
                    var data = db.Feedbacks.Where(x => x.Feedback_Id == fed.Feedback_Id).FirstOrDefault();
                    data.Feedback_Id = fed.Feedback_Id;
                    data.Tr_Institute_Id = fed.Tr_Institute_Id;
                    data.Traing_Type_Id = fed.Traing_Type_Id;
                    data.From_Date = fed.From_Date;
                    data.To_Date = fed.To_Date;
                    data.Tr_Relevant = fed.Tr_Relevant;
                    data.Tr_Length = fed.Tr_Length;
                    data.Tr_Inst_Clear = fed.Tr_Inst_Clear;
                    data.Trainer_Effective = fed.Trainer_Effective;
                    data.Tr_Expectation = fed.Tr_Expectation;
                    data.Tr_Quality = fed.Tr_Quality;
                    data.Tr_Daily_Work = fed.Tr_Daily_Work;
                    data.Imp_Dev_Process = fed.Imp_Dev_Process;
                    data.Explain_Training = fed.Explain_Training;

                    db.SaveChanges();
                    count = 2;
                }
                else
                {
                    Feedback data = new Feedback();
                    data.Tr_Institute_Id = fed.Tr_Institute_Id;
                    data.Traing_Type_Id = fed.Traing_Type_Id;
                    data.From_Date = fed.From_Date;
                    data.To_Date = fed.To_Date;
                    data.Tr_Relevant = fed.Tr_Relevant;
                    data.Tr_Length = fed.Tr_Length;
                    data.Tr_Inst_Clear = fed.Tr_Inst_Clear;
                    data.Trainer_Effective = fed.Trainer_Effective;
                    data.Tr_Expectation = fed.Tr_Expectation;
                    data.Tr_Quality = fed.Tr_Quality;
                    data.Tr_Daily_Work = fed.Tr_Daily_Work;
                    data.Imp_Dev_Process = fed.Imp_Dev_Process;
                    data.Explain_Training = fed.Explain_Training;
                    data.CreatedBy = fed.CreatedBy;
                    data.CreatedOn = DateTime.Now;
                    data.IsActive = true;
                    data.IsDelete = false;
                    db.Feedbacks.Add(data);
                    db.SaveChanges();
                    count = 1;
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Vw_Feedback> vw_Feedbacks()
        {
            var data = db.Vw_Feedback.ToList();
            return data;
        }

        public List<vw_employee_institute> vw_employee_institute(int id)
        {
            var data = db.vw_employee_institute.Where(x => x.Tr_AssignID == id).ToList();
            return data;
        }
    }

}
