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
    
    public partial class tb_pedido_pagamento_picpay
    {
        public long cd_pedido_pagamento_picpay { get; set; }
        public Nullable<long> cd_pedido { get; set; }
        public Nullable<int> cd_status { get; set; }
        public string ds_payment_url { get; set; }
        public string ds_link_qrcode { get; set; }
        public string ds_imagem_qrcode { get; set; }
        public Nullable<System.DateTime> dt_registro { get; set; }
        public Nullable<System.DateTime> dt_expiracao { get; set; }
        public Nullable<System.DateTime> dt_notificacao { get; set; }
        public Nullable<System.DateTime> dt_pagamento { get; set; }
        public Nullable<decimal> vl_pedido { get; set; }
        public Nullable<decimal> vl_pago { get; set; }
    
        public virtual tb_pedido tb_pedido { get; set; }
        public virtual tb_pedido_pagamento_picpay tb_pedido_pagamento_picpay1 { get; set; }
        public virtual tb_pedido_pagamento_picpay tb_pedido_pagamento_picpay2 { get; set; }
    }
}
