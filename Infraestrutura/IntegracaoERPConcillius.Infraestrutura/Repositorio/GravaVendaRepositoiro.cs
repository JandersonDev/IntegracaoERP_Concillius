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

namespace IntegracaoERPConcillius.Infraestrutura.Repositorio
{
    public class GravaVendaRepositoiro : IGravaVendaRepositoiro
    {
        public string stringConexao { get; set; }
        public GravaVendaRepositoiro()
        {
            this.stringConexao = ConfigurationManager.ConnectionStrings["IntegracaoContext"].ConnectionString;
        }

        public int Verificar(string dataVenda, string nomeDbCompleto)
        {
            var idHistorico = 0;

            var query = GravaVendaScript.Verificar();

            var parametros = new { DATAVENDA = dataVenda, NOME_BD_COMPLETO = nomeDbCompleto };

            using (var conexao = new SqlConnection(this.stringConexao))
            {
                idHistorico = conexao.Query<int>(query, parametros).FirstOrDefault();
            }

            return idHistorico;
        }

        public List<VendasPdvDTO> spVendasPdv(string dataVenda, string nomeBanco)
        {
            List<VendasPdvDTO> vendas = new List<VendasPdvDTO>();

            var query = GravaVendaScript.spVendasPdv();

            var parametros = new { DATA = dataVenda, BANCO = nomeBanco};

            using (var conexao = new SqlConnection(this.stringConexao))
            {
                vendas = conexao.Query<VendasPdvDTO>(query, parametros).ToList();
            }

            return vendas;
        }

        public int Gravar(int hitorico, VendasPdvDTO venda)
        {
            try
            {
                
                var query = GravaVendaScript.Movimentacao();

                var parametros = new { HISTORICO = hitorico, DATAVENDA = venda.datasessaocaixa.ToShortDateString(), CD_MVE = venda.CD_MVE, COD_LOJA = venda.Cod_loja, DATASESSAOCAIXA = venda.datasessaocaixa, NSU = venda.NSU, NUMPARCELAS = venda.NUMPARCELAS, VALOR = venda.VALOR };

                using (var conexao = new SqlConnection(this.stringConexao))
                {
                    conexao.Query<string>(query, parametros);
                }

                return 0;
            }
            catch (SqlException ex)
            {

                throw;
            }
            
        }
    }
}
