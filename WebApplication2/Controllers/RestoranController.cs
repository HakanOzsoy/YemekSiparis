using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    
    public class RestoranController : Controller
    {
        // GET: Restoran
        YemekSiparisEntities n = new YemekSiparisEntities();
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            if(User.Identity.Name == "")
            {
                List<RestoranListele2> restoranList = n.RestoranListele2.ToList();
                return View(restoranList);
            }
            else
            {
                Kullanici k = n.Kullanici.FirstOrDefault(x => x.kullaniciAdi == User.Identity.Name);
                Adres a = n.Adres.FirstOrDefault(x => x.AdresKodu == k.AdresKodu);
                Sehir s = n.Sehir.FirstOrDefault(x => x.PlakaKodu == a.SehirKodu);

                string sehirAdi = s.Ad;
                List<RestoranListele2> restoranList = n.RestoranListele2.SqlQuery("Select * from RestoranListele2 where SehirAdi=@p0", sehirAdi).ToList();
                return View(restoranList);
            }   
            
        }

        [AllowAnonymous]
        public ActionResult Detay(int id)
        {
            RestoranListele2 r = n.RestoranListele2.FirstOrDefault(x => x.RestoranKodu == id);
            //List<UrunMenusu> menu = n.UrunMenusu.ToList();
            List<UrunMenusuP_Result> menu = n.UrunMenusuP().ToList();
            
            ViewBag.urunMenu = menu;
            
            return View(r);
        }

        [Authorize(Roles = "R")]
        public ActionResult urunSil(int urunID, int menuID, int resKod)
        {
            UrunMenu urunM = n.UrunMenu.FirstOrDefault(x => x.UrunKodu == urunID && x.MenuKodu == menuID);
            Siparis sip = n.Siparis.FirstOrDefault(x => x.UMKodu == urunM.UrunMenuNo);

            if (sip == null)
            {
                n.UrunMenu.Remove(urunM);
            }
            else
            {
                while(sip != null)
                {
                    n.Siparis.Remove(sip);
                    n.SaveChanges();
                    sip = n.Siparis.FirstOrDefault(x => x.UMKodu == urunM.UrunMenuNo);
                }
                n.UrunMenu.Remove(urunM);
                
            }
            
            n.SaveChanges();
            return RedirectToAction("Detay", new { id = resKod });
        }

        [Authorize(Roles = "R")]
        public ActionResult urunEkle(int id)
        {
            ViewBag.menuK = id;
            return View();
        }

        public ActionResult urunEkle2(Urun u, UrunMenu um)
        {
            n.Urun.Add(u);
            n.SaveChanges();
            um.UrunKodu = u.UrunKodu;
            n.UrunMenu.Add(um);
            n.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}