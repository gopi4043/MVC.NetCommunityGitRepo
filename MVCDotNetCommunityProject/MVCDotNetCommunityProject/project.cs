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
    
    public partial class project
    {
        public int projectid { get; set; }
        public string projecttype { get; set; }
        public string filename { get; set; }
        public string filetype { get; set; }
        public Nullable<int> loginid { get; set; }
    
        public virtual userlogin userlogin { get; set; }
    }
}