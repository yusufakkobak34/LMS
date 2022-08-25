using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VeritabanıKatman;

namespace LMS.Controllers
{
    public class SatinAlimController : Controller
    {
        
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();
        
        // GET: SatinAlim  
        public ActionResult YeniSatinAlim() 
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            double toplamtutar = 0;
            var gecicisatinalim = db.tbl_SatinAlinanDetay.ToList();
            foreach (var item in gecicisatinalim)
            {
                toplamtutar += (item.adet * item.birimFiyat);
            }

            ViewBag.ToplamTutar = toplamtutar;

            return View(gecicisatinalim);
        }

        [HttpPost]
        public ActionResult UrunEkle(int BID,int adet,float fiyat) 
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var find = db.tbl_SatinAlinanDetay.Where(i => i.id_Kitap == BID).FirstOrDefault();
            if (find == null)
            {
                if (BID > 0 && adet > 0 && fiyat > 0)
                {
                    var yeniurun = new tbl_SatinAlinanDetay()
                    {
                        id_Kitap = BID,
                        adet = adet,
                        birimFiyat = fiyat
                    };

                    db.tbl_SatinAlinanDetay.Add(yeniurun);
                    db.SaveChanges();
                    ViewBag.Message = "Başarıyla eklendi";

                }
            }
            else
            {
                ViewBag.Message = "Bu kitap zaten satın alınmış";
            }
            return RedirectToAction("YeniSatinAlim");
        }

        public ActionResult DeleteConfirm(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var kitap = db.tbl_SatinAlinanDetay.Find(id);
            if (kitap != null)
            {
                db.Entry(kitap).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                ViewBag.Message = "Başarıyla silindi";
                return RedirectToAction("YeniSatinAlim");
            }

            ViewBag.Message = "Beklenmeyen bir durum oluştu,iletişime geçin";
            return View("YeniSatinAlim");
        }

        [HttpGet]
        public ActionResult KitapGetir() 
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            List<KitapMV> list = new List<KitapMV>();
            var kitaplist = db.tbl_Kitap.ToList();
            foreach (var item in kitaplist)
            {
                list.Add(new KitapMV { kitapAdi = item.kitapAdi, id_Kitap = item.id_Kitap });
            }

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SatinAlimIptal()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var list = db.tbl_SatinAlinanDetay.ToList();
            bool iptaldurumu = false;
            foreach (var item in list)
            {
                db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                int kayitdurumu = db.SaveChanges();
                if (iptaldurumu == false)
                {
                    if (kayitdurumu > 0)
                    {
                        iptaldurumu = true;
                    }
                }
            }
            if (iptaldurumu == true)
            {
                ViewBag.Message = "Satın alım iptal edildi";
                return RedirectToAction("YeniSatinAlim");
            }
            ViewBag.Message = "Beklenmeyen bir durum oluştu,iletişime geçin";
            return RedirectToAction("YeniSatinAlim");          
        }

        public ActionResult SaticiSec()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var satinalimdetay = db.tbl_SatinAlinanDetay.ToList();
            if (satinalimdetay.Count == 0)
            {
                ViewBag.Message = "Sepet boş";
                return RedirectToAction("YeniSatinAlim");
            }
            var saticilar = db.tbl_Satici.ToList();
            return View(saticilar);
        }

        [HttpPost]
        public ActionResult SatinAlimOnayla(FormCollection collection) 
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            int saticiId = 0;
            string[] keys = collection.AllKeys;
            foreach (var name in keys)
            {
                if (name.Contains("name"))
                {
                    string idname = name;
                    string[] valueids = idname.Split(' ');
                    saticiId = Convert.ToInt32(valueids[1]);

                }
            }

            var satinalimdetay = db.tbl_SatinAlinanDetay.ToList();
            double toplamtutar = 0;
            foreach (var item in satinalimdetay)
            {
                toplamtutar = toplamtutar + (item.adet * item.birimFiyat);
            }

            if (toplamtutar == 0)
            {
                ViewBag.Message = "Satın alım sepeti boş";
                return View("YeniSatinAlim");
            }

            var satinAlimHeader = new tbl_SatinAlim();
            satinAlimHeader.id_Satici = saticiId;
            satinAlimHeader.satinAlimTarihi = DateTime.Now;
            satinAlimHeader.satinAlimMiktari = toplamtutar;
            satinAlimHeader.id_Kullanici = kullaniciId;
            db.tbl_SatinAlim.Add(satinAlimHeader);
            db.SaveChanges();

            foreach (var item in satinalimdetay)
            {
                var satindetaylar = new tbl_SatinAlimDetay() 
                {
                    id_Kitap = item.id_Kitap,
                    id_SatinAlim = satinAlimHeader.id_SatinAlim,
                    adet = item.adet,
                    birimFiyat = item.birimFiyat
                };
                db.tbl_SatinAlimDetay.Add(satindetaylar);
                db.SaveChanges();

                var stokguncelle = db.tbl_Kitap.Find(item.id_Kitap);
                stokguncelle.toplamKopya = stokguncelle.toplamKopya + item.adet;
                stokguncelle.fiyat = item.birimFiyat;
                db.Entry(stokguncelle).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }

            
            db.tbl_SatinAlinanDetay.ToList().ForEach(x =>
            {
                db.Entry(x).State = System.Data.Entity.EntityState.Deleted;
            });
     

            ViewBag.Message = "Satın alım onaylandı";
            return View("TumSatinAlim");
            
            }


        public ActionResult TumSatinAlim() 
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var list = db.tbl_SatinAlim.ToList();
            return View(list);

        }

        public ActionResult SatinAlimDetayView(int? id) 
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var alimDetay = db.tbl_SatinAlimDetay.Where(b => b.id_SatinAlim == id);
            if (alimDetay == null)
            {
                return HttpNotFound();
            }

            return View(alimDetay);

        }

        }
    }

