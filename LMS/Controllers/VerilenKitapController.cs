using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VeritabanıKatman;

namespace LMS.Controllers
{
    public class VerilenKitapController : Controller
    {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();

        // GET: VerilenKitap
        public ActionResult VerilenKitap()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var tbl_VerilenKitap = db.tbl_VerilenKitap.Include(t => t.tbl_Calisan).Include(t => t.tbl_Kitap).Include(t => t.tbl_Kitap1).Include(t => t.tbl_Kitap2).Include(t => t.tbl_Kullanici).Include(t => t.tbl_Kullanici1).Where(b => b.durum == true && b.rezerveKopya == false);
            return View(tbl_VerilenKitap.ToList());
        }

        public ActionResult RezerveKitap()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var tbl_VerilenKitap = db.tbl_VerilenKitap.Include(t => t.tbl_Calisan).Include(t => t.tbl_Kitap).Include(t => t.tbl_Kitap1).Include(t => t.tbl_Kitap2).Include(t => t.tbl_Kullanici).Include(t => t.tbl_Kullanici1).Where(b => b.durum == false && b.rezerveKopya == true && b.donusTarihi > DateTime.Now);
            return View(tbl_VerilenKitap.ToList());
        }

        public ActionResult DonusBekleyenKitap()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            List<tbl_VerilenKitap> list = new List<tbl_VerilenKitap>();
            var tbl_VerilenKitap = db.tbl_VerilenKitap.Where(b => b.durum == true && b.rezerveKopya == true).ToList();
            foreach (var item in tbl_VerilenKitap)
            {

                var donustarihi = item.donusTarihi;
                int gunsayisi = (donustarihi - DateTime.Now).Days;
                if (gunsayisi <= 3)
                {
                    list.Add(new tbl_VerilenKitap
                    {
                        id_Kitap = item.id_Kitap,
                        tbl_Kitap = item.tbl_Kitap,
                        aciklama = item.aciklama,
                        id_Calisan = item.id_Calisan,
                        tbl_Calisan = item.tbl_Calisan,
                        id_VerilenKitap = item.id_VerilenKitap,
                        verilenKopya = item.verilenKopya,
                        verilisTarihi = item.verilisTarihi,
                        rezerveKopya = item.rezerveKopya,
                        donusTarihi = item.donusTarihi,
                        durum = item.durum,
                        id_Kullanici = item.id_Kullanici,
                        tbl_Kullanici = item.tbl_Kullanici,
                    });
                }
            }

               return View(list.ToList());
        }

       

        // GET: VerilenKitap/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_VerilenKitap tbl_VerilenKitap = db.tbl_VerilenKitap.Find(id);
            if (tbl_VerilenKitap == null)
            {
                return HttpNotFound();
            }
            return View(tbl_VerilenKitap);
        }

        // GET: VerilenKitap/Create
        public ActionResult Create()
        {
            ViewBag.id_Calisan = new SelectList(db.tbl_Calisan, "id_Calisan", "tamAdi","0");
            ViewBag.id_Kitap = new SelectList(db.tbl_Kitap, "id_Kitap", "kitapBaslik","0");
            ViewBag.id_Kitap = new SelectList(db.tbl_Kitap, "id_Kitap", "kitapBaslik","0");
            ViewBag.id_Kitap = new SelectList(db.tbl_Kitap, "id_Kitap", "kitapBaslik","0");
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi","0");
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi","0");
            return View();
        }

        // POST: VerilenKitap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_VerilenKitap tbl_VerilenKitap)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_VerilenKitap.id_Kullanici = kullaniciId;
            
            if (ModelState.IsValid)
            {
                var find = db.tbl_VerilenKitap.Where(b => b.donusTarihi >= DateTime.Now && b.id_Kitap == tbl_VerilenKitap.id_Kitap && (b.durum == true || b.rezerveKopya == true)).ToList();
                int verilenkitapsayi = 0;
                
                foreach (var item in find)
                {
                    verilenkitapsayi = verilenkitapsayi + item.verilenKopya;
                }

                var kitapstok = db.tbl_Kitap.Where(b => b.id_Kitap == tbl_VerilenKitap.id_Kitap).FirstOrDefault();
                if ((verilenkitapsayi == kitapstok.toplamKopya) || (verilenkitapsayi + tbl_VerilenKitap.verilenKopya > kitapstok.toplamKopya))
                {
                    ViewBag.Message = "Stok boş";
                    return View(tbl_VerilenKitap);
                }


                db.tbl_VerilenKitap.Add(tbl_VerilenKitap);
                db.SaveChanges();
                ViewBag.Message = "Kitap başarıyla verildi";
                return RedirectToAction("Index");
            }

            ViewBag.id_Calisan = new SelectList(db.tbl_Calisan, "id_Calisan", "tamAdi", tbl_VerilenKitap.id_Calisan);
            ViewBag.id_Kitap = new SelectList(db.tbl_Kitap, "id_Kitap", "kitapBaslik", tbl_VerilenKitap.id_Kitap);
            ViewBag.id_Kitap = new SelectList(db.tbl_Kitap, "id_Kitap", "kitapBaslik", tbl_VerilenKitap.id_Kitap);
            ViewBag.id_Kitap = new SelectList(db.tbl_Kitap, "id_Kitap", "kitapBaslik", tbl_VerilenKitap.id_Kitap);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_VerilenKitap.id_Kullanici);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_VerilenKitap.id_Kullanici);
            return View(tbl_VerilenKitap);
        }

        public ActionResult KitapDonus(int? id) 
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kulllaniciId = Convert.ToInt32(Convert.ToInt32(Convert.ToString(Session["id_Kullanici"])));

            var kitap = db.tbl_VerilenKitap.Find(id);
            int ceza = 0;
            var donustarihi = kitap.donusTarihi;
            int gunsayisi = (DateTime.Now - donustarihi).Days;
            if (gunsayisi > 0)
            {
                ceza = 20 * gunsayisi;
            }
            if (kitap.durum == true && kitap.rezerveKopya == false)
            {
                var kitapDonus = new tbl_KitapDonus()
                {
                    id_Kitap = kitap.id_Kitap,
                    gecerliTarih = DateTime.Now,
                    id_Calisan = kitap.id_Calisan,
                    verilisTarihi = kitap.verilisTarihi,
                    donusTarihi = kitap.donusTarihi,
                    id_Kullanici = kulllaniciId,
                };
                db.tbl_KitapDonus.Add(kitapDonus);
                db.SaveChanges();
            }

            kitap.durum = false;
            kitap.rezerveKopya = false;
            db.Entry(kitap).State = EntityState.Modified;
            db.SaveChanges();

            if (ceza > 0)
            {
                var cezaekle = new tbl_KitapCeza()
                {
                    id_Kitap = kitap.id_Kitap,
                    id_Calisan = kitap.id_Calisan,
                    cezaMiktar = ceza,
                    cezaTarihi = DateTime.Now,
                    kalanGun = gunsayisi,
                    kalanMiktar = 0,
                    id_Kullanici = kulllaniciId,
                };
                db.tbl_KitapCeza.Add(cezaekle);
                db.SaveChanges();
            }
            return RedirectToAction("VerilenKitap");
        }


      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
