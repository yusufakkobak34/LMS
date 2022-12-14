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

    public partial class tbl_KitapDonus
    {
        public int id_KitapDonus { get; set; }
        public int id_Kullanici { get; set; }
        [Required(ErrorMessage = "Lütfen kitap seçin")]
        public int id_Kitap { get; set; }
        [Required(ErrorMessage = "Lütfen çalışan seçin")]
        public int id_Calisan { get; set; }
        [Required(ErrorMessage = "Lütfen veriliş tarihi seçin")]
        [DataType(DataType.Date)]    
        public System.DateTime verilisTarihi { get; set; }
        [Required(ErrorMessage = "Lütfen dönüş tarihi seçin")]
        [DataType(DataType.Date)]
        public System.DateTime donusTarihi { get; set; }
        public System.DateTime gecerliTarih { get; set; }
    
        public virtual tbl_Calisan tbl_Calisan { get; set; }
        public virtual tbl_Kullanici tbl_Kullanici { get; set; }
        public virtual tbl_Kullanici tbl_Kullanici1 { get; set; }
    }
}
