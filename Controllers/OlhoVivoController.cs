using Newtonsoft.Json;
using RestSharp;
using sptrans_api.Models;
using sptrans_api.Retorno;
using sptrans_api.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace sptrans_api.Controllers
{
    
    public class OlhoVivoController : ApiController
    {
        /// <summary>
        /// Realiza uma busca das linhas do sistema com base no parâmetro informado. Se a linha não é encontrada então é realizada uma busca fonetizada na denominação das linhas( Ex. "Lap" retornará "Lapa").
        /// </summary>
        /// <param name="busca"> 
        /// Informar nome da linha e/ou numero e/ou destino.
        /// </param>
        // <response code="200"> 
        // Retornos:
        // cl =  Código identificador da linha. Este é um código identificador único de cada linha do sistema (por sentido de operação).
        // lc = Indica se uma linha opera no modo circular (sem um terminal secundário) - boleano
        // lt = Informa a primeira parte do letreiro numérico da linha.
        // tl = Informa a segunda parte do letreiro numérico da linha, que indica se a linha opera nos modos: base - 10 , atendimento 21,23,32,41 
        // sl = Informa o sentido ao qual a linha atende, onde 1 significa Terminal Principal para Terminal Secundário e 2 para Terminal Secundário para Terminal Principal
        // tp = Informa o letreiro descritivo da linha no sentido Terminal Principal para Terminal Secundário
        // ts = Informa o letreiro descritivo da linha no sentido Terminal Secundário para Terminal Principal</response>

        [HttpGet]
        [Route("Linhas")]
        public List<LinhasLocalizadas> Linhas()
        {
            return new OlhoVivoService().Linhas();
        }

        [HttpGet]
        [Route("Linha/Buscar")]
        public List<BuscarLinhaOutput> BuscarLinha(string busca)
        {
            return new OlhoVivoService().BuscarLinha(busca);
        }

        [HttpPost]
        [Route("Linha/BuscarLinhaSentido")]
        public List<BuscarLinhaOutput> BuscarLinhaSentido(BuscarLinhaSentidoInput buscarLinhaSentido)
        {
            return new OlhoVivoService().BuscarLinhaSentido(buscarLinhaSentido);
        }
        [HttpGet]
        [Route("Paradas")]
        public List<BuscarParadaOutput> Paradas()
        {
            return new OlhoVivoService().Paradas();
        }
        [HttpGet]
        [Route("Parada/Buscar/")]
        public List<BuscarParadaOutput> BuscarParada(string busca)
        {
            return new OlhoVivoService().BuscarParada(busca);
        }

        [HttpGet]
        [Route("Parada/BuscarParadaLinha/")]
        public List<BuscarParadaOutput> BuscarParadaLinha(string busca)
        {
            return new OlhoVivoService().BuscarParadaLinha(busca);
        }

        [HttpGet]
        [Route("Parada/BuscarParadaCorredor/")]
        public List<BuscarParadaOutput> BuscarParadaCorredor(int idCorredor)
        {
            return new OlhoVivoService().BuscarParadaCorredor(idCorredor);
        }

        [HttpGet]
        [Route("Corredores")]
        public List<CorredoresOutput> Corredores()
        {
            return new OlhoVivoService().Corredores();
        }

        [HttpGet]
        [Route("Empresas")]
        public EmpresasOutput Empresas()
        {
            return new OlhoVivoService().Empresas();
        }

        [HttpGet]
        [Route("PosicaoLinhas")]
        public PosicaoLinhaOutput PosicaoLinhas()
        {
            return new OlhoVivoService().Posicao();
        }
        [HttpGet]
        [Route("PosicaoPorLinha/")]
        public PosicaoPorLinhaOutput PosicaoPorLinha(int idLinha)
        {
            return new OlhoVivoService().PosicaoPorLinha(idLinha);
        }

        [HttpGet]
        [Route("VeiculosGaragem/")]
        public VeiculosGaragemOutput VeiculosGaragem(int idCodigoEmpresa, int? idCodigoLinha = 0)
        {
            return new OlhoVivoService().VeiculosGaragem(idCodigoEmpresa, idCodigoLinha);
        }

        [HttpGet]
        [Route("PrevisaoChegadaParadaLinha/")]
        public PrevisaoChegadaParadaLinhaOutput PrevisaoChegadaParadaLinha(int idCodigoParada, int idCodigoLinha)
        {
            return new OlhoVivoService().PrevisaoChegadaParadaLinha(idCodigoParada,idCodigoLinha);
        }

        [HttpGet]
        [Route("PrevisaoChegadaLinha/")]
        public PrevisaoChegadaLinhaOutput PrevisaoChegadaLinha(int idCodigoLinha)
        {
            return new OlhoVivoService().PrevisaoChegadaLinha(idCodigoLinha);
        }

        [HttpGet]
        [Route("PrevisaoChegadaParada/")]
        public PrevisaoChegadaParadaOutput PrevisaoChegadaParada(int idCodigoParada)
        {
            return new OlhoVivoService().PrevisaoChegadaParada(idCodigoParada);
        }
    }
}
