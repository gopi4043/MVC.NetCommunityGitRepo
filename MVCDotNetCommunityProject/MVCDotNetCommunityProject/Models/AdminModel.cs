using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDotNetCommunityProject.Models
{
    public class AdminModel
    {
        dotnetcommunitydbEntities objdb = new dotnetcommunitydbEntities();
       

        //Task-1: Admin Login
        public string AdminLogin(adminlogin objadmin)
        {
            adminlogin objadmincheck = objdb.adminlogins.FirstOrDefault(a => a.userid.Equals(objadmin.userid) && a.userpwd.Equals(objadmin.userpwd));
            if (objadmincheck != null)
            {
                return objadmincheck.userid;
            }
            else
            {
                return null;
            }
        }
        //Task-2: Admin Change Password
        public int AdminChangePassword(adminlogin objadmin, string newpassword)
        {
            adminlogin objadmincheck = objdb.adminlogins.FirstOrDefault(a => a.userid.Equals(objadmin.userid) && a.userpwd.Equals(objadmin.userpwd));
            if (objadmincheck != null)
            {
                objadmin = objdb.adminlogins.Find(objadmin.userid);
                objadmin.userpwd = newpassword;
                objdb.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //Task-3: Admin Forgot Password
        public string GetPassword(adminlogin objadmin)
        {
            adminlogin objadmincheck = objdb.adminlogins.FirstOrDefault(a => a.userid.Equals(objadmin.userid) && a.hint.Equals(objadmin.hint));
            if (objadmincheck != null)
            {
                return objadmincheck.userpwd;
            }
            else
            {
                return null;
            }
        }

        //Task-4: View Users List
        public List<userlogin> GetUserList()
        {
            return objdb.userlogins.ToList();
        }

        //Task-5: Change User Tasks (Approve/Block/UnBlock)
        public void ChangeUserStatus(userlogin objuser,string status)
        {
            objuser = objdb.userlogins.Find(objuser.loginid);
            objuser.status = status;
            objdb.SaveChanges();
        }

        //Task-6: Forums(Query List)
        public IQueryable GetQueries()
        {
            var query = from u in objdb.userlogins
                        join f in objdb.forums on u.loginid equals f.loginid
                        select new PropertiesModel
                        {
                            QuestionId=f.questionid,
                            Question = f.question,
                            Name = u.name
                        };
            return query;
        }

        //Task-7: Delete Queries
        public int DeleteQuery(forum objforum)
        {
            objforum = objdb.forums.Find(objforum.questionid);
            List<int> replyids = objdb.forumreplies.Where(r => r.questionid==objforum.questionid).Select(r=>r.replyid).ToList();
            try
            {
                //if we want to delete query
                //first we have to delete child table records that is forum replies
                //later we have to delete parent table query
                if (replyids != null)
                {
                    foreach (int rid in replyids)
                    {
                        forumreply objreply = objdb.forumreplies.Find(rid);
                        objdb.forumreplies.Remove(objreply);
                    }
                }
                objdb.forums.Remove(objforum);
                objdb.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //Task-8: Display Articles
        public IQueryable GetArticles()
        {
            var query = from u in objdb.userlogins
                        join a in objdb.articles on u.loginid equals a.loginid
                        select new PropertiesModel
                        {
                            ArticleId=a.articleid,
                            ArticleType=a.articletype,
                            ArticleDesc = a.articledesc,
                            Date = a.posteddate,
                            Name = u.name
                        };
            return query;
        }

        //Task-9: Deleting Aricles
        public int DeleteArticles(article objarticle)
        {
            objarticle = objdb.articles.Find(objarticle.articleid);
            try
            {
                objdb.articles.Remove(objarticle);
                objdb.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //Task-10: Projects List
        public IQueryable GetProjects()
        {
            var query = from u in objdb.userlogins
                        join p in objdb.projects on u.loginid equals p.loginid
                        select new PropertiesModel
                        {
                            ProjectId=p.projectid,
                            ProjectType = p.projecttype,
                            ProjectName = p.filename,
                            FileType = p.filetype,
                            Name = u.name
                        };
            return query;
        }

        //Task-11: Delete Projects
        public string DeleteProjects(project objproject)
        {
            objproject = objdb.projects.Find(objproject.projectid);
            try
            {
                objdb.projects.Remove(objproject);
                objdb.SaveChanges();
                return objproject.filename;
            }
            catch
            {
                return null;
            }
        }

        //Task-12: Download Projects
        //we write Download request code in AdminController
        public List<project> DownloadProject(int projectid)
        {
            return objdb.projects.Where(u => u.projectid == projectid).ToList();
        }

    }
}