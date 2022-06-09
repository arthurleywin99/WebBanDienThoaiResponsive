namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MemberAccount")]
    public partial class MemberAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MemberAccount()
        {
            Comments = new HashSet<Comment>();
        }

        public Guid ID { get; set; }

        public Guid? MemberTypeID { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(65)]
        public string Password { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        public string Address { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public Guid IDQuestion { get; set; }

        public string Answer { get; set; }

        [StringLength(100)]
        public string ResetPasswordCode { get; set; }

        public bool? Status { get; set; }

        public virtual AccountType AccountType { get; set; }

        public virtual AuthenticationQAndA AuthenticationQAndA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
