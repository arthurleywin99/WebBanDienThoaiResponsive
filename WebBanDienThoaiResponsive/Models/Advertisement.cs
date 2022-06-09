namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advertisement")]
    public partial class Advertisement
    {
        public Guid ID { get; set; }

        [StringLength(100)]
        public string AdvertisementName { get; set; }

        [StringLength(12)]
        public string BeginDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string LinkTo { get; set; }

        public string ImageURL { get; set; }

        public bool? Status { get; set; }
    }
}
