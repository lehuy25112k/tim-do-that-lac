using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModalEF.EF;

namespace timdothatlac.Controllers
{
    public class DangKyNhanLaisController : Controller
    {
        private ContextDB db = new ContextDB();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DangKyNhanLai dangKyNhanLai, int id)
        {
            var session = (timdothatlac.Common.LoginUserSession)Session[timdothatlac.Common.Constant.USER_SESSION];

            if (ModelState.IsValid)
            {
                dangKyNhanLai.MaTaiKhoan = session.MaUser;
                dangKyNhanLai.MaBaiDang = id;
                dangKyNhanLai.NgayDangKyNhan = DateTime.Now;
                db.DangKyNhanLais.Add(dangKyNhanLai);
                db.SaveChanges();
                return RedirectToAction("Index", "BaiDangs");
            }

            ViewBag.MaBaiDang = new SelectList(db.BaiDangs, "MaBaiDang", "TieuDe", dangKyNhanLai.MaBaiDang);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien", dangKyNhanLai.MaTaiKhoan);
            return View(dangKyNhanLai);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
