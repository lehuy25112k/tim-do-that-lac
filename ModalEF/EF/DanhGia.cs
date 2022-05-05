namespace ModalEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhGia")]
    public partial class DanhGia
    {
        [Key]
        public int MaDanhGia { get; set; }

        public int? MaTaiKhoan { get; set; }

        [StringLength(50)]
        public string NoiDung { get; set; }

        public int? SoSao { get; set; }

        public DateTime? NgayTao { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
