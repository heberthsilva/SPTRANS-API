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
    
    public partial class tb_log_black_list_usuario_forma_pagamento
    {
        public int cd_log_black_list_usuario_forma_pagamento { get; set; }
        public short ic_tipo_log { get; set; }
        public string nu_cpf_usuario { get; set; }
        public short cd_forma_pagamento { get; set; }
        public System.DateTime dt_data_hora_log { get; set; }
        public int cd_usuario_adm { get; set; }
    
        public virtual tb_forma_pagamento tb_forma_pagamento { get; set; }
        public virtual tb_usuario_adm tb_usuario_adm { get; set; }
    }
}
