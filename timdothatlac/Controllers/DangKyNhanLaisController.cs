using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModalEF.EF;
using timdothatlac.Common;

namespace timdothatlac.Controllers
{
    public class DangKyNhanLaisController : BaseController
    {
        private ContextDB db = new ContextDB();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DangKyNhanLai dangKyNhanLai, int id, HttpPostedFileBase file)
        {
            var session = (timdothatlac.Common.LoginUserSession)Session[timdothatlac.Common.Constant.USER_SESSION];

            var baiDang = db.BaiDangs.SingleOrDefault(x => x.MaBaiDang == id);

            if (session.MaUser == baiDang.MaTaiKhoan)
            {
                ModelState.AddModelError("", "Không thể đăng ký bài đăng này vì bạn là người đăng!");
            }
            else if (ModelState.IsValid)
            {
                //file 
                string path = Server.MapPath("~/FileUpload");
                try
                {
                    if (file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string pathFull = Path.Combine(path, fileName);
                        file.SaveAs(pathFull);
                        dangKyNhanLai.AnhMinhChung = file.FileName;
                    }
                    else
                    {
                        dangKyNhanLai.AnhMinhChung = null;
                    }
                }
                catch (Exception e) { }

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

        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login", routeValues: new { Area = "" });
        }
    }
}
