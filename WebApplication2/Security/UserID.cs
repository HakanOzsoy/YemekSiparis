using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Security
{
    public static class UserID
    {
        static YemekSiparisEntities n = new YemekSiparisEntities();

        public static bool kullaniciID(string name, int resID)
        {
            Kullanici k = n.Kullanici.FirstOrDefault(x => x.kullaniciAdi == name);
            Restoran r = n.Restoran.FirstOrDefault(x => x.RestoranKodu == resID);
            if(k == null)
            {
                return false;
            }
            else
            {
                if (r.YetkiliID == k.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }                   
        }

        public static int idBul(string name)
        {
            Kullanici k = n.Kullanici.FirstOrDefault(x => x.kullaniciAdi == name);
            return k.Id;
        }

        public static string adresBul (int id)
        {
            Kullanici k = n.Kullanici.FirstOrDefault(x => x.Id == id);
            Adres a = n.Adres.FirstOrDefault(x => x.AdresKodu == k.AdresKodu);

            return a.DetayliAdres;
        }
    }
}