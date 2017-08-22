namespace WeChatPro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysSample")]
    public partial class SysSample
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? Age { get; set; }

        public DateTime? Bir { get; set; }

        [StringLength(50)]
        public string Photo { get; set; }

        [Column(TypeName = "text")]
        public string Note { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
