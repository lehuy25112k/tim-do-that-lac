namespace ModalEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiDang")]
    public partial class BaiDang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiDang()
        {
            DangKyNhanLais = new HashSet<DangKyNhanLai>();
        }

        [Key]
        public int MaBaiDang { get; set; }

        public int? MaDanhMuc { get; set; }

        public int? MaTaiKhoan { get; set; }

        public int? MaAnhDinhKem { get; set; }

        public int? MaTrangThaiBaiDang { get; set; }

        [StringLength(50)]
        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgayDuyet { get; set; }

        public bool? TrangThaiDuyet { get; set; }

        public virtual AnhDinhKem AnhDinhKem { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual TrangThaiBaiDang TrangThaiBaiDang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKyNhanLai> DangKyNhanLais { get; set; }
    }
}
