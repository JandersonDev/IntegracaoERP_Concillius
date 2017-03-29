using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoERPConcillius.Dominio.GravaVenda
{
    public class VendasPdvDTO
    {
        public int CD_MVE { get; set; }
        public DateTime datasessaocaixa { get; set; }
        public int Cod_loja { get; set; }
        public int NUMPARCELAS { get; set; }
        public string TIPO_AUTENTICACAO { get; set; }
        public decimal VALOR { get; set; }
        public string TIPO { get; set; }
        public string CARTAO { get; set; }
        public string    NSU { get; set; }
        public int ID_PDV { get; set; }
        public int ECF { get; set; }
        public string DATA8 { get; set; }
        public string HORA { get; set; }
    }
}
