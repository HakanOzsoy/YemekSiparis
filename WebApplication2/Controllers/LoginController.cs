using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Web.Security;

namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Kullanici k)
        {
            YemekSiparisEntities n = new YemekSiparisEntities();
            Kullanici user = n.Kullanici.FirstOrDefault(x => x.kullaniciAdi == k.kullaniciAdi && x.sifre == k.sifre);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.kullaniciAdi, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.mesaj = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }       
        }

        public ActionResult Logout()
        {
            string name = FormsAuthentication.FormsCookieName;
            HttpCookie authcookie = HttpContext.Request.Cookies[name];
            FormsAuthenticationTicket t = FormsAuthentication.Decrypt(authcookie.Value);
            string tname = t.Name;

            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}