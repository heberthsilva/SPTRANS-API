using System;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class FormaPagamentoDTO
    {
        public int CodigoFormaPagamento { get; set; }
        public string NomeFormaPagamento { get; set; }
        public bool FormaPagamentoInativo { get; set; }
        public float ValorTaxa { get; set; }
        public bool Porcentagem { get; set; }
        public string NumeroCartao { get; set; }
        public long CodigoUsuarioCartao { get; set; }
        public short CodigoGateway { get; set; }
        public int TipoPedido { get; set; }
        public string MotivoPagamentoInativo { get; set; }
        public float desconto { get; set; }
        public decimal VlTaxaDesconto { get; set; }
        public bool HasElegible { get; set; }
        public int? CodTipoCartao { get; set; }
        public decimal VlCashBack { get; set; }
    }
}