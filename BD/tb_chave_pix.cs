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
    
    public partial class tb_chave_pix
    {
        public int cd_chave_pix { get; set; }
        public Nullable<short> cd_gateway { get; set; }
        public string ds_chave_pix { get; set; }
        public Nullable<bool> ic_ativo { get; set; }
    
        public virtual tb_gateway tb_gateway { get; set; }
    }
}
