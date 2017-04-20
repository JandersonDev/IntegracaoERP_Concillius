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
        int Verificar(string dataVenda, string nomeDbCompleto);
        List<VendasPdvDTO> spVendasPdv(string dataVenda, string nomeBanco);
        int Gravar(int hitorico, VendasPdvDTO venda);
        int GerarIdHistoricoAtualizacao(int hitorico, string dataVenda, string nomeDbCompleto);
    }
}
