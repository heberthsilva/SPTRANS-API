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
    
    public partial class tb_usuario_dependente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_usuario_dependente()
        {
            this.tb_usuario_dependente_documento_banco = new HashSet<tb_usuario_dependente_documento_banco>();
        }
    
        public long cd_usuario_dependente { get; set; }
        public long cd_usuario { get; set; }
        public string nm_dependente { get; set; }
        public string nu_cpf_dependente { get; set; }
        public string nm_mae_dependente { get; set; }
        public Nullable<System.DateTime> dt_nascimento_dependente { get; set; }
        public string ds_email_dependente { get; set; }
        public string ds_nivel_tipo_dependente { get; set; }
        public string ds_tipo_dependente { get; set; }
        public string nu_rg_dependente { get; set; }
        public System.DateTime dt_emissao_rg_dependente { get; set; }
        public string nm_logradouro_dependente { get; set; }
        public int nu_logradouro_dependente { get; set; }
        public string nm_logradouro_complemento_dependente { get; set; }
        public string nm_bairro_dependente { get; set; }
        public string nm_municipio_dependente { get; set; }
        public short cd_uf_dependente { get; set; }
        public string nm_pais_dependente { get; set; }
        public string nu__cep_dependente { get; set; }
        public string nu_ddi_telefone_dependente { get; set; }
        public string nu_ddd_telefone_dependente { get; set; }
        public string nu_telefone_dependente { get; set; }
        public Nullable<System.DateTime> dt_cadastro { get; set; }
        public Nullable<bool> ic_inativo { get; set; }
    
        public virtual tb_uf tb_uf { get; set; }
        public virtual tb_usuario tb_usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_usuario_dependente_documento_banco> tb_usuario_dependente_documento_banco { get; set; }
    }
}
