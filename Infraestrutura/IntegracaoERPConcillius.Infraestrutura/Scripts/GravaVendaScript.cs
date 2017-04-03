using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoERPConcillius.Infraestrutura.Scripts
{
    public static class GravaVendaScript
    {
        public static string Verificar()
        {
            var sql = new StringBuilder();

            sql.AppendLine("DECLARE @STRSQL VARCHAR (MAX)                   ")
               .AppendLine("DECLARE @DATABASENAMEPARAMETER VARCHAR (100) =  @NOME_BD_COMPLETO                 ")
               .AppendLine("SET @STRSQL = ' SET DATEFORMAT DMY SELECT  COUNT(*) FROM ' +  @DATABASENAMEPARAMETER + '.DBO.VENDAS_PDV' + ' WHERE DATA_VENDA = ' + @DATAVENDA")
               .AppendLine("EXEC(@STRSQL)");     
               
            
            return sql.ToString();
        }

        public static string spVendasPdv()
        {
            var sql = new StringBuilder();

            sql.AppendLine("EXEC BABY_CENTER..sp_ws_Vendas_PDV @DATA");

            return sql.ToString();
        }

        public static string Movimentacao()
        {
            var sql = new StringBuilder();

            sql.AppendLine(" --BEGIN TRAN																																")
               .AppendLine(" Se @HISTORICO <> 0																														")
               .AppendLine("    DELETE Conciliacao_Cartao_BC..VENDAS_PDV            WHERE ID_HISTORICO_ATUALIZACAO = @HISTORICO										")
               .AppendLine("    DELETE Conciliacao_Cartao_BC..HISTORICO_ATUALIZACAO WHERE ID_HISTORICO_ATUALIZACAO = @HISTORICO										")
               .AppendLine(" ENd-if																																	")
               .AppendLine(" 																																			")
               .AppendLine(" DECLARE @ADMINISTRADORA INT = 1     -- fixo																								")
               .AppendLine(" DECLARE @EMPRESA	  INT = 1     -- fixo																									")
               .AppendLine(" DECLARE @ARQUIVO        VARCHAR(30) -- = 'PDV_19022017.XLSX'																				")
               .AppendLine(" DECLARE @USUARIO	  INT = 1     -- fixo																									")
               .AppendLine(" DECLARE @DATA		  VARCHAR(10) = '19/02/2017'																						")
               .AppendLine(" 																																			")
               .AppendLine(" DECLARE @IDENTITY       INT 																												")
               .AppendLine(" SELECT @ARQUIVO =  'PDV_'+ SUBSTRING(@DATA,1,2) + SUBSTRING(@DATA,4,2) + SUBSTRING(@DATA,7,4) + '.XLSX'									")
               .AppendLine(" 																																			")
               .AppendLine(" INSERT INTO Conciliacao_Cartao_BC..HISTORICO_ATUALIZACAO     																			")
               .AppendLine(" (ID_ADMINISTRADORA,ID_EMPRESA,DATA_MOVIMENTO,NOME_ARQUIVO, USUARIO_INCLUSAO,DATA_INCLUSAO, CT_LOCK,TIPO_ATUALIZACAO, VERSAO_APLICACAO )  ")
               .AppendLine(" VALUES  (@ADMINISTRADORA, @EMPRESA, @DATA, @ARQUIVO, @USUARIO, GETDATE(), 0, 'O', 'WEB')  												")
               .AppendLine(" set @IDENTITY = @@IDENTITY																												")
               .AppendLine(" 																																			")
               .AppendLine(" set dateformat dmy																														")
               .AppendLine(" 																																			")
               .AppendLine(" INSERT INTO Conciliacao_Cartao_BC..VENDAS_PDV    																						")
               .AppendLine(" 	(ID_EMPRESA, ID_ADMINISTRADORA, ID_FILIAL, DATA_VENDA,																					")
               .AppendLine(" 	TIPO_AUTENTICACAO, QUANTIDADE_PARCELAS, QUANTIDADE_CUPONS, VALOR_VENDA,																	")
               .AppendLine(" 	ID_HISTORICO_ATUALIZACAO, ID_ADMINISTRADORA_ANTERIOR, ECF, DATA_VENDA_ANTERIOR,															")
               .AppendLine(" 	USUARIO_INCLUSAO, DATA_INCLUSAO, USUARIO_ALTERACAO, DATA_ALTERACAO, CT_LOCK)															")
               .AppendLine(" SELECT  1, cd_mve, Cod_loja, datasessaocaixa, CASE WHEN NSU <> 0 THEN 'A' ELSE 'M' END AS TIPO_AUTENTICACAO, 							")
               .AppendLine("	NUMPARCELAS, 1, VALOR, @IDENTITY, NULL, NSU AS ECF, NULL, 1, GETDATE(), 1, GETDATE(), 0												");
            
            return sql.ToString();
        }
    }
}
