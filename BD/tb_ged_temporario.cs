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
    
    public partial class tb_ged_temporario
    {
        public long cd_ged_temp { get; set; }
        public long cd_usuario { get; set; }
        public short nu_tipo { get; set; }
        public byte[] ds_imagem_binary { get; set; }
        public System.DateTime dt_registro { get; set; }
        public string nu_codigo_usuario_operadora { get; set; }
        public Nullable<long> cd_requisicao_via { get; set; }
    
        public virtual tb_usuario tb_usuario { get; set; }
    }
}
