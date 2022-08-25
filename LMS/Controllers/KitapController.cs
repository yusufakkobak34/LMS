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
    public class KitapController : Controller
    {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();

        // GET: Kitap
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var tbl_Kitap = db.tbl_Kitap.Include(t => t.tbl_Departman).Include(t => t.tbl_KitapTipi).Include(t => t.tbl_Kullanici);
            return View(tbl_Kitap.ToList());
        }

        // GET: Kitap/Details/5
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
            tbl_Kitap tbl_Kitap = db.tbl_Kitap.Find(id);
            if (tbl_Kitap == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kitap);
        }

        // GET: Kitap/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.id_Departman = new SelectList(db.tbl_Departman, "id_Departman", "adi","0");
            ViewBag.id_KitapTipi = new SelectList(db.tbl_KitapTipi, "id_KitapTipi", "isim","0");
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi","0");
            return View();
        }

        // POST: Kitap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Kitap tbl_Kitap)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_Kitap.id_Kullanici = kullaniciId;


            if (ModelState.IsValid)
            {
                db.tbl_Kitap.Add(tbl_Kitap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Departman = new SelectList(db.tbl_Departman, "id_Departman", "adi", tbl_Kitap.id_Departman);
            ViewBag.id_KitapTipi = new SelectList(db.tbl_KitapTipi, "id_KitapTipi", "isim", tbl_Kitap.id_KitapTipi);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_Kitap.id_Kullanici);
            return View(tbl_Kitap);
        }

        // GET: Kitap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Kitap tbl_Kitap = db.tbl_Kitap.Find(id);
            if (tbl_Kitap == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Departman = new SelectList(db.tbl_Departman, "id_Departman", "adi", tbl_Kitap.id_Departman);
            ViewBag.id_KitapTipi = new SelectList(db.tbl_KitapTipi, "id_KitapTipi", "isim", tbl_Kitap.id_KitapTipi);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_Kitap.id_Kullanici);
            return View(tbl_Kitap);
        }

        // POST: Kitap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Kitap,id_Kullanici,id_Departman,id_KitapTipi,kitapBaslik,kisaAciklama,yazar,kitapAdi,edisyon,toplamKopya,kayitTarihi,fiyat,aciklama")] tbl_Kitap tbl_Kitap)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_Kitap.id_Kullanici = kullaniciId;

            if (ModelState.IsValid)
            {
                db.Entry(tbl_Kitap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Departman = new SelectList(db.tbl_Departman, "id_Departman", "adi", tbl_Kitap.id_Departman);
            ViewBag.id_KitapTipi = new SelectList(db.tbl_KitapTipi, "id_KitapTipi", "isim", tbl_Kitap.id_KitapTipi);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_Kitap.id_Kullanici);
            return View(tbl_Kitap);
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
