namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminConfig")]
    public partial class AdminConfig
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(50)]
        public string AdEmail { get; set; }

        [Required]
        [StringLength(12)]
        public string AdPhoneNumber { get; set; }

        [Required]
        [StringLength(64)]
        public string AdPassword { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool? Status { get; set; }
    }
}
