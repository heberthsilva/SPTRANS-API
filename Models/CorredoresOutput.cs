using Newtonsoft.Json;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class CorredoresOutput
    {
        /// <summary>Código identificador da corredor. Este é um código identificador único de cada corredor inteligente do sistema</summary>  
        [JsonProperty(PropertyName = "cc")]
        public int CodigoCorredor { get; set; }
        /// <summary>Nome do corredor</summary> 
        [JsonProperty(PropertyName = "nc")]
        public string NomeCorredor { get; set; }
    }

}