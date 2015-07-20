using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitFirstApp.Models.My_Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public string AuthorID { get; set; }
        public string Body { get; set; }
        public DateTimeOffset Created { get; set; }
        public Nullable<DateTimeOffset> Updated { get; set; }
        public string UpdateReason { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual BlogPost Post { get; set; }
    }
}