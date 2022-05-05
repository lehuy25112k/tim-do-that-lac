namespace ModalEF.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheoDoi")]
    public partial class TheoDoi
    {
        [Key]
        public int MaTheoDoi { get; set; }

        public int? MaTaiKhoan { get; set; }

        public int? MaiTaiKhoanDuocTheoDoi { get; set; }

        public DateTime? NgayTao { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual TaiKhoan TaiKhoan1 { get; set; }
    }
}
