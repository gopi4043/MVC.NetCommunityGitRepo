using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDotNetCommunityProject.Models;

namespace MVCDotNetCommunityProject.Controllers
{
    public class UserController : Controller
    {
        UserModel objusermodel = new UserModel();
        public ActionResult UserHome()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(FormCollection coll)
        {
            userlogin objuser = new userlogin();
            objuser.username = Request["txtUName"];
            objuser.password = Request["txtUPwd"];
            Session["loginid"] = objusermodel.UserLogin(objuser);
            if ((int)Session["loginid"] != 0)
            {
                Session["name"] = objusermodel.CurrentUserName((int)Session["loginid"]);
                return View("~/Views/User/UserHome.cshtml");
            }
            else
            {
                ViewData["lblUloginMsg"] = "invalid username/password";
                return View("~/Views/Home/UserLogin.cshtml");
            }
        }

        public ActionResult PostQuery()
        {
            if (Request["btnPostQuery"] == "Post Query")
            {

            }
            return View();
        }

        public ActionResult ViewAllQueries()
        {
            return View();
        }

        public ActionResult ViewAllAnsweredQueries()
        {
            return View();
        }

        public ActionResult PostArticle()
        {
            return View();
        }

        public ActionResult SearchArticles()
        {
            return View();
        }

        public ActionResult ViewAllArticles()
        {
            return View();
        }

        public ActionResult UploadProject()
        {
            return View();
        }

        public ActionResult DownloadProject()
        {
            return View();
        }
        public ActionResult UserLogout()
        {
            ViewData["lblUloginMsg"] = "";
            return View("~/Views/Home/UserLogin.cshtml");
        }
    }
}
