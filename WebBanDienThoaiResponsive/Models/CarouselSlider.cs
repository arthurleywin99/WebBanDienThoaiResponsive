namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CarouselSlider")]
    public partial class CarouselSlider
    {
        public Guid ID { get; set; }

        [StringLength(20)]
        public string SliderName { get; set; }

        public string ImageURL { get; set; }

        public string UrlTo { get; set; }

        public bool? Status { get; set; }
    }
}
