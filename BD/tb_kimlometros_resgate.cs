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
    
    public partial class tb_EMPRESA3lometros_resgate
    {
        public long cd_EMPRESA3lometros_resgate { get; set; }
        public long cd_usuario { get; set; }
        public int qt_pontos_resgatados { get; set; }
        public System.DateTime dt_data_resgate { get; set; }
        public decimal vl_valor_creditado { get; set; }
        public Nullable<long> id_conta { get; set; }
        public string ds_chave_transacao { get; set; }
    }
}