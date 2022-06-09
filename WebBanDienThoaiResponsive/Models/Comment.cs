namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public Guid ID { get; set; }

        public Guid? ProductID { get; set; }

        public Guid? MemberID { get; set; }

        public string Content { get; set; }

        [Required]
        [StringLength(11)]
        public string CommentDate { get; set; }

        public virtual MemberAccount MemberAccount { get; set; }

        public virtual Product Product { get; set; }
    }
}
