using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class ProductTypeViewModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Không được để trống tên loại sản phẩm")]
        [StringLength(100)]
        public string ProductTypeName { get; set; }

        [Required(ErrorMessage = "Không được để trống icon")]

        public string IconURL { get; set; }

        public bool? Status { get; set; }
    }
}