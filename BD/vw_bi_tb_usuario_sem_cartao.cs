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
    
    public partial class vw_bi_tb_usuario_sem_cartao
    {
        public long cd_usuario { get; set; }
        public string nm_usuario { get; set; }
        public string nm_sexo_usuario { get; set; }
        public Nullable<System.DateTime> dt_nascimento_usuario { get; set; }
        public bool ic_usuario_inativo { get; set; }
        public System.DateTime dt_cadastro_usuario { get; set; }
        public string ic_pessoa_fisica_judirica { get; set; }
    }
}