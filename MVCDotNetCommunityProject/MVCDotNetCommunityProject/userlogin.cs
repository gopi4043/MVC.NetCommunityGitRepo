//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCDotNetCommunityProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class userlogin
    {
        public userlogin()
        {
            this.articles = new HashSet<article>();
            this.forumreplies = new HashSet<forumreply>();
            this.forums = new HashSet<forum>();
            this.projects = new HashSet<project>();
        }
    
        public int loginid { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string secquestion { get; set; }
        public string answer { get; set; }
        public string status { get; set; }
    
        public virtual ICollection<article> articles { get; set; }
        public virtual ICollection<forumreply> forumreplies { get; set; }
        public virtual ICollection<forum> forums { get; set; }
        public virtual ICollection<project> projects { get; set; }
    }
}