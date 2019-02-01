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
        dotnetcommunitydbEntities objdb = new dotnetcommunitydbEntities();
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
                forum objforum = new forum();
                objforum.questionid=int.Parse(Request["txtQueryId"]);
                objforum.question=Request["txtQuery"];
                objforum.loginid=(int)Session["loginid"];
                ViewData["PostQueryValue"] = objusermodel.PostQuery(objforum);
            }
            ViewData["txtQueryId"] = objdb.forums.Select(f => f.questionid).DefaultIfEmpty(0).Max() +1;
            return View();
        }

        public ActionResult ViewAllQueries()
        {
            ViewBag.forums = objusermodel.GetQueries();
            return View();
        }
        public ActionResult ReplyQuery()
        {
            if (Request["btnReply"] == "Reply")
            {
                forumreply objfreply = new forumreply();
                objfreply.replyid = objdb.forumreplies.Select(r => r.replyid).DefaultIfEmpty(0).Max() + 1;
                objfreply.reply = Request["txtQReply"];
                objfreply.questionid =Convert.ToInt32(TempData["questionid"]);
                objfreply.loginid = (int)Session["loginid"];
                ViewData["replyvalue"] = objusermodel.ReplyQuery(objfreply);
                ViewBag.forums = objusermodel.GetQueries();
                return View("~/Views/User/ViewAllQueries.cshtml");
            }
            else
            {
                forum objforum = objdb.forums.Find(int.Parse(Request.QueryString["qid"]));
                ViewData["SelectedQuery"] = objforum.question;
                TempData["questionid"] = Request.QueryString["qid"];
                return View();
            }
        }

        public ActionResult ViewAllAnsweredQueries()
        {
            ViewBag.AnsweredQueries = objusermodel.GetAllAnsweredQuestions();
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
