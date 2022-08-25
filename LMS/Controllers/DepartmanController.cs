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
    public class DepartmanController : Controller
    {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();

        // GET: Departman
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var tbl_Departman = db.tbl_Departman.Include(t => t.tbl_Kullanici);
            return View(tbl_Departman.ToList());
        }

        // GET: Departman/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Departman tbl_Departman = db.tbl_Departman.Find(id);
            if (tbl_Departman == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Departman);
        }

        // GET: Departman/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            return View();
        }

        // POST: Departman/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Departman tbl_Departman)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_Departman.id_Kullanici = kullaniciId;

            if (ModelState.IsValid)
            {
                db.tbl_Departman.Add(tbl_Departman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_Departman.id_Kullanici);
            return View(tbl_Departman);
        }

        // GET: Departman/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Departman tbl_Departman = db.tbl_Departman.Find(id);
            if (tbl_Departman == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_Departman.id_Kullanici);
            return View(tbl_Departman);
        }

        // POST: Departman/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Departman tbl_Departman)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_Departman.id_Kullanici = kullaniciId;

            if (ModelState.IsValid)
            {
                db.Entry(tbl_Departman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_Departman.id_Kullanici);
            return View(tbl_Departman);
        }

        // GET: Departman/Delete/5
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
            tbl_Departman tbl_Departman = db.tbl_Departman.Find(id);
            if (tbl_Departman == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Departman);
        }

        // POST: Departman/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            tbl_Departman tbl_Departman = db.tbl_Departman.Find(id);
            db.tbl_Departman.Remove(tbl_Departman);
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
