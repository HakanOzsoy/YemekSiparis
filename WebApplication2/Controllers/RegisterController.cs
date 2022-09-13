using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        YemekSiparisEntities n = new YemekSiparisEntities();

        // GET: Register
        public ActionResult Index(int plaka)
        {
            List<Ilce> ilceList = n.Ilce.SqlQuery("Select * from ilce where SehirKodu=@p0", plaka).ToList();
            ViewBag.ilist = ilceList;
            return View();
        }

        [HttpPost]
        public ActionResult Index(Kullanici k, Adres a)
        {       
            Kullanici test = n.Kullanici.FirstOrDefault(x => x.kullaniciAdi == k.kullaniciAdi);
            if (test != null)
            {
                ViewBag.uyari = "Lütfen farklı bir kullanıcı adı seçiniz.";
                return View();
            }
            else
            {
                n.Adres.Add(a);
                n.SaveChanges();
                List<Adres> adrList = n.Adres.SqlQuery("SELECT TOP 1 * FROM Adres ORDER BY AdresKodu DESC;").ToList();

                k.AdresKodu = adrList[0].AdresKodu;
                n.Kullanici.Add(k);
                n.SaveChanges();
                return RedirectToAction("Index", "Login");
            }           
        }

        public ActionResult Index2()
        {
            List<Sehir> sehirList = n.Sehir.ToList();
            ViewBag.slist = sehirList;
            return View();
        }

        [HttpPost]
        public ActionResult Index2(Adres a)
        {
            return RedirectToAction("Index", new { plaka = a.SehirKodu });
        }
    }
}