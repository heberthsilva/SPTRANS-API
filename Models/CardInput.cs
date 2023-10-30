using System;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class CardInput
    {
        public string NumeroCartao { get; set; }
        public long CodigoUsuarioCartao { get; set; }
        public long CodigoUsuario { get; set; }
        public int CodigoTipoCartao { get; set; }
        public string NomeTipoCartao { get; set; }
        public string SiglaTipoCartao { get; set; }
        public string DataCadastroCartao { get; set; }
        public string DataValidadeCartao { get; set; }
        public string NumeroFisico { get; set; }
        public string NomeCartao { get; set; }
        public bool CartaoInativo { get; set; }
        public bool CartaoBloqueado { get; set; }
        public string DataSaldoAtualizado { get; set; }
        public decimal ValorSaldo { get; set; }

        public long CodigoOperadora { get; set; }
        public string NomeOperadora { get; set; }
        public float ValorRecarga { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string NomeDependente { get; set; }
        public bool AssociarCPF { get; set; }
        public string urlLogoOperadora { get; set; }

        public bool HasRecargaAutomatica { get; set; }
        public bool HasRecargaProgramada { get; set; }
        public bool HasExtrato { get; set; }

        public bool HasBloqueio { get; set; }

        //Tipo Cartao
        public bool HasSegundaVia { get; set; }
        public decimal ValorSegundaVia { get; set; }

        public bool HasRevalidacaoCartao { get; set; }
        public bool HasQrCode { get; set; }

        public decimal ValorRevalidacaoOperadora { get; set; }
        public decimal ValorRevalidacaoServico { get; set; }
        //

        public bool HasCartaoElegivel { get; set; }
        public bool RecargaProgramadaAvailable { get; set; }
        public bool RecargaAgendadaAvailable { get; set; }
        public short TipoPedido { get; set; }

        public bool? TipoCartaoIdentificado { get; set; }

        public decimal ValorRecargaMinima { get; set; }
        public decimal ValorRecargaMaxima { get; set; }

        public bool CartaoFavorito { get; set; }

        public bool CartaoFavoritoEMPRESA3lometros { get; set; }

        public string DataInicio { get; set; }
        public string DataFim { get; set; }

        public bool? isRecarga { get; set; }
        public decimal ValorEntrega { get; set; }
        public decimal ValorEmissao { get; set; }
        public string Mensagem { get; set; }
        public string PreMensagem { get; set; }

        public DateTime DtInicio
        {
            get
            {
                return Convert.ToDateTime(DataInicio);
            }
        }

        public DateTime DtFim
        {
            get
            {
                return Convert.ToDateTime(DataFim);
            }
        }

    }
}