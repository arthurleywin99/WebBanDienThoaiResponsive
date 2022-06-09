namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BrandAdvertisement")]
    public partial class BrandAdvertisement
    {
        public Guid ID { get; set; }

        [StringLength(20)]
        public string BrandAdName { get; set; }

        public string ImageURL { get; set; }

        public string URLTo { get; set; }

        public bool? Status { get; set; }
    }
}
