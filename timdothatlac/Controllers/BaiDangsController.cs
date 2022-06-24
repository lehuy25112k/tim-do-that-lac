using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModalEF.DAO;
using ModalEF.EF;
using timdothatlac.Common;

namespace timdothatlac.Controllers
{
    public class BaiDangsController : BaseController
    {
        private ContextDB db = new ContextDB();
        public AnhDinhKem adk = new AnhDinhKem();

        public ActionResult Index(string searchString, int page = 1, int pageSize = 9)
        {
            var dao = new BaiDangDao();
            var model = dao.ListAllPagings(searchString, page, pageSize);

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
                string path = Server.MapPath("~/FileUpload");
                try
                {
                    if (file != null)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string pathFull = Path.Combine(path, fileName);
                        file.SaveAs(pathFull);
                        adk.AnhBia = file.FileName;
                        var a = db.AnhDinhKems.Add(adk);
                        baiDang.MaAnhDinhKem = a.MaAnhDinhKem;
                    }
                    else
                    {
                        baiDang.MaAnhDinhKem = null;
                    }
                }
                catch (Exception e) { }

                baiDang.NgayTao = DateTime.Now;
                baiDang.MaTaiKhoan = session.MaUser;
                baiDang.LuotXem = 0;
                baiDang.TrangThaiDuyet = false;

                db.BaiDangs.Add(baiDang);
                db.SaveChanges();
                return RedirectToAction("Index", "BaiDangs");
            }

            ViewBag.MaAnhDinhKem = new SelectList(db.AnhDinhKems, "MaAnhDinhKem", "AnhBia", baiDang.MaAnhDinhKem);
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", baiDang.MaDanhMuc);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien", baiDang.MaTaiKhoan);
            ViewBag.MaTrangThaiBaiDang = new SelectList(db.TrangThaiBaiDangs, "MaTrangThaiBaiDang", "TenTrangThai", baiDang.MaTrangThaiBaiDang);
            return View(baiDang);
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

        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login", routeValues: new { Area = "" });
        }
    }
}