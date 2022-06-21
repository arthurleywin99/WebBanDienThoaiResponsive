namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid ID { get; set; }

        public Guid? SupplierID { get; set; }

        public Guid? ProductTypeID { get; set; }

        public Guid? BrandID { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }

        public decimal? Price { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string Config { get; set; }

        public string Describe { get; set; }

        public string ImageURL { get; set; }

        public int? QuantityInStock { get; set; }

        public int? RatingCount { get; set; }

        public int? CommentCount { get; set; }

        public int? OrderedCount { get; set; }

        public bool? Status { get; set; }

        public decimal? Discount { get; set; }

        public virtual Brand Brand { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
