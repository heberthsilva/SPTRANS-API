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
    
    public partial class tb_usuario_cupom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_usuario_cupom()
        {
            this.tb_pedido = new HashSet<tb_pedido>();
        }
    
        public long cd_usuario_cupom { get; set; }
        public long cd_usuario { get; set; }
        public long cd_cupom { get; set; }
        public System.DateTime dt_cadastro { get; set; }
    
        public virtual tb_cupom tb_cupom { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_pedido> tb_pedido { get; set; }
        public virtual tb_usuario tb_usuario { get; set; }
    }
}
