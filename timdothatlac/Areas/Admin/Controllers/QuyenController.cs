using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModalEF.EF;
using timdothatlac.Common;

namespace timdothatlac.Areas.Admin.Controllers
{
    public class QuyenController : BaseController
    {
        private ContextDB db = new ContextDB();

        public ActionResult Index()
        {
            return View(db.Quyens.ToList());
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
