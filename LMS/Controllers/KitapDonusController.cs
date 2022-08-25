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
    public class KitapDonusController : Controller
    {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();

        // GET: KitapDonus
        public ActionResult Index()
        {
            var tbl_KitapDonus = db.tbl_KitapDonus.Include(t => t.tbl_Calisan).Include(t => t.tbl_Kullanici).Include(t => t.tbl_Kullanici1);
            return View(tbl_KitapDonus.ToList());
        }

        // GET: KitapDonus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_KitapDonus tbl_KitapDonus = db.tbl_KitapDonus.Find(id);
            if (tbl_KitapDonus == null)
            {
                return HttpNotFound();
            }
            return View(tbl_KitapDonus);
        }

        // GET: KitapDonus/Create
        public ActionResult Create()
        {
            ViewBag.id_Calisan = new SelectList(db.tbl_Calisan, "id_Calisan", "tamAdi");
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi");
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi");
            return View();
        }

        // POST: KitapDonus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_KitapDonus,id_Kullanici,id_Kitap,id_Calisan,verilisTarihi,donusTarihi,gecerliTarih")] tbl_KitapDonus tbl_KitapDonus)
        {
            if (ModelState.IsValid)
            {
                db.tbl_KitapDonus.Add(tbl_KitapDonus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Calisan = new SelectList(db.tbl_Calisan, "id_Calisan", "tamAdi", tbl_KitapDonus.id_Calisan);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_KitapDonus.id_Kullanici);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_KitapDonus.id_Kullanici);
            return View(tbl_KitapDonus);
        }

        // GET: KitapDonus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_KitapDonus tbl_KitapDonus = db.tbl_KitapDonus.Find(id);
            if (tbl_KitapDonus == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Calisan = new SelectList(db.tbl_Calisan, "id_Calisan", "tamAdi", tbl_KitapDonus.id_Calisan);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_KitapDonus.id_Kullanici);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_KitapDonus.id_Kullanici);
            return View(tbl_KitapDonus);
        }

        // POST: KitapDonus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_KitapDonus,id_Kullanici,id_Kitap,id_Calisan,verilisTarihi,donusTarihi,gecerliTarih")] tbl_KitapDonus tbl_KitapDonus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_KitapDonus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Calisan = new SelectList(db.tbl_Calisan, "id_Calisan", "tamAdi", tbl_KitapDonus.id_Calisan);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_KitapDonus.id_Kullanici);
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_KitapDonus.id_Kullanici);
            return View(tbl_KitapDonus);
        }

        // GET: KitapDonus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_KitapDonus tbl_KitapDonus = db.tbl_KitapDonus.Find(id);
            if (tbl_KitapDonus == null)
            {
                return HttpNotFound();
            }
            return View(tbl_KitapDonus);
        }

        // POST: KitapDonus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_KitapDonus tbl_KitapDonus = db.tbl_KitapDonus.Find(id);
            db.tbl_KitapDonus.Remove(tbl_KitapDonus);
            db.SaveChanges();
            return RedirectToAction("Index");
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
