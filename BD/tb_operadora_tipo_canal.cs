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
    
    public partial class tb_operadora_tipo_canal
    {
        public int cd_operadora_tipo_canal { get; set; }
        public Nullable<int> cd_operadora { get; set; }
        public Nullable<int> cd_tipo_canal { get; set; }
    
        public virtual tb_operadora tb_operadora { get; set; }
        public virtual tb_tipo_canal tb_tipo_canal { get; set; }
    }
}