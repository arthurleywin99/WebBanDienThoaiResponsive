namespace WebBanDienThoaiResponsive.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WebInfo")]
    public partial class WebInfo
    {
        public Guid ID { get; set; }

        public string Keyword { get; set; }

        public string Value { get; set; }

        public bool? Status { get; set; }
    }
}
