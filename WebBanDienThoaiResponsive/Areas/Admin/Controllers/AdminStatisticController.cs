﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanDienThoaiResponsive.Areas.Admin.Controllers
{
    public class AdminStatisticController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Signin", "AdminAccount");
            }
            return View();
        }
    }
}