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
    
    public partial class tb_tipo_pedido_item_fluxo_status
    {
        public int cd_tipo_pedido_item_fluxo_status { get; set; }
        public int cd_tipo_pedido_item { get; set; }
        public short cd_status_pedido_item { get; set; }
        public short nu_ordem_etapa { get; set; }
        public bool ic_inativo { get; set; }
    
        public virtual tb_status_pedido_item tb_status_pedido_item { get; set; }
        public virtual tb_tipo_pedido_item tb_tipo_pedido_item { get; set; }
    }
}