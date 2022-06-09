using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebBanDienThoaiResponsive.ViewModels;
using WebBanDienThoaiResponsive.Models;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult SearchData(string keyword, int? page)
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
                int pageSize = 8;
                int pageNum = (page ?? 1);
                return View(result.ToPagedList(pageNum, pageSize));
            }
        }
    }
}