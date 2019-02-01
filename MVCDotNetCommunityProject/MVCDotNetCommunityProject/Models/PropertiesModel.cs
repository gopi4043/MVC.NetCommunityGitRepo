using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDotNetCommunityProject.Models
{
    public class PropertiesModel
    {
        public string Name { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int ReplyId { get; set; }
        public string Reply { get; set; }
        public int ArticleId { get; set; }
        public string ArticleType { get; set; }
        public string ArticleDesc { get; set; }
        public DateTime Date { get; set; }
        public int ProjectId { get; set; }
        public string ProjectType { get; set; }
        public string ProjectName { get; set; }
        public string FileType { get; set; }
    }
}