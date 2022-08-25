using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeritabanıKatman;

namespace LMS.Controllers
{
    public class KitapCezasiController : Controller
    {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();
        
        
        // GET: KitapCezasi
        public ActionResult BekleyenCeza()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["id_Kullanici"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var bekleyenceza = db.tbl_KitapCeza.Where(f => f.kalanMiktar == 0);
            return View(bekleyenceza.ToList());
            
        }
    }
}