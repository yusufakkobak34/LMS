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

    public partial class tbl_KullaniciTipi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_KullaniciTipi()
        {
            this.tbl_Kullanici = new HashSet<tbl_Kullanici>();
        }
    
        public int id_KullaniciTipi { get; set; }
        [Required(ErrorMessage = "Lütfen kullanıcı tipi girin")]
        public string kullaniciTipi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Kullanici> tbl_Kullanici { get; set; }
    }
}
