using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;

namespace WebBanDienThoaiResponsive.Areas.Admin.Controllers
{
    public class AdminBrandController : Controller
    {
        public ActionResult Index()
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Singin", "AdminAccount");
            }
            using (var context = new Context())
            {
                List<Brand> brandList = context.Brands.ToList();
                return View(brandList);
            }
        }
    }
}