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
    
    public partial class tb_usuario_banco_conta_chave
    {
        public long cd_usuario_banco_conta_chave { get; set; }
        public long cd_usuario_banco_conta { get; set; }
        public string ds_chave { get; set; }
        public string ds_valorchave { get; set; }
        public string ds_tipochave { get; set; }
        public bool ic_principal { get; set; }
        public Nullable<System.DateTime> dt_cadastro { get; set; }
        public string cd_idtx { get; set; }
        public string ds_text_pixcode { get; set; }
        public string ds_img_pixcode { get; set; }
        public string ds_emv_pixcode { get; set; }
        public string ds_url_pixcode { get; set; }
        public Nullable<System.DateTime> dt_cadastro_pixcode { get; set; }
        public bool ic_inativo { get; set; }
        public Nullable<System.DateTime> dt_inativo { get; set; }
    
        public virtual tb_usuario_banco_conta tb_usuario_banco_conta { get; set; }
    }
}
