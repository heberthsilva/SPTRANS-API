using System.Collections.Generic;
using Newtonsoft.Json;

namespace sptrans_api.Models
{
    public class PrevisaoChegadaParadaOutput
    {
        [JsonProperty(PropertyName = "hr")]
        public string HorarioRefencia { get; set; }


        [JsonProperty(PropertyName = "p")]
        public PontoParadaChegada PontoParadaChegada { get; set; }

    }

    public class PontoParadaChegada
    {
        [JsonProperty(PropertyName = "cp")]
        public int CodigoParada { get; set; }

        [JsonProperty(PropertyName = "np")]
        public string NomeParada { get; set; }

        [JsonProperty(PropertyName = "py")]
        public double LatitudeParada { get; set; }

        [JsonProperty(PropertyName = "px")]
        public double LongitudeParada { get; set; }

        [JsonProperty(PropertyName = "l")]
        public List<LinhasLocalizadasParada> LinhasLocalizadasParada { get; set; }

    }

    public class LinhasLocalizadasParada
    {
        [JsonProperty(PropertyName = "c")]
        public string LetreiroCompleto { get; set; }

        [JsonProperty(PropertyName = "cl")]
        public int CodigoLinha { get; set; }

        [JsonProperty(PropertyName = "sl")]
        public int SentidoLinha { get; set; }

        [JsonProperty(PropertyName = "lt0")]
        public string LetreiroDestino { get; set; }
        [JsonProperty(PropertyName = "lt1")]
        public string LetreiroOrigem { get; set; }

        [JsonProperty(PropertyName = "qv")]
        public int QuantidadeVeiculos { get; set; }

        [JsonProperty(PropertyName = "vs")]
        public List<RelacaoVeiculosParada> RelacaoVeiculosParada { get; set; }

    }
    public class RelacaoVeiculosParada
    {
        [JsonProperty(PropertyName = "p")]
        public string PrefixoVeiculo { get; set; }

        [JsonProperty(PropertyName = "t")]
        public string PrevisaoChegadaParada { get; set; }

        [JsonProperty(PropertyName = "a")]
        public bool AcessivelDeficiencia { get; set; }

        [JsonProperty(PropertyName = "ta")]
        public string HorarioCaptura { get; set; }

        [JsonProperty(PropertyName = "py")]
        public double LatitudeVeiculo { get; set; }

        [JsonProperty(PropertyName = "px")]
        public double LongitudeVeiculo { get; set; }
    }

}


