using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoERPConcillius.Dominio.Acesso
{
    public class Acesso
    {
        public string ServidorCliente { get; set; }
        public string NomeDbCliente { get; set; }
        public string UsuarioCliente { get; set; }
        public string SenhaCliente { get; set; }
        public string TabelaCliente { get; set; }
        public string NomeDbCompleto { get; set; }
    }
}
