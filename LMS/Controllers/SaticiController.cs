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
    public class SaticiController : Controller
    {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();

        // GET: Satici
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var tbl_Satici = db.tbl_Satici.Include(t => t.tbl_Kullanici);
            return View(tbl_Satici.ToList());
        }

        // GET: Satici/Details/5
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
            tbl_Satici tbl_Satici = db.tbl_Satici.Find(id);
            if (tbl_Satici == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Satici);
        }

        // GET: Satici/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            return View();
        }

        // POST: Satici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Satici tbl_Satici)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_Satici.id_Kullanici = kullaniciId;

            if (ModelState.IsValid)
            {

                var find = db.tbl_Satici.Where(s => s.adi == tbl_Satici.adi && s.iletisimNo == tbl_Satici.iletisimNo).FirstOrDefault();
                if (find == null)
                {
                    db.tbl_Satici.Add(tbl_Satici);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Satici zaten kayıtlı";
                }

                return View();

                db.tbl_Satici.Add(tbl_Satici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            return View(tbl_Satici);
        }

        // GET: Satici/Edit/5
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
            tbl_Satici tbl_Satici = db.tbl_Satici.Find(id);
            if (tbl_Satici == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_Satici.id_Kullanici);
            return View(tbl_Satici);
        }

        // POST: Satici/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Satici tbl_Satici)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int kullaniciId = Convert.ToInt32(Convert.ToString(Session["id_Kullanici"]));
            tbl_Satici.id_Kullanici = kullaniciId;

            if (ModelState.IsValid)
            {
                db.Entry(tbl_Satici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Kullanici = new SelectList(db.tbl_Kullanici, "id_Kullanici", "kullaniciAdi", tbl_Satici.id_Kullanici);
            return View(tbl_Satici);
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
