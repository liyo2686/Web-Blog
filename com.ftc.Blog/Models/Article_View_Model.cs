using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.ftc.Blog.Models
{
    public class Article_View_Model
    {
        public int PostID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
    }
}