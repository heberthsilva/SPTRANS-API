using sptrans_api.Models;
using sptrans_api.Retorno;
using sptrans_api.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace sptrans_api.Controllers
{
    
    public class CardController : ApiController
    {
        private HeaderDto _headerDto;
        public HeaderDto HeaderDto => _headerDto ?? (_headerDto = new HeaderController().GetHeaderVariableDefault(Request));


        [HttpPost]
        [Route("NovoCartao")]
        public DefaultRetorno NovoCartao(CardInput input)
        {
            return new CardService().NovoCartao(input);
        }

        [HttpPost]
        [Route("NovoPedido")]
        public DefaultRetorno NovoPedido(PedidoInput pedido, int UserId)
        {
            return new PedidoService().NovoPedido(pedido);
        }

        [HttpPost]
        [Route("ConfirmarPedido")]
        public Task<bool> ConfirmarPedido(int NumeroPedido)
        {
            return new PedidoService().ConfirmarPedido(NumeroPedido);
        }

        [HttpPost]
        [Route("CancelarPedido")]
        public Task<bool> CancelarPedido(int NumeroPedido)
        {
            return new PedidoService().CancelarPedido(NumeroPedido);
        }
    }
}
