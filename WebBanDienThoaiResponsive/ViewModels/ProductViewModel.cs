using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanDienThoaiResponsive.CustomModelStateValidation;
using WebBanDienThoaiResponsive.Models;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class ProductViewModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Phải chọn nhà cung cấp")]
        public Guid? SupplierID { get; set; }

        [Required(ErrorMessage = "Phải chọn loại sản phẩm")]
        public Guid? ProductTypeID { get; set; }

        public Guid? BrandID { get; set; }

        [Required(ErrorMessage = "Phải điền tên sản phẩm")]
        [StringLength(255)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Giá gốc không được bỏ trống")]
        [IsNumber(ErrorMessage = "Phải là một số nguyên")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Giảm giá không được bỏ trống")]
        [IsNumber(ErrorMessage = "Phải là một số nguyên")]
        public decimal Discount { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Required(ErrorMessage = "Thông số kỹ thuật không được bỏ trống")]
        public string Config { get; set; }

        [Required(ErrorMessage = "Mô tả sản phẩm không được bỏ trống")]
        public string Describe { get; set; }

        public string ImageURL { get; set; }

        [Required(ErrorMessage = "Số lượng tồn không được bỏ trống")]
        public int QuantityInStock { get; set; }

        public bool? Status { get; set; }

        public double AverageRatingStar { get; set; }

        public int? RatingCount { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<ProductType> ProductTypes { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
    }
}