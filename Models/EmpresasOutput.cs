using Newtonsoft.Json;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class EmpresasOutput
    {
        /// <summary>Horário de referência da geração das informações</summary>
        [JsonProperty(PropertyName = "hr")]
        public string HorarioRefencia { get; set; }
        /// <summary>Relação de empresas por área de operação</summary>
        [JsonProperty(PropertyName = "e")]
        public EmpresasPorOperacao[] EmpresasPorOperação { get; set; }

    }

    public class EmpresasPorOperacao
    {   /// <summary>Código da área de operação</summary>
        [JsonProperty(PropertyName = "a")]
        public int CodigoAreaOperacao { get; set; }
        /// <summary> Relação de empresas</summary>
        [JsonProperty(PropertyName = "e")]
        public RelacaoEmpresas[] RelacaoEmpresas { get; set; }

    }
    public class RelacaoEmpresas
    {   /// <summary>Código da área de operação</summary>
        [JsonProperty(PropertyName = "a")]
        public int CodigoAreaOperacao { get; set; }
        /// <summary>Código de referência da empresa</summary>
        [JsonProperty(PropertyName = "c")]
        public int CodigoEmpresa { get; set; }
        /// <summary>Nome da empresa</summary>
        [JsonProperty(PropertyName = "n")]
        public string NomeEmpresa { get; set; }
    }

}

