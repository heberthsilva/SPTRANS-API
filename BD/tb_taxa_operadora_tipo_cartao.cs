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
    
    public partial class tb_taxa_operadora_tipo_cartao
    {
        public int cd_taxa_operadora_tipo_cartao { get; set; }
        public int cd_operadora { get; set; }
        public short cd_forma_pagamento { get; set; }
        public Nullable<decimal> vl_percentual_faturamento { get; set; }
        public short nu_qtd_dias_repasse { get; set; }
        public Nullable<System.DateTime> dt_validade_inicio { get; set; }
        public Nullable<System.DateTime> dt_validade_fim { get; set; }
    
        public virtual tb_forma_pagamento tb_forma_pagamento { get; set; }
        public virtual tb_operadora tb_operadora { get; set; }
    }
}
