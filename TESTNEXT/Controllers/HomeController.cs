using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TESTNEXT.Models;

namespace TESTNEXT.Controllers
{
   
    public class HomeController : Controller
    {
        private TESTEntities db = new TESTEntities();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    // Nếu là Admin, trả về view About
                    ViewBag.Message = "Your application description page for Admin.";
                    return View();
                }
                else if (Session["Role"].ToString() == "User ")
                {
                    // Nếu là User, có thể chuyển hướng đến view Index hoặc một view khác
                    return RedirectToAction("Index");
                }
            }

            // Nếu không có quyền, trả về một view thông báo
            ViewBag.Message = "Bạn không được phép truy cập";
            return View("Login"); // Giả sử bạn có một view AccessDenied
        }

        public ActionResult Contact()
        {
            if (Session["Role"] != null)
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    // Nếu là Admin, trả về view About
                    ViewBag.Message = "Your application description page for Admin.";
                    return View();
                }
                else if (Session["Role"].ToString() == "User ")
                {
                    // Nếu là User, có thể chuyển hướng đến view Index hoặc một view khác
                    return RedirectToAction("Index");
                }
            }

            // Nếu không có quyền, trả về một view thông báo
            ViewBag.Message = "Bạn không được phép truy cập";
            return View("Index"); // Giả sử bạn có một view AccessDenied
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user)
        {
            using (var db = new TESTEntities())
            {
                var validUser = db.Users.FirstOrDefault(u => u.UserName == user.UserName && u.UserPassword == user.UserPassword);
                if (validUser != null)
                {
                    FormsAuthentication.SetAuthCookie(validUser.UserName, false);
                    Session["Role"] = validUser.RoleUser;  // Lưu vai trò người dùng trong session
                    Session["User Name"] =validUser.UserName;
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ErrorMessage = "Invalid Username or Password";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Role"] = null;  // Xóa session vai trò người dùng
            return RedirectToAction("Login", "Home");
        }

    }
}