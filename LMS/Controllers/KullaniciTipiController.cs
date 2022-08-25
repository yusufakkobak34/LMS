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
    public class KullaniciTipiController : Controller
    {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();

        // GET: KullaniciTipi
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home"); 
            }
            
            return View(db.tbl_KullaniciTipi.ToList());
        }

        // GET: KullaniciTipi/Details/5
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
            tbl_KullaniciTipi tbl_KullaniciTipi = db.tbl_KullaniciTipi.Find(id);
            if (tbl_KullaniciTipi == null)
            {
                return HttpNotFound();
            }
            return View(tbl_KullaniciTipi);
        }

        // GET: KullaniciTipi/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            return View();
        }

        // POST: KullaniciTipi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_KullaniciTipi tbl_KullaniciTipi)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            if (ModelState.IsValid)
            {
                db.tbl_KullaniciTipi.Add(tbl_KullaniciTipi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_KullaniciTipi);
        }

        // GET: KullaniciTipi/Edit/5
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
            tbl_KullaniciTipi tbl_KullaniciTipi = db.tbl_KullaniciTipi.Find(id);
            if (tbl_KullaniciTipi == null)
            {
                return HttpNotFound();
            }
            return View(tbl_KullaniciTipi);
        }

        // POST: KullaniciTipi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_KullaniciTipi tbl_KullaniciTipi)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            if (ModelState.IsValid)
            {
                db.Entry(tbl_KullaniciTipi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_KullaniciTipi);
        }

        // GET: KullaniciTipi/Delete/5
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
            tbl_KullaniciTipi tbl_KullaniciTipi = db.tbl_KullaniciTipi.Find(id);
            if (tbl_KullaniciTipi == null)
            {
                return HttpNotFound();
            }
            return View(tbl_KullaniciTipi);
        }

        // POST: KullaniciTipi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            tbl_KullaniciTipi tbl_KullaniciTipi = db.tbl_KullaniciTipi.Find(id);
            db.tbl_KullaniciTipi.Remove(tbl_KullaniciTipi);
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
