using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? page, int? option)
        {
            using (var context = new Context())
            {
                List<Product> productList = context.Products.ToList();
                List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
                foreach (var item in productList)
                {
                    ProductViewModel productView = new ProductViewModel
                    {
                        ID = item.ID,
                        SupplierID = item.SupplierID,
                        ProductTypeID = item.ProductTypeID,
                        BrandID = item.BrandID,
                        ProductName = item.ProductName,
                        Price = Convert.ToDecimal(item.Price),
                        Discount = Convert.ToDecimal(item.Discount),
                        UpdateDate = item.UpdateDate,
                        Config = item.Config,
                        Describe = item.Describe,
                        ImageURL = item.ImageURL,
                        QuantityInStock = Convert.ToInt32(item.QuantityInStock),
                        RatingCount = (from A in context.Products
                                       join B in context.OrderDetails
                                       on A.ID equals B.ProductID
                                       where B.RatingStar != null & B.Content != null
                                       select A).ToList().Count(p => p.ID == item.ID),
                        Status = item.Status
                    };
                    double averageStar = Convert.ToDouble(context.OrderDetails.Where(p => p.ProductID == productView.ID).ToList().Average(p => p.RatingStar));
                    if (averageStar - Math.Truncate(averageStar) > 0 && averageStar - Math.Truncate(averageStar) < 0.25)
                    {
                        averageStar = Math.Truncate(averageStar);
                    }
                    else if (averageStar - Math.Truncate(averageStar) >= 0.25 && averageStar - Math.Truncate(averageStar) < 0.75)
                    {
                        averageStar = Math.Truncate(averageStar) + 0.5;
                    }
                    else if (averageStar - Math.Truncate(averageStar) >= 0.75)
                    {
                        averageStar = Math.Truncate(averageStar) + 1;
                    }
                    productView.AverageRatingStar = averageStar;
                    productViewModelList.Add(productView);
                }
                int pageSize = 18;
                int pageNum = (page ?? 1);
                int optionSelected = (option ?? 1);
                ViewBag.Option = option;
                ViewBag.ResCount = productViewModelList.Count().ToString();
                switch (optionSelected)
                {
                    case 1:
                        {
                            return View(productViewModelList.OrderBy(p => p.Price).ToPagedList(pageNum, pageSize));
                        }
                    case 2:
                        {
                            return View(productViewModelList.OrderByDescending(p => p.Price).ToPagedList(pageNum, pageSize));
                        }
                    case 3:
                        {
                            productViewModelList.Sort(delegate (ProductViewModel x, ProductViewModel y)
                            {
                                return string.Compare(x.ProductName, y.ProductName);
                            });
                            return View(productViewModelList.ToPagedList(pageNum, pageSize));
                        }
                    case 4:
                        {
                            productViewModelList.Sort(delegate (ProductViewModel x, ProductViewModel y)
                            {
                                return string.Compare(x.ProductName, y.ProductName);
                            });
                            productViewModelList.Reverse();
                            return View(productViewModelList.ToPagedList(pageNum, pageSize));
                        }
                }
                return View(productViewModelList.ToPagedList(pageNum, pageSize));
            }
        }

        [HttpGet]
        public ActionResult ProductsByProductType(Guid Id, int? page, int? option)
        {
            using (var context = new Context())
            {
                List<Product> productList = (from A in context.Products
                                             join B in context.ProductTypes
                                             on A.ProductTypeID equals B.ID
                                             where B.ID.Equals(Id)
                                             select A).ToList();
                List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
                foreach (var item in productList)
                {
                    ProductViewModel productView = new ProductViewModel
                    {
                        ID = item.ID,
                        SupplierID = item.SupplierID,
                        ProductTypeID = item.ProductTypeID,
                        BrandID = item.BrandID,
                        ProductName = item.ProductName,
                        Price = Convert.ToDecimal(item.Price),
                        Discount = Convert.ToDecimal(item.Discount),
                        UpdateDate = item.UpdateDate,
                        Config = item.Config,
                        Describe = item.Describe,
                        ImageURL = item.ImageURL,
                        QuantityInStock = Convert.ToInt32(item.QuantityInStock),
                        RatingCount = (from A in context.Products
                                       join B in context.OrderDetails
                                       on A.ID equals B.ProductID
                                       where B.RatingStar != null & B.Content != null
                                       select A).ToList().Count(p => p.ID == item.ID),
                        Status = item.Status
                    };
                    double averageStar = Convert.ToDouble(context.OrderDetails.Where(p => p.ProductID == productView.ID).ToList().Average(p => p.RatingStar));
                    if (averageStar - Math.Truncate(averageStar) > 0 && averageStar - Math.Truncate(averageStar) < 0.25)
                    {
                        averageStar = Math.Truncate(averageStar);
                    }
                    else if (averageStar - Math.Truncate(averageStar) >= 0.25 && averageStar - Math.Truncate(averageStar) < 0.75)
                    {
                        averageStar = Math.Truncate(averageStar) + 0.5;
                    }
                    else if (averageStar - Math.Truncate(averageStar) >= 0.75)
                    {
                        averageStar = Math.Truncate(averageStar) + 1;
                    }
                    productView.AverageRatingStar = averageStar;
                    productViewModelList.Add(productView);
                }
                int pageSize = 18;
                int pageNum = (page ?? 1);
                int optionSelected = (option ?? 1);
                ViewBag.Option = option;
                ViewBag.ResCount = productViewModelList.Count().ToString();
                switch (optionSelected)
                {
                    case 1:
                        {
                            return View(productViewModelList.OrderBy(p => p.Price).ToPagedList(pageNum, pageSize));
                        }
                    case 2:
                        {
                            return View(productViewModelList.OrderByDescending(p => p.Price).ToPagedList(pageNum, pageSize));
                        }
                    case 3:
                        {
                            productViewModelList.Sort(delegate (ProductViewModel x, ProductViewModel y)
                            {
                                return string.Compare(x.ProductName, y.ProductName);
                            });
                            return View(productViewModelList.ToPagedList(pageNum, pageSize));
                        }
                    case 4:
                        {
                            productViewModelList.Sort(delegate (ProductViewModel x, ProductViewModel y)
                            {
                                return string.Compare(x.ProductName, y.ProductName);
                            });
                            productViewModelList.Reverse();
                            return View(productViewModelList.ToPagedList(pageNum, pageSize));
                        }
                }
                return View(productViewModelList.ToPagedList(pageNum, pageSize));
            }
        }

        [HttpGet]
        public ActionResult ProductDetails(Guid Id)
        {
            using (var context = new Context())
            {
                Product product = context.Products.FirstOrDefault(p => p.ID.Equals(Id));
                ProductViewModel productView = new ProductViewModel
                {
                    ID = product.ID,
                    SupplierID = product.SupplierID,
                    ProductTypeID = product.ProductTypeID,
                    BrandID = product.BrandID,
                    ProductName = product.ProductName,
                    Price = Convert.ToDecimal(product.Price),
                    Discount = Convert.ToDecimal(product.Discount),
                    UpdateDate = product.UpdateDate,
                    Config = product.Config,
                    Describe = product.Describe,
                    ImageURL = product.ImageURL,
                    QuantityInStock = Convert.ToInt32(product.QuantityInStock),
                    RatingCount = (from A in context.Products
                                   join B in context.OrderDetails
                                   on A.ID equals B.ProductID
                                   where B.RatingStar != null & B.Content != null
                                   select A).ToList().Count(p => p.ID == product.ID),
                    Status = product.Status
                };
                double averageStar = Convert.ToDouble(context.OrderDetails.Where(p => p.ProductID == productView.ID).ToList().Average(p => p.RatingStar));
                if (averageStar - Math.Truncate(averageStar) > 0 && averageStar - Math.Truncate(averageStar) < 0.25)
                {
                    averageStar = Math.Truncate(averageStar);
                }
                else if (averageStar - Math.Truncate(averageStar) >= 0.25 && averageStar - Math.Truncate(averageStar) < 0.75)
                {
                    averageStar = Math.Truncate(averageStar) + 0.5;
                }
                else if (averageStar - Math.Truncate(averageStar) >= 0.75)
                {
                    averageStar = Math.Truncate(averageStar) + 1;
                }
                productView.AverageRatingStar = averageStar;
                return View(productView);
            }
        }
    }
}