using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace com.ftc.Blog.Models
{
    public class RegisterViewModel
    {

        [Display(Name = "帳號")]
        public string Account { get; set; }
        [Display(Name = "密碼")]
        public string Password { get; set; }
        [Display(Name = "電子信箱")]
        public string Email { get; set; }

    }
}