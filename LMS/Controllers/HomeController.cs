 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeritabanıKatman;

namespace LMS.Controllers
{
    public class HomeController : Controller
     {
        private KutuphaneOtomasyonSistemiDBEntities db = new KutuphaneOtomasyonSistemiDBEntities();

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LoginUser(string kullaniciAdi, string sifre) 
        {
            try
            {
                if (kullaniciAdi != null && sifre != null)
                {
                    var kullaniciBul = db.tbl_Kullanici.Where(u => kullaniciAdi == kullaniciAdi && u.sifre == sifre && u.aktifMi == true).ToList();
                    if (kullaniciBul.Count() == 1)
                    {
                        Session["id_Kullanici"] = kullaniciBul[0].id_Kullanici;
                        Session["id_KullaniciTipi"] = kullaniciBul[0].id_KullaniciTipi;
                        Session["kullaniciAdi"] = kullaniciBul[0].kullaniciAdi;
                        Session["sifre"] = kullaniciBul[0].sifre;
                        Session["id_Calisan"] = kullaniciBul[0].id_Calisan;

                        string url = string.Empty;
                        if (kullaniciBul[0].id_KullaniciTipi == 2)
                        {
                            return RedirectToAction("About");
                        }

                        else if (kullaniciBul[0].id_KullaniciTipi == 3)
                        {
                            return RedirectToAction("About");
                        }

                        else if (kullaniciBul[0].id_KullaniciTipi == 4)
                        {
                            return RedirectToAction("About");
                        }

                        else if (kullaniciBul[0].id_KullaniciTipi == 1)
                        {
                            url = "About";
                        }

                        else
                        {
                            url = "About";
                        }

                        return RedirectToAction(url);                    
                    }

                    else
                    {
                        Session["id_Kullanici"] = string.Empty;
                        Session["id_KullaniciTipi"] = string.Empty;
                        Session["kullaniciAdi"] = string.Empty;
                        Session["sifre"] = string.Empty;
                        Session["id_Calisan"] = string.Empty;
                        ViewBag.Message = "Kullanıcı adı veya şifre yanlış";
                    }                   
                }

                else
                {
                    Session["id_Kullanici"] = string.Empty;
                    Session["id_KullaniciTipi"] = string.Empty;
                    Session["kullaniciAdi"] = string.Empty;
                    Session["sifre"] = string.Empty;
                    Session["id_Calisan"] = string.Empty;
                    ViewBag.Message = "Beklenmeyen bir durum oluştu,iletişime geçin";
                }
                
            }
            
            catch (Exception ex)
            {
                Session["id_Kullanici"] = string.Empty;
                Session["id_KullaniciTipi"] = string.Empty;
                Session["kullaniciAdi"] = string.Empty;
                Session["sifre"] = string.Empty;
                Session["id_Calisan"] = string.Empty;
                ViewBag.Message = "Beklenmeyen bir durum oluştu,iletişime geçin";
            }

            return View("Login");


        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout() 
        {
            Session["id_Kullanici"] = string.Empty;
            Session["id_KullaniciTipi"] = string.Empty;
            Session["kullaniciAdi"] = string.Empty;
            Session["sifre"] = string.Empty;
            Session["id_Calisan"] = string.Empty;
            return RedirectToAction("Login");
        }

    }
}