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
    
    public partial class tb_operadora_banco_conta
    {
        public short cd_operadora_banco_conta { get; set; }
        public int cd_operadora { get; set; }
        public string ds_cod_banco { get; set; }
        public string ds_ag_banco { get; set; }
        public string ds_ag_banco_dv { get; set; }
        public string ds_cc { get; set; }
        public string ds_cc_dv { get; set; }
        public string ds_cpf_cnpj_favorecido { get; set; }
        public string ds_nome_favorecido { get; set; }
        public System.DateTime dt_cadastro { get; set; }
        public bool ic_inativo { get; set; }
    
        public virtual tb_operadora tb_operadora { get; set; }
    }
}
