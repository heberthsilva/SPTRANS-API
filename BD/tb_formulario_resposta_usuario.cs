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
    
    public partial class tb_formulario_resposta_usuario
    {
        public long cd_formulario_resposta_usuario { get; set; }
        public int cd_formulario_pergunta { get; set; }
        public long cd_usuario { get; set; }
        public string ds_resposta { get; set; }
        public System.DateTime dt_resposta { get; set; }
    
        public virtual tb_formulario_pergunta tb_formulario_pergunta { get; set; }
        public virtual tb_usuario tb_usuario { get; set; }
    }
}
