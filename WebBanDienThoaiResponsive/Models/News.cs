namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        public Guid ID { get; set; }

        public string ImageURL { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [StringLength(12)]
        public string PostDate { get; set; }

        public string LinkTo { get; set; }

        public bool? Status { get; set; }
    }
}
