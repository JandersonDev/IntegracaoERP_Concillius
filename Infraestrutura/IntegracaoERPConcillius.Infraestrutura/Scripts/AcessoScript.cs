using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoERPConcillius.Infraestrutura.Scripts
{
    public static class AcessoScript
    {
        public static string LocalizaNumeroHistoricoData()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SET DATEFORMAT DMY                   ")
               .AppendLine("SELECT ID_HISTORICO_ATUALIZACAO     ")
               .AppendLine("FROM Conciliacao_Cartao_BC..HISTORICO_ATUALIZACAO           ")
               .AppendLine("WHERE DATA_MOVIMENTO = @DataMovimento");
            
            return sql.ToString();
        }

        public static string RetornaAcesso()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT SERVIDOR_CLIENTE          ")
               .AppendLine("      ,NOME_BD_CLIENTE           ")
               .AppendLine("      , USUARIO_CLIENTE          ")
               .AppendLine("      , SENHA_CLIENTE            ")
               .AppendLine("      , TABELA_CLIENTE           ")
               .AppendLine("      , NOME_BD_COMPLETO         ")
               .AppendLine("FROM CONCILLIUS..EMPRESA (NOLOCK)")
               .AppendLine("WHERE CNPJ = @CNPJ               ");

            return sql.ToString();
        }

    }
}
