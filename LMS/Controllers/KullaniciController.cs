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
    public class KullaniciController : Controller
    {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();

        // GET: Kullanici
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            var tbl_Kullanici = db.tbl_Kullanici.Include(t => t.tbl_Calisan).Include(t => t.tbl_KullaniciTipi);
            return View(tbl_Kullanici.ToList());
        }

        // GET: Kullanici/Details/5
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
            tbl_Kullanici tbl_Kullanici = db.tbl_Kullanici.Find(id);
            if (tbl_Kullanici == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kullanici);
        }

        // GET: Kullanici/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.id_Calisan = new SelectList(db.tbl_Calisan, "id_Calisan", "tamAdi");
            ViewBag.id_KullaniciTipi = new SelectList(db.tbl_KullaniciTipi, "id_KullaniciTipi", "kullaniciTipi");
            return View();
        }

        // POST: Kullanici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Kullanici tbl_Kullanici)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                db.tbl_Kullanici.Add(tbl_Kullanici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Calisan = new SelectList(db.tbl_Calisan, "id_Calisan", "tamAdi", tbl_Kullanici.id_Calisan);
            ViewBag.id_KullaniciTipi = new SelectList(db.tbl_KullaniciTipi, "id_KullaniciTipi", "kullaniciTipi", tbl_Kullanici.id_KullaniciTipi);
            return View(tbl_Kullanici);
        }

        // GET: Kullanici/Edit/5
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
            tbl_Kullanici tbl_Kullanici = db.tbl_Kullanici.Find(id);
            if (tbl_Kullanici == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Calisan = new SelectList(db.tbl_Calisan, "id_Calisan", "tamAdi", tbl_Kullanici.id_Calisan);
            ViewBag.id_KullaniciTipi = new SelectList(db.tbl_KullaniciTipi, "id_KullaniciTipi", "kullaniciTipi", tbl_Kullanici.id_KullaniciTipi);
            return View(tbl_Kullanici);
        }

        // POST: Kullanici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Kullanici tbl_Kullanici)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tbl_Kullanici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Calisan = new SelectList(db.tbl_Calisan, "id_Calisan", "tamAdi", tbl_Kullanici.id_Calisan);
            ViewBag.id_KullaniciTipi = new SelectList(db.tbl_KullaniciTipi, "id_KullaniciTipi", "kullaniciTipi", tbl_Kullanici.id_KullaniciTipi);
            return View(tbl_Kullanici);
        }

        // GET: Kullanici/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Kullanici tbl_Kullanici = db.tbl_Kullanici.Find(id);
            if (tbl_Kullanici == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Kullanici);
        }

        // POST: Kullanici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            tbl_Kullanici tbl_Kullanici = db.tbl_Kullanici.Find(id);
            db.tbl_Kullanici.Remove(tbl_Kullanici);
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
