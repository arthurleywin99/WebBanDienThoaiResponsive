namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid ID { get; set; }

        public DateTime OrderDate { get; set; }

        [StringLength(20)]
        public string OrderStatus { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public bool? IsPaid { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Total { get; set; }

        [Required]
        [StringLength(12)]
        public string OrderPhone { get; set; }

        [Required]
        [StringLength(100)]
        public string OrderEmail { get; set; }

        [StringLength(100)]
        public string TransferID { get; set; }

        public string Note { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderName { get; set; }

        [Required]
        public string OrderAddress { get; set; }

        public Guid? MemberID { get; set; }

        public virtual MemberAccount MemberAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
