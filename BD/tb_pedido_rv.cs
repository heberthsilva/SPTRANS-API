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
    
    public partial class tb_pedido_rv
    {
        public long cd_pedido_rv { get; set; }
        public Nullable<long> cd_pedido_item { get; set; }
        public string id_idempotencia { get; set; }
        public Nullable<System.DateTime> dt_solicitacao { get; set; }
        public string id_produto_rv { get; set; }
        public string vl_valor { get; set; }
        public string id_transacao { get; set; }
        public string ds_message { get; set; }
        public string ds_status { get; set; }
        public Nullable<System.DateTime> dt_status { get; set; }
        public Nullable<bool> ic_confirmado { get; set; }
        public string ds_pin { get; set; }
        public string ds_nsu { get; set; }
        public string ds_codigo_autorizacao { get; set; }
    }
}