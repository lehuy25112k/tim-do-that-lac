namespace ModalEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            BaiDangs = new HashSet<BaiDang>();
            DangKyNhanLais = new HashSet<DangKyNhanLai>();
            DanhGias = new HashSet<DanhGia>();
            TheoDois = new HashSet<TheoDoi>();
            TheoDois1 = new HashSet<TheoDoi>();
        }

        [Key]
        public int MaTaiKhoan { get; set; }

        public int? MaQuyen { get; set; }

        [StringLength(13)]
        public string MaSinhVien { get; set; }

        [StringLength(50)]
        public string Ten { get; set; }

        public bool? GioiTinh { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(256)]
        public string MatKhau { get; set; }

        [StringLength(10)]
        public string SDT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(50)]
        public string AnhDaiDien { get; set; }

        [StringLength(50)]
        public string AnhTheSV { get; set; }

        public bool? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiDang> BaiDangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKyNhanLai> DangKyNhanLais { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGia> DanhGias { get; set; }

        public virtual Quyen Quyen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TheoDoi> TheoDois { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TheoDoi> TheoDois1 { get; set; }
    }
}
