using System;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class HeaderDto
    {
        public long UsuarioId { get; set; }
        public string UsuarioToken { get; set; }
        public int OperadoraId { get; set; }
        public int FormaPagamento { get; set; }
    }
}