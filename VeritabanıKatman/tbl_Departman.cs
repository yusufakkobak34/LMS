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

    public partial class tbl_Departman
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Departman()
        {
            this.tbl_Calisan = new HashSet<tbl_Calisan>();
            this.tbl_Kitap = new HashSet<tbl_Kitap>();
        }
    
        public int id_Departman { get; set; }
        [Required(ErrorMessage = "Lütfen departman adı girin")]
        public string adi { get; set; }
        public int id_Kullanici { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Calisan> tbl_Calisan { get; set; }
        public virtual tbl_Kullanici tbl_Kullanici { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Kitap> tbl_Kitap { get; set; }
    }
}
