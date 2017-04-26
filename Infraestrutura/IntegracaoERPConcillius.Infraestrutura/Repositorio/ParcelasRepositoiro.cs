using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using IntegracaoERPConcillius.Infraestrutura.Scripts;
using IntegracaoERPConcillius.Infraestrutura.Interface;
using IntegracaoERPConcillius.Dominio.GravaVenda;
using IntegracaoERPConcillius.Dominio.Parcelas;
using IntegracaoERPConcillius.Dominio.Acesso;

namespace IntegracaoERPConcillius.Infraestrutura.Repositorio
{
    public class ParcelasRepositoiro : IParcelasRepositoiro
    {
        public string stringConexao { get; set; }
        public string stringConexaoCliente { get; set; }
        public ParcelasRepositoiro()
        {
            this.stringConexao = ConfigurationManager.ConnectionStrings["IntegracaoContext"].ConnectionString;
            this.stringConexaoCliente = ConfigurationManager.ConnectionStrings["ClienteContext"].ConnectionString;
        }

        public int Verificar(string data, string nomeDbCompleto)
        {
            var idHistorico = 0;

            var query = ParcelasScript.Verificar();

            var parametros = new { DATAVENDA = data, NOME_BD_COMPLETO = nomeDbCompleto };

            using (var conexao = new SqlConnection(this.stringConexao))
            {
                idHistorico = conexao.Query<int>(query, parametros).FirstOrDefault();
            }

            return idHistorico;
        }
        
        public List<ParcelasPdvDTO> spParcelasPdv(string data, Acesso acesso)
        {
            List<ParcelasPdvDTO> vendas = new List<ParcelasPdvDTO>();

            var query = ParcelasScript.spParcelasPdv();

            var parametros = new { DATA = data}; //, BANCO = acesso.NomeDbCliente 

            using (var conexao = new SqlConnection(string.Format(this.stringConexaoCliente, acesso.NomeDbCliente, acesso.UsuarioCliente, acesso.SenhaCliente, acesso.ServidorCliente)))
            {
                vendas = conexao.Query<ParcelasPdvDTO>(query, parametros).ToList();
            }

            return vendas;
        }

        public string Gravar(VendasPdvDTO venda, int idHistoricoAtualizacao, string nomeBanco, string layout)
        {
            try
            {   
                var query = GravaVendaScript.Movimentacao(layout);

                var parametros = new { NOMEBANCO = nomeBanco, IDENTITY = idHistoricoAtualizacao, DATAVENDA = venda.datasessaocaixa.ToShortDateString(), CD_MVE = venda.CD_MVE, COD_LOJA = venda.Cod_loja,  NSU = venda.NSU, NUMPARCELAS = venda.NUMPARCELAS, VALOR = venda.VALOR };

                using (var conexao = new SqlConnection(this.stringConexao))
                {
                    conexao.Query<string>(query, parametros);
                }

                return string.Empty;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }

        public int GerarIdHistoricoAtualizacao(int hitorico, string dataVenda, string nomeDbCompleto)
        {
            try
            {
                int retorno = 0;

                var query = GravaVendaScript.GerarIdHistoricoAtualizacao();

                var parametros = new { ID_HISTORICO = hitorico, DATAVENDA = dataVenda, NOME_BD_COMPLETO = nomeDbCompleto};

                using (var conexao = new SqlConnection(this.stringConexao))
                {
                    retorno = conexao.Query<int>(query, parametros).FirstOrDefault();
                }

                return retorno;
            }
            catch (SqlException ex)
            {

                throw;
            }

        }
    }
}
