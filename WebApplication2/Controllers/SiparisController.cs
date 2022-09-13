using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SiparisController : Controller
    {
        YemekSiparisEntities n = new YemekSiparisEntities();
        List<UrunMenu> sepet = new List<UrunMenu>();

        // GET: Siparis
        [Authorize(Roles = "M,A")]
        public ActionResult Index()
        {
            List<SiparisListe> siparisList = n.SiparisListe.ToList();

            return View(siparisList.OrderByDescending(x => x.SiparisKodu).ToList());
        }

        [Authorize(Roles = "R,A")]
        public ActionResult RIndex()
        {
            List<SiparisListe> siparisList = n.SiparisListe.ToList();

            return View(siparisList.OrderByDescending(x => x.SiparisKodu).ToList());
        }

        [Authorize(Roles = "M,A")]
        public ActionResult Ekle(int urunID, int menuID, int resKod)
        {
            Kullanici k = n.Kullanici.FirstOrDefault(x => x.kullaniciAdi == User.Identity.Name);
            UrunMenu um = n.UrunMenu.FirstOrDefault(x => x.MenuKodu == menuID && x.UrunKodu == urunID);

            Siparis sip = new Siparis();
            sip.KullaniciID = k.Id;
            sip.RestoranKodu = resKod;
            sip.UMKodu = um.UrunMenuNo;
            sip.ToplamTutar = um.Fiyat;
            sip.Tamamlandi = 0;
            n.Siparis.Add(sip);
            n.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult urunCikar(int id)
        {
            Siparis s = n.Siparis.FirstOrDefault(x => x.SiparisKodu == id);
            n.Siparis.Remove(s);
            n.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Tamamla(int id)
        {
            List<SiparisListe> siparisList = n.SiparisListe.ToList();
            foreach(SiparisListe s in siparisList)
            {
                if(s.KullaniciID==id && s.Tamamlandi == 0)
                {
                    Siparis siparis = n.Siparis.FirstOrDefault(x => x.SiparisKodu == s.SiparisKodu);
                    siparis.Tamamlandi = 1;
                    n.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}