using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanDienThoaiResponsive.ViewModels
{
    public class BannerViewModel
    {
        public Guid ID { get; set; }

        [StringLength(100)]
        public string BannerName { get; set; }

        public string ImageURL { get; set; }

        public string LinkTo { get; set; }

        public bool? Status { get; set; }
    }
}