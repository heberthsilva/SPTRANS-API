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
    
    public partial class tb_status_conta
    {
        public short cd_status_conta { get; set; }
        public string ds_status_conta { get; set; }
    
        public virtual tb_status_conta tb_status_conta1 { get; set; }
        public virtual tb_status_conta tb_status_conta2 { get; set; }
    }
}