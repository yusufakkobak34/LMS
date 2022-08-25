//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VeritabanıKatman
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class tbl_KitapCeza
    {
        public int id_KitapCeza { get; set; }
        [Required(ErrorMessage = "Lütfen çalışan seçin")]
        public int id_Calisan { get; set; }
        [Required(ErrorMessage = "Lütfen kitap seçin")]
        public int id_Kitap { get; set; }
        public int id_Kullanici { get; set; }
        [Required(ErrorMessage = "Lütfen tarih seçin")]
        [DataType(DataType.Date)]
        public System.DateTime cezaTarihi { get; set; }
        [Required(ErrorMessage = "Lütfen ceza miktarını girin")]
        public double cezaMiktar { get; set; }
        [Required(ErrorMessage = "Lütfen kalan ceza miktarını girin")]
        public Nullable<double> kalanMiktar { get; set; }
        [Required(ErrorMessage = "Lütfen kalan günü girinw")]
        public int kalanGun { get; set; }
    
        public virtual tbl_Calisan tbl_Calisan { get; set; }
    }
}
