using ModalEF.DAO;
using ModalEF.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using timdothatlac.Common;

namespace timdothatlac.Areas.Admin.Controllers
{
    public class BaiDangController : BaseController
    {
        private ContextDB db = new ContextDB();
        public AnhDinhKem adk = new AnhDinhKem();

        //Phân trang list, default = 9
        public ActionResult Index(string searchString, int page = 1, int pageSize = 9)
        {
            var dao = new BaiDangDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }

        //Thêm
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia");
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien");
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBaiDang,MaDanhMuc,MaTaiKhoan,MaAnhDinhKem,MaTrangThaiBaiDang,TieuDe,NoiDung,NgayTao,NgayDuyet,TrangThaiDuyet")] BaiDang baiDang, HttpPostedFileBase file)
        {
            var session = (timdothatlac.Common.LoginUserSession)Session[timdothatlac.Common.Constant.USER_SESSION];

            if (ModelState.IsValid)
            {
                //file 
                string path = Server.MapPath("~/FileUpload");
                string fileName = Path.GetFileName(file.FileName);
                string pathFull = Path.Combine(path, fileName);
                file.SaveAs(pathFull);
                adk.AnhBia = file.FileName;
                var a = db.AnhDinhKems.Add(adk);

                baiDang.MaAnhDinhKem = a.MaAnhDinhKem;
                baiDang.NgayTao = DateTime.Now;
                baiDang.MaTaiKhoan = session.MaUser;
                baiDang.LuotXem = 0;

                if (baiDang.TrangThaiDuyet)
                {
                    baiDang.NgayDuyet = DateTime.Now;
                }
                db.BaiDangs.Add(baiDang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia", baiDang.MaAnhDinhKem);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", baiDang.MaDanhMuc);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien", baiDang.MaTaiKhoan);
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai", baiDang.MaTrangThaiBaiDang);
            return View(baiDang);
        }

        //Sửa
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDang baiDang = db.BaiDangs.Find(id);
            if (baiDang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia", baiDang.MaAnhDinhKem);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", baiDang.MaDanhMuc);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "Ten", baiDang.MaTaiKhoan);
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai", baiDang.MaTrangThaiBaiDang);
            return View(baiDang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBaiDang,MaDanhMuc,MaTaiKhoan,MaAnhDinhKem,MaTrangThaiBaiDang,TieuDe,NoiDung,NgayTao,NgayDuyet,TrangThaiDuyet")] BaiDang baiDang, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //file 
                string path = Server.MapPath("~/FileUpload");
                string fileName = Path.GetFileName(file.FileName);
                string pathFull = Path.Combine(path, fileName);
                file.SaveAs(pathFull);
                adk.AnhBia = file.FileName;
                var a = db.AnhDinhKems.Add(adk);
                baiDang.MaAnhDinhKem = a.MaAnhDinhKem;

                baiDang.NgayTao = DateTime.Now;
                db.Entry(baiDang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia", baiDang.MaAnhDinhKem);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", baiDang.MaDanhMuc);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "Ten", baiDang.MaTaiKhoan);
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai", baiDang.MaTrangThaiBaiDang);
            return View(baiDang);
        }

        //Xoá
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new BaiDangDao().Delete(id);
            return RedirectToAction("Index");
        }

        //Xem chi tiết
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaiDang baiDang = db.BaiDangs.Find(id);
            if (baiDang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia", baiDang.MaAnhDinhKem);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", baiDang.MaDanhMuc);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "Ten", baiDang.MaTaiKhoan);
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai", baiDang.MaTrangThaiBaiDang);
            return View(baiDang);
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new BaiDangDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        //Đăng xuất
        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login", routeValues: new { Area = "" });
        }
    }
}