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
    
    public partial class tb_usuario_solicitacao_exclusao
    {
        public int cd_usuario_solicitacao_exclusao { get; set; }
        public Nullable<long> cd_usuario { get; set; }
        public string ds_email_usuario { get; set; }
        public string nu_cpf_usuario { get; set; }
        public Nullable<System.DateTime> dt_solicitacao { get; set; }
        public Nullable<bool> ic_inativado { get; set; }
        public Nullable<bool> ic_excluido { get; set; }
        public Nullable<System.DateTime> dt_exclusao { get; set; }
    }
}