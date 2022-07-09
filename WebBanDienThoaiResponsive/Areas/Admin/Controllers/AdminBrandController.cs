using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;

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

        [HttpGet]
        public ActionResult Update(Guid Id)
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Signin", "AdminAccount");
            }
            using (var context = new Context())
            {
                if (context.Brands.Any(p => p.ID == Id))
                {
                    Brand brand = context.Brands.Single(p => p.ID == Id);
                    BrandViewModel viewModel = new BrandViewModel
                    {
                        ID = brand.ID,
                        BrandName = brand.BrandName,
                        LogoURL = brand.LogoURL,
                        Describe = brand.Describe,
                        Status = brand.Status
                    };
                    return View(viewModel);
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(BrandViewModel viewModel, HttpPostedFileBase file)
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Signin", "AdminAccount");
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "AdminBrand");
            }
            using (var context = new Context())
            {
                if (context.Brands.Any(p => p.ID == viewModel.ID))
                {
                    Brand brand = context.Brands.Single(p => p.ID == viewModel.ID);
                    brand.BrandName = viewModel.BrandName;
                    string _FileName = "";
                    string _path = "";
                    string _FileExtension = "";
                    if (file != null && file.ContentLength > 0)
                    {
                        _FileName = Path.GetFileName(file.FileName);
                        _FileExtension = Path.GetExtension(file.FileName);
                        _path = Path.Combine(Server.MapPath("~/Content/Images"), _FileName);
                        file.SaveAs(_path.Split('.')[0] + brand.ID.ToString() + "." + _path.Split('.')[1]);
                        string NewPath = _path.Split('.')[0] + brand.ID.ToString() + "." + _path.Split('.')[1];

                        brand.LogoURL = NewPath.Split('\\')[NewPath.Split('\\').Length - 1];
                    }
                    brand.Describe = viewModel.Describe;
                    brand.Status = viewModel.Status;
                    context.SaveChanges();
                    return RedirectToAction("Index", "AdminBrand");
                }
            }
            return View();
        }
    }
}