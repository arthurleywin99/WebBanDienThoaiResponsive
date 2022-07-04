using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProductTypes()
        {
            using (var context = new Context())
            {
                List<ProductType> productTypes = context.ProductTypes.ToList();
                return PartialView(productTypes);
            }
        }

        [HttpGet]
        public ActionResult CarouselSlider()
        {
            using (var context = new Context())
            {
                List<CarouselSlider> sliderImages = context.CarouselSliders.ToList();
                return PartialView(sliderImages);
            }
        }

        [HttpGet]
        public ActionResult MidBanner()
        {
            using (var context = new Context())
            {
                Banner midBanner = context.Banners.SingleOrDefault(p => p.BannerName.Equals("MID"));
                return PartialView(midBanner);
            }
        }

        [HttpGet]
        public ActionResult TopCellPhones()
        {
            using (var context = new Context())
            {
                List<Product> cellphoneList = (from A in context.Products
                                               join B in context.ProductTypes on A.ProductTypeID equals B.ID
                                               where B.ProductTypeName.Equals("Điện thoại")
                                               orderby A.OrderedCount
                                               select A).OrderBy(p => p.OrderedCount).Take(12).ToList();
                List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
                foreach (var item in cellphoneList)
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
                        Describe = item.Describe,
                        ImageURL = item.ImageURL,
                        QuantityInStock = item.QuantityInStock,
                        RatingCount = item.RatingCount,
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
                return PartialView(productViewModelList);
            }
        }

        [HttpGet]
        public ActionResult TopLaptops()
        {
            using (var context = new Context())
            {
                List<Product> laptopList = (from A in context.Products
                                            join B in context.ProductTypes on A.ProductTypeID equals B.ID
                                            where B.ProductTypeName.Equals("Laptop")
                                            orderby A.OrderedCount
                                            select A).OrderBy(p => p.OrderedCount).Take(12).ToList();
                List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
                foreach (var item in laptopList)
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
                        Describe = item.Describe,
                        ImageURL = item.ImageURL,
                        QuantityInStock = item.QuantityInStock,
                        RatingCount = item.RatingCount,
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
                return PartialView(productViewModelList);
            }
        }

        [HttpGet]
        public ActionResult BrandAdvertisements()
        {
            using (var context = new Context())
            {
                List<BrandAdvertisement> brandAdsList = context.BrandAdvertisements.ToList();
                return PartialView(brandAdsList);
            }
        }
    }
}