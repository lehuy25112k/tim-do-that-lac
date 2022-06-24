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

namespace timdothatlac.Controllers
{
    public class TaiKhoansController : BaseController
    {
        private ContextDB db = new ContextDB();

        //Xem chi tiết
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        //Sửa
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", taiKhoan.MaQuyen);
            return View(taiKhoan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaiKhoan taiKhoan, HttpPostedFileBase file)
        {
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
                        taiKhoan.AnhDaiDien = file.FileName;
                    }

                    db.Entry(taiKhoan).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = taiKhoan.MaTaiKhoan });
                }
                catch (Exception e) { }
            }
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", taiKhoan.MaQuyen);
            return View(taiKhoan);
        }

        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login", routeValues: new { Area = "" });
        }
    }
}