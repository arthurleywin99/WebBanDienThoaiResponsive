namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Banner")]
    public partial class Banner
    {
        public Guid ID { get; set; }

        [StringLength(100)]
        public string BannerName { get; set; }

        public string ImageURL { get; set; }

        public string LinkTo { get; set; }

        public bool? Status { get; set; }
    }
}
