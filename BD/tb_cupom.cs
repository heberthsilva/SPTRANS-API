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
    
    public partial class tb_cupom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_cupom()
        {
            this.tb_cupom_usuario = new HashSet<tb_cupom_usuario>();
            this.tb_usuario_cupom = new HashSet<tb_usuario_cupom>();
        }
    
        public long cd_cupom { get; set; }
        public string ds_codigo { get; set; }
        public string ds_cupom_conteudo { get; set; }
        public System.DateTime dt_cadastro { get; set; }
        public System.DateTime dt_validade { get; set; }
        public Nullable<decimal> vl_desconto { get; set; }
        public string tp_cupom { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_cupom_usuario> tb_cupom_usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_usuario_cupom> tb_usuario_cupom { get; set; }
    }
}
