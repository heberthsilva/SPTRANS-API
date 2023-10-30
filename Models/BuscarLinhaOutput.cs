using Newtonsoft.Json;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class BuscarLinhaOutput
    {
        /// <summary>Código identificador da linha. Este é um código identificador único de cada linha do sistema (por sentido de operação)</summary>        
        [JsonProperty(PropertyName = "cl")]
        public int CodigoLinha { get; set; }
        /// <summary>Indica se uma linha opera no modo circular (sem um terminal secundário)</summary>    
        [JsonProperty(PropertyName = "lc")]
        public bool Circular { get; set; }
        /// <summary>Informa a primeira parte do letreiro numérico da linha</summary>  
        [JsonProperty(PropertyName = "lt")]
        public string LetreiroLinha { get; set; }
        /// <summary>Informa a segunda parte do letreiro numérico da linha, que indica se a linha opera nos modos: Base(10), Atendimento(21,23,32,41)</summary> 
        [JsonProperty(PropertyName = "sl")]
        public int SentidoLinha { get; set; }
        /// <summary>Informa o sentido ao qual a linha atende, onde 1 significa Terminal Principal para Terminal Secundário e 2 para Terminal Secundário para Terminal Principal</summary> 
        [JsonProperty(PropertyName = "tl")]
        public int ModoLinha { get; set; }
        /// <summary>Informa o letreiro descritivo da linha no sentido Terminal Principal para Terminal Secundário</summary> 
        [JsonProperty(PropertyName = "tp")]
        public string DescritivoPrincipalSecundário { get; set; }
        /// <summary>Informa o letreiro descritivo da linha no sentido Terminal Secundário para Terminal Principal</summary> 
        [JsonProperty(PropertyName = "ts")]
        public string DescritivoSecundarioPrincipal { get; set; }

    }

}