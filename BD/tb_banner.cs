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
    
    public partial class tb_banner
    {
        public int cd_banner { get; set; }
        public string ds_banner { get; set; }
        public string lst_operadora { get; set; }
        public Nullable<int> cd_acao { get; set; }
        public Nullable<System.DateTime> dt_inicio { get; set; }
        public Nullable<System.DateTime> dt_validade { get; set; }
        public Nullable<bool> ic_ativo { get; set; }
        public string ds_link_banner { get; set; }
        public string ds_link_acao { get; set; }
        public Nullable<bool> ic_prioritario { get; set; }
    
        public virtual tb_acao_banner tb_acao_banner { get; set; }
    }
}