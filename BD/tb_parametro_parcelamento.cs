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
    
    public partial class tb_parametro_parcelamento
    {
        public int cd_parametro_parcelamento { get; set; }
        public Nullable<int> nu_qtd_max_parcelas { get; set; }
        public Nullable<decimal> vl_min_parcela { get; set; }
        public Nullable<decimal> vl_max_parcelamento { get; set; }
        public Nullable<decimal> vl_min_parcelamento { get; set; }
        public Nullable<decimal> vl_taxa_mes { get; set; }
        public Nullable<short> cd_gateway { get; set; }
    
        public virtual tb_gateway tb_gateway { get; set; }
    }
}