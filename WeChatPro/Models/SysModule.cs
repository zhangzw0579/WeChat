namespace WeChatPro.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysModule")]
    public partial class SysModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysModule()
        {
            SysModule1 = new HashSet<SysModule>();
        }

        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string EnglishName { get; set; }

        [StringLength(50)]
        public string ParentId { get; set; }

        [StringLength(200)]
        public string Url { get; set; }

        [StringLength(200)]
        public string Iconic { get; set; }

        public int? Sort { get; set; }

        [StringLength(4000)]
        public string Remark { get; set; }

        public bool? State { get; set; }

        [StringLength(200)]
        public string CreatePerson { get; set; }

        public DateTime? CreateTime { get; set; }

        public bool IsLast { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] Version { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysModule> SysModule1 { get; set; }

        public virtual SysModule SysModule2 { get; set; }
    }
}
