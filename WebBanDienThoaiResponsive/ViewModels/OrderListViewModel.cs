using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanDienThoaiResponsive.Models;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class OrderListViewModel
    {
        public Order order { get; set; }
        public List<ShoppingCartViewModel> carts { get; set; }
    }
}