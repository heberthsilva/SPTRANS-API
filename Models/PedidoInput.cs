using System;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class PedidoInput
    {
        public long CodigoPedido { get; set; }
        public long CodigoUsuario { get; set; }
        public string DataHoraPedido { get; set; }
        public short CodigoFormaPagamento { get; set; }
        public long CodigoRequisicaoVia { get; set; }
        public short CodigoStatusPedido { get; set; }
        public string DescricaoStatusPedido { get; set; }
        public string NomeIconeStatus { get; set; }
        public decimal ValorPedido { get; set; }
        public decimal ValorTaxa { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorTotalPedido { get; set; }
        public decimal ValorEntrega { get; set; }
        public DateTime? DataPagamento { get; set; }
        public decimal? ValorPago { get; set; }
        public string ChaveTransacao { get; set; }
        public string CodigoBarrasBoleto { get; set; }
        public string LinhaDigitavelBoleto { get; set; }
        public string DataVencimentoBoleto { get; set; }
        public string UsuarioIP { get; set; }
        //public bool SavarCartaoCredito { get; set; }
        public short CodigoMotivoBaixa { get; set; }
        public DateTime? DataManualBaixa { get; set; }
        public int CodigoUsuarioAdmBaixaManual { get; set; }
        public string CorPedido { get; set; }
        public bool? ExportadoErp { get; set; }
        public DateTime? DataExportadoErp { get; set; }
        public long? CodigoUsuarioCupom { get; set; }
        public UsuarioCupomDescontoDTO UsuarioCupomDesconto { get; set; }
        //public CartaoPagamentoDTO CartaoPagamento { get; set; }
        public long CodigoUsuarioCartaoCredito { get; set; }
        public string cvvCartao { get; set; }
        public List<ItemPedidoDto> ListaItemPedido { get; set; }

        public List<DetalhePedidoHistoricoDTO> ListaDetalhePedido { get; set; }

        public AdyenDto adyen { get; set; }
        public FastCashDTO fastCash { get; set; }

        public List<LogStatusPedidoDTO> ListaLogStatusPedido { get; set; }

        public string TokeUsuario { get; set; }

        public EnderecoEntregaDto EnderecoEntrega { get; set; }

        public short CanalVenda { get; set; }

        public FastCashBankDTO FastCashBank { get; set; }

        public decimal VlTaxaDesconto { get; set; }

        public short CreditoConfianca { get; set; }

        //public short CodigoStatusPedidoItem { get; set; }

        //public string DescricaoStatusPedidoItem { get; set; }

        public int CodFidelidadeResgate { get; set; }

        public bool? RepetirPedido { get; set; }

        public string AgenciaBancaria { get; set; }

        public string NumeroContaBancaria { get; set; }

        public string BancoFastCashParaPagamento { get; set; }

        public decimal ValorTotalCashBack { get; set; }

        public bool PixExpirado { get; set; }
        public string DataPagamentoPix { get; set; }
        public string DataValidadePix { get; set; }
        public string QRCodePix { get; set; }
        public string LinkPix { get; set; }
        public string EMVPix { get; set; }

        public string UrlPicPay { get; set; }
        public bool PagamentoParcelado { get; set; }
        public ParcelasCarrinhoDTO DadosParcelamento { get; set; }
    }
}