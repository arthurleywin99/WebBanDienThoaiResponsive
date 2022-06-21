using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoaiResponsive.Models;
using WebBanDienThoaiResponsive.ViewModels;

namespace WebBanDienThoaiResponsive.Controllers
{
    public class ShoppingCartController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<ShoppingCartViewModel> carts = GetAlls();
            return View(carts);
        }

        private List<ShoppingCartViewModel> GetAlls()
        {
            List<ShoppingCartViewModel> list = Session["Cart"] as List<ShoppingCartViewModel>;
            if (list == null)
            {
                list = new List<ShoppingCartViewModel>();
                Session["Cart"] = list;
            }
            return list;
        }

        private void AddItems(List<ShoppingCartViewModel> carts, Guid ProductID, string ProductName, decimal Price, string ImageUrl)
        {
            using (var context = new Context())
            {
                bool isExist = carts.Any(p => p.ID == ProductID);

                if (isExist)
                {
                    ShoppingCartViewModel itemCart = carts.Find(p => p.ID == ProductID);
                    bool isInStock = IsInStock(itemCart, ProductID);
                    if (isInStock)
                    {
                        foreach (var item in carts)
                        {
                            if (item.ID == ProductID)
                            {
                                item.Quantity += 1;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    carts.Add(new ShoppingCartViewModel
                    {
                        ID = ProductID,
                        ProductName = ProductName,
                        Price = Convert.ToInt64(Price),
                        Quantity = 1,
                        ImageUrl = ImageUrl
                    });
                }
                Session["Cart"] = carts;
            }
        }

        [HttpGet]
        public ActionResult AddToCart(Guid ProductID, string CurrentURL, string ProductName, decimal Price, string ImageUrl)
        {
            using (var context = new Context())
            {
                List<ShoppingCartViewModel> carts = GetAlls();
                AddItems(carts, ProductID, ProductName, Price, ImageUrl);
            }
            return Redirect(CurrentURL);
        }

        private bool IsInStock(ShoppingCartViewModel itemCart, Guid ProductID)
        {
            using (var context = new Context())
            {
                return context.Products.Any(p => p.QuantityInStock >= itemCart.Quantity && p.ID.Equals(ProductID));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateShoppingCart(Guid Id, FormCollection form, string CurrentURL)
        {
            List<ShoppingCartViewModel> cartList = Session["Cart"] as List<ShoppingCartViewModel>;
            int quantity = Convert.ToInt32(form["quantity"].ToString());
            foreach (var item in cartList)
            {
                if (item.ID == Id)
                {
                    item.Quantity = quantity;
                    break;
                }
            }
            Session["Cart"] = cartList;
            return Redirect(CurrentURL);
        }

        [HttpGet]
        public ActionResult AddAndGoToShoppingCart(Guid ProductID, string ProductName, decimal Price, string ImageUrl)
        {
            using (var context = new Context())
            {
                List<ShoppingCartViewModel> carts = GetAlls();
                AddItems(carts, ProductID, ProductName, Price, ImageUrl);
            }
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}