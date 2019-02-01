using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDotNetCommunityProject.Models
{
    public class UserModel
    {
        dotnetcommunitydbEntities objdb = new dotnetcommunitydbEntities();

        //Task-1: User Login
        public int UserLogin(userlogin objuser)
        {
            userlogin objusercheck = objdb.userlogins.FirstOrDefault(u => u.username.Equals(objuser.username) && u.password.Equals(objuser.password));
            if (objusercheck != null)
            {
                return objusercheck.loginid;
            }
            else
            {
                return 0;
            }
        }

        //Getting current User Name
        public string CurrentUserName(int loginid)
        {
            userlogin objusercheck = objdb.userlogins.Find(loginid);
            return objusercheck.name;
        }

        //Task-2: User Registration
        public int UserRegistration(userlogin objuser)
        {
            try
            {
                objdb.userlogins.Add(objuser);
                objdb.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //Task-3: User Forgot Password
        public string GetPassword(userlogin objuser)
        {
            userlogin objusercheck = objdb.userlogins.FirstOrDefault(u => u.username.Equals(objuser.username) && u.secquestion.Equals(objuser.secquestion) && u.answer.Equals(objuser.answer));
            if (objusercheck != null)
            {
                return objusercheck.password;
            }
            else
            {
                return null;
            }
        }

        //Task-4: User Change Password
        public int UserChangePassword(userlogin objuser,string newpassword)
        {
            objuser = objdb.userlogins.Find(objuser.loginid);
            objuser.password = newpassword;
            try
            {
                objdb.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //Task-5: Displaying QueryList
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

        //Task-6: Post Query
        public int PostQuery(forum objforum)
        {
            try
            {
                objforum = objdb.forums.Add(objforum);
                objdb.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //Task-7: Reply Query
        public int ReplyQuery(forumreply objfreply)
        {
            try
            {
                objfreply = objdb.forumreplies.Add(objfreply);
                objdb.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //Task-8: Display Answered Queries
        public IQueryable GetAllAnsweredQuestions()
        {
            var query = from u in objdb.userlogins
                        join f in objdb.forums on u.loginid equals f.loginid
                        join r in objdb.forumreplies on f.questionid equals r.questionid
                        select new PropertiesModel
                            {
                                Name = u.name,
                                Question = f.question,
                                Reply = r.reply
                            };
            return query;
        }

        //Task-9: Display Articles
        public IQueryable GetArticles()
        {
            var query = from u in objdb.userlogins
                        join a in objdb.articles on u.loginid equals a.loginid
                        select new PropertiesModel
                        {
                            ArticleDesc = a.articledesc,
                            Date = a.posteddate,
                            Name = u.name
                        };
            return query;
        }

        //Task-10: Post Articles
        public int PostArticles(article objarticle)
        {
            try
            {
                objarticle = objdb.articles.Add(objarticle);
                objdb.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //Task-11: Search Articles
        public IQueryable GetSearchedArticles(string articletype)
        {
            var query = from u in objdb.userlogins
                        join a in objdb.articles on u.loginid equals a.loginid
                        where a.articletype == articletype
                        select new PropertiesModel
                        {
                            ArticleDesc = a.articledesc,
                            Date = a.posteddate,
                            Name = u.name
                        };
            return query;
        }

        //Task-12: Display list of Projects
        public IQueryable GetProjects()
        {
            var query = from u in objdb.userlogins
                        join p in objdb.projects on u.loginid equals p.loginid
                        select new PropertiesModel
                        {
                            ProjectType = p.projecttype,
                            ProjectName = p.filename,
                            FileType = p.filetype,
                            Name = u.name
                        };
            return query;
        }

        //Task-13: Upload Project
        //we write Upload request code in UserController
        public int UploadProject(project objproject)
        {
            try
            {
                objproject = objdb.projects.Add(objproject);
                objdb.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //Task-14: Download Project
        //we write Download request code in UserController
        public List<project> DownloadProject(int projectid)
        {
            return objdb.projects.Where(u => u.projectid == projectid).ToList();
        }

        //Task-15: Search Projects
        public IQueryable GetSearchedProjects(string projecttype)
        {
            var query = from u in objdb.userlogins
                        join p in objdb.projects on u.loginid equals p.loginid
                        where p.projecttype == projecttype
                        select new PropertiesModel
                        {
                            ProjectType = p.projecttype,
                            ProjectName = p.filename,
                            FileType = p.filetype,
                            Name = u.name
                        };
            return query;
        }
    }
}