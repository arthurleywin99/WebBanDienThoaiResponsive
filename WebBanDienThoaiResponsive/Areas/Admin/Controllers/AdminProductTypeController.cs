using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;

namespace WebBanDienThoaiResponsive.Areas.Admin.Controllers
{
    public class AdminProductTypeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (var context = new Context())
            {
                List<ProductType> productTypeList = context.ProductTypes.ToList();
                List<ProductTypeViewModel> viewModelList = new List<ProductTypeViewModel>();
                foreach (var productType in productTypeList)
                {
                    ProductTypeViewModel viewModel = new ProductTypeViewModel
                    {
                        ID = productType.ID,
                        ProductTypeName = productType.ProductTypeName,
                        IconURL = productType.IconURL,
                        Status = productType.Status,
                    };
                    viewModelList.Add(viewModel);
                }
                return View(viewModelList);
            }
        }

        [HttpGet]
        public ActionResult Update(Guid Id)
        {
            using (var context = new Context())
            {
                ProductType productType = context.ProductTypes.Single(p => p.ID == Id);
                ProductTypeViewModel viewModel = new ProductTypeViewModel
                {
                    ID = productType.ID,
                    ProductTypeName = productType.ProductTypeName,
                    IconURL = productType.IconURL,
                    Status = productType.Status,
                };
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductTypeViewModel viewModel)
        {
            using (var context = new Context())
            {
                ProductType productType = context.ProductTypes.FirstOrDefault(p => p.ID == viewModel.ID);
                productType.ProductTypeName = viewModel.ProductTypeName;
                productType.IconURL = viewModel.IconURL;
                context.SaveChanges();
                return RedirectToAction("Index", "AdminProductType");
            }
        }

        public ActionResult Disable(Guid Id, string CurrentURL)
        {
            using (var context = new Context())
            {
                ProductType productType = context.ProductTypes.FirstOrDefault(p => p.ID == Id);
                productType.Status = !productType.Status;
                context.SaveChanges();
                return Redirect(CurrentURL);
            }
        }
    }
}