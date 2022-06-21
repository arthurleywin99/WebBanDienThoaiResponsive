using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Statistic()
        {
            return View();
        }
    }
}