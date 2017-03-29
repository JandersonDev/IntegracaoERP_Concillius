using IntegracaoERPConcillius.Dominio.GravaVenda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegracaoERPConcillius.Infraestrutura.Interface
{
    public interface IGravaVendaRepositoiro
    {
        List<int> Verificar(string dataVenda);
        List<VendasPdvDTO> spVendasPdv(string dataVenda);
        void Movimentacao(string hitorico);
    }
}
