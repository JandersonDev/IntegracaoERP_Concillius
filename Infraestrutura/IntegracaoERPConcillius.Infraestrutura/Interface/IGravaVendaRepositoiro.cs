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
        string Gravar(VendasPdvDTO venda, int idHistoricoAtualizacao, string nomeBanco, string layout);
        int GerarIdHistoricoAtualizacao(int hitorico, string dataVenda, string nomeDbCompleto);
    }
}
