using Newtonsoft.Json;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class PrevisaoChegadaLinhaOutput
    {
        /// <summary>Horário de referência da geração das informações</summary>
        [JsonProperty(PropertyName = "hr")]
        public string HorarioRefencia { get; set; }

        /// <summary>Representa um ponto de parada onde:</summary>
        [JsonProperty(PropertyName = "ps")]
        public List<PontosParadas> PontoParada { get; set; }

    }
    public class PontosParadas
    {
        /// <summary>Código identificador da parada</summary>
        [JsonProperty(PropertyName = "cp")]
        public int CodigoParada { get; set; }

        /// <summary>Nome da parada</summary>
        [JsonProperty(PropertyName = "np")]
        public string NomeParada { get; set; }
        ///<summary>Informação de latitude da localização do veículo</summary>
        [JsonProperty(PropertyName = "py")]
        public double LatitudeVeiculo { get; set; }
        ///<summary>Informação de longitude da localização do veículo</summary>
        [JsonProperty(PropertyName = "px")]
        public double LongitudeVeiculo { get; set; }
        ///<summary>Relação de veículos localizados onde:</summary>
        [JsonProperty(PropertyName = "vs")]
        public List<LinhaLocalizada> LinhasLocalizadas { get; set; }

    }
    public class LinhaLocalizada
    {
        ///<summary>Prefixo do veículo</summary>
        [JsonProperty(PropertyName = "p")]
        public int PrefixoVeiculo { get; set; }
        ///<summary>Horário previsto para chegada do veículo no ponto de parada relacionado</summary>
        [JsonProperty(PropertyName = "t")]
        public string HorarioPrevistoChegada { get; set; }
        ///<summary>Indica se o veículo é (true) ou não (false) acessível para pessoas com deficiência</summary>
        [JsonProperty(PropertyName = "a")]
        public bool VeiculoAcessivel { get; set; }
        ///<summary>Indica o horário universal (UTC) em que a localização foi capturada. Essa informação está no padrão ISO 8601</summary>
        [JsonProperty(PropertyName = "ta")]
        public string HorarioCaptura { get; set; }
        ///<summary>Informação de latitude da localização do veículo</summary>
        [JsonProperty(PropertyName = "py")]
        public double LatitudeVeiculo { get; set; }
        ///<summary>Informação de longitude da localização do veículo</summary>
        [JsonProperty(PropertyName = "px")]
        public double LongitudeVeiculo { get; set; }

    }

}