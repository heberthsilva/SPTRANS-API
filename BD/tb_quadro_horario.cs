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
    
    public partial class tb_quadro_horario
    {
        public int cd_quadro_horario { get; set; }
        public string id_linha_sbe { get; set; }
        public Nullable<int> cd_operadora { get; set; }
        public string ds_origem { get; set; }
        public string ds_horario { get; set; }
        public string ds_tipoDia { get; set; }
    }
}