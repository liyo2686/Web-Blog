using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.ftc.Blog.Models
{
    public class LoginViewModel
    {
        [Display(Name = "帳號")]
        public string Username { get; set; }

        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}