﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.ftc.Blog.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Post()
        {
            return View();
        }
    }
}