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
    
    public partial class tb_usuario_termo_de_uso
    {
        public long cd_usuario_termo_de_uso { get; set; }
        public short cd_termo_de_uso { get; set; }
        public long cd_usuario { get; set; }
        public string ds_ip { get; set; }
        public System.DateTime dt_aceite { get; set; }
    
        public virtual tb_termo_de_uso tb_termo_de_uso { get; set; }
        public virtual tb_usuario tb_usuario { get; set; }
    }
}
