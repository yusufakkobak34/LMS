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
    public class KitapTipiController : Controller
    {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();

        // GET: KitapTipi
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            return View(db.tbl_KitapTipi.ToList());
        }

        // GET: KitapTipi/Details/5
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
            tbl_KitapTipi tbl_KitapTipi = db.tbl_KitapTipi.Find(id);
            if (tbl_KitapTipi == null)
            {
                return HttpNotFound();
            }
            return View(tbl_KitapTipi);
        }

        // GET: KitapTipi/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        // POST: KitapTipi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_KitapTipi tbl_KitapTipi)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_KitapTipi.id_Kullanici = kullaniciId;

            if (ModelState.IsValid)
            {
                db.tbl_KitapTipi.Add(tbl_KitapTipi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_KitapTipi);
        }

        // GET: KitapTipi/Edit/5
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
            tbl_KitapTipi tbl_KitapTipi = db.tbl_KitapTipi.Find(id);
            if (tbl_KitapTipi == null)
            {
                return HttpNotFound();
            }
            return View(tbl_KitapTipi);
        }

        // POST: KitapTipi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_KitapTipi tbl_KitapTipi)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_KitapTipi.id_Kullanici = kullaniciId;

            if (ModelState.IsValid)
            {
                db.Entry(tbl_KitapTipi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_KitapTipi);
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
