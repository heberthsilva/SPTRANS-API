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
    
    public partial class vw_bi_controle_adyen
    {
        public string Data_de_Recebimento { get; set; }
        public string Arquivo_Adyen { get; set; }
        public long Cod_Pedido_EMPRESA3 { get; set; }
        public string Bandeira_Cartão { get; set; }
        public string Forma_de_Pagamento { get; set; }
        public string TypeSeler { get; set; }
        public Nullable<decimal> ValorDebito { get; set; }
        public Nullable<decimal> ValorCredito { get; set; }
        public decimal Taxa_EMPRESA3 { get; set; }
        public string ModificationReference { get; set; }
        public Nullable<decimal> NetCreditNC { get; set; }
        public Nullable<decimal> NetDebitNC { get; set; }
        public Nullable<decimal> CommissionNC { get; set; }
        public Nullable<decimal> MarkupNC { get; set; }
        public Nullable<decimal> SchemeFeesNC { get; set; }
        public Nullable<decimal> InterchangeNC { get; set; }
        public short cd_status_pedido { get; set; }
    }
}
