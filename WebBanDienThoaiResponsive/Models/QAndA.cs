namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QAndA")]
    public partial class QAndA
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Fullname { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        public string Content { get; set; }

        public Guid IDProblem { get; set; }

        public bool? Status { get; set; }

        public virtual ProblemContact ProblemContact { get; set; }
    }
}
