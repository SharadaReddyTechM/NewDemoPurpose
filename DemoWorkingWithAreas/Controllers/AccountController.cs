using DemoWorkingWithAreas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWorkingWithAreas.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        VizagDbEntities dbObj = new VizagDbEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            tblUserDetail usr = new tblUserDetail();
            return View(usr);
        }

        [HttpPost]
        public ActionResult Login(tblUserDetail loggedUser)
        {
            var searchUser = dbObj.tblUserDetails.FirstOrDefault(u=>u.username==loggedUser.username&&u.password==loggedUser.password&&u.usertype==loggedUser.usertype);
            if(searchUser!=null)
            {
                Session["username"] = searchUser.username;
                Session["userType"] = searchUser.usertype;
                
                int usrtype = (int)Session["userType"];
                switch (usrtype)
                {
                    case 1://Usertype =1 means the user Admin
                        {
                            //we want to redirec the admins to the admin area and in that area rspective controller, model n views.
                            return RedirectToAction("Index", "Admin",new { area= "AdminAr" });
                            //break;
                        }
                    case 2://Usertype =2 means the user Faculty
                        {
                            return RedirectToAction("Index", "Faculty",new { area="Faculty"});
                            //break;
                        }
                    case 3://Usertype =3 means the user Student
                        {
                            return RedirectToAction("Index", "Student",new { area="Student"});
                            //break;
                        }
                    default:
                        return RedirectToAction("InvalidLogin");
                        
                }

            }
            else
            {
                Session.Clear();
                Session.Abandon();
                //return Content("Invalid User Credentials, Please login again");
                return RedirectToAction("InvalidLogin");
            }
        }
    }
}