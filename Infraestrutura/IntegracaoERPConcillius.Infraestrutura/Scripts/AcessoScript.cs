using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoERPConcillius.Infraestrutura.Scripts
{
    public static class AcessoScript
    {
        public static string RetornaAcesso()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT SERVIDOR_CLIENTE ServidorCliente  ")
               .AppendLine("      , NOME_BD_CLIENTE NomeDbCliente    ")
               .AppendLine("      , USUARIO_CLIENTE UsuarioCliente   ")
               .AppendLine("      , SENHA_CLIENTE SenhaCliente       ")
               .AppendLine("      , TABELA_CLIENTE TabelaCliente     ")
               .AppendLine("      , NOME_BD_COMPLETO NomeDbCompleto  ")
               .AppendLine("FROM CONCILLIUS..EMPRESA (NOLOCK)        ")
               .AppendLine("WHERE CNPJ = @CNPJ                       ");

            return sql.ToString();
        }

    }
}
