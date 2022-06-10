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
    public class DanhMucController : Controller
    {
        private ContextDB db = new ContextDB();

        //Phân trang list, default = 5
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new DanhMucDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }

        //Thêm
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDanhMuc,TenDanhMuc,NgayTao")] DanhMuc danhMuc, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                //file 
                string path = Server.MapPath("~/FileUpload");
                string fileName = Path.GetFileName(file.FileName);
                string pathFull = Path.Combine(path, fileName);
                file.SaveAs(pathFull);

                danhMuc.NgayTao = DateTime.Now;
                danhMuc.AnhMinhHoa = file.FileName;
                danhMuc.LuotTim = 0;
                db.DanhMucs.Add(danhMuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(danhMuc);
        }

        //Sửa
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDanhMuc,TenDanhMuc,NgayTao")] DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhMuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhMuc);
        }

        //Xoá
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new DanhMucDao().Delete(id);
            return RedirectToAction("Index");
        }

        //Xem chi tiết
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        //Đăng xuất
        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login", routeValues: new { Area = "" });
        }
    }
}