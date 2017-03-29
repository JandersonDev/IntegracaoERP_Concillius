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
        Acesso RetornaAcesso(string Cnpj);
    }
}
