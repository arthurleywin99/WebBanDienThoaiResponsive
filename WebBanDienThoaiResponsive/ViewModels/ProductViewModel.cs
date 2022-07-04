using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanDienThoaiResponsive.Models;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class ProductViewModel
    {
        public Guid ID { get; set; }

        public Guid? SupplierID { get; set; }

        public Guid? ProductTypeID { get; set; }

        public Guid? BrandID { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }

        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string Config { get; set; }

        public string Describe { get; set; }

        public string ImageURL { get; set; }

        public int? QuantityInStock { get; set; }

        public int? RatingCount { get; set; }

        public int? CommentCount { get; set; }

        public int? OrderedCount { get; set; }

        public bool? Status { get; set; }

        public double AverageRatingStar { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
    }
}