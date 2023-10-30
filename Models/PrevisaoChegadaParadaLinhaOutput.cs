using Newtonsoft.Json;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class PrevisaoChegadaParadaLinhaOutput
    {
        /// <summary>Horário de referência da geração das informações</summary>
        [JsonProperty(PropertyName = "hr")]
        public string HorarioRefencia { get; set; }

        /// <summary>Representa um ponto de parada onde:</summary>
        [JsonProperty(PropertyName = "p")]
        public PontoParada[] PontoParada { get; set; }

    }
    public class PontoParada
    {
        /// <summary>código identificador da parada</summary>
        [JsonProperty(PropertyName = "cp")]
        public int CodigoParada { get; set; }
        /// <summary>Nome da parada</summary>
        [JsonProperty(PropertyName = "np")]
        public string NomeParada { get; set; }
        /// <summary>Informação de latitude da localização do veículo</summary>
        [JsonProperty(PropertyName = "py")]
        public double LatitudeVeiculo { get; set; }
        /// <summary>Informação de longitude da localização do veículo</summary>
        [JsonProperty(PropertyName = "px")]
        public double LongitudeVeiculo { get; set; }
        /// <summary>Informação de longitude da localização do veículo</summary>
        [JsonProperty(PropertyName = "l")]
        public List<LinhasLocalizadas> LinhasLocalizadas { get; set; }

    } 

}