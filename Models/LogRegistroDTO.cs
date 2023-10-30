using System;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class LogRegistroDTO
    {
        public long CodigoUsuario { get; set; }
        public short TipoRegistro { get; set; }
        public int CodigoCanal { get; set; }
    }
}