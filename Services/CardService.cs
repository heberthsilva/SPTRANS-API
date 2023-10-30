using System;
using System.Linq;
using System.Threading.Tasks;
using sptrans_api.Retorno;
using sptrans_api.Models;
using System.Net.Http;
using System.Data;
using sptrans_api.Entidades;
using sptrans_api.BD;
using System.Configuration;
using Newtonsoft.Json;

namespace sptrans_api.Services
{

    public class CardService
    {
        HttpClient client = new HttpClient();
        public DefaultRetorno NovoCartao(CardInput card)
        {
            var result = new DefaultRetorno();
            bool testeCard = false;

            try
            {
                var validacao = ConsultarCartaoAsync(card.NumeroCartao).Result;
                //var validacao = true;

                switch(validacao.Validador)
                {
                    #region Sucesso Consulta Cartão
                    case '0':

                        using (var db = new BD_EMPRESA3_Entities())
                        {

                            var operadora = db.tb_operadora.Where(r => r.cd_operadora == card.CodigoOperadora).FirstOrDefault();
                            var existente = db.tb_usuario_cartao.FirstOrDefault(x => x.nu_cartao == card.NumeroCartao && x.cd_usuario == card.CodigoUsuario && x.ic_cartao_inativo == false);

                            if (existente != null)
                            {
                                if (existente.ic_cartao_bloqueado == true)
                                {
                                    result.Mensagem = MensagemConstants.MensagemUsuarioCartaoBloqueado;
                                    result.Status = 1;
                                }
                                else
                                {
                                    result.Mensagem = MensagemConstants.MensagemUsuarioCartaoJaCadastrado;
                                    result.Status = 1;
                                }
                                return result;
                            }

                            var codExt = "";

                            var citDto = new CitSoaDTO
                            {
                                praca = operadora.nm_sigla_operadora,
                                codigoExternoCartao = card.NumeroCartao,
                                codigoTitularRevenda = operadora.nu_codigo_revenda_operadora.Value,
                                uriPrefix = operadora.ds_servidor_conexao,
                                usuarioSoa = operadora.ds_usuario_auth,
                                senhaSoa = operadora.ds_senha_auth,
                                CitSoaVersao = (int)operadora.cd_operadora_versao_api,
                                TokenCitSoa = operadora.ds_token_CitSoa,
                                TokenOperadora = operadora.ds_token_operadora,
                                CodOperadora = operadora.cd_operadora,
                                doctoIdentificacao = "0"
                            };

                            string value_return;
                            var tipoCartao = db.tb_tipo_cartao.FirstOrDefault(r => r.ds_sigla_tipo_cartao == card.CodigoTipoCartao.ToString() && r.cd_operadora == operadora.cd_operadora &&
                              r.ic_cartao_virtual == false &&
                              r.cd_operadora == 74);// colocar o codigo da SpTrans de forma a evitar a inclusão de outras operadoras por essa API

                            if (tipoCartao != null)
                            {

                                var isUsuarioCartao = db.tb_usuario_cartao.FirstOrDefault(
                                    r => r.nu_cartao == card.NumeroCartao &&
                                    r.cd_tipo_cartao == tipoCartao.cd_tipo_cartao &&
                                    r.ic_cartao_inativo == false &&
                                    r.cd_usuario == card.CodigoUsuario);

                                if (isUsuarioCartao == null)
                                {
                                    var usuarioCartao = new tb_usuario_cartao()
                                    {
                                        cd_usuario = card.CodigoUsuarioCartao,
                                        cd_tipo_cartao = tipoCartao.cd_tipo_cartao,
                                        dt_cadastro_cartao = DateTime.Now,
                                        nu_cartao = card.NumeroCartao,
                                        nu_fisico = card.NumeroFisico,
                                        nm_cartao = card.NomeCartao,
                                        ic_cartao_inativo = false,
                                        ic_cartao_bloqueado = false,
                                        vr_saldo = card.ValorSaldo,
                                        dt_saldo_atualizado = DateTime.Now.ToString(),
                                    };

                                    db.tb_usuario_cartao.Add(usuarioCartao);
                                    db.SaveChanges();

                                    card.CodigoUsuarioCartao = usuarioCartao.cd_usuario_cartao;
                                    card.CodigoOperadora = operadora.cd_operadora;
                                    card.NomeOperadora = operadora.nm_fantasia_operadora;
                                    card.urlLogoOperadora = PathUrlLogoOperadora(tipoCartao.ds_url_imagem_tipo_cartao);
                                    card.DataSaldoAtualizado = "Não Informado.";
                                    card.HasCartaoElegivel = true;
                                    card.CodigoTipoCartao = usuarioCartao.cd_tipo_cartao;
                                    card.DataCadastroCartao = usuarioCartao.dt_cadastro_cartao.ToString();
                                    card.ValorSaldo = usuarioCartao.vr_saldo;
                                    card.HasBloqueio = usuarioCartao.ic_cartao_bloqueado;
                                    card.HasRevalidacaoCartao = (bool)usuarioCartao.tb_tipo_cartao.ic_revalidacao_cartao;
                                    card.ValorRevalidacaoOperadora = (decimal)usuarioCartao.tb_tipo_cartao.vl_revalidacao_operadora;
                                    card.HasExtrato = (bool)operadora.ic_exibir_extrato;

                                    if (card.ValorRecargaMinima == 0)
                                        card.ValorRecargaMinima = tipoCartao.vl_minimo_credito;
                                    if (card.ValorRecargaMaxima == 0)
                                        card.ValorRecargaMaxima = tipoCartao.vl_maximo_credito;

                                    result.ListaObjeto.Add(card);
                                    result.Mensagem = MensagemConstants.MensagemSucesso;
                                    result.Status = 0;
                                }
                                else if (isUsuarioCartao.ic_cartao_bloqueado == true)
                                {
                                    result.Mensagem = MensagemConstants.MensagemUsuarioCartaoBloqueado;
                                    result.Status = 1;
                                }
                                else
                                {
                                    result.Mensagem = MensagemConstants.MensagemUsuarioCartaoJaCadastrado;
                                    result.Status = 1;
                                }



                            }
                            else
                            {
                                result.Mensagem = MensagemConstants.MensagemUsuarioCartaoTipoCartaoNaoCadastrado;
                                result.Status = 1;
                            }

                        }
                        break;
                    #endregion
                    case '1':
                        result.Mensagem = MensagemConstants.CartaoNotFound;
                        result.Status = 1;
                        break;
                    case '2':
                        result.Mensagem = MensagemConstants.CartaoNaoLiberado;
                        result.Status = 1;
                        break;
                    case '3':
                        result.Mensagem = MensagemConstants.TipoProfessorDeny;
                        result.Status = 1;
                        break;
                    case '4':
                        result.Mensagem = MensagemConstants.PedidoDeny;
                        result.Status = 1;
                        break;
                    default:
                        result.Mensagem = MensagemConstants.ErroCard;
                        result.Status = 1;
                        break;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar novo cartão:");
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public async Task<RetornoConsultaCartao> ConsultarCartaoAsync(string numeroCartao)
        {
            string url = ConfigurationManager.AppSettings["host"] + "ConsultarCartao/" + numeroCartao.ToString();
            var response = await client.GetStringAsync(url);
            var cartao = JsonConvert.DeserializeObject<RetornoConsultaCartao>(response);
            return cartao;
        }

        public static string PathUrlLogoOperadora(string caminhoLogo)
        {
            var localPath = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
            var absolutUri = System.Web.HttpContext.Current.Request.Url.OriginalString.Replace(localPath, "");
            var path = caminhoLogo;

            if (!path.StartsWith(@"/"))
                path = @"/" + path;

            return string.Format("{0}/API.EMPRESA3{1}", absolutUri, path);
        }

    }
}
