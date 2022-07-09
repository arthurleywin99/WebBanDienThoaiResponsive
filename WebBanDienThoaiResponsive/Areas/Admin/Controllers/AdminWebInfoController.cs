using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;

namespace WebBanDienThoaiResponsive.Areas.Admin.Controllers
{
    public class AdminWebInfoController : Controller
    {
        public ActionResult Index()
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Singin", "AdminAccount");
            }
            using (var context = new Context())
            {
                List<WebInfo> list = context.WebInfoes.ToList();
                return View(list);
            }
        }
    }
}