namespace ModalEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnhDinhKem")]
    public partial class AnhDinhKem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AnhDinhKem()
        {
            BaiDangs = new HashSet<BaiDang>();
        }

        [Key]
        public int MaAnhDinhKem { get; set; }

        [StringLength(50)]
        public string AnhBia { get; set; }

        [Column(TypeName = "xml")]
        public string AnhPreview { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiDang> BaiDangs { get; set; }
    }
}
