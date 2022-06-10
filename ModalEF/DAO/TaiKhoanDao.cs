using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ModalEF.EF;
using PagedList;

namespace ModalEF.DAO
{
    public class TaiKhoanDao
    {
        ContextDB db = null;
        public TaiKhoanDao()
        {
            db = new ContextDB();
        }

        public TaiKhoan GetById(string userMail)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.Email.Contains(userMail));
        }

        public TaiKhoan ViewDetail(int id)
        {
            return db.TaiKhoans.Find(id);
        }

        //Đăng nhập
        public int Login(string userMail, string passWord)
        {
            var result = db.TaiKhoans.SingleOrDefault(x => x.Email.Contains(userMail));
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.TrangThai == false)
                {
                    return -1;
                }
                else if (result.MatKhau.Contains(passWord))
                { return 1; }
                else
                { return 0; }
            }
        }

        //Phân trang
        public IEnumerable<TaiKhoan> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<TaiKhoan> model = db.TaiKhoans;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Ten.Contains(searchString) || x.MaSinhVien.Contains(searchString));
            }

            return model.OrderByDescending(x => x.NgayTao).ToPagedList(page, pageSize);
        }

        //Sửa
        public bool Update(TaiKhoan entity)
        {
            try
            {
                var taiKhoan = db.TaiKhoans.Find(entity.MaTaiKhoan);
                taiKhoan.Ten = entity.Ten;
                if (!string.IsNullOrEmpty(entity.MatKhau))
                {
                    taiKhoan.MatKhau = entity.MatKhau;
                }
                taiKhoan.MaSinhVien = entity.MaSinhVien;
                taiKhoan.Ten = entity.Ten;
                taiKhoan.GioiTinh = entity.GioiTinh;
                taiKhoan.SDT = entity.SDT;
                taiKhoan.NgaySinh = entity.NgaySinh;
                taiKhoan.AnhDaiDien = entity.AnhDaiDien;
                taiKhoan.AnhTheSV = entity.AnhTheSV;
                taiKhoan.TrangThai = entity.TrangThai;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Lỗi đăng nhập
                return false;
            }

        }

        //Thêm
        public long Insert(TaiKhoan taiKhoan)
        {
            db.TaiKhoans.Add(taiKhoan);
            db.SaveChanges();
            return taiKhoan.MaTaiKhoan;
        }

        //Xoá
        public bool Delete(int id)
        {
            try
            {
                var nv = db.TaiKhoans.Find(id);
                db.TaiKhoans.Remove(nv);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            { return false; }
        }

        //Đổi trạng thái
        public bool ChangeStatus(int id)
        {
            var taiKhoan = db.TaiKhoans.Find(id);
            taiKhoan.TrangThai = !taiKhoan.TrangThai;
            db.SaveChanges();
            return taiKhoan.TrangThai;
        }

        //Gửi mail
        public string SendEmail(string receiverMail)
        {
            Random rd = new Random();
            var code = rd.Next(100000, 999999).ToString();
            var message = "Cảm ơn bạn đã đăng ký tài khoản trên timdothatlac.vn. Mã xác nhận của bạn là: " + code;
            var subject = "KÍCH HOẠT TÀI KHOẢN CỦA BẠN TRÊN TIMDOTHATLAC.VN";
            try
            {
                var senderEmail = new MailAddress("timdothatlac.vn@gmail.com", "timdothatlac");
                var receiverEmail = new MailAddress(receiverMail, "ReceiverMail");
                var password = "DLH1811505310";
                var sub = subject;
                var body = message;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };

                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }

                return code;

            }
            catch (Exception e)
            {
                var a = e;
            }
            return null;
        }
    }
}
