using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class GeciciSatinAlimMV
    {
        public int id_SatinAlinan { get; set; }
        public int id_Kitap { get; set; }
        public int adet { get; set; }
        public double birimFiyat { get; set; }
    }
}