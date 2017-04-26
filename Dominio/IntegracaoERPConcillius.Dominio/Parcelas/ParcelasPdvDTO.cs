using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoERPConcillius.Dominio.Parcelas
{
    public class ParcelasPdvDTO
    {
        public int ID_EMPRESA { get; set; }
        public string ID_ADMINISTRADORA { get; set; }
        public string ID_FILIAL { get; set; }
        public DateTime DATA_VENDA { get; set; }
        public DateTime DATA_CREDITO { get; set; }
        public Decimal VALOR_BRUTO { get; set; }
        public Decimal VALOR_LIQUIDO { get; set; }
        public string PARCELA { get; set; }
        public string PLANO { get; set; }
        public string FLAG_GERENCIAL { get; set; }
        public string AUTORIZACAO { get; set; }
        public string CONTROLE { get; set; }
        public string E1_VENCREA { get; set; }
        public double VL_PARCELA { get; set; }
        public string TITULO { get; set; }
    }
}
