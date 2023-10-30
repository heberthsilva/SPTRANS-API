using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sptrans_api.Models
{
    public class MensagemConstants
    {
        public static string MensagemSucesso = "Sucesso";

        public static string MensagemUsuarioCartaoBloqueado = "Cartão está bloqueado. Procure a EMPRESA3 MAIS e regularize a situação do seu cartão de transporte.";
        public static string MensagemUsuarioCartaoJaCadastrado = "Cartão já cadastrado, favor informar outro número de cartão.";
        public static string MensagemUsuarioCartaoTipoCartaoNaoCadastrado = "O tipo de cartão informado não é permitido para utilização na aplicação.";
        public static string ErroAutenticacao = "O tipo de cartão informado não é permitido para utilização na aplicação.";
        public static string MensagemGatewayNaoLocalizado = "Seu pedido não pôde ser processado. Por Favor tente mais tarde.";
        public static string MensagemAutenticacao = "Falha na autenticação para a transação solicitada!";

        public static string CartaoNotFound = "O número de cartão informado não existe ou não está liberado para uso.";
        public static string CartaoNaoLiberado = "Tipo de Cartão não liberado para compra de creditos via WEB. Dirija-se a um posto de venda da SPTrans.";
        public static string TipoProfessorDeny = "Não é possível efetuar a compra de crédito comum em um cartão do tipo Professor.";
        public static string PedidoDeny = "Pedidode Crédito não permitido. Favor entrar em contato com a SPTrans.";
        public static string ErroCard = "Ocorreu um problema durante a Consulta do Cartão. Por favor tente novamente em alguns instantes ou entre em contato com a SPTrans.";

    }
}