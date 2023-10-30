using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sptrans_api.Entidades
{
        public class CitSoaDTO
        {
            public int codigoTitularRevenda { get; set; }
            public int codigoPedidoExterno { get; set; }
            public int codigoPedidoSBE { get; set; }
            public string codigoExternoCartao { get; set; }
            public string praca { get; set; }
            public string cpf { get; set; }
            public string nomeDependente { get; set; }
            public string telefone { get; set; }
            public string email { get; set; }
            public string dataInicial { get; set; }
            public string dataFinal { get; set; }
            public int codigoMotivoBloqueio { get; set; }
            public string motivoBloqueio { get; set; }
            public int codigoOcorrencia { get; set; }
            public decimal valorSolicitado { get; set; }
            public int formaDePagamento { get; set; }
            public string uriPrefix { get; set; }
            public string usuarioSoa { get; set; }
            public string senhaSoa { get; set; }
            public string autorizacaoAPI { get; set; }
            public int anoRevalidacao { get; set; }


            public byte[] documentoComprovanteByte { get; set; }
            public string documentoComprovante
            {
                get
                {
                    if (documentoComprovanteByte != null)
                        return Convert.ToBase64String(documentoComprovanteByte);
                    else
                        return null;
                }
            }

            public byte[] FotoByte { get; set; }
            public string foto
            {
                get
                {
                    if (FotoByte != null)
                        return Convert.ToBase64String(FotoByte);
                    else
                        return null;
                }
            }

            public long codigoPedido { get; set; }
            public long codigoUsuario { get; set; }
            public string tokenPush { get; set; }

            public decimal valorTarifaTipoCartao { get; set; }

            public DateTime Data { get; set; }
            public long codigoPedidoItem { get; set; }
            public int CodOperadora { get; set; }
            public int CitSoaVersao { get; set; }

            public DateTime dtInicial { get; set; }
            public DateTime dtFinal { get; set; }

            public string doctoIdentificacao { get; set; }

            public string TokenCitSoa { get; set; }
            public string TokenOperadora { get; set; }

            public string cpfUsuario { get; set; }
            public string CodLinhaOnibus { get; set; }
            public string linhaOnibus { get; set; }
            public string codigoImpresso { get; set; }

            public string VlTarifa { get; set; }

        }

        public class CitSoaDTORetorno
        {
            public string codigoExternoCartao { get; set; }
            public decimal saldoDisponivelCartao { get; set; }
            public string dataUltimoUsoCartao { get; set; }

        }

        public class LinhaVigenteDTO
        {
            public string id { get; set; }
            public string codLinhaExterno { get; set; }
            public string nomeLinha { get; set; }
            public decimal valorTarifa { get; set; }
            public string tagPesquisa { get; set; }
            public int codOperadora { get; set; }
            public bool possuiQrcode { get; set; }
            public bool possuiQuadroHorario { get; set; }
            public List<string> itinerarios { get; set; }

        }
        public class TitularCartaoRevendaDTO
        {
            public int codigo { get; set; }
            public string nome { get; set; }
            public string cpf { get; set; }
            public string codigoExternoCartao { get; set; }
            public decimal saldoDisponivelCartao { get; set; }
            public string dataUltimoUsoCartao { get; set; }

        }

        public class RetornoConsultaCarga
        {
            public int pedidoId { get; set; }
            public int codigoPedidoExterno { get; set; }
            public string codigoCartaoExterno { get; set; }
            public string valorSolicitado { get; set; }
            public string status { get; set; }
            public string dataDaRecarga { get; set; }
            public string dataEstocagem { get; set; }
            public string statusPedido { get; set; }
            public int codigoDependente { get; set; }
            public string dataPedido { get; set; }
            public string codigoRetorno { get; set; }
        }

        public class dadosPedidoCitSoa
        {
            public int pedidoId { get; set; }
            public long codigoPedidoExterno { get; set; }
            public string codigoTitularRevenda { get; set; }
            public string mensagemDeErro { get; set; }
            public string dataEstocagem { get; set; }
            public string codigoRetorno { get; set; }
        }


        public class PedidoAbertoCitSoaRetorno
        {
            public string code { get; set; }
            public string status { get; set; }
            public string message { get; set; }
            public List<dadosPedidoCitSoa> data { get; set; }
        }

        public class PedidoCanceladoCitSoaRetorno
        {
            public string code { get; set; }
            public string status { get; set; }
            public string message { get; set; }
        }

        public class RetornoErroQRCode
        {
            public bool resultado { get; set; }
            public string mensagem { get; set; }
        }
    }
