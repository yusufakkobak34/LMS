using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class KitapMV
    {
        public int id_Kitap { get; set; }
        public int id_Kullanici { get; set; }       
        public int id_Departman { get; set; } 
        public int id_KitapTipi { get; set; } 
        public string kitapBaslik { get; set; }      
        public string kisaAciklama { get; set; }      
        public string yazar { get; set; }       
        public string kitapAdi { get; set; }      
        public double edisyon { get; set; }      
        public int toplamKopya { get; set; }      
        public System.DateTime kayitTarihi { get; set; }  
        public double fiyat { get; set; }   
        public string aciklama { get; set; }
    }
}