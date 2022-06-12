using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class OrderDetailsViewModel
    {
        public string ImageURL { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public long PriceNow { get; set; }

        public OrderDetailsViewModel(string imageURL, string productName, int quantity, long priceNow)
        {
            ImageURL = imageURL;
            ProductName = productName;
            Quantity = quantity;
            PriceNow = priceNow;
        }

        public OrderDetailsViewModel()
        {

        }
    }
}