using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebBanDienThoaiResponsive.Models;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? page)
        {
            using (var context = new Context())
            {
                List<Product> productList = context.Products.ToList();
                int pageSize = 8;
                int pageNum = (page ?? 1);
                return View(productList.ToPagedList(pageNum, pageSize));
            }
        }

        [HttpGet]
        public ActionResult ProductsByProductType(Guid Id, int? page)
        {
            using (var context = new Context())
            {
                List<Product> listSanPham = (from A in context.Products
                                             join B in context.ProductTypes
                                             on A.ProductTypeID equals B.ID
                                             where B.ID.Equals(Id)
                                             select A).ToList();
                int pageSize = 8;
                int pageNum = (page ?? 1);
                return View(listSanPham.ToPagedList(pageNum, pageSize));
            }
        }

        [HttpGet]
        public ActionResult ProductDetails(Guid Id)
        {
            using (var context = new Context())
            {
                Product product = context.Products.FirstOrDefault(p => p.ID.Equals(Id));
                return View(product);
            }
        }
    }
}