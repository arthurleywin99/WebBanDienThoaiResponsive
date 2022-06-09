using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;

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
                                               select A).OrderBy(p => p.OrderedCount).Take(8).ToList();
                return PartialView(cellphoneList);
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
                                            select A).OrderBy(p => p.OrderedCount).Take(8).ToList();
                return PartialView(laptopList);
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