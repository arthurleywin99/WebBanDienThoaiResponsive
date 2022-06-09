namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        public Guid OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid ProductID { get; set; }

        public decimal? PriceNow { get; set; }

        public int Quantity { get; set; }

        public string Content { get; set; }

        public int? RatingStar { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
