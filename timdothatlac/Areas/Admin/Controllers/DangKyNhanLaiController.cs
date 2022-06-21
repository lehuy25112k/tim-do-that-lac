using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModalEF.EF;

namespace timdothatlac.Areas.Admin.Controllers
{
    public class DangKyNhanLaiController : Controller
    {
        private ContextDB db = new ContextDB();

        // GET: Admin/DangKyNhanLai
        public ActionResult Index()
        {
            var dangKyNhanLais = db.DangKyNhanLais.Include(d => d.BaiDang).Include(d => d.TaiKhoan);
            return View(dangKyNhanLais.ToList());
        }

        // GET: Admin/DangKyNhanLai/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyNhanLai dangKyNhanLai = db.DangKyNhanLais.Find(id);
            if (dangKyNhanLai == null)
            {
                return HttpNotFound();
            }
            return View(dangKyNhanLai);
        }

        // GET: Admin/DangKyNhanLai/Create
        public ActionResult Create()
        {
            ViewBag.MaBaiDang = new SelectList(db.BaiDangs, "MaBaiDang", "TieuDe");
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien");
            return View();
        }

        // POST: Admin/DangKyNhanLai/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDangKyNhan,MaBaiDang,MaTaiKhoan,LoiNhan,NgayDangKyNhan,TrangThai")] DangKyNhanLai dangKyNhanLai)
        {
            if (ModelState.IsValid)
            {
                db.DangKyNhanLais.Add(dangKyNhanLai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaBaiDang = new SelectList(db.BaiDangs, "MaBaiDang", "TieuDe", dangKyNhanLai.MaBaiDang);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien", dangKyNhanLai.MaTaiKhoan);
            return View(dangKyNhanLai);
        }

        // GET: Admin/DangKyNhanLai/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyNhanLai dangKyNhanLai = db.DangKyNhanLais.Find(id);
            if (dangKyNhanLai == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBaiDang = new SelectList(db.BaiDangs, "MaBaiDang", "TieuDe", dangKyNhanLai.MaBaiDang);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien", dangKyNhanLai.MaTaiKhoan);
            return View(dangKyNhanLai);
        }

        // POST: Admin/DangKyNhanLai/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDangKyNhan,MaBaiDang,MaTaiKhoan,LoiNhan,NgayDangKyNhan,TrangThai")] DangKyNhanLai dangKyNhanLai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dangKyNhanLai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaBaiDang = new SelectList(db.BaiDangs, "MaBaiDang", "TieuDe", dangKyNhanLai.MaBaiDang);
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaSinhVien", dangKyNhanLai.MaTaiKhoan);
            return View(dangKyNhanLai);
        }

        // GET: Admin/DangKyNhanLai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyNhanLai dangKyNhanLai = db.DangKyNhanLais.Find(id);
            if (dangKyNhanLai == null)
            {
                return HttpNotFound();
            }
            return View(dangKyNhanLai);
        }

        // POST: Admin/DangKyNhanLai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DangKyNhanLai dangKyNhanLai = db.DangKyNhanLais.Find(id);
            db.DangKyNhanLais.Remove(dangKyNhanLai);
            db.SaveChanges();
            return RedirectToAction("Index");
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
