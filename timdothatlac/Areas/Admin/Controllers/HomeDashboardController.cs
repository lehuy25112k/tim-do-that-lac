using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using timdothatlac.Common;

namespace timdothatlac.Areas.Admin.Controllers
{
    public class HomeDashboardController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        //Đăng xuất
        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return RedirectToAction("Index", "Login", routeValues: new { Area = "" });
        }
    }
}