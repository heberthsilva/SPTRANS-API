//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sptrans_api.BD
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_log_usuario_revenda_suspenso
    {
        public long cd_log_revenda_suspenso { get; set; }
        public long cd_usuario { get; set; }
        public short nu_tipo_evento { get; set; }
        public string nm_evento { get; set; }
        public System.DateTime dt_evento { get; set; }
    
        public virtual tb_usuario tb_usuario { get; set; }
    }
}
