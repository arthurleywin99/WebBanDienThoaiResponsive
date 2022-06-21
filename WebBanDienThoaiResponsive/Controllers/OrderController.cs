using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class OrderController : Controller
    { 
        public ActionResult OrderingAction()
        {
            if (Session["Account"] == null)
            {
                return RedirectToAction("Signin", "Account");
            }
            else
            {
                return RedirectToAction("ShipmentDetails", "Order");
            }
        }

        [HttpGet]
        public ActionResult ShipmentDetails()
        {
            ViewBag.UserAccount = Session["Account"] as MemberAccount;
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult CreateShipmentDetails(PayDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserAccount = Session["Account"] as MemberAccount;
                return View("ShipmentDetails");    
            }
            else
            {
                Session["ShipmentDetails"] = viewModel;
                return RedirectToAction("PaymentDetails", "Order");
            } 
        }

        [HttpGet]
        public ActionResult PaymentDetails()
        {
            return View();
        }

        public ActionResult AddPayment(PayDetailsViewModel viewModel)
        {
            using (var context = new Context())
            {
                PayDetailsViewModel viewModelTemp = Session["ShipmentDetails"] as PayDetailsViewModel;
                List<ShoppingCartViewModel> carts = Session["Cart"] as List<ShoppingCartViewModel>;
                MemberAccount account = Session["Account"] as MemberAccount;
                viewModelTemp.IsPaid = viewModel.IsPaid;

                Order order = new Order();
                order.ID = Guid.NewGuid();
                order.MemberID = account.ID;
                order.OrderDate = DateTime.Now;
                order.OrderStatus = Utility.PENDING;
                order.DeliveryDate = order.OrderDate.AddDays(Utility.DELIVERY_DAYS);
                order.IsPaid = viewModelTemp.IsPaid;
                if (viewModel.IsPaid)
                {
                    //order.TransferID = viewModel.TransferID;  => Xây dựng chức năng này sau khi đã thanh toán
                }
                order.Discount = 0; //=> Xây dựng chức năng này sau khi đã làm giảm giá
                order.Total = carts.Sum(p => p.Price * p.Quantity);
                order.OrderPhone = viewModelTemp.OrderPhoneNumber;
                order.OrderEmail = account.Email.Trim();
                if (viewModel.OrderNote != null)
                {
                    order.Note = viewModelTemp.OrderNote;
                }
                order.OrderName = viewModelTemp.OrderFullName;
                order.OrderAddress = viewModelTemp.OrderAddress;

                context.Orders.Add(order);
                context.SaveChanges();
                foreach (var item in carts)
                {
                    Product product = context.Products.FirstOrDefault(p => p.ID == item.ID);
                    product.OrderedCount++;
                    context.SaveChanges();
                }

                foreach (var item in carts)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderID = order.ID,
                        ProductID = item.ID,
                        PriceNow = item.Price,
                        Quantity = item.Quantity
                    };
                    context.OrderDetails.Add(orderDetail);
                }
                context.SaveChanges();

                Session["ShipmentDetails"] = null;
                /*Add thanh toán*/
                Session["Order"] = order;
                return RedirectToAction("OrderingConfirmation", "Order");
            }
        }

        public ActionResult OrderingConfirmation()
        {
            return View();
        }

        public ActionResult Confirmed()
        {
            Session["Cart"] = null;
            Session["Order"] = null;
            Session["ShipmentDetails"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}