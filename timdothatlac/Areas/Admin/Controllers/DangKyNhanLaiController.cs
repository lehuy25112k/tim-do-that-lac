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

namespace timdothatlac.Areas.Admin.Controllers
{
    public class DangKyNhanLaiController : BaseController
    {
        private ContextDB db = new ContextDB();

        //Phân trang list, default = 10
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BaiDangDao();
            var model = dao.ListAllPagingDKN(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DangKyNhanLai dknl = db.DangKyNhanLais.Find(id);
            if (dknl == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaBaiDang = new SelectList(db.BaiDangs, "MaBaiDang", "TieuDe");
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "Ten");
            return View(dknl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Đăng xuất
        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login", routeValues: new { Area = "" });
        }
    }
}
