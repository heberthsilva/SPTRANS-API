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
    
    public partial class tb_integracao_historico
    {
        public int cd_integracao_historico { get; set; }
        public Nullable<int> cd_integracao { get; set; }
        public Nullable<System.DateTime> dt_indisponibilidade { get; set; }
        public Nullable<System.DateTime> dt_retorno { get; set; }
    
        public virtual tb_integracao tb_integracao { get; set; }
    }
}
