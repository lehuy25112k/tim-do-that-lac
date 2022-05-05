namespace ModalEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrangThaiBaiDang")]
    public partial class TrangThaiBaiDang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrangThaiBaiDang()
        {
            BaiDangs = new HashSet<BaiDang>();
        }

        [Key]
        public int MaTrangThaiBaiDang { get; set; }

        [StringLength(50)]
        public string TenTrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiDang> BaiDangs { get; set; }
    }
}
