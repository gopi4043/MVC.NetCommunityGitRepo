using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDotNetCommunityProject.Models;

namespace MVCDotNetCommunityProject.Controllers
{
    public class HomeController : Controller
    {
        dotnetcommunitydbEntities objdb = new dotnetcommunitydbEntities();

        AdminModel objadminmodel = new AdminModel();

        UserModel objusermodel = new UserModel();
        public ActionResult Home()
        {
            //lllllll
            return View();
        }

        public ActionResult AdminLogin()
        {
            ViewData["lblAloginMsg"] = "";
            return View();
        }
        public ActionResult AdminForgotPassword()
        {
            ViewData["lblAForgotPasswordMsg"] = "";
            ViewBag.AFPwdandbtnStyle = "";
            return View();
        }

        [HttpPost]
        public ActionResult AdminForgotPassword(string s)
        {
            adminlogin objadminlogin = new adminlogin();
            objadminlogin.userid = Request["txtAUname"];
            objadminlogin.hint = Request["txtAHint"];
            string pass = objadminmodel.GetPassword(objadminlogin);
            if(pass!=null)
            {
                ViewData["lblAForgotPasswordMsg"] = "Your Password is: " + pass;
                ViewBag.AFPwdandbtnStyle = "color:green";
            }
            else
            {
                ViewData["lblAForgotPasswordMsg"] = "Wrong Userid/Hint";
                ViewBag.AFPwdandbtnStyle = "color:red";
            }
            return View();
        }

        public ActionResult UserRegistration()
        {
            ViewData["regmsg"] = "";
            ViewData["regmsgstyle"] = "";
            ViewData["txtLoginId"] = Convert.ToInt32(objdb.userlogins.Max(u => u.loginid)) + 1;
            return View();
        }

        [HttpPost]
        public ActionResult UserRegistration(userlogin objuserreg)
        {
            objuserreg.loginid = int.Parse(Request["txtLoginId"]);
            objuserreg.status = "not active";
            int i = objusermodel.UserRegistration(objuserreg);
            if (i == 1)
            {
                ViewData["regmsg"] = "Registration Successfull";
                ViewData["regmsgstyle"] = "position:relative;left:110px;color:green";
                ModelState.Clear();
            }
            else
            {
                ViewData["regmsg"] = "Username already used";
                ViewData["regmsgstyle"] = "position:relative;left:110px;color:red";
            }
            ViewData["txtLoginId"] = objdb.userlogins.Select(u => u.loginid).DefaultIfEmpty(0).Max()+1;
            return View();
        }

        public ActionResult UserLogin()
        {
            ViewData["lblUloginMsg"] = "";
            return View();
        }

        public ActionResult UserForgotPassword()
        {
            ViewData["UPwdMsg"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult UserForgotPassword(FormCollection UFPwdCollection)
        {
            userlogin objuser = new userlogin();
            objuser.username = UFPwdCollection["txtUName"].ToString();
            objuser.secquestion = UFPwdCollection["DropSecQueList"].ToString();
            objuser.answer = UFPwdCollection["txtUAnswer"].ToString();
            string upwd = objusermodel.GetPassword(objuser);
            if(upwd!=null)
            {
                ViewData["UPwdMsg"] = upwd;
                ViewData["uflabelstyle"] = "color:green;position:relative;left:120px";
            }
            else
            {
                ViewData["UPwdMsg"] = "Wrong Details Entered";
                ViewData["uflabelstyle"] = "color:red;position:relative;left:120px";
            }
            return View();
        }
    }
}
