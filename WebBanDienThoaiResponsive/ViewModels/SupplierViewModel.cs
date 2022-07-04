using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class SupplierViewModel
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(100)]
        public string SupplierName { get; set; }

        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public bool? Status { get; set; }
    }
}