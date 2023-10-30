using Newtonsoft.Json;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class PosicaoPorLinhaOutput
    {
        /// <summary>Horário de referência da geração das informações</summary>
        [JsonProperty(PropertyName = "hr")]
        public string HorarioRefencia { get; set; }
        /// <summary>Relação de veículos localizados, onde:</summary>
        [JsonProperty(PropertyName = "vs")]
        public List<RelacaoVeiculos> LinhasLocalizadas { get; set; }

    }

}