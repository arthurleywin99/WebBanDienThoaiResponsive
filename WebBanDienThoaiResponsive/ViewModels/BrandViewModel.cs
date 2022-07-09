using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class BrandViewModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Không được để trống tên thương hiệu")]
        [StringLength(100)]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "Không được để trống Logo")]

        public string LogoURL { get; set; }

        [Required(ErrorMessage = "Không được để trống Mô tả")]
        public string Describe { get; set; }

        public bool? Status { get; set; }
    }
}