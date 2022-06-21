using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Guid OrderId { get; set; }
        public string ImageURL { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public long PriceNow { get; set; }
        public int RatingStar { get; set; }
        public string Content { get; set; }
    }
}