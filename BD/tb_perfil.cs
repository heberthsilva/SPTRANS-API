//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sptrans_api.BD
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_perfil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_perfil()
        {
            this.tb_perfil_sub_menu_adm = new HashSet<tb_perfil_sub_menu_adm>();
            this.tb_usuario_adm = new HashSet<tb_usuario_adm>();
        }
    
        public short cd_perfil { get; set; }
        public string ds_perfil { get; set; }
        public bool ic_perfil_inativo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_perfil_sub_menu_adm> tb_perfil_sub_menu_adm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_usuario_adm> tb_usuario_adm { get; set; }
    }
}