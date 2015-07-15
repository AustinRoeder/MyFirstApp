using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GitFirstApp.Models.My_Models
{
    public class BlogPost
    {
        public BlogPost()
        {
            this.Comments = new HashSet<Comment>();
        }

        public int ID { get; set; }
        public DateTime Created { get; set; }
        public Nullable<DateTime> Updated { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        [AllowHtml]
        [Required]
        public string Body { get; set; }
        public string MediaURL { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}