namespace ModalEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangKyNhanLai")]
    public partial class DangKyNhanLai
    {
        [Key]
        public int MaDangKyNhan { get; set; }

        public int? MaBaiDang { get; set; }

        public int? MaTaiKhoan { get; set; }

        [StringLength(300)]
        public string LoiNhan { get; set; }

        public DateTime? NgayDangKyNhan { get; set; }

        public bool? TrangThai { get; set; }

        public virtual BaiDang BaiDang { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
