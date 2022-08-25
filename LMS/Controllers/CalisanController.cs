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
    public class CalisanController : Controller
    {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();

        // GET: Calisan
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            var tbl_Calisan = db.tbl_Calisan.Include(t => t.tbl_Atama).Include(t => t.tbl_Departman);
            return View(tbl_Calisan.ToList());
        }

        // GET: Calisan/Details/5
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
            tbl_Calisan tbl_Calisan = db.tbl_Calisan.Find(id);
            if (tbl_Calisan == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Calisan);
        }

        // GET: Calisan/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            ViewBag.id_Atama = new SelectList(db.tbl_Atama, "id_Atama", "isim");
            ViewBag.id_Departman = new SelectList(db.tbl_Departman, "id_Departman", "adi");
            return View();
        }

        // POST: Calisan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Calisan tbl_Calisan)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_Calisan.id_Kullanici = kullaniciId;

            if (ModelState.IsValid)
            {
                db.tbl_Calisan.Add(tbl_Calisan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Atama = new SelectList(db.tbl_Atama, "id_Atama", "isim", tbl_Calisan.id_Atama);
            ViewBag.id_Departman = new SelectList(db.tbl_Departman, "id_Departman", "adi", tbl_Calisan.id_Departman);
            return View(tbl_Calisan);
        }

        // GET: Calisan/Edit/5
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
            tbl_Calisan tbl_Calisan = db.tbl_Calisan.Find(id);
            if (tbl_Calisan == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Atama = new SelectList(db.tbl_Atama, "id_Atama", "isim", tbl_Calisan.id_Atama);
            ViewBag.id_Departman = new SelectList(db.tbl_Departman, "id_Departman", "adi", tbl_Calisan.id_Departman);
            return View(tbl_Calisan);
        }

        // POST: Calisan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Calisan tbl_Calisan)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_Calisan.id_Kullanici = kullaniciId;

            if (ModelState.IsValid)
            {
                db.Entry(tbl_Calisan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Atama = new SelectList(db.tbl_Atama, "id_Atama", "isim", tbl_Calisan.id_Atama);
            ViewBag.id_Departman = new SelectList(db.tbl_Departman, "id_Departman", "adi", tbl_Calisan.id_Departman);
            return View(tbl_Calisan);
        }

        // GET: Calisan/Delete/5
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
            tbl_Calisan tbl_Calisan = db.tbl_Calisan.Find(id);
            if (tbl_Calisan == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Calisan);
        }

        // POST: Calisan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            tbl_Calisan tbl_Calisan = db.tbl_Calisan.Find(id);
            db.tbl_Calisan.Remove(tbl_Calisan);
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
