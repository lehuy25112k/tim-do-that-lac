using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using timdothatlac.Common;
using timdothatlac.Models;
using ModalEF.DAO;

namespace timdothatlac.Controllers
{
    public class LoginController : Controller
    {
        public LoginUserSession userSession = new LoginUserSession();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //Đăng nhập
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new TaiKhoanDao();
                if (model.MailUser == null)
                {
                    ModelState.AddModelError("", "Vui lòng không bỏ trống Email!");
                }
                else if (model.MatKhauUser == null)
                {
                    ModelState.AddModelError("", "Vui lòng không bỏ trống Mật khẩu!");
                }
                else
                {
                    var result = dao.Login(model.MailUser, Encryptor.MD5Hash(model.MatKhauUser));
                    if (result == 1)
                    {
                        var user = dao.GetById(model.MailUser);
                        userSession.MailUser = user.Email;
                        userSession.MaUser = user.MaTaiKhoan;
                        userSession.TenUser = user.Ten;
                        userSession.QuyenUser = user.MaQuyen;

                        Session.Add(Constant.USER_SESSION, userSession);
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result == 0)
                    {
                        ModelState.AddModelError("", "Email hoặc mật khẩu không chính xác!");
                    }
                }



            }
            return View("Index");
        }
        //Đăng xuất
        public ActionResult Logout()
        {
            Session[Constant.USER_SESSION] = null;
            return Redirect("/");
        }
    }
}