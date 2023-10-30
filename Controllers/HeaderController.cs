using sptrans_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace sptrans_api.Controllers
{
    public class HeaderController : ApiController
    {
        public HeaderDto GetHeaderVariableDefault(HttpRequestMessage request)
        {
            int UsuarioID;
            int.TryParse(request.Headers.GetValues("User").First(), out UsuarioID);


            IEnumerable<string> listFormaPagamentoIdId;
            int FormaPagamentoId = 0;

            var existFormaPagamento = request.Headers.TryGetValues("FormaPagamento", out listFormaPagamentoIdId);

            if (existFormaPagamento)
            {
                int.TryParse(listFormaPagamentoIdId.First(), out FormaPagamentoId);
            }


            IEnumerable<string> listOperatorId;
            int OperatorId = 0;

            var existOperator = request.Headers.TryGetValues("OperatorId", out listOperatorId);

            if (existOperator)
                int.TryParse(listOperatorId.First(), out OperatorId);

            return new HeaderDto()
            {
                FormaPagamento = FormaPagamentoId,
                UsuarioId = UsuarioID,
                UsuarioToken = request.Headers.GetValues("Authorization").First(),
                OperadoraId = OperatorId
            };
        }
    }
}
