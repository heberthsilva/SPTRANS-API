using Newtonsoft.Json;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class BuscarParadaOutput
    {
        /// <summary>Código identificador da parada</summary>  
        [JsonProperty(PropertyName = "cp")]
        public int CodigoParada { get; set; }
        /// <summary>Nome da parada</summary>  
        [JsonProperty(PropertyName = "np")] 
        public string NomeParada { get; set; }
        /// <summary>Endereço de localização da parada</summary>
        [JsonProperty(PropertyName = "ed")]
        public string EnderecoParada { get; set; }
        /// <summary>Informação de latitude da localização da parada</summary>
        [JsonProperty(PropertyName = "py")]
        public double LatitudeParada { get; set; }
        /// <summary> Informação de longitude da localização da parada</summary>
        [JsonProperty(PropertyName = "px")]
        public double LongitudeParada{ get; set; }

    }

}