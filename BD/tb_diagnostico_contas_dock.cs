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
    
    public partial class tb_diagnostico_contas_dock
    {
        public int cd_diagnostico_contas_dock { get; set; }
        public Nullable<int> cd_usuario { get; set; }
        public Nullable<int> cd_usuario_banco_conta { get; set; }
        public Nullable<int> id_conta { get; set; }
        public Nullable<int> id_pessoa { get; set; }
        public string id_registration { get; set; }
        public Nullable<int> id_status_dock { get; set; }
        public string desc_status_individual { get; set; }
        public Nullable<bool> ic_conta_vinculada { get; set; }
        public Nullable<bool> ic_pessoa_vinculada { get; set; }
    }
}