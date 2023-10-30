using System;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class ItemPedidoDto
    {
        public long CodigoItemPedido { get; set; }
        public long CodigoPedido { get; set; }
        public long? CodigoUsuarioCartao { get; set; }
        public string NomeUsuarioCartao { get; set; }
        public string NumeroFisico { get; set; }
        public decimal ValorRecarga { get; set; }
        public string DescricaoPedidoOperadora { get; set; }
        public DateTime? DataHoraPedidoOperadora { get; set; }
        public short CodigoStatusItemPedido { get; set; }
        public string DescricaoStatusItemPedido { get; set; }
        public decimal ValorTaxa { get; set; }
        public decimal ValorDesconto { get; set; }
        public bool CartaoInativo { get; set; }
        public bool CartaoBloqueado { get; set; }
        public long CodigoOperadora { get; set; }
        public long CodigoTipoCartao { get; set; }
        public string NumeroCartao { get; set; }
        public bool HasCartaoElegivel { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public decimal ValorEntrega { get; set; }
        public decimal ValorComissao { get; set; }

        private short _tipoPedido;//1 - Recarga comum, 2 - Recarga Diaria, 3 - Recarga mensal
        public short TipoPedido
        {
            get { return _tipoPedido; }
            set
            {
                _tipoPedido = value;
                TipoPedidoDescricao = value == 1 ? "Recarga comum" : value == 2 ? "Recarga Diaria" : value == 3 ? "Recarga mensal": "Outras_Recargas";
            }
        }
        public string TipoPedidoDescricao { get; set; }
        public decimal VlTaxaDesconto { get; set; }
        public short CreditoConfianca { get; set; }
        public bool CreditoConfiancaItem { get; set; }
        public bool ProcessadoFidelidade { get; set; }
        public string dddCelRecarga { get; set; }
        public string NumeroCelRecarga { get; set; }
        public string CodigoAssinante { get; set; }
        public int TipoCartaoValor { get; set; }
        public decimal ValorCashBack { get; set; }
        public string NomeOperadora { get; set; }
        public string LogoOperadora { get; set; }
        public bool isRetirada { get; set; }
        public int codPosto { get; set; }
        public string tipoLogradouroEntrega { get; set; }
        public string logradouroEntrega { get; set; }
        public int numLogradouroEntrega { get; set; }
        public string compEntrega { get; set; }
        public string bairroEntrega { get; set; }
        public string municipioEntrega { get; set; }
        public short codUFEntrega { get; set; }
        public string cepEntrega { get; set; }
        public List<LogStatusItem> listaLogStatus { get; set; }
        public string Mensagem { get; set; }
        public string PreMensagem { get; set; }
    }
    public class LogStatusItem
    {
        public int codStatus { get; set; }
        public string descStatus { get; set; }
        public string dataStatus { get; set; }
    }
}