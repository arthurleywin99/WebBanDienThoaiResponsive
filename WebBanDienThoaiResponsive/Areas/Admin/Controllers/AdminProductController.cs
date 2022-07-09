using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;

namespace WebBanDienThoaiResponsive.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Signin", "AdminAccount");
            }
            using (var context = new Context())
            {
                List<Product> productList = context.Products.Include(p => p.Supplier).Include(p => p.ProductType).Include(p => p.Brand).ToList();
                return View(productList);
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
                Product product = context.Products.FirstOrDefault(p => p.ID == Id);
                ProductViewModel productViewModel = new ProductViewModel
                {
                    ID = product.ID,
                    SupplierID = product.SupplierID,
                    ProductTypeID = product.ProductTypeID,
                    BrandID = product.BrandID,
                    ProductName = product.ProductName,
                    Price = Convert.ToDecimal(product.Price),
                    UpdateDate = product.UpdateDate,
                    Config = product.Config,
                    Describe = product.Describe,
                    ImageURL = product.ImageURL,
                    QuantityInStock = Convert.ToInt32(product.QuantityInStock),
                    Status = product.Status,
                    Discount = Convert.ToDecimal(product.Discount),
                    Suppliers = context.Suppliers.ToList(),
                    ProductTypes = context.ProductTypes.ToList(),
                    Brands = context.Brands.ToList()
                };
                return View(productViewModel);
            }
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductViewModel viewModel, FormCollection form, HttpPostedFileBase file)
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

                    string _FileName = "";
                    string _path = "";
                    string _FileExtension = "";
                    if (file != null && file.ContentLength > 0)
                    {
                        _FileName = Path.GetFileName(file.FileName);
                        _FileExtension = Path.GetExtension(file.FileName);
                        _path = Path.Combine(Server.MapPath("~/Content/Images"), _FileName);
                        file.SaveAs(_path.Split('.')[0] + product.ID.ToString() + "." + _path.Split('.')[1]);
                        string NewPath = _path.Split('.')[0] + product.ID.ToString() + "." + _path.Split('.')[1];

                        product.ImageURL = NewPath.Split('\\')[NewPath.Split('\\').Length - 1];
                    }
                    product.QuantityInStock = viewModel.QuantityInStock;
                    context.SaveChanges();
                    return RedirectToAction("Index", "AdminProduct");
                }
            }
        }

        public ActionResult Diable(Guid Id, string CurrentURL)
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Signin", "AdminAccount");
            }
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

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Signin", "AdminAccount");
            }
            using (var context = new Context())
            {
                ViewBag.Suppliers = context.Suppliers.ToList();
                ViewBag.ProductTypes = context.ProductTypes.ToList();
                ViewBag.Brands = context.Brands.ToList();
                return View();
            }
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel viewModel, HttpPostedFileBase file)
        {
            using (var context = new Context())
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Suppliers = context.Suppliers.ToList();
                    ViewBag.ProductTypes = context.ProductTypes.ToList();
                    ViewBag.Brands = context.Brands.ToList();
                    return View();
                }
                else
                {
                    Product product = new Product
                    {
                        ID = Guid.NewGuid(),
                        SupplierID = viewModel.SupplierID,
                        ProductTypeID = viewModel.ProductTypeID,
                        BrandID = viewModel.BrandID,
                        ProductName = viewModel.ProductName,
                        Price = viewModel.Price,
                        Discount = viewModel.Discount,
                        UpdateDate = DateTime.Now,
                        Config = viewModel.Config,
                        Describe = viewModel.Describe,
                        //ImageURL = _FileName,
                        QuantityInStock = viewModel.QuantityInStock,
                        Status = true
                    };

                    string _FileName = "";
                    string _path = "";
                    string _FileExtension = "";
                    if (file.ContentLength > 0)
                    {
                        _FileName = Path.GetFileName(file.FileName);
                        _FileExtension = Path.GetExtension(file.FileName);
                        _path = Path.Combine(Server.MapPath("~/Content/Images"), _FileName);
                        file.SaveAs(_path.Split('.')[0] + product.ID.ToString() + "." + _path.Split('.')[1]);
                    }

                    string NewPath = _path.Split('.')[0] + product.ID.ToString() + "." + _path.Split('.')[1];

                    product.ImageURL = NewPath.Split('\\')[NewPath.Split('\\').Length - 1];

                    if (!context.Products.Any(p => p.ProductName == product.ProductName))
                    {
                        context.Products.Add(product);
                        context.SaveChanges();
                    }
                    else
                    {
                        ViewData["Error"] = "Đã tồn tại sản phẩm với tên này. Vui lòng kiểm tra lại";
                        ViewBag.Suppliers = context.Suppliers.ToList();
                        ViewBag.ProductTypes = context.ProductTypes.ToList();
                        ViewBag.Brands = context.Brands.ToList();
                        return View();
                    }
                    return RedirectToAction("Index", "AdminProduct");
                }
            }
        }
    }
}