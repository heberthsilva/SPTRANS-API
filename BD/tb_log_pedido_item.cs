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
    
    public partial class tb_log_pedido_item
    {
        public long cd_log_pedido_item { get; set; }
        public long cd_pedido_item { get; set; }
        public short cd_status_pedido_item { get; set; }
        public Nullable<int> cd_dicionario_erro { get; set; }
        public System.DateTime dt_log { get; set; }
    
        public virtual tb_dicionario_erro tb_dicionario_erro { get; set; }
        public virtual tb_pedido_item tb_pedido_item { get; set; }
        public virtual tb_status_pedido_item tb_status_pedido_item { get; set; }
    }
}
