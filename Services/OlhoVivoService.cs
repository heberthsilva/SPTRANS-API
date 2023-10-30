using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Configuration;
using sptrans_api.Models;
using RestSharp;
using System.Collections.Generic;
using System;
using System.Linq;

namespace sptrans_api.Services
{

    public class OlhoVivoService
    {
        static RestClient restclient = new RestClient(ConfigurationManager.AppSettings["OlhoVivoBaseUrl"]);

        #region "Autenticação API"  
        /// <summary>
        /// Para utilizar os EndPoints da API e nescessario realizar a autenticação a cada 1m, o sistema retorna "True"(altenticação realizada com sucesso) e um Cache com o Token de acesso.
        /// </summary>
        
        public async Task<RestResponse> Autenticar()
        {
            try
            {
                var request = new RestRequest("Login/Autenticar?token=" + ConfigurationManager.AppSettings["OlhoVivoToken"]);
                RestResponse Loginresponse = restclient.Post(request);

                return Loginresponse;
            } 
            catch( Exception ex)
            {
                Console.WriteLine(MensagemConstants.ErroAutenticacao);
                Console.WriteLine(ex.Message);
                return null;
            } 
        }

        #endregion

        #region "Linhas"  
        public List<LinhasLocalizadas> Linhas()
        {
            try
            {

                RestResponse autorizar = Autenticar().Result;

                var request = new RestRequest("Posicao");
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var LinhasResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<PosicaoLinhaOutput>(LinhasResponse.Content);

                return temp.LinhasLocalizadas;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Linhas :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public List<BuscarLinhaOutput> BuscarLinha(string busca)
        {
            try
            {

                RestResponse autorizar = Autenticar().Result;
                List<BuscarLinhaOutput> retorno = new List<BuscarLinhaOutput>();                

                    var request = new RestRequest("Linha/Buscar?termosBusca=" + busca);
                    request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                    var buscarLinhaResponse = restclient.Get(request);
                    var temp = JsonConvert.DeserializeObject<List<BuscarLinhaOutput>>(buscarLinhaResponse.Content);
                    if (temp != null)
                    {
                        foreach (var item in temp)
                        {
                            retorno.Add(item);
                        }
                    }
                    return retorno;

            }
            catch (Exception ex)
            {                
                Console.WriteLine("Erro ao Buscar Linha :");
                Console.WriteLine(ex.Message);
                return null;
            }
                     
        }


        public List<BuscarLinhaOutput> BuscarLinhaSentido(BuscarLinhaSentidoInput buscaLinhaSentido)
        {
            RestResponse autorizar = Autenticar().Result;
            List<BuscarLinhaOutput> retorno = new List<BuscarLinhaOutput>();
           try { 

                var request = new RestRequest("Linha/BuscarLinhaSentido?termosBusca=" + buscaLinhaSentido.LinhaBusca+"&sentido="+buscaLinhaSentido.Sentido);
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var buscarLinhaResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<List<BuscarLinhaOutput>>(buscarLinhaResponse.Content);
                if (temp != null)
                {
                    foreach (var item in temp)
                    {
                        retorno.Add(item);
                    }
                }
                return retorno;

        }
            catch (Exception ex)
            {                
                Console.WriteLine("Erro ao Buscar Linha pelo Sentido :");
                Console.WriteLine(ex.Message);
                return null;
            }

}

        #endregion

        #region "Paradas" 
        public List<BuscarParadaOutput> Paradas()
        {
            try
            {

                RestResponse autorizar = Autenticar().Result;
                List<BuscarParadaOutput> retorno = new List<BuscarParadaOutput>();

                var corredores = Corredores();
                foreach (var item in corredores)
                {
                    var paradas = BuscarParadaCorredor(item.CodigoCorredor);
                    retorno.AddRange(paradas);
                }

                return retorno;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Parada :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public List<BuscarParadaOutput> BuscarParada(string busca)
        {
            try
            {

                RestResponse autorizar = Autenticar().Result;
                List<BuscarParadaOutput> retorno = new List<BuscarParadaOutput>();

                var request = new RestRequest("/Parada/Buscar?termosBusca=" + busca);
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var buscarParadaResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<List<BuscarParadaOutput>>(buscarParadaResponse.Content);
                if (temp != null)
                {
                    foreach (var item in temp)
                    {
                        retorno.Add(item);
                    }
                }

                return retorno;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Parada :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public List<BuscarParadaOutput> BuscarParadaLinha(string busca)
        {
            try
            {
                RestResponse autorizar = Autenticar().Result;
                List<BuscarParadaOutput> retorno = new List<BuscarParadaOutput>();

                var request = new RestRequest("/Parada/BuscarParadasPorLinha?codigoLinha=" + busca);
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var buscarParadaResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<List<BuscarParadaOutput>>(buscarParadaResponse.Content);
                if (temp != null)
                {
                    foreach (var item in temp)
                    {
                        retorno.Add(item);
                    }
                }

                return retorno;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Parada :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public List<BuscarParadaOutput> BuscarParadaCorredor(int idCorredor)
        {
            try
            {
                RestResponse autorizar = Autenticar().Result;
                List<BuscarParadaOutput> retorno = new List<BuscarParadaOutput>();

                var request = new RestRequest("/Parada/BuscarParadasPorCorredor?codigoCorredor=" + idCorredor);
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var buscarParadaResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<List<BuscarParadaOutput>>(buscarParadaResponse.Content);
                if (temp != null)
                {
                    foreach (var item in temp)
                    {
                        retorno.Add(item);
                    }
                }

                return retorno;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Parada :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        #endregion

        #region "Corredores" 
        public List<CorredoresOutput> Corredores()
        {
            try
            {

                RestResponse autorizar = Autenticar().Result;
                List<CorredoresOutput> retorno = new List<CorredoresOutput>();

                var request = new RestRequest("Corredor");
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var corredorResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<List<CorredoresOutput>>(corredorResponse.Content);
                if (temp != null)
                {
                    foreach (var item in temp)
                    {
                        retorno.Add(item);
                    }
                }

                return retorno;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Corredores :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        #endregion

        #region "Empresas" 
        public EmpresasOutput Empresas()
        {
            try
            {

                RestResponse autorizar = Autenticar().Result;
                List<EmpresasOutput> retorno = new List<EmpresasOutput>();

                var request = new RestRequest("Empresa");
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var corredorResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<EmpresasOutput>(corredorResponse.Content);


                return temp;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Empresa :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        #endregion

         #region "Posição Veiculos" 
        public PosicaoLinhaOutput Posicao()
        {
            try
            {

                RestResponse autorizar = Autenticar().Result;

                var request = new RestRequest("Posicao");
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var corredorResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<PosicaoLinhaOutput>(corredorResponse.Content);

                return temp;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Posição dos Veiculos :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public PosicaoPorLinhaOutput PosicaoPorLinha(int idLinha)
        {
            try
            {
                RestResponse autorizar = Autenticar().Result;

                var request = new RestRequest("Posicao/Linha?codigoLinha=" + idLinha);
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var posicaoPorLinhaResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<PosicaoPorLinhaOutput>(posicaoPorLinhaResponse.Content);

                return temp;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Posição por Linha :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public VeiculosGaragemOutput VeiculosGaragem(int idCodigoEmpresa, int? idCodigoLinha=0)
        {
            try
            {
                RestResponse autorizar = Autenticar().Result;
                var url = "/Posicao/Garagem?codigoEmpresa=" + idCodigoEmpresa;
                if (idCodigoLinha!=0)
                { url = url + "&codigoLinha=" + idCodigoLinha; }                    
                var request = new RestRequest(url);
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var veiculosGaragemResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<VeiculosGaragemOutput>(veiculosGaragemResponse.Content);


                return temp;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Veiculos na Garagem :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        #endregion

        #region "Previsão de Chegada" 

        public PrevisaoChegadaParadaLinhaOutput PrevisaoChegadaParadaLinha(int idCodigoParada, int idCodigoLinha)
        {
            try
            {
                RestResponse autorizar = Autenticar().Result;
                var url = "Previsao?codigoParada="+ idCodigoParada + "&codigoLinha=" + idCodigoLinha;
                var request = new RestRequest(url);
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var previsaoChegadaResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<PrevisaoChegadaParadaLinhaOutput>(previsaoChegadaResponse.Content);
                return temp;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Previsão de chegada :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public PrevisaoChegadaLinhaOutput PrevisaoChegadaLinha(int idCodigoLinha)
        {
            try
            {
                RestResponse autorizar = Autenticar().Result;
                var url = "Previsao/Linha?codigoLinha=" + idCodigoLinha;
                var request = new RestRequest(url);
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var previsaoChegadaResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<PrevisaoChegadaLinhaOutput>(previsaoChegadaResponse.Content);

                return temp;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Previsão de chegada :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public PrevisaoChegadaParadaOutput PrevisaoChegadaParada(int idCodigoParada)
        {
            try
            {
                RestResponse autorizar = Autenticar().Result;
                var url = "Previsao/Parada?codigoParada=" + idCodigoParada;
                var request = new RestRequest(url);
                request.AddHeader("Set-Cookie", autorizar.Cookies[0].Name + "=" + autorizar.Cookies[0].Value);
                var previsaoChegadaResponse = restclient.Get(request);
                var temp = JsonConvert.DeserializeObject<PrevisaoChegadaParadaOutput>(previsaoChegadaResponse.Content);

                return temp;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao Buscar Previsão de chegada :");
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        #endregion
                
    }
}
