using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModalEF.DAO;
using ModalEF.EF;
using timdothatlac.Common;

namespace timdothatlac.Controllers
{
    public class RegisterController : Controller
    {
        private ContextDB db = new ContextDB();

        public ActionResult Create()
        {
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTaiKhoan,MaQuyen,MaSinhVien,Ten,GioiTinh,Email,MatKhau,SDT,NgaySinh,AnhDaiDien,AnhTheSV,TrangThai")] TaiKhoan taiKhoan)
        {
            var dao = new TaiKhoanDao();

            if (ModelState.IsValid)
            {
                taiKhoan.MatKhau = Encryptor.MD5Hash(taiKhoan.MatKhau);
                taiKhoan.MaQuyen = 2;
                taiKhoan.TrangThai = true;
                taiKhoan.NgayTao = DateTime.Now;
                taiKhoan.MaXacNhan = dao.SendEmail(taiKhoan.Email);
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index", "Login");
            }
            return View(taiKhoan);
        }

        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login", routeValues: new { Area = "" });
        }
    }
}
