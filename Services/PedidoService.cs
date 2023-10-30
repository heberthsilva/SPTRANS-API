using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using sptrans_api.Retorno;
using sptrans_api.Models;
using System.Net.Http;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using sptrans_api.Entidades;
using sptrans_api.BD;
using sptrans_api.Enum;

namespace sptrans_api.Services
{

    public class PedidoService
    {
        HttpClient client = new HttpClient();
        CardService card = new CardService();
        public DefaultRetorno NovoPedido(PedidoInput obj)
        {
            var result = new DefaultRetorno();
            var resultItem = new DefaultRetorno();
            var calculoFormPagamento = new DefaultRetorno();
            List<FormaPagamentoDTO> listFormaPag = new List<FormaPagamentoDTO>();
            var vlPedido = 0M;

            #region "Registra a geração de novo pedido no Log"
            List<LogRegistroDTO> reg = new List<LogRegistroDTO>();
            reg.Add(new LogRegistroDTO
            {
                CodigoUsuario = obj.CodigoUsuario,
                TipoRegistro = 16
            });

            string url = ConfigurationManager.AppSettings["EMPRESA3API"] + "Log/logRegistro";
            var response = client.PostAsJsonAsync(url, reg);

            #endregion

            try
            {
                using (var db = new BD_EMPRESA3_Entities())
                {

                    obj.ValorTaxa = 0M;
                    obj.ValorEntrega = 0M;
                    obj.ValorDesconto = 0M;
                    var ValorTotalPedidoConf = 0M;
                    var codOperadora = Convert.ToInt32(obj.ListaItemPedido[0].CodigoOperadora);
                    var formaPagamento = db.tb_forma_pagamento.FirstOrDefault(r => r.cd_forma_pagamento == obj.CodigoFormaPagamento && r.ic_forma_pagamento_inativo == false);

                    if (formaPagamento != null)
                    {
                        var gatewayPagamento = db.tb_operadora_forma_pagamento_gateway.Where(x => x.cd_forma_pagamento == obj.CodigoFormaPagamento && x.cd_operadora == codOperadora && x.ic_processamento_inativo == false).OrderBy(y => y.ic_ordem_execucao).ToList();

                        if (gatewayPagamento.Count == 0)
                        {
                            result.Mensagem = MensagemConstants.MensagemGatewayNaoLocalizado;
                            return result;
                        }
                        var ip = string.Empty;

                        if (obj.CodigoUsuarioCupom == null)
                            obj.CodigoUsuarioCupom = 0;

                        if (!String.IsNullOrEmpty(obj.UsuarioIP))
                            ip = obj.UsuarioIP.Length > 50 ? string.Empty : obj.UsuarioIP;

                        #region Cálculo Taxas e Validações
                        foreach (var itemPedido in obj.ListaItemPedido)
                        {

                            vlPedido = vlPedido + itemPedido.ValorRecarga;
                            var antifraude = ValidaRegrasAntiFraude(itemPedido.TipoPedido, obj.CodigoUsuario, itemPedido.dddCelRecarga, itemPedido.NumeroCelRecarga, itemPedido.ValorRecarga, itemPedido.CodigoUsuarioCartao);

                            if (antifraude != "OK")
                            {
                                result.Mensagem = antifraude;
                                result.Status = 1;
                                return result;
                            }
                            UsuarioCartaoDto usuCartaoDTO = new UsuarioCartaoDto();

                            if (itemPedido.CodigoUsuarioCartao == null)
                            {
                                itemPedido.CodigoUsuarioCartao = 0;
                            }

                            if (itemPedido.CodigoUsuarioCartao.Value > 0)
                            {
                                var usuCard = db.tb_usuario_cartao.FirstOrDefault(x => x.cd_usuario_cartao == itemPedido.CodigoUsuarioCartao);
                                itemPedido.NumeroCartao = usuCard.nu_cartao;
                            }

                            if (itemPedido.CodigoUsuarioCartao > 0)
                            {
                                usuCartaoDTO = ((from itUsuCard in db.tb_usuario_cartao
                                                 join itTipoCard in db.tb_tipo_cartao on itUsuCard.cd_tipo_cartao equals itTipoCard.cd_tipo_cartao
                                                 where itUsuCard.cd_usuario_cartao == itemPedido.CodigoUsuarioCartao.Value
                                                 select new
                                                 {
                                                     CodigoTipoCartao = itTipoCard.cd_tipo_cartao,
                                                     CodigoUsuarioCartao = itemPedido.CodigoUsuarioCartao.Value,
                                                     ValorRecarga = itemPedido.ValorRecarga,
                                                     CodigoOperadora = itTipoCard.cd_operadora
                                                 }).AsEnumerable().Select(r => new UsuarioCartaoDto()
                                                 {
                                                     CodigoTipoCartao = r.CodigoTipoCartao,
                                                     CodigoUsuarioCartao = r.CodigoUsuarioCartao,
                                                     ValorRecarga = (float)r.ValorRecarga,
                                                     CodigoOperadora = r.CodigoOperadora
                                                 }).FirstOrDefault());
                            }
                            else
                            {
                                if (itemPedido.TipoPedido != 10)
                                {
                                    if (itemPedido.TipoCartaoValor > 0)
                                    {
                                        var tbTipoCartaoValor = db.tb_tipo_cartao_valor.FirstOrDefault(r => r.cd_tipo_cartao_valor == itemPedido.TipoCartaoValor);

                                        if (itemPedido.ValorRecarga != tbTipoCartaoValor.vl_tipo_cartao)
                                        {
                                            result.Mensagem = "O valor informado não corresponde ao produto selecionado.Tente novamente.";
                                            result.Status = 1;
                                            return result;
                                        }

                                        if (tbTipoCartaoValor != null)
                                            itemPedido.CodigoTipoCartao = tbTipoCartaoValor.cd_tipo_cartao;
                                    }
                                }
                                usuCartaoDTO = (from itUsuCard in db.tb_tipo_cartao
                                                where itUsuCard.cd_tipo_cartao == itemPedido.CodigoTipoCartao
                                                select new
                                                {
                                                    CodigoTipoCartao = itUsuCard.cd_tipo_cartao,
                                                    ValorRecarga = itemPedido.ValorRecarga,
                                                    CodigoOperadora = itUsuCard.cd_operadora,
                                                    TipoPedido = itemPedido.TipoPedido
                                                }).AsEnumerable().Select(r => new UsuarioCartaoDto()
                                                {
                                                    CodigoTipoCartao = r.CodigoTipoCartao,
                                                    ValorRecarga = (float)r.ValorRecarga,
                                                    CodigoOperadora = r.CodigoOperadora,
                                                    TipoPedido = r.TipoPedido
                                                }).FirstOrDefault();
                            }

                            if (obj.CodigoFormaPagamento != 17)
                            {
                                var retornoCalculo = CalculoTaxasPorTipoPagamento(usuCartaoDTO, itemPedido.TipoPedido, obj.CodigoFormaPagamento);

                                if (retornoCalculo != null)
                                {
                                    itemPedido.ValorTaxa = (decimal)((FormaPagamentoDTO)retornoCalculo.ListaObjeto[0]).ValorTaxa;
                                    obj.ValorTaxa += itemPedido.ValorTaxa;
                                }
                            }

                            if (obj.ValorPedido > 0 || (itemPedido.TipoPedido == 4 || itemPedido.TipoPedido == 5))
                            {
                                if (itemPedido.TipoPedido != 3)
                                {
                                    if (itemPedido.ValorEntrega > 0)
                                    {
                                        obj.ValorEntrega += itemPedido.ValorEntrega;
                                    }
                                }

                                if (obj.CodigoUsuarioCupom > 0 && !db.tb_pedido.Any(r => r.cd_usuario_cupom == obj.CodigoUsuarioCupom.Value && r.cd_usuario == obj.CodigoUsuario))
                                {
                                    var usuarioCupom = db.tb_usuario_cupom.FirstOrDefault(r => r.cd_usuario_cupom == obj.CodigoUsuarioCupom);

                                    if (usuarioCupom != null && usuarioCupom.tb_cupom.dt_validade >= DateTime.Now)
                                    {
                                        itemPedido.ValorDesconto = Math.Round((itemPedido.ValorTaxa * usuarioCupom.tb_cupom.vl_desconto.Value) / 100, 2);
                                        obj.ValorDesconto += itemPedido.ValorDesconto;
                                    }
                                }
                            }
                        }

                        obj.ValorTotalPedido = vlPedido + obj.ValorTaxa + obj.ValorEntrega - obj.ValorDesconto;
                        #endregion
                        #region Salva Pedido
                        var pedido = new tb_pedido()
                        {
                            cd_pedido = obj.CodigoPedido,
                            cd_usuario = obj.CodigoUsuario,
                            cd_forma_pagamento = obj.CodigoFormaPagamento,
                            cd_status_pedido = short.Parse(StatusPedidoEnumerado.EmAberto.GetHashCode().ToString()),
                            dt_datahora_pedido = DateTime.Now,
                            dt_data_pagamento = obj.DataPagamento,
                            vl_valor_pago = obj.ValorPago,
                            vl_pedido = Decimal.Round(vlPedido, 2),
                            vl_taxa = Decimal.Round(obj.ValorTaxa, 2),
                            vl_desconto = obj.ValorDesconto,
                            vl_total_pedido = Decimal.Round(obj.ValorTotalPedido, 2),
                            ds_chave_transacao = obj.ChaveTransacao,
                            ic_export_erp = false,
                            dt_export_erp = null,
                            cd_usuario_cupom = obj.CodigoUsuarioCupom == 0 ? null : obj.CodigoUsuarioCupom,
                            cd_canal = obj.CanalVenda,
                            cd_usuario_cupom_programa_fidelidade = 0,
                            ic_repetir_pedido = obj.RepetirPedido,
                            vl_cashback = obj.ValorTotalCashBack,
                            ic_cashback_processado = false,
                            cd_gateway = gatewayPagamento[0].cd_gateway,
                            vl_total_entrega = obj.ValorEntrega,
                            ds_ip_usuario = ip,
                            ic_pagamento_parcelado = false

                        };

                        db.tb_pedido.Add(pedido);
                        db.SaveChanges();
                        obj.CodigoPedido = pedido.cd_pedido;

                        GravarLogPedido(pedido.cd_pedido, StatusPedidoEnumerado.PedidoRegistrado);
                        #endregion
                        #region Salva e trata Itens do Pedido
                        foreach (var item in obj.ListaItemPedido)
                        {
                            var usuarioCupons = db.tb_usuario_cupom.FirstOrDefault(r => r.cd_usuario_cupom == obj.CodigoUsuarioCupom);

                            item.CodigoPedido = pedido.cd_pedido;

                            if (item.ValorEntrega > 0)
                                ValorTotalPedidoConf += (item.ValorRecarga + item.ValorTaxa + item.ValorEntrega) - item.ValorDesconto;
                            else
                                ValorTotalPedidoConf += (item.ValorRecarga + item.ValorTaxa) - item.ValorDesconto;

                            resultItem = NovoItemPedido(item);

                            if (resultItem.Mensagem != "Sucesso")
                            {
                                CancelarPedido(((int)obj.CodigoPedido));
                                result.Mensagem = resultItem.Mensagem;
                                return result;
                            }
                            else
                            {
                                if (item.TipoPedido == 1 || item.TipoPedido == 2 || item.TipoPedido == 3)
                                {
                                    var usuCard = db.tb_usuario_cartao.FirstOrDefault(x => x.cd_usuario_cartao == item.CodigoUsuarioCartao);
                                    item.CodigoTipoCartao = usuCard.cd_tipo_cartao;

                                    var novoItem = (ItemPedidoDto)resultItem.ListaObjeto[0];
                                    var resultSBE = NovoPedidoSpTrans(item, obj.CodigoFormaPagamento);

                                    if (resultSBE.Mensagem == MensagemConstants.MensagemSucesso)
                                    {
                                        var pedidoAberto = (dadosPedidoCitSoa)resultSBE.ListaObjeto[0];

                                        var itemPedido = db.tb_pedido_item.FirstOrDefault(x => x.cd_pedido_item == item.CodigoItemPedido);
                                        itemPedido.ds_pedido_operadora = pedidoAberto.pedidoId.ToString();
                                        itemPedido.dt_pre_pedido_operadora = DateTime.Now;
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        CancelarPedido(obj.CodigoPedido, headerDto, false, false);
                                        result.Mensagem = resultSBE.Mensagem;
                                        return result;
                                    }
                                }
                                else if (item.TipoPedido == 6 || item.TipoPedido == 7 || item.TipoPedido == 8)
                                {
                                    if (db.tb_parametro.FirstOrDefault().ic_ambiente_producao == true)
                                    {
                                        var ret = PreAutorizacaoRecargaServicos(item);
                                        if (ret.Status == -1)
                                        {
                                            CancelarPedido(obj.CodigoPedido, headerDto, false, false);
                                            result = ret;
                                            if (result.Mensagem == null)
                                                result.Mensagem = "Não foi possivel realizar a recarga, tente novamente mais tarde";
                                            return result;
                                        }
                                    }
                                }

                                if (item.TipoPedido == 4 || item.TipoPedido == 5)
                                {
                                    var documents = db.tb_ged_pedido_cartao.Where(x => x.cd_usuario == headerDto.UsuarioId && x.cd_pedido == null && x.nu_tipo_documento <= 5 && x.cd_requisicao_via == null).OrderByDescending(y => y.dt_registro).ToList();
                                    List<int> tipos = new List<int>();

                                    foreach (var doc in documents)
                                    {
                                        if (tipos.Count == 0)
                                        {
                                            tipos.Add(doc.nu_tipo_documento);
                                            doc.cd_pedido = obj.CodigoPedido;
                                        }
                                        else if (!tipos.Contains(doc.nu_tipo_documento))
                                        {
                                            tipos.Add(doc.nu_tipo_documento);
                                            doc.cd_pedido = obj.CodigoPedido;
                                        }
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                        #endregion

                        //Confere valor do pedido com valor dos itens (evitar fraude)
                        if (ValorTotalPedidoConf != obj.ValorTotalPedido)
                        {
                            CancelarPedido(pedido.cd_pedido, headerDto, false, false);
                            UtilController.GravarErro(ref result, new Exception("O valor total do pedido não coincide com a soma dos itens. Pedido= " + pedido.cd_pedido), "ValorPedido", "NovoPedido Exception", JsonConvert.SerializeObject(obj), headerDto.UsuarioId);
                            return result;
                        }

                        #region Atualiza dados da Solicitação de vias e revalidacao
                        if (obj.CodigoRequisicaoVia > 0)
                        {
                            var requisicao = db.tb_requisicao_via.FirstOrDefault(x => x.cd_requisicao_via == obj.CodigoRequisicaoVia);
                            var documentos = db.tb_ged_pedido_cartao.Where(x => x.cd_requisicao_via == obj.CodigoRequisicaoVia).ToList();
                            requisicao.cd_pedido = pedido.cd_pedido;
                            var statusReq = 11;

                            if (documentos.Any(x => x.nu_tipo_documento == 5))
                                statusReq = 10;

                            requisicao.cd_status_requisicao = statusReq;

                            db.tb_historico_requisicao_via.Add(new tb_historico_requisicao_via
                            {
                                cd_requisicao_via = requisicao.cd_requisicao_via,
                                cd_status_requisicao = statusReq,
                                dt_historico = DateTime.Now
                            });

                            foreach (var doc in documentos)
                            {
                                doc.cd_pedido = pedido.cd_pedido;
                            }

                            db.SaveChanges();
                        }
                        #endregion

                        #region Pagamento com Cartão de Crédito
                        if (obj.CodigoFormaPagamento == 1 && (obj.CanalVenda > 42))
                        {
                            var card = new CartaoCreditoController();

                            int operaMundipagg = 90;

                            if (db.tb_parametro.FirstOrDefault().ic_ambiente_producao.Value == true)
                                operaMundipagg = 32;

                            if (obj.ListaItemPedido[0].CodigoOperadora == operaMundipagg)
                            {
                                var resultMP = card.ProcessaPagamento(obj.CodigoUsuario, obj.cvvCartao, obj.CodigoUsuarioCartaoCredito, obj.CodigoPedido, obj.ValorTotalPedido, obj.UsuarioIP, true);

                                if (resultMP.Mensagem == "OK")
                                {
                                    var objRetorno = JsonConvert.DeserializeObject<RetornoAuthorizeDTO>(resultMP.ListaObjeto[0].ToString());

                                    if (objRetorno.returnCode == 201 && objRetorno.payload.status == "CAPTURED")
                                    {
                                        pedido.ds_chave_transacao = objRetorno.payload.paymentUuid;


                                        db.SaveChanges();

                                        result.ListaObjeto.Add(new
                                        {
                                            obj.CodigoPedido
                                        });
                                        return result;

                                    }
                                    else
                                    {
                                        CancelarPedido(obj.CodigoPedido, headerDto, false, false);
                                        result.Mensagem = "Pagamento não autorizado. Verifique os dados informados e tente novamente.";

                                        var erro = new tb_erro_sistema
                                        {
                                            ds_erro_sistema = "Erro ao processar pagamento, pedido: " + obj.CodigoPedido,
                                            ds_trace_erro_sistema = JsonConvert.SerializeObject(resultMP),
                                            dt_erro_sistema = DateTime.Now,
                                            nm_classe = "Mundipagg",
                                            nm_operacao = "PagamentoMundipagg Exception"
                                        };

                                        db.tb_erro_sistema.Add(erro);
                                        db.SaveChanges();
                                        return result;
                                    }
                                }
                                else
                                {
                                    CancelarPedido(obj.CodigoPedido, headerDto, false, false);
                                    result.Mensagem = resultMP.Mensagem;
                                    return result;
                                }
                            }
                            else
                            {
                                var resultAD = new DefaultRetorno();

                                if (obj.PagamentoParcelado == true && obj.DadosParcelamento != null)
                                {
                                    db.tb_pagamento_pedido_parcelado.Add(new tb_pagamento_pedido_parcelado
                                    {
                                        cd_gateway = gatewayPagamento[0].cd_gateway,
                                        cd_pedido = pedido.cd_pedido,
                                        nu_qtd_parcelas = obj.DadosParcelamento.numeroParcela,
                                        vl_parcela = obj.DadosParcelamento.valorParcela,
                                        vl_taxa_mes = obj.DadosParcelamento.taxaJuros,
                                        vl_total_com_taxa = obj.DadosParcelamento.valorTotal,
                                        vl_total_sem_taxa = obj.DadosParcelamento.valorOriginal
                                    });
                                    pedido.vl_acrescimo = obj.DadosParcelamento.valorTotal - obj.DadosParcelamento.valorOriginal;
                                    pedido.vl_total_pedido = obj.DadosParcelamento.valorTotal;
                                    pedido.ic_pagamento_parcelado = true;

                                    db.SaveChanges();
                                    resultAD = card.ProcessaPagamentoParcelado(obj.CodigoUsuario, obj.cvvCartao, obj.CodigoUsuarioCartaoCredito, obj.CodigoPedido, obj.DadosParcelamento.valorTotal, obj.DadosParcelamento.numeroParcela, null);
                                }
                                else
                                    resultAD = card.ProcessaPagamento(obj.CodigoUsuario, obj.cvvCartao, obj.CodigoUsuarioCartaoCredito, obj.CodigoPedido, obj.ValorTotalPedido, null, false);

                                if (resultAD.Mensagem == "OK")
                                {
                                    var objRetorno = JsonConvert.DeserializeObject<AdyenAuthorizeRetorno>(resultAD.ListaObjeto[0].ToString());

                                    if (objRetorno.resultCode == "Authorised")
                                    {
                                        pedido.ds_chave_transacao = objRetorno.pspReference;

                                        db.SaveChanges();

                                        result.ListaObjeto.Add(new
                                        {
                                            obj.CodigoPedido
                                        });
                                        return result;
                                    }
                                    else
                                    {
                                        CancelarPedido(obj.CodigoPedido, headerDto, false, false);
                                        result.Mensagem = "Pagamento não autorizado. Verifique os dados informados e tente novamente.";

                                        var erro = new tb_erro_sistema
                                        {
                                            ds_erro_sistema = "Erro ao processar pagamento, pedido: " + obj.CodigoPedido,
                                            ds_trace_erro_sistema = JsonConvert.SerializeObject(resultAD),
                                            dt_erro_sistema = DateTime.Now,
                                            nm_classe = "Adyen",
                                            nm_operacao = "PagamentoAdyen Exception"
                                        };

                                        db.tb_erro_sistema.Add(erro);
                                        db.SaveChanges();
                                        return result;
                                    }
                                }
                                else
                                {
                                    CancelarPedido(obj.CodigoPedido, headerDto, false, false);
                                    result.Mensagem = resultAD.Mensagem;
                                    return result;
                                }
                            }
                        }
                        #endregion
                        #region Pagamento por depósito ou transferência
                        if ((obj.CodigoFormaPagamento == 4 || obj.CodigoFormaPagamento == 5) && result.Status != -1) // 4 transferencia / 5 deposito
                        {
                            var usuario = db.tb_usuario.Where(r => r.cd_usuario == headerDto.UsuarioId).ToList();

                            var PaymentMethod = obj.CodigoFormaPagamento == 4 ? 2 : 1; // 2- transferencia / 1 -deposito

                            foreach (var item in usuario)
                            {
                                var ObjNovoPedidoFastCash = new
                                {
                                    Tid = obj.CodigoPedido.ToString(),
                                    Pid = db.tb_gateway_fastcash.FirstOrDefault().nu_pid,
                                    ProdId = db.tb_gateway_fastcash.FirstOrDefault().nu_prodid.ToString(),
                                    Custom = "",
                                    Amount = Decimal.Round(obj.ValorTotalPedido, 2),
                                    Description = "PedidoEMPRESA3:" + obj.CodigoPedido.ToString(),
                                    PaymentMethod = PaymentMethod,
                                    SubPaymentMethod = obj.BancoFastCashParaPagamento,
                                    Name = item.nm_usuario,
                                    Email = item.ds_email_usuario,
                                    MobilePhone = item.nu_ddd_telefone + item.nu_telefone,
                                    Document = item.nu_cpf_usuario == null || item.nu_cpf_usuario == "" ? item.nu_cnpj_usuario : item.nu_cpf_usuario,
                                    BankAgency = obj.AgenciaBancaria,
                                    BankAccountNumber = obj.NumeroContaBancaria,
                                    BankAccountDocument = ""
                                };

                                var PedidoFastCash = ApiFastCashController.NovoPedidoFastCash(ObjNovoPedidoFastCash);

                                if (PedidoFastCash.MensagemLog == "1")
                                {
                                    ApiFastCashController.CancelarPedidoFastCash(obj.CodigoPedido.ToString(), item.nu_cpf_usuario, item.nu_cnpj_usuario);
                                    CancelarPedido(obj.CodigoPedido, headerDto, false, false);
                                    result.Mensagem = "Pedido cancelado. Erro ao gravar pedido na fastCash";
                                    return result;
                                }
                            }
                        }
                        #endregion
                        #region Pagamento por Pix
                        if (obj.CodigoFormaPagamento == 14)
                        {
                            var pixOK = false;
                            string msgRetorno = string.Empty;

                            foreach (var gtw in gatewayPagamento)
                            {
                                if (gtw.cd_gateway == 8 && pixOK == false)
                                {
                                    var pag = new PagamentoPixDTO();
                                    pag.CodigoPedido = obj.CodigoPedido;
                                    pag.CodigoUsuario = Convert.ToInt32(obj.CodigoUsuario);
                                    pag.ValorPagamento = obj.ValorTotalPedido;

                                    var resultPix = new PixBBController().GerarCobrancaPix(pag);

                                    if (resultPix.Mensagem == "OK")
                                    {
                                        var codPedidoPix = Convert.ToInt64(resultPix.ListaObjeto[0]);
                                        var dadosPix = db.tb_pedido_pagamento_pix.FirstOrDefault(x => x.cd_pedido_pagamento_pix == codPedidoPix);

                                        result.ListaObjeto.Add(new
                                        {
                                            obj.CodigoPedido,
                                            imagem_base64 = dadosPix.ds_pix_imagem,
                                            pix_link = "",
                                            emv = dadosPix.ds_pix_emv,
                                            dadosPix.dt_pix_validade
                                        });
                                        result.Mensagem = MensagemConstants.MensagemSucesso;
                                        pixOK = true;
                                        pedido.cd_gateway = gtw.cd_gateway;
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        msgRetorno = resultPix.Mensagem;
                                    }

                                }
                                if (gtw.cd_gateway == 3 && pixOK == false)
                                {
                                    var pag = new PagamentoPixDTO();
                                    pag.CodigoPedido = obj.CodigoPedido;
                                    pag.CodigoUsuario = Convert.ToInt32(obj.CodigoUsuario);
                                    pag.ValorPagamento = obj.ValorTotalPedido;

                                    var resultPix = new PagamentoPixController().GerarCobrancaPix(pag);

                                    if (resultPix.Mensagem == "OK")
                                    {
                                        var dadosPix = db.tb_pedido_pagamento_pix.Where(x => x.cd_pedido == pedido.cd_pedido).OrderByDescending(y => y.cd_pedido_pagamento_pix).FirstOrDefault();

                                        var pix = JsonConvert.DeserializeObject<PagamentoQRCodeDto>(resultPix.ListaObjeto[0].ToString());
                                        result.ListaObjeto.Add(new
                                        {
                                            obj.CodigoPedido,
                                            pix.imagem_base64,
                                            pix.pix_link,
                                            pix.emv,
                                            dadosPix.dt_pix_validade
                                        });
                                        result.Mensagem = MensagemConstants.MensagemSucesso;
                                        pixOK = true;
                                        pedido.cd_gateway = gtw.cd_gateway;
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        msgRetorno = resultPix.Mensagem;
                                    }

                                }
                            }

                            if (!pixOK)
                            {
                                CancelarPedido(obj.CodigoPedido, headerDto, false, false);
                                UtilController.GravarErro(ref result, new Exception(msgRetorno), "NovoPedido", "NovoPedido Exception", JsonConvert.SerializeObject(obj), headerDto.UsuarioId);

                                return result;
                            }
                        }
                        #endregion
                        #region Gratuito
                        if (obj.CodigoFormaPagamento == 17 && (obj.ListaItemPedido[0].TipoPedido == 4 || obj.ListaItemPedido[0].TipoPedido == 5))
                        {
                            pedido.cd_status_pedido = 2;
                            pedido.dt_data_pagamento = DateTime.Now;
                            pedido.vl_valor_pago = 0;
                            pedido.ds_chave_transacao = pedido.cd_pedido.ToString();

                            result.Status = 9;

                            GravarLogPedido(pedido.cd_pedido, StatusPedidoEnumerado.Finalizado);
                            db.SaveChanges();
                        }
                        #endregion
                        #region Pagamento por Boleto
                        if (obj.CodigoFormaPagamento == 3)
                        {
                            var boletoDto = BoletoController.GerarBoleto(obj.CodigoPedido, obj.ValorTotalPedido, obj.ValorTaxa, obj.ValorPedido, headerDto.UsuarioId);

                            if (string.IsNullOrWhiteSpace(boletoDto.result.Mensagem))
                            {
                                result.ListaObjeto.Add(new
                                {
                                    obj.CodigoPedido,
                                    boletoDto.LinhaDigitavel,
                                    boletoDto.DataVencimento
                                });

                                result.Mensagem = MensagemConstants.MensagemSucesso;
                            }
                            else
                            {
                                result.Mensagem = "Falha ao gerar o Boleto.";
                                CancelarPedido(obj.CodigoPedido, headerDto, false, false);
                                return result;
                            }
                        }
                        #endregion

                        if (result.Status == 9)
                        {
                            EmailController.EnviarEmailRegistroPedido(obj.CodigoPedido, headerDto.UsuarioId);
                            EmailController.EnviarEmailPagamentoAprovado(obj.CodigoPedido, obj.CodigoUsuario);
                        }
                        else
                        {
                            EmailController.EnviarEmailRegistroPedido(obj.CodigoPedido, headerDto.UsuarioId);
                        }

                        result.ListaObjeto.Add(new { obj.CodigoPedido });
                    }
                    else
                    {
                        result.Mensagem = MensagemConstants.MensagemFormaPagamentoNaoDisponivel;
                    }

                }
            }
            catch (Exception ex)
            {
                CancelarPedido(obj.CodigoPedido, headerDto, false, false);

                obj.cvvCartao = string.Empty;
                UtilController.GravarErro(ref result, ex, "NovoPedido", "NovoPedido Exception", JsonConvert.SerializeObject(obj), headerDto.UsuarioId);

                return result;
            }
            return result;
        }


        public async Task<bool> ConfirmarPedido(int numeroPedido)
        {
            string url = ConfigurationManager.AppSettings["host"] + "ConfirmarPedido/" + numeroPedido.ToString();
            var response = await client.GetStringAsync(url);
            var cartao = JsonConvert.DeserializeObject(response);
            return cartao == null ? false : true;
        }

        public async Task<bool> CancelarPedido(int numeroPedido)
        {
            string url = ConfigurationManager.AppSettings["host"] + "CancelarPedido/" + numeroPedido.ToString();
            var response = await client.GetStringAsync(url);
            var cartao = JsonConvert.DeserializeObject(response);
            return cartao == null ? false : true;
        }

        public Task<string> GerarPedidoAsync(Dictionary<string, string> postParams = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("Post"), $"{ConfigurationManager.AppSettings["host"]}/GerarPedido");
            if (postParams != null)
                requestMessage.Content = new FormUrlEncodedContent(postParams);
            HttpResponseMessage response = client.SendAsync(requestMessage).Result;
            string apiResponse = response.Content.ReadAsStringAsync().Result;
            if (apiResponse != "")
                return Task.FromResult((string)JsonConvert.DeserializeObject(apiResponse));
            else
                throw new Exception();
        }
        private static void GravarLogPedido(long codigoPedido, StatusPedidoEnumerado statusPedido)
        {
            using (var db = new BD_EMPRESA3_Entities())
            {
                var ExistePedido = db.tb_pedido.Where(r => r.cd_pedido == codigoPedido).ToList();
                if (ExistePedido.Count > 0)
                {
                    db.tb_log_pedido.Add(new tb_log_pedido()
                    {
                        cd_pedido = codigoPedido,
                        dt_log = DateTime.Now,
                        cd_status_pedido = short.Parse(statusPedido.GetHashCode().ToString())
                    });

                    db.SaveChanges();
                }
            }
        }
        public static string ValidaRegrasAntiFraude(int tipoItemPedido, long codusuario, string ddd, string cel, decimal valorRecarga, long? CodigoUsuarioCartao = 0)
        {
            using (var db = new BD_EMPRESA3_Entities())
            {

                var existePedidoCartao = db.tb_pedido_item.Any(x => x.cd_usuario_cartao == CodigoUsuarioCartao && (x.tp_pedido_item == 1 || x.tp_pedido_item == 2)
                                                                        && x.tb_pedido.cd_status_pedido != 2 && x.tb_pedido.cd_status_pedido != 3);

                if (existePedidoCartao)
                {
                    return "Opa! Identificamos que você já possui um pedido em aberto para o item selecionado. Caso já tenha feito o pagamento, aguarde a confirmação pelo EMPRESA3 + . Caso ainda não tenha pago, vá em Pedidos e cancele o que está em aberto para realizar um novo.";

                }

                var parametros = db.tb_parametro_anti_fraude.FirstOrDefault();

                var today = DateTime.Now.Date;

                if (tipoItemPedido == 6)
                {
                    var celRecarga = ddd + cel;

                    var pedidosRecarga = db.vw_Pedidos_AntiFraude_Recarga_Celular.Where(x => x.celular == celRecarga).ToList();

                    var pedidosDia = pedidosRecarga.Where(x => x.data_pedido == today).FirstOrDefault();

                    if (pedidosDia != null)
                    {
                        if (pedidosDia.qtd_pedidos == parametros.nu_qtd_dia_recarga_celular)
                            return "A quantidade diária máxima de pedidos de recarga de celular foi atingida.";
                    }

                    var pedidosSemana = pedidosRecarga.Sum(x => x.qtd_pedidos);

                    if (pedidosSemana == parametros.nu_qtd_semana_recarga_celular)
                        return "A quantidade semanal máxima de pedidos de recarga de celular foi atingida.";
                }
                else if (tipoItemPedido == 7)
                {
                    var pedidosDigitais = db.vw_Pedidos_AntiFraude_Servicos_Digitais.Where(x => x.cd_usuario == codusuario && x.tp_pedido_item == 7).ToList();

                    var pedidosDia = pedidosDigitais.Where(x => x.data_pedido == today).FirstOrDefault();

                    if (pedidosDia != null)
                    {
                        if (pedidosDia.qtd_pedidos == parametros.nu_qtd_dia_recarga_servico_digital)
                            return "A quantidade diária máxima de pedidos de serviços digitais foi atingida.";
                    }

                    var pedidosSemana = pedidosDigitais.Sum(x => x.qtd_pedidos);

                    if (pedidosSemana == parametros.nu_qtd_semana_recarga_servico_digital)
                        return "A quantidade semanal máxima de pedidos de serviços digitais foi atingida.";
                }
                else if (tipoItemPedido == 8)
                {
                    var pedidosDigitaisTV = db.vw_Pedidos_AntiFraude_Servicos_Digitais.Where(x => x.cd_usuario == codusuario && x.tp_pedido_item == 8).ToList();

                    var pedidosDia = pedidosDigitaisTV.Where(x => x.data_pedido == today).FirstOrDefault();

                    if (pedidosDia != null)
                    {
                        if (pedidosDia.qtd_pedidos == parametros.nu_qtd_dia_recarga_tv)
                            return "A quantidade diária máxima de pedidos recarga de TV foi atingida.";
                    }

                    var pedidosSemana = pedidosDigitaisTV.Sum(x => x.qtd_pedidos);

                    if (pedidosSemana == parametros.nu_qtd_semana_recarga_tv)
                        return "A quantidade semanal máxima de pedidos de recarga de TV foi atingida.";
                }
                else if (tipoItemPedido == 1)
                {
                    var usuCartao = db.tb_usuario_cartao.FirstOrDefault(x => x.cd_usuario_cartao == CodigoUsuarioCartao);

                    if (usuCartao != null)
                    {
                        if (usuCartao.tb_tipo_cartao.vl_minimo_credito > valorRecarga)
                            return "O valor informado está abaixo do mínimo permitido para este cartão.";

                        if (usuCartao.tb_tipo_cartao.cd_operadora == 32)
                        {
                            var hoje = DateTime.Today;
                            var listCartoes = db.tb_usuario_cartao.Where(x => x.nu_cartao == usuCartao.nu_cartao && x.tb_tipo_cartao.cd_operadora == 32);

                            foreach (var card in listCartoes)
                            {
                                var existePedidoConcluido = db.tb_pedido_item.Any(x => x.cd_usuario_cartao == card.cd_usuario_cartao && (x.tp_pedido_item == 1)
                                                                          && x.tb_pedido.cd_status_pedido == 2 && x.tb_pedido.dt_datahora_pedido > hoje);

                                if (existePedidoConcluido)
                                    return "Opa! De acordo com as regras da Transfácil, você só pode fazer uma recarga diária por cartão.";
                            }
                        }
                    }
                }

            }
            return "OK";
        }

        private DefaultRetorno CalculoTaxasPorTipoPagamento(UsuarioCartaoDto usuCartaoDTO, int tipoPedido, int cdformapagamento)
        {
            var result = new DefaultRetorno();
            double DescontoTaxaServico = 0;

            try
            {
                using (var db = new BD_EMPRESA3_Entities())
                {
                    var item = db.tb_forma_pagamento.FirstOrDefault(r => !r.ic_forma_pagamento_inativo && r.cd_forma_pagamento == cdformapagamento);

                    if (item != null)
                    {
                        tb_tipo_cartao tipoCartao;

                        if (usuCartaoDTO.CodigoTipoCartao > 0)
                        {
                            tipoCartao = (from itemA in db.tb_tipo_cartao
                                          join itemC in db.tb_tipo_cartao_forma_pagamento on itemA.cd_tipo_cartao equals itemC.cd_tipo_cartao
                                          where itemA.cd_tipo_cartao == usuCartaoDTO.CodigoTipoCartao &&
                                                itemC.cd_forma_pagamento == item.cd_forma_pagamento &&
                                                itemC.ic_tipo_cartao_forma_pagamento_inativo == false
                                          select itemA).FirstOrDefault();
                        }
                        else
                        {
                            tipoCartao = (from itemA in db.tb_usuario_cartao
                                          join itemB in db.tb_tipo_cartao on itemA.cd_tipo_cartao equals itemB.cd_tipo_cartao
                                          join itemC in db.tb_tipo_cartao_forma_pagamento on itemB.cd_tipo_cartao equals itemC.cd_tipo_cartao
                                          where itemA.cd_usuario_cartao == usuCartaoDTO.CodigoUsuarioCartao &&
                                                itemC.cd_forma_pagamento == item.cd_forma_pagamento &&
                                                itemC.ic_tipo_cartao_forma_pagamento_inativo == false
                                          select itemB).FirstOrDefault();
                        }

                        if (tipoCartao != null)
                        {
                            var valorTaxa = 0M;

                            if (tipoPedido == 1)
                            {
                                var valorRecargaCartao = Convert.ToDecimal(usuCartaoDTO.ValorRecarga);

                                var listTaxaAdministrativa = item.tb_taxa_adm.Where(r => r.cd_forma_pagamento == item.cd_forma_pagamento &&
                                             r.cd_operadora == usuCartaoDTO.CodigoOperadora);

                                foreach (var txAdministrativa in listTaxaAdministrativa)
                                {
                                    if (valorRecargaCartao < txAdministrativa.nu_range_qtd_inicio ||
                                        valorRecargaCartao > txAdministrativa.nu_range_qtd_fim)
                                        continue;

                                    if (txAdministrativa.ic_tipo_range.Equals("V"))
                                        valorTaxa += txAdministrativa.vl_taxa_pedido;
                                    else if (txAdministrativa.ic_tipo_range.Equals("P") &&
                                             txAdministrativa.vl_taxa_pedido > 0)
                                        valorTaxa +=
                                            (valorRecargaCartao * txAdministrativa.vl_taxa_pedido) / 100;

                                    valorTaxa += txAdministrativa.tx_item_credito;

                                    break;
                                }
                            }
                            else if (tipoPedido == 3)
                            {
                                valorTaxa = (decimal)tipoCartao.vl_revalidacao_servico;
                            }

                            if (item.nm_descricao.Equals("Boleto"))
                                valorTaxa = 0M;

                            if (tipoPedido == 6)
                            {
                                if (valorTaxa == 0)
                                {
                                    var tpCartao = db.tb_tipo_cartao.FirstOrDefault(r => r.cd_tipo_cartao == usuCartaoDTO.CodigoTipoCartao);
                                    usuCartaoDTO.CodigoOperadora = tpCartao.cd_operadora;

                                    var listTaxaAdministrativa =
                                        item.tb_taxa_adm.Where(
                                            r => r.cd_forma_pagamento == item.cd_forma_pagamento &&
                                                 r.cd_operadora == usuCartaoDTO.CodigoOperadora);

                                    foreach (var txAdministrativa in listTaxaAdministrativa)
                                    {
                                        if (Convert.ToDecimal(usuCartaoDTO.ValorRecarga) < txAdministrativa.nu_range_qtd_inicio ||
                                            Convert.ToDecimal(usuCartaoDTO.ValorRecarga) > txAdministrativa.nu_range_qtd_fim)
                                            continue;

                                        if (txAdministrativa.ic_tipo_range.Equals("V"))
                                            valorTaxa += txAdministrativa.vl_taxa_pedido;
                                        else if (txAdministrativa.ic_tipo_range.Equals("P") &&
                                                 txAdministrativa.vl_taxa_pedido > 0)
                                            valorTaxa +=
                                                (Convert.ToDecimal(usuCartaoDTO.ValorRecarga) * txAdministrativa.vl_taxa_pedido) / 100;

                                        valorTaxa += txAdministrativa.tx_item_credito;

                                        break;
                                    }
                                }

                                result.ListaObjeto.Add(new FormaPagamentoDTO()
                                {
                                    CodigoFormaPagamento = item.cd_forma_pagamento,
                                    NomeFormaPagamento = item.nm_descricao,
                                    FormaPagamentoInativo = item.ic_forma_pagamento_inativo,
                                    Porcentagem = false,
                                    ValorTaxa = float.Parse(Decimal.Round(valorTaxa, 2).ToString()),
                                    NumeroCartao = usuCartaoDTO.NumeroCartao,
                                    CodigoGateway = item.cd_gateway,
                                    TipoPedido = tipoPedido,
                                    VlTaxaDesconto = Decimal.Round((valorTaxa - (decimal)DescontoTaxaServico), 2),
                                    HasElegible = true
                                });
                            }
                            else
                            {
                                if (valorTaxa == 0)
                                {
                                    usuCartaoDTO.CodigoOperadora = tipoCartao.cd_operadora;

                                    var listTaxaAdministrativa = item.tb_taxa_adm.Where(
                                            r => r.cd_forma_pagamento == item.cd_forma_pagamento &&
                                                 r.cd_operadora == usuCartaoDTO.CodigoOperadora);

                                    foreach (var txAdministrativa in listTaxaAdministrativa)
                                    {
                                        if (Convert.ToDecimal(usuCartaoDTO.ValorRecarga) < txAdministrativa.nu_range_qtd_inicio ||
                                            Convert.ToDecimal(usuCartaoDTO.ValorRecarga) > txAdministrativa.nu_range_qtd_fim)
                                            continue;

                                        if (txAdministrativa.ic_tipo_range.Equals("V"))
                                            valorTaxa += txAdministrativa.vl_taxa_pedido;
                                        else if (txAdministrativa.ic_tipo_range.Equals("P") &&
                                                 txAdministrativa.vl_taxa_pedido > 0)
                                            valorTaxa +=
                                                (Convert.ToDecimal(usuCartaoDTO.ValorRecarga) * txAdministrativa.vl_taxa_pedido) / 100;

                                        valorTaxa += txAdministrativa.tx_item_credito;

                                        break;
                                    }
                                }

                                var elegible = true;

                                result.ListaObjeto.Add(new FormaPagamentoDTO()
                                {
                                    CodigoFormaPagamento = item.cd_forma_pagamento,
                                    NomeFormaPagamento = item.nm_descricao,
                                    FormaPagamentoInativo = item.ic_forma_pagamento_inativo,
                                    Porcentagem = false,
                                    ValorTaxa = float.Parse(Decimal.Round(valorTaxa, 2).ToString()),
                                    NumeroCartao = usuCartaoDTO.NumeroCartao,
                                    CodigoUsuarioCartao = usuCartaoDTO.CodigoUsuarioCartao,
                                    CodigoGateway = item.cd_gateway,
                                    TipoPedido = tipoPedido,
                                    VlTaxaDesconto = Decimal.Round((valorTaxa - (decimal)DescontoTaxaServico), 2),
                                    HasElegible = elegible,
                                    CodTipoCartao = usuCartaoDTO.CodigoTipoCartao
                                });
                            }
                        }
                        else
                        {
                            result.ListaObjeto.Add(new FormaPagamentoDTO()
                            {
                                CodigoFormaPagamento = item.cd_forma_pagamento,
                                NomeFormaPagamento = item.nm_descricao,
                                HasElegible = false
                            });
                        }
                    }

                    result.Mensagem = MensagemConstants.MensagemSucesso;
                    result.Status = 0;

                }

            }

            catch (Exception ex)
            {
                result.Status = 1;
                result.MensagemLog = ex.Message;
                result.Mensagem = string.Format("Sistema indisponível no momento. Tente novamente em alguns minutos e caso a indisponibilidade persistir, contate a central de atendimento informando o seguinte código: {0}");

            }

            return result;
        }

        public DefaultRetorno NovoItemPedido(ItemPedidoDto obj, bool verificaPermissao = true)
        {
            var result = new DefaultRetorno();
            int codOperadora = 0;

            if (obj.CodigoOperadora > 0)
                codOperadora = (int)obj.CodigoOperadora;

            try
            {
                using (var db = new BD_EMPRESA3_Entities())
                {
                    if (obj.CreditoConfianca == 1)//Credito confiança
                    {

                        var VlTarifaCreditoConfianca = (from TipoCartao in db.tb_tipo_cartao
                                                        join UsuCartao in db.tb_usuario_cartao on TipoCartao.cd_tipo_cartao equals UsuCartao.cd_tipo_cartao
                                                        where UsuCartao.cd_usuario_cartao == obj.CodigoUsuarioCartao
                                                        select TipoCartao.vl_tarifa).SingleOrDefault();

                        var itemPedidoCreditoConfianca = new tb_pedido_item()
                        {
                            cd_pedido = obj.CodigoPedido,
                            cd_usuario_cartao = obj.CodigoUsuarioCartao,
                            vl_valor_recarga = (2 * (decimal)VlTarifaCreditoConfianca),
                            ds_pedido_operadora = string.Empty,
                            dt_datahora_pedido_operadora = obj.DataHoraPedidoOperadora,
                            cd_status_pedido_item = 1,
                            vl_taxa = 0,
                            vl_desconto = 0,
                            tp_pedido_item = 1,
                            ic_processado_fidelidade = false,
                            ic_credito_confianca = true,
                            vl_entrega = obj.ValorEntrega
                        };

                        db.tb_pedido_item.Add(itemPedidoCreditoConfianca);

                        db.SaveChanges();

                        obj.CodigoItemPedido = itemPedidoCreditoConfianca.cd_pedido_item;

                        db.tb_log_pedido_item.Add(new tb_log_pedido_item()
                        {
                            cd_pedido_item = itemPedidoCreditoConfianca.cd_pedido_item,
                            cd_status_pedido_item = 1,
                            dt_log = DateTime.Now
                        });

                        db.SaveChanges();


                        var status = short.Parse(StatusItemPedidoEnumerado.AguardandoProcessamento.GetHashCode().ToString());

                        var itemPedido = new tb_pedido_item()
                        {
                            cd_pedido = obj.CodigoPedido,
                            cd_usuario_cartao = obj.CodigoUsuarioCartao,
                            vl_valor_recarga = (obj.ValorRecarga - (2 * (decimal)VlTarifaCreditoConfianca)),
                            ds_pedido_operadora = string.Empty,
                            dt_datahora_pedido_operadora = obj.DataHoraPedidoOperadora,
                            cd_status_pedido_item = status,
                            vl_taxa = obj.ValorTaxa,
                            vl_desconto = obj.ValorDesconto,
                            tp_pedido_item = obj.TipoPedido,
                            ic_processado_fidelidade = false,
                            ic_credito_confianca = false,
                            nu_cpf_cod_assinatura = obj.CodigoAssinante,
                            vl_entrega = obj.ValorEntrega
                        };

                        db.tb_pedido_item.Add(itemPedido);

                        db.SaveChanges();

                        db.tb_log_pedido_item.Add(new tb_log_pedido_item()
                        {
                            cd_pedido_item = itemPedido.cd_pedido_item,
                            cd_status_pedido_item = status,
                            dt_log = DateTime.Now
                        });

                        db.SaveChanges();

                        result.Mensagem = MensagemConstants.MensagemSucesso;
                        result.Status = 0;

                        obj.CodigoItemPedido = itemPedido.cd_pedido_item;

                    }
                    else
                    {
                        var status = short.Parse(StatusItemPedidoEnumerado.AguardandoProcessamento.GetHashCode().ToString());
                        bool prePedidoOperadora = false;


                        if (obj.TipoPedido > 5 && obj.TipoPedido != 10)
                        {
                            var itemPedido = new tb_pedido_item()
                            {
                                cd_pedido = obj.CodigoPedido,
                                vl_valor_recarga = Decimal.Round(obj.ValorRecarga, 2),
                                ds_pedido_operadora = string.Empty,
                                dt_datahora_pedido_operadora = obj.DataHoraPedidoOperadora,
                                cd_status_pedido_item = status,
                                vl_taxa = obj.ValorTaxa,
                                vl_desconto = obj.ValorDesconto,
                                tp_pedido_item = obj.TipoPedido,
                                ic_processado_fidelidade = false,
                                ic_credito_confianca = false,
                                nu_ddd_recarga_celular = obj.dddCelRecarga,
                                nu_tel_recarga_celular = obj.NumeroCelRecarga,
                                cd_tipo_cartao = (int)obj.CodigoTipoCartao,
                                cd_tipo_cartao_valor = obj.TipoCartaoValor,
                                vl_cashback = obj.ValorCashBack,
                                ic_pre_pedido_operadora = prePedidoOperadora,
                                nu_cpf_cod_assinatura = obj.CodigoAssinante,
                                vl_entrega = obj.ValorEntrega
                            };

                            db.tb_pedido_item.Add(itemPedido);

                            db.SaveChanges();

                            obj.CodigoItemPedido = itemPedido.cd_pedido_item;

                            db.tb_log_pedido_item.Add(new tb_log_pedido_item()
                            {
                                cd_pedido_item = itemPedido.cd_pedido_item,
                                cd_status_pedido_item = status,
                                dt_log = DateTime.Now
                            });

                            db.SaveChanges();

                            result.Mensagem = MensagemConstants.MensagemSucesso;
                            result.Status = 0;

                        }
                        else if (obj.TipoPedido == 4 || obj.TipoPedido == 5)
                        {
                            var itemPedido = new tb_pedido_item();

                            if (obj.TipoPedido == 4)
                            {
                                itemPedido = new tb_pedido_item()
                                {
                                    cd_pedido = obj.CodigoPedido,
                                    vl_valor_recarga = Decimal.Round(obj.ValorRecarga, 2),
                                    ds_pedido_operadora = string.Empty,
                                    dt_datahora_pedido_operadora = obj.DataHoraPedidoOperadora,
                                    cd_status_pedido_item = status,
                                    vl_taxa = obj.ValorTaxa,
                                    vl_desconto = obj.ValorDesconto,
                                    tp_pedido_item = obj.TipoPedido,
                                    ic_processado_fidelidade = false,
                                    cd_tipo_cartao = (int)obj.CodigoTipoCartao,
                                    vl_cashback = obj.ValorCashBack,
                                    ic_pre_pedido_operadora = prePedidoOperadora,
                                    nu_cpf_cod_assinatura = obj.CodigoAssinante,
                                    vl_entrega = obj.ValorEntrega,
                                    cd_operadora = codOperadora
                                };
                            }
                            else
                                itemPedido = new tb_pedido_item()
                                {
                                    cd_pedido = obj.CodigoPedido,
                                    vl_valor_recarga = Decimal.Round(obj.ValorRecarga, 2),
                                    ds_pedido_operadora = string.Empty,
                                    dt_datahora_pedido_operadora = obj.DataHoraPedidoOperadora,
                                    cd_status_pedido_item = status,
                                    cd_usuario_cartao = obj.CodigoUsuarioCartao,
                                    vl_taxa = obj.ValorTaxa,
                                    vl_desconto = obj.ValorDesconto,
                                    tp_pedido_item = obj.TipoPedido,
                                    ic_processado_fidelidade = false,
                                    cd_tipo_cartao = (int)obj.CodigoTipoCartao,
                                    vl_cashback = obj.ValorCashBack,
                                    ic_pre_pedido_operadora = prePedidoOperadora,
                                    nu_cpf_cod_assinatura = obj.CodigoAssinante,
                                    vl_entrega = obj.ValorEntrega,
                                    cd_operadora = codOperadora
                                };

                            db.tb_pedido_item.Add(itemPedido);

                            db.SaveChanges();
                            obj.CodigoItemPedido = itemPedido.cd_pedido_item;

                            db.tb_log_pedido_item.Add(new tb_log_pedido_item()
                            {
                                cd_pedido_item = itemPedido.cd_pedido_item,
                                cd_status_pedido_item = status,
                                dt_log = DateTime.Now
                            });

                            db.SaveChanges();

                            result.Mensagem = MensagemConstants.MensagemSucesso;
                            result.Status = 0;

                            InsereComplementoPedidoCartao(obj);

                        }
                        else if (obj.TipoPedido == 10)
                        {
                            var itemPedido = new tb_pedido_item()
                            {
                                cd_pedido = obj.CodigoPedido,
                                vl_valor_recarga = Decimal.Round(obj.ValorRecarga, 2),
                                ds_pedido_operadora = string.Empty,
                                dt_datahora_pedido_operadora = obj.DataHoraPedidoOperadora,
                                cd_status_pedido_item = status,
                                vl_taxa = obj.ValorTaxa,
                                vl_desconto = obj.ValorDesconto,
                                tp_pedido_item = obj.TipoPedido,
                                ic_processado_fidelidade = false,
                                ic_credito_confianca = false,
                                nu_ddd_recarga_celular = obj.dddCelRecarga,
                                nu_tel_recarga_celular = obj.NumeroCelRecarga,
                                cd_tipo_cartao = (int)obj.CodigoTipoCartao,
                                vl_cashback = obj.ValorCashBack,
                                ic_pre_pedido_operadora = prePedidoOperadora,
                                nu_cpf_cod_assinatura = obj.CodigoAssinante,
                                vl_entrega = obj.ValorEntrega,
                                cd_operadora = codOperadora
                            };

                            db.tb_pedido_item.Add(itemPedido);

                            db.SaveChanges();

                            obj.CodigoItemPedido = itemPedido.cd_pedido_item;

                            db.tb_log_pedido_item.Add(new tb_log_pedido_item()
                            {
                                cd_pedido_item = itemPedido.cd_pedido_item,
                                cd_status_pedido_item = status,
                                dt_log = DateTime.Now
                            });

                            db.SaveChanges();

                            result.Mensagem = MensagemConstants.MensagemSucesso;
                            result.Status = 0;

                        }
                        else
                        {
                            if (obj.TipoPedido == 1)
                            {
                                var prePedido = db.tb_tipo_cartao.Join(db.tb_usuario_cartao,
                                                                       tc => tc.cd_tipo_cartao, uc => uc.cd_tipo_cartao, (tc, uc) => new { tc, uc })
                                                                 .Where(r => r.uc.cd_usuario_cartao == obj.CodigoUsuarioCartao && r.tc.cd_tipo_cartao == r.uc.cd_tipo_cartao).ToList();

                                foreach (var item in prePedido)
                                {
                                    if (item.tc.ic_pre_pedido_operadora == true)
                                    {
                                        var formaPagamento = db.tb_pedido.FirstOrDefault(x => x.cd_pedido == obj.CodigoPedido);

                                        if ("345".Contains(formaPagamento.cd_forma_pagamento.ToString()))
                                            prePedidoOperadora = true;
                                    }
                                }
                            }

                            var itemPedido = new tb_pedido_item()
                            {
                                cd_pedido = obj.CodigoPedido,
                                cd_usuario_cartao = obj.CodigoUsuarioCartao,
                                vl_valor_recarga = Decimal.Round(obj.ValorRecarga, 2),
                                ds_pedido_operadora = string.Empty,
                                dt_datahora_pedido_operadora = obj.DataHoraPedidoOperadora,
                                cd_status_pedido_item = status,
                                vl_taxa = obj.ValorTaxa,
                                vl_desconto = obj.ValorDesconto,
                                tp_pedido_item = obj.TipoPedido,
                                ic_processado_fidelidade = false,
                                ic_credito_confianca = false,
                                vl_cashback = obj.ValorCashBack,
                                ic_pre_pedido_operadora = prePedidoOperadora,
                                vl_entrega = obj.ValorEntrega,
                                cd_operadora = codOperadora
                                //   cd_tipo_cartao = (int)obj.CodigoTipoCartao,
                                // cd_tipo_cartao_valor = obj.TipoCartaoValor
                            };

                            db.tb_pedido_item.Add(itemPedido);

                            db.SaveChanges();
                            obj.CodigoItemPedido = itemPedido.cd_pedido_item;

                            db.tb_log_pedido_item.Add(new tb_log_pedido_item()
                            {
                                cd_pedido_item = itemPedido.cd_pedido_item,
                                cd_status_pedido_item = status,
                                dt_log = DateTime.Now
                            });

                            db.SaveChanges();

                            result.Mensagem = MensagemConstants.MensagemSucesso;
                            result.Status = 0;
                        }

                        result.ListaObjeto.Add(obj);
                    }


                }
            }
            catch (Exception ex)
            {
                result.Mensagem = MensagemConstants.MensagemAutenticacao;
                result.Status = 1;
            }

            return result;
        }
        private void InsereComplementoPedidoCartao(ItemPedidoDto obj)
        {
            using (var db = new BD_EMPRESA3_Entities())
            {
                var pedCartao = new tb_pedido_item_cartao();

                pedCartao.cd_pedido_item = obj.CodigoItemPedido;
                pedCartao.dt_registro = DateTime.Now;
                pedCartao.cd_status_pedido_item_cartao = 0;
                pedCartao.ic_finalizado_operadora = false;


                if (obj.isRetirada)
                {
                    pedCartao.ic_retirada_entrega = false;

                    if (obj.codPosto > 0)
                        pedCartao.cd_posto = obj.codPosto;
                    else
                    {
                        var postoEntrega = (from postoOpe in db.tb_posto_operadora
                                            join posto in db.tb_posto on postoOpe.cd_posto equals posto.cd_posto
                                            where postoOpe.cd_operadora == obj.CodigoOperadora
                                            && posto.ic_posto_inativo == false && posto.ic_posto_padrao == true
                                            select postoOpe).FirstOrDefault();

                        if (postoEntrega != null)
                            pedCartao.cd_posto = postoEntrega.cd_posto;
                    }
                }
                else
                {
                    var postoEntrega = (from postoOpe in db.tb_posto_operadora
                                        join posto in db.tb_posto on postoOpe.cd_posto equals posto.cd_posto
                                        where postoOpe.cd_operadora == obj.CodigoOperadora
                                        && posto.ic_posto_inativo == false && posto.ic_posto_padrao == true
                                        select postoOpe).FirstOrDefault();

                    if (postoEntrega != null)
                        pedCartao.cd_posto = postoEntrega.cd_posto;

                    pedCartao.ic_retirada_entrega = true;
                    pedCartao.nm_logradouro_entrega = obj.logradouroEntrega;
                    pedCartao.nu_logradouro_entrega = obj.numLogradouroEntrega;
                    pedCartao.cd_uf = obj.codUFEntrega;
                    pedCartao.ds_complemento_logradouro_entrega = obj.compEntrega;
                    pedCartao.nu_cep_entrega = obj.cepEntrega;
                    pedCartao.nm_bairro_entrega = obj.bairroEntrega;
                    pedCartao.nm_municipio_entrega = obj.municipioEntrega;

                }

                db.tb_pedido_item_cartao.Add(pedCartao);

                db.SaveChanges();
            }
        }

        private DefaultRetorno NovoPedidoSpTrans(ItemPedidoDto item, int formaPag)
        {
            var result = new DefaultRetorno();
            PedidoOutput pedidoOutput;

            if (formaPag == 1)
                formaPag = 3;
            else if (formaPag == 3)
                formaPag = 1;

            using (var db = new BD_EMPRESA3_Entities())
            {
                
                var operadora = db.tb_operadora.FirstOrDefault(x => x.cd_operadora == item.CodigoOperadora);
                var usuCard = db.tb_usuario_cartao.FirstOrDefault(x => x.cd_usuario_cartao == item.CodigoUsuarioCartao);

                var validacao = card.ConsultarCartaoAsync(usuCard.nu_cartao).Result;

                // Devera ser Ajustado abaixo conforme os retornos da TIVIT, pois no momento da criação a VPN não estava criada!
               
                // Recarga comun
                if (item.TipoPedido == '1')
                {
                    pedidoOutput = new PedidoOutput()
                    {
                        numeroLogicoCartao = int.Parse(usuCard.nu_cartao),
                        codigoTipoCredito = validacao.TipoCredito.FirstOrDefault().Codigo,
                        codigoProduto = '1',
                        quantidade ='0',
                        valorTotal = item.ValorRecarga
                    };

                }
                //Recarga Diaria
                else 
                {
                    pedidoOutput = new PedidoOutput()
                    {
                        numeroLogicoCartao = int.Parse(usuCard.nu_cartao),
                        codigoTipoCredito = validacao.TipoCredito.FirstOrDefault().Codigo,
                        codigoProduto = '2',
                        quantidade = '1',
                        valorTotal = item.ValorRecarga  //esse será o valor da quantidade solicitada multiplicada pelo valor unitário do produto.
                    };
                }


                string url = ConfigurationManager.AppSettings["host"] + "GerarPedido";
                var response = client.PostAsJsonAsync(url, pedidoOutput);



                if (retornoCitsoa.Messagem == "OK")
                {
                    dadosPedidoCitSoa pedidoAberto = JsonConvert.DeserializeObject<dadosPedidoCitSoa>(retornoCitsoa.data[0].ToString());

                    //var retornoConsulta = new ApiCitSoaController().ConsultaPedidoCarga(CitSoaDto);
                    var retornoConsulta = new ApiCitSoaController().ConsultaPedidoPorStatus(CitSoaDto);

                    if (retornoConsulta.Messagem == "OK")
                    {
                        RetornoConsultaCarga dados = JsonConvert.DeserializeObject<RetornoConsultaCarga>(retornoCitsoa.data[0].ToString());
                        if (dados.statusPedido == "ABERTO")
                        {
                            pedidoAberto.pedidoId = dados.pedidoId;
                        }
                        result.ListaObjeto.Add(pedidoAberto);
                        result.Mensagem = MensagemConstants.MensagemSucesso;
                        return result;
                    }
                    else
                        return retornoCitsoa.result;
                }
                else
                {
                    return retornoCitsoa.result;
                }
            }
        }

    }
}
