using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using IntegracaoERPConcillius.Infraestrutura.Interface;
using IntegracaoERPConcillius.Infraestrutura.Scripts;
using IntegracaoERPConcillius.Dominio.Acesso;

namespace IntegracaoERPConcillius.Infraestrutura.Repositorio
{
    public class AcessoRepositorio : IAcessoRepositorio
    {
        public string stringConexao { get; set; }

        public AcessoRepositorio()
        {
            this.stringConexao = ConfigurationManager.ConnectionStrings["IntegracaoContext"].ConnectionString;
        }
        
        public List<int> LocalizaNumeroHistorico()
        {
            var idHistorico = new List<int>();

            var query = AcessoScript.LocalizaNumeroHistoricoData();

            var parametros = new {DataMovimento = "19/02/2017" };

            using (var conexao = new SqlConnection(this.stringConexao))
            {
                idHistorico = conexao.Query<int>(query, parametros).ToList();
            }

            return idHistorico;
        }

        public Acesso RetornaAcesso(string Cnpj)
        {
            var acesso = new Acesso();

            var query = AcessoScript.RetornaAcesso();

            var parametros = new { CNPJ = Cnpj };

            using (var conexao = new SqlConnection(this.stringConexao))
            {
                acesso = conexao.Query<Acesso>(query, parametros).FirstOrDefault();
            }

            return acesso;
        }
    }
}


