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
    public class AdminSellingController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Singin", "AdminAccount");
            }
            using (var context = new Context())
            {
                List<Order> orderList = context.Orders.Include(p => p.MemberAccount).ToList();
                return View(orderList);
            }
        }

        public ActionResult Confirm(Guid Id, string CurrentURL)
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Singin", "AdminAccount");
            }
            using (var context = new Context())
            {
                if (!context.Orders.Any(p => p.ID == Id))
                {
                    return View();
                }
                Order order = context.Orders.Single(p => p.ID == Id);
                order.OrderStatus = "Đang Giao";
                context.SaveChanges();
                return Redirect(CurrentURL);
            }
        }

        public ActionResult Cancel(Guid Id, string CurrentURL)
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Singin", "AdminAccount");
            }
            using (var context = new Context())
            {
                if (!context.Orders.Any(p => p.ID == Id))
                {
                    return View();
                }
                Order order = context.Orders.Single(p => p.ID == Id);
                order.OrderStatus = "Huỷ Đơn";
                context.SaveChanges();
                return Redirect(CurrentURL);
            }
        }

        public ActionResult Detail(Guid Id, string CurrentURL)
        {
            if (Session["AdminAccount"] == null)
            {
                return RedirectToAction("Singin", "AdminAccount");
            }
            using (var context = new Context())
            {
                if (!context.Orders.Any(p => p.ID == Id))
                {
                    return View();
                }
                List<OrderDetail> detailList = (from A in context.Orders
                                                join B in context.OrderDetails
                                                on A.ID equals B.OrderID
                                                where A.ID == Id
                                                select B).ToList();
                List<ShoppingCartViewModel> viewModelList = new List<ShoppingCartViewModel>();
                foreach (var item in detailList)
                {
                    ShoppingCartViewModel shoppingCartViewModel = new ShoppingCartViewModel
                    {
                        ID = item.OrderID,
                        ProductName = context.Products.FirstOrDefault(p => p.ID == item.ProductID).ProductName,
                        Price = Convert.ToInt64(item.PriceNow),
                        Quantity = item.Quantity,
                        ImageUrl = context.Products.FirstOrDefault(p => p.ID == item.ProductID).ImageURL
                    };
                    viewModelList.Add(shoppingCartViewModel);
                }
                ViewBag.Order = context.Orders.Single(p => p.ID == Id);
                ViewBag.BeforeURL = CurrentURL;
                return View(viewModelList);
            }
        }
    }
}