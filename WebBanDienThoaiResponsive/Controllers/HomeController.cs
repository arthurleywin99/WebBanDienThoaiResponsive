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
                List<Product> cellphoneList = new List<Product>();
                List<Top12CellPhone> topCellPhoneList = context.Top12CellPhone.ToList();
                foreach (var item in topCellPhoneList)
                {
                    Product product = context.Products.Single(p => p.ID == item.ID);
                    cellphoneList.Add(product);
                }

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
                        Price = Convert.ToDecimal(item.Price),
                        Discount = Convert.ToDecimal(item.Discount),
                        UpdateDate = item.UpdateDate,
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
                return PartialView(productViewModelList);
            }
        }

        [HttpGet]
        public ActionResult TopLaptops()
        {
            using (var context = new Context())
            {
                List<Product> laptopList = new List<Product>();
                List<Top12Laptop> topLaptopList = context.Top12Laptop.ToList();
                foreach (var item in topLaptopList)
                {
                    Product product = context.Products.Single(p => p.ID == item.ID);
                    laptopList.Add(product);
                }

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
                        Price = Convert.ToDecimal(item.Price),
                        Discount = Convert.ToDecimal(item.Discount),
                        UpdateDate = item.UpdateDate,
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