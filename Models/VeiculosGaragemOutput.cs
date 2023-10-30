using Newtonsoft.Json;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class VeiculosGaragemOutput
    {
        /// <summary>Horário de referência da geração das informações</summary>
        [JsonProperty(PropertyName = "hr")]
        public string HorarioRefencia { get; set; }

        /// <summary>Relação de linhas localizadas onde:</summary>
        [JsonProperty(PropertyName = "l")]
        public List<LinhasLocalizadas> LinhasLocalizadas { get; set; }

    }  

}