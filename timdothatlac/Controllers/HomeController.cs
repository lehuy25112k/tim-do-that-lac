using ModalEF.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using timdothatlac.Common;

namespace timdothatlac.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var baiDangDao = new BaiDangDao();
            var danhMucDao = new DanhMucDao();
            ViewBag.ListFeatureBaiDangs = baiDangDao.ListFeatureBaiDang(3);
            ViewBag.ListFeatureDanhMucs = danhMucDao.ListFeatureDanhMuc(3);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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