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
    
    public partial class tb_EMPRESA3lometros
    {
        public long cd_EMPRESA3lometros { get; set; }
        public long cd_usuario { get; set; }
        public long cd_usuario_cartao { get; set; }
        public string ds_transacao { get; set; }
        public int vl_pontos { get; set; }
        public System.DateTime dt_transacao { get; set; }
        public System.DateTime dt_expiracao { get; set; }
        public Nullable<System.DateTime> dt_atualizacao { get; set; }
        public Nullable<bool> ic_resgatado { get; set; }
        public Nullable<bool> ic_expirado { get; set; }
        public string ds_linha { get; set; }
    
        public virtual tb_usuario tb_usuario { get; set; }
        public virtual tb_usuario_cartao tb_usuario_cartao { get; set; }
    }
}
