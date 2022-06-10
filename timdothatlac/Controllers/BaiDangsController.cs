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

namespace timdothatlac.Controllers
{
    public class BaiDangsController : Controller
    {
        private ContextDB db = new ContextDB();

        //public ActionResult Index()
        //{
        //    var baiDangs = db.BaiDangs.Include(b => b.AnhDinhKem).Include(b => b.DanhMuc).Include(b => b.TaiKhoan).Include(b => b.TrangThaiBaiDang);
        //    return View(baiDangs.ToList());
        //}

        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new BaiDangDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }
    }
}