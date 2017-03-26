using IntegracaoERPConcillius.Dominio.Acesso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoERPConcillius.Infraestrutura.Interface
{
    public interface IAcessoRepositorio
    {
       List<int> LocalizaNumeroHistorico();

        Acesso RetornaAcesso(string Cnpj);
    }
}
