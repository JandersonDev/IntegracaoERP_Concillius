﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoERPConcillius.Infraestrutura.Scripts
{
    public static class ParcelasScript
    {
        public static string Verificar()
        {
            var sql = new StringBuilder();

            //sql.AppendLine("DECLARE @STRSQL VARCHAR (MAX)                   ")
            //   .AppendLine("DECLARE @DATABASENAMEPARAMETER VARCHAR (100) =  @NOME_BD_COMPLETO                 ")
            //   .AppendLine("SET @STRSQL = ' SET DATEFORMAT DMY SELECT  COUNT(*) FROM ' +  @DATABASENAMEPARAMETER + '.DBO.VENDAS_PDV' + ' WHERE DATA_VENDA = ' + @DATAVENDA")
            //   .AppendLine("EXEC(@STRSQL)");     

            sql.AppendLine(" declare @sql varchar(max)  ")
               .AppendLine(" declare @NomeBanco varchar(max)  ")
               .AppendLine(" set @NomeBanco = @NOME_BD_COMPLETO  ")
               .AppendLine(" set @sql = 'set dateformat dmy select id_historico_atualizacao from ' + @NomeBanco + '.dbo.historico_atualizacao where TIPO_ATUALIZACAO = ''Z'' and ID_ADMINISTRADORA = 1 and DATA_MOVIMENTO = ''' + @DATAVENDA + ''''  ")
               .AppendLine(" exec(@sql)  ");
            
            return sql.ToString();
        }

        public static string spVendasPdv()
        {
            var sql = new StringBuilder();

            //sql.AppendLine("EXEC BABY_CENTER..sp_ws_Vendas_PDV @DATA");

            sql.AppendLine(" declare @sql varchar(max) ")
               .AppendLine(" set @sql = 'set dateformat dmy EXEC ' + @BANCO + '.dbo.sp_ws_Vendas_PDV ''' + @DATA + '''' ")
               .AppendLine(" exec(@sql) ");

            return sql.ToString();
        }

        public static string spParcelasPdv()
        {
            var sql = new StringBuilder();

            //sql.AppendLine("EXEC BABY_CENTER..sp_ws_Vendas_PDV @DATA");

            sql.AppendLine(" declare @sql varchar(max) ")
               //.AppendLine(" set @sql = 'set dateformat dmy EXEC ' + @BANCO + '.dbo.sp_ws_Parcelas_PDV ''' + @DATA + '''' ")
               .AppendLine(" set @sql = 'set dateformat dmy EXEC dbo.sp_ws_Parcelas_PDV ''' + @DATA + '''' ")
               .AppendLine(" exec(@sql) ");

            return sql.ToString();
        }


        public static string Movimentacao(string layout)
        {
            var sql = new StringBuilder();

            switch (layout)
            {
                case "1": //EMPRESA XXXX
                    sql.AppendLine(" DECLARE @SQL VARCHAR(MAX) = '' ")
                       .AppendLine(" SET @SQL = ' SET DATEFORMAT DMY	 ")
                       .AppendLine(" INSERT INTO  ' + @NOMEBANCO + '.DBO.VENDAS_PDV ")
                       .AppendLine(" (ID_EMPRESA, ID_ADMINISTRADORA, ID_FILIAL, DATA_VENDA, ")
                       .AppendLine(" TIPO_AUTENTICACAO, QUANTIDADE_PARCELAS, QUANTIDADE_CUPONS, VALOR_VENDA, ")
                       .AppendLine(" ID_HISTORICO_ATUALIZACAO, ID_ADMINISTRADORA_ANTERIOR, ECF, DATA_VENDA_ANTERIOR, ")
                       .AppendLine(" USUARIO_INCLUSAO, DATA_INCLUSAO, USUARIO_ALTERACAO, DATA_ALTERACAO, CT_LOCK) ")
                       .AppendLine(" SELECT  1, ' + CONVERT(VARCHAR(MAX),@CD_MVE) + ', ' + CONVERT(VARCHAR(MAX),@COD_LOJA) + ', ''' + CONVERT(VARCHAR(MAX),@DATAVENDA) + ''', CASE WHEN ' + @NSU + ' <> 0 THEN ''A'' ELSE ''M'' END AS TIPO_AUTENTICACAO, 			")
                       .AppendLine(" ' + CONVERT(VARCHAR(MAX),@NUMPARCELAS) + ', 1, ' + CONVERT(VARCHAR(MAX),@VALOR) + ', ' + CONVERT(VARCHAR(MAX),@IDENTITY) + ', NULL, ' + @NSU + ' AS ECF, NULL, 1, ''' + CONVERT(VARCHAR(MAX),GETDATE()) + ''', 1, ''' + CONVERT(VARCHAR(MAX),GETDATE()) + ''', 0' ")
                       .AppendLine("  EXEC(@SQL) ");
                    break;
                case "2":
                    sql.AppendLine(" DECLARE @SQL VARCHAR(MAX) = '' ")
                       .AppendLine(" SET @SQL = ' SET DATEFORMAT DMY	 ")
                       .AppendLine(" INSERT INTO  ' + @NOMEBANCO + '.DBO.VENDAS_PDV ")
                       .AppendLine(" (ID_EMPRESA, ID_ADMINISTRADORA, ID_FILIAL, DATA_VENDA, ")
                       .AppendLine(" TIPO_AUTENTICACAO, QUANTIDADE_PARCELAS, QUANTIDADE_CUPONS, VALOR_VENDA, ")
                       .AppendLine(" ID_HISTORICO_ATUALIZACAO, ID_ADMINISTRADORA_ANTERIOR, ECF, DATA_VENDA_ANTERIOR, ")
                       .AppendLine(" USUARIO_INCLUSAO, DATA_INCLUSAO, USUARIO_ALTERACAO, DATA_ALTERACAO, CT_LOCK) ")
                       .AppendLine(" SELECT  1, ' + CONVERT(VARCHAR(MAX),@CD_MVE) + ', ' + CONVERT(VARCHAR(MAX),@COD_LOJA) + ', ''' + CONVERT(VARCHAR(MAX),@DATAVENDA) + ''', CASE WHEN ' + @NSU + ' <> 0 THEN ''A'' ELSE ''M'' END AS TIPO_AUTENTICACAO, 			")
                       .AppendLine(" ' + CONVERT(VARCHAR(MAX),@NUMPARCELAS) + ', 1, ' + CONVERT(VARCHAR(MAX),@VALOR) + ', ' + CONVERT(VARCHAR(MAX),@IDENTITY) + ', NULL, ' + @NSU + ' AS ECF, NULL, 1, ''' + CONVERT(VARCHAR(MAX),GETDATE()) + ''', 1, ''' + CONVERT(VARCHAR(MAX),GETDATE()) + ''', 0' ")
                       .AppendLine("  EXEC(@SQL) ");
                    break;
                default:
                    sql.AppendLine(" DECLARE @SQL VARCHAR(MAX) = '' ")
                      .AppendLine(" SET @SQL = ' SET DATEFORMAT DMY	 ")
                      .AppendLine(" INSERT INTO  ' + @NOMEBANCO + '.DBO.VENDAS_PDV ")
                      .AppendLine(" (ID_EMPRESA, ID_ADMINISTRADORA, ID_FILIAL, DATA_VENDA, ")
                      .AppendLine(" TIPO_AUTENTICACAO, QUANTIDADE_PARCELAS, QUANTIDADE_CUPONS, VALOR_VENDA, ")
                      .AppendLine(" ID_HISTORICO_ATUALIZACAO, ID_ADMINISTRADORA_ANTERIOR, ECF, DATA_VENDA_ANTERIOR, ")
                      .AppendLine(" USUARIO_INCLUSAO, DATA_INCLUSAO, USUARIO_ALTERACAO, DATA_ALTERACAO, CT_LOCK) ")
                      .AppendLine(" SELECT  1, ' + CONVERT(VARCHAR(MAX),@CD_MVE) + ', ' + CONVERT(VARCHAR(MAX),@COD_LOJA) + ', ''' + CONVERT(VARCHAR(MAX),@DATAVENDA) + ''', CASE WHEN ' + @NSU + ' <> 0 THEN ''A'' ELSE ''M'' END AS TIPO_AUTENTICACAO, 			")
                      .AppendLine(" ' + CONVERT(VARCHAR(MAX),@NUMPARCELAS) + ', 1, ' + CONVERT(VARCHAR(MAX),@VALOR) + ', ' + CONVERT(VARCHAR(MAX),@IDENTITY) + ', NULL, ' + @NSU + ' AS ECF, NULL, 1, ''' + CONVERT(VARCHAR(MAX),GETDATE()) + ''', 1, ''' + CONVERT(VARCHAR(MAX),GETDATE()) + ''', 0' ")
                      .AppendLine("  EXEC(@SQL) ");
                    break;
            }
            
            return sql.ToString();
        }

        public static string GerarIdHistoricoAtualizacao()
        {
            var sql = new StringBuilder();

            sql.AppendLine(" --begin tran																																				")
               .AppendLine(" 																																							")
               .AppendLine(" DECLARE @ADMINISTRADORA INT = 1     -- FIXO																												")
               .AppendLine(" DECLARE @EMPRESA	      INT = 1     -- FIXO																												")
               .AppendLine(" DECLARE @ARQUIVO        VARCHAR(30) -- = 'PAR_02012017.XLS'																								")
               .AppendLine(" DECLARE @USUARIO	      INT = 1     -- FIXO																												")
               .AppendLine(" DECLARE @DATA		      VARCHAR(10) = @DATAVENDA																										")
               .AppendLine(" DECLARE @IDENTITY       INT 																																")
               .AppendLine(" DECLARE @SQL VARCHAR(MAX) = ''																															")
               .AppendLine(" declare @NomeBanco varchar(max) = @NOME_BD_COMPLETO																									")
               .AppendLine(" DECLARE @HISTORICO INT = @ID_HISTORICO																																")
               .AppendLine(" 																																							")
               .AppendLine(" SET DATEFORMAT DMY																																		")
               .AppendLine(" 																																							")
               .AppendLine(" IF @HISTORICO <> 0																																		")
               .AppendLine(" BEGIN																																						")
               .AppendLine("     SET @SQL = ' DELETE ' + @NomeBanco + '.dbo.vw_MOVIMENTO_CAR2            WHERE ID_HISTORICO_ATUALIZACAO = ' + convert(varchar(max),@HISTORICO) + ' 			")
               .AppendLine(" 	             DELETE ' + @NomeBanco + '.dbo.HISTORICO_ATUALIZACAO WHERE ID_HISTORICO_ATUALIZACAO = ' + convert(varchar(max),@HISTORICO) + ''				")
               .AppendLine("     EXEC(@SQL) 																																			")
               .AppendLine(" END																																						")
               .AppendLine("                 																																			")
               .AppendLine(" SET @ARQUIVO =  'PAR_'+ SUBSTRING(@DATA,1,2) + SUBSTRING(@DATA,4,2) + SUBSTRING(@DATA,7,4) + '.XLSX'														")
               .AppendLine(" 																																							")
               .AppendLine(" SET @SQL = ''																																				")
               .AppendLine(" 																																							")
               .AppendLine(" SET @SQL = ' INSERT INTO ' + @NomeBanco + '.dbo.HISTORICO_ATUALIZACAO     																				")
               .AppendLine("            (ID_ADMINISTRADORA,ID_EMPRESA,DATA_MOVIMENTO,NOME_ARQUIVO, USUARIO_INCLUSAO,DATA_INCLUSAO, CT_LOCK,TIPO_ATUALIZACAO, VERSAO_APLICACAO )        ")
               .AppendLine("             VALUES (' + convert(varchar(max),@ADMINISTRADORA) + ', ' + convert(varchar(max),@EMPRESA) + ', ''' + @DATA + ''', ''' + @ARQUIVO + ''', ' + convert(varchar(max),@USUARIO) + ', ''' + convert(varchar(max),GETDATE()) + ''', ''0'', ''Z'', ''WEB'')' ")
               .AppendLine("																																							")
               .AppendLine(" EXEC(@SQL) 																																				")
               .AppendLine(" 																																							")
               .AppendLine(" SET @IDENTITY = @@IDENTITY																																")
               .AppendLine(" 																																							")
               .AppendLine(" select @IDENTITY																																			")
               .AppendLine(" 																																							")
               .AppendLine(" --rollback																																				");
            
            return sql.ToString();
        }
    }
}
