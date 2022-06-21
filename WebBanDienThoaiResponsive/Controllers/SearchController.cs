using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebBanDienThoaiResponsive.ViewModels;
using WebBanDienThoaiResponsive.Models;
using System.Configuration;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public ActionResult SearchData(string keyword, int? page, int? option)
        {
            using (var context = new Context())
            {
                string data = "";
                if (keyword != null)
                {
                    data = keyword.ToString().Trim();
                }
                List<Product> result = new List<Product>();
                if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
                {
                    result = context.Products.ToList();
                }
                else
                {
                    List<Product> productsByName = context.Products.Where(p => p.ProductName.Contains(data)).ToList();
                    List<Product> productsByType = (from A in context.Products
                                                    join B in context.ProductTypes
                                                    on A.ProductTypeID equals B.ID
                                                    where B.ProductTypeName.Contains(data)
                                                    select A).ToList();
                    List<Product> productsByBrand = (from A in context.Products
                                                     join B in context.Brands
                                                     on A.BrandID equals B.ID
                                                     where B.BrandName.Contains(data)
                                                     select A).ToList();

                    HashSet<Product> temp = new HashSet<Product>();
                    foreach (var item in productsByName)
                    {
                        temp.Add(item);
                    }
                    foreach (var item in productsByType)
                    {
                        temp.Add(item);
                    }
                    foreach (var item in productsByBrand)
                    {
                        temp.Add(item);
                    }
                    result = temp.ToList();
                }
                List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
                foreach (var item in result)
                {
                    ProductViewModel productView = new ProductViewModel
                    {
                        ID = item.ID,
                        SupplierID = item.SupplierID,
                        ProductTypeID = item.ProductTypeID,
                        BrandID = item.BrandID,
                        ProductName = item.ProductName,
                        Price = item.Price,
                        Discount = item.Discount,
                        UpdateDate = item.UpdateDate,
                        Config = item.Config,
                        Describe = item.Describe,
                        ImageURL = item.ImageURL,
                        QuantityInStock = item.QuantityInStock,
                        RatingCount = item.RatingCount,
                        CommentCount = item.CommentCount,
                        OrderedCount = item.OrderedCount,
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
                ViewBag.Keyword = keyword;
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

        [HttpPost]
        public ActionResult SearchData(string keyword, int? page, int? option, FormCollection f)
        {
            using (var context = new Context())
            {
                string data = "";
                if (keyword != null)
                {
                    data = keyword.ToString().Trim();
                }
                List<Product> result = new List<Product>();
                if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
                {
                    result = context.Products.ToList();
                }
                else
                {
                    List<Product> productsByName = context.Products.Where(p => p.ProductName.Contains(data)).ToList();
                    List<Product> productsByType = (from A in context.Products
                                                    join B in context.ProductTypes
                                                    on A.ProductTypeID equals B.ID
                                                    where B.ProductTypeName.Contains(data)
                                                    select A).ToList();
                    List<Product> productsByBrand = (from A in context.Products
                                                     join B in context.Brands
                                                     on A.BrandID equals B.ID
                                                     where B.BrandName.Contains(data)
                                                     select A).ToList();

                    HashSet<Product> temp = new HashSet<Product>();
                    foreach (var item in productsByName)
                    {
                        temp.Add(item);
                    }
                    foreach (var item in productsByType)
                    {
                        temp.Add(item);
                    }
                    foreach (var item in productsByBrand)
                    {
                        temp.Add(item);
                    }
                    result = temp.ToList();
                }
                List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
                foreach (var item in result)
                {
                    ProductViewModel productView = new ProductViewModel
                    {
                        ID = item.ID,
                        SupplierID = item.SupplierID,
                        ProductTypeID = item.ProductTypeID,
                        BrandID = item.BrandID,
                        ProductName = item.ProductName,
                        Price = item.Price,
                        Discount = item.Discount,
                        UpdateDate = item.UpdateDate,
                        Config = item.Config,
                        Describe = item.Describe,
                        ImageURL = item.ImageURL,
                        QuantityInStock = item.QuantityInStock,
                        RatingCount = item.RatingCount,
                        CommentCount = item.CommentCount,
                        OrderedCount = item.OrderedCount,
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
                ViewBag.Keyword = keyword;
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
    }
}