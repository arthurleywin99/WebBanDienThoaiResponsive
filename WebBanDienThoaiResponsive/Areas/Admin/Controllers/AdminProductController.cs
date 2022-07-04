using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;
using System.Data.Entity;

namespace WebBanDienThoaiResponsive.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (var context = new Context())
            {
                List<Product> productList = context.Products.Include(p => p.Supplier).Include(p => p.ProductType).Include(p => p.Brand).ToList();
                return View(productList);
            }
        }

        [HttpGet]
        public ActionResult Update(Guid Id)
        {
            using (var context = new Context())
            {
                Product product = context.Products.FirstOrDefault(p => p.ID == Id);
                ProductViewModel productViewModel = new ProductViewModel
                {
                    ID = product.ID,
                    SupplierID = product.SupplierID,
                    ProductTypeID = product.ProductTypeID,
                    BrandID = product.BrandID,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    UpdateDate = product.UpdateDate,
                    Config = product.Config,
                    Describe = product.Describe,
                    ImageURL = product.ImageURL,
                    QuantityInStock = product.QuantityInStock,
                    RatingCount = product.RatingCount,
                    OrderedCount = product.OrderedCount,
                    Status = product.Status,
                    Discount = product.Discount,
                    Suppliers = context.Suppliers.ToList(),
                    ProductTypes = context.ProductTypes.ToList(),
                    Brands = context.Brands.ToList()
                };
                return View(productViewModel);
            }
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductViewModel viewModel, FormCollection form)
        {
            using (var context = new Context())
            {
                if (!context.Products.Any(p => p.ID == viewModel.ID))
                {
                    return View(viewModel);
                }
                else
                {
                    Product product = context.Products.FirstOrDefault(p => p.ID == viewModel.ID);
                    product.ID = viewModel.ID;
                    product.SupplierID = viewModel.SupplierID;
                    product.ProductTypeID = viewModel.ProductTypeID;
                    product.BrandID = viewModel.BrandID;
                    product.ProductName = viewModel.ProductName;
                    product.Price = viewModel.Price;
                    product.UpdateDate = DateTime.Now;
                    product.Config = viewModel.Config;  
                    product.Describe = viewModel.Describe;
                    product.Discount = viewModel.Discount;
                    //ImageURL = null;
                    product.QuantityInStock = viewModel.QuantityInStock;
                    context.SaveChanges();
                    return RedirectToAction("Index", "AdminProduct");
                }
            }
        }

        public ActionResult Diable(Guid Id, string CurrentURL)
        {
            using (var context = new Context())
            {
                if (!context.Products.Any(p => p.ID == Id))
                {
                    return View();
                }
                Product product = context.Products.Single(p => p.ID == Id);
                product.Status = !product.Status;
                context.SaveChanges();
                return Redirect(CurrentURL);
            }
        }
    }
}