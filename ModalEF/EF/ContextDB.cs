namespace ModalEF.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContextDB : DbContext
    {
        public ContextDB()
            : base("name=ContextDB")
        {
        }

        public virtual DbSet<AnhDinhKem> AnhDinhKems { get; set; }
        public virtual DbSet<BaiDang> BaiDangs { get; set; }
        public virtual DbSet<DangKyNhanLai> DangKyNhanLais { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TheoDoi> TheoDois { get; set; }
        public virtual DbSet<TrangThaiBaiDang> TrangThaiBaiDangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaSinhVien)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Email)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.TheoDois)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.MaTaiKhoan);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.TheoDois1)
                .WithOptional(e => e.TaiKhoan1)
                .HasForeignKey(e => e.MaiTaiKhoanDuocTheoDoi);
        }
    }
}
