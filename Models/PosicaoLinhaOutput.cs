using Newtonsoft.Json;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class PosicaoLinhaOutput
    {
        /// <summary>Horário de referência da geração das informações</summary>
        [JsonProperty(PropertyName = "hr")]
        public string HorarioRefencia { get; set; }
        /// <summary>Relação de linhas localizadas onde:</summary>
        [JsonProperty(PropertyName = "l")]
        public List<LinhasLocalizadas> LinhasLocalizadas { get; set; }

    }

    public class LinhasLocalizadas
    {   /// <summary>Letreiro completo</summary>
        [JsonProperty(PropertyName = "c")]
        public string LetreiroCompleto { get; set; }
        /// <summary>Código identificador da linha</summary>
        [JsonProperty(PropertyName = "cl")]
        public int CodigoLinha { get; set; }
        /// <summary>Sentido de operação onde 1 significa de Terminal Principal para Terminal Secundário e 2 de Terminal Secundário para Terminal Principal</summary>
        [JsonProperty(PropertyName = "sl")]
        public int SentidoLinha { get; set; }
        /// <summary> Letreiro de destino da linha</summary>
        [JsonProperty(PropertyName = "lt0")]
        public string LetreiroDestino { get; set; }
        /// <summary> Letreiro de origem da linha</summary>
        [JsonProperty(PropertyName = "lt1")]
        public string LetreiroOrigem { get; set; }
        /// <summary>Quantidade de veículos localizados</summary>
        [JsonProperty(PropertyName = "qv")]
        public int QuantidadeVeiculos { get; set; }
        /// <summary>Relação de veículos localizados, onde:</summary>
        [JsonProperty(PropertyName = "vs")]
        public List<RelacaoVeiculos> RelacaoVeiculos { get; set; }

    }
    public class RelacaoVeiculos
    {   /// <summary>Prefixo do veículo</summary>
        [JsonProperty(PropertyName = "p")]
        public int PrefixoVeiculo { get; set; }
        /// <summary>Indica se o veículo é (true) ou não (false) acessível para pessoas com deficiência</summary>
        [JsonProperty(PropertyName = "a")]
        public bool AcessivelDeficiencia { get; set; }
        /// <summary> Indica o horário universal (UTC) em que a localização foi capturada. Essa informação está no padrão ISO 8601</summary>
        [JsonProperty(PropertyName = "ta")]
        public string HorarioCaptura { get; set; }
        /// <summary> Informação de latitude da localização do veículo</summary>
        [JsonProperty(PropertyName = "py")]
        public double LatitudeVeiculo { get; set; }
        /// <summary> Informação de longitude da localização do veículo</summary>
        [JsonProperty(PropertyName = "px")]
        public double LongitudeVeiculo { get; set; }
    }


}








