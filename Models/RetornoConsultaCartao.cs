using System;
using System.Collections.Generic;

namespace sptrans_api.Models
{
    public class RetornoConsultaCartao
    {
        public List<TipoCredito> TipoCredito;

        public int Validador;

    }
    public class TipoCredito
    {
        public int Codigo; // 1 - Credito Comun 2 - Credito Estudante;
        public string Descricao;
        public List<ListaProdutos> ListaProdutos;
    }

    public class ListaProdutos
    {
        public int CodigoProduto; 
        public string DescricaoProduto;
        public double ValorProduto;
        public int QuantidadeMinimaVenda;
        public int QuantidadeMaximaVenda;

    }

}