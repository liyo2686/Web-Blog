using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.ftc.Blog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Post", "Post");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

    }
}