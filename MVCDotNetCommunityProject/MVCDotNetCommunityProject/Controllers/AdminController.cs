using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MVCDotNetCommunityProject.Models;

namespace MVCDotNetCommunityProject.Controllers
{
    public class AdminController : Controller
    {
        AdminModel objadminmodel = new AdminModel();

        [HttpPost]
        public ActionResult AdminLogin()
        {
            adminlogin objadminlogin = new adminlogin();
            objadminlogin.userid = Request["txtAName"];
            objadminlogin.userpwd = Request["txtAPwd"];
            Session["adminname"] = objadminmodel.AdminLogin(objadminlogin);
            if (Session["adminname"] != null)
            {
                ViewData["lblAdminWelcome"] = Session["adminname"];
                return View("~/Views/Admin/AdminHome.cshtml");
            }
            else
            {
                ViewData["lblAloginMsg"] = "invalid username/password";
                return View("~/Views/Home/AdminLogin.cshtml");
            }
        }
        public ActionResult AdminHome()
        {
            ViewData["lblAdminWelcome"] = Session["adminname"];
            return View();
        }
        public ActionResult UsersList()
        {
            ViewBag.users = objadminmodel.GetUserList();
            return View();
        }
        public ActionResult UserStatusChange()
        {
            userlogin objuserlogin = new userlogin();
            if(Request.QueryString["status"]=="not active")
            {
                objuserlogin.loginid = int.Parse(Request.QueryString["lid"]);
                objadminmodel.ChangeUserStatus(objuserlogin,"active");
            }
            if (Request.QueryString["status"] == "active")
            {
                objuserlogin.loginid = int.Parse(Request.QueryString["lid"]);
                objadminmodel.ChangeUserStatus(objuserlogin, "blocked");
            }
            if (Request.QueryString["status"] == "blocked")
            {
                objuserlogin.loginid = int.Parse(Request.QueryString["lid"]);
                objadminmodel.ChangeUserStatus(objuserlogin, "active");
            }
            ViewBag.users = objadminmodel.GetUserList();
            return View("~/Views/Admin/UsersList.cshtml");
        }
        public ActionResult ForumsList()
        {
            ViewBag.forums = objadminmodel.GetQueries();
            return View();
        }
        public ActionResult DeleteForums()
        {
            forum objforum = new forum();
            objforum.questionid = int.Parse(Request.QueryString["fid"]);
            objadminmodel.DeleteQuery(objforum);
            ViewBag.forums = objadminmodel.GetQueries();
            return View("~/Views/Admin/ForumsList.cshtml");
        }
        public ActionResult ArticlesList()
        {
            ViewBag.articles = objadminmodel.GetArticles();
            return View();
        }
        public ActionResult DeleteArticle()
        {
            article objarticle=new article();
            objarticle.articleid = int.Parse(Request.QueryString["aid"]);
            objadminmodel.DeleteArticles(objarticle);
            ViewBag.articles = objadminmodel.GetArticles();
            return View("~/Views/Admin/ArticlesList.cshtml");
        }
        public ActionResult ProjectsList()
        {
            ViewBag.projects = objadminmodel.GetProjects();
            return View();
        }
        public ActionResult DownloadProject()
        {
            int projectid = int.Parse(Request.QueryString["pid"]);
            List<project> project = objadminmodel.DownloadProject(projectid);
            foreach(var item in project)
            {
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = item.filetype;
            response.AddHeader("Content-Disposition", "attachment; filename=" + item.filename + ";");
            response.TransmitFile(Server.MapPath(@"~/Uploads/" + item.filename));
            response.Flush();
            response.End();
            }
            ViewBag.projects = objadminmodel.GetProjects();
            return View("~/Views/Admin/ProjectsList.cshtml");
        }
        public ActionResult DeleteProject()
        {
            project objproject = new project();
            objproject.projectid = int.Parse(Request.QueryString["pid"]);
            string filename =objadminmodel.DeleteProjects(objproject);
            string checkfile = Server.MapPath(@"~/Uploads/" + filename);
            if (System.IO.File.Exists(checkfile))
            {
                System.IO.File.Delete(checkfile);
            }
            ViewBag.projects = objadminmodel.GetProjects();
            return View("~/Views/Admin/ProjectsList.cshtml");
        }
        public ActionResult AdminChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminChangePassword(string s)
        {
            adminlogin objadmin = new adminlogin();
            objadmin.userid = Session["adminname"].ToString();
            objadmin.userpwd = Request["txtOldPwd"];
            ViewData["lblChPwdMsg"] = objadminmodel.AdminChangePassword(objadmin, Request["txtReNewPwd"]);
            return View();
        }
        public ActionResult AdminLogout()
        {
            ViewData["lblAloginMsg"] = "";
            return View("~/Views/Home/AdminLogin.cshtml");
        }
    }
}
