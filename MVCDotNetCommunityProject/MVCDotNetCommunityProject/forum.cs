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
    
    public partial class forum
    {
        public forum()
        {
            this.forumreplies = new HashSet<forumreply>();
        }
    
        public int questionid { get; set; }
        public string question { get; set; }
        public Nullable<int> loginid { get; set; }
    
        public virtual ICollection<forumreply> forumreplies { get; set; }
        public virtual userlogin userlogin { get; set; }
    }
}