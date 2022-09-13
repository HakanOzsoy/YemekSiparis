using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ProfileController : Controller
    {
        YemekSiparisEntities n = new YemekSiparisEntities();
        // GET: Profile
        public ActionResult Index()
        {
            Kullanici k = n.Kullanici.FirstOrDefault(x => x.kullaniciAdi == User.Identity.Name);
            Adres a = n.Adres.FirstOrDefault(x => x.AdresKodu == k.AdresKodu);
            ViewBag.detayli = a.DetayliAdres;

            return View();
        }
    }
}