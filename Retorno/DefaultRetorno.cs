using System.Collections.Generic;

namespace sptrans_api.Retorno
{
    public class DefaultRetorno
    {
        public string Mensagem { get; set; }
        public string MensagemLog { get; set; }
        public string MensagemStatus { get; set; }
        public int Status { get; set; }
        public List<object> ListaObjeto { get; set; } = new List<object>();
    }
}