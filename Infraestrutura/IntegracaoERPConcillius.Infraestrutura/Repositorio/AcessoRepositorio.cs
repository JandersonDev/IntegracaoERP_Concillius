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
        
        public Acesso RetornaAcesso(string Cnpj)
        {
            try
            {
                Acesso acesso;

                var query = AcessoScript.RetornaAcesso();

                var parametros = new { CNPJ = Cnpj };

                using (var conexao = new SqlConnection(this.stringConexao))
                {
                    acesso = conexao.Query<Acesso>(query, parametros).FirstOrDefault();
                }

                return acesso;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
          
        }
    }
}


