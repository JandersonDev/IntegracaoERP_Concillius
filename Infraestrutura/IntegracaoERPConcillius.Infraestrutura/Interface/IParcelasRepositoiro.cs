using IntegracaoERPConcillius.Dominio.GravaVenda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegracaoERPConcillius.Dominio.Acesso;
using IntegracaoERPConcillius.Dominio.Parcelas;

namespace IntegracaoERPConcillius.Infraestrutura.Interface
{
    public interface IParcelasRepositoiro
    {
        int Verificar(string data, string nomeDbCompleto);
        string Gravar(ParcelasPdvDTO parcela, int idHistoricoAtualizacao, string nomeBanco, string layout);
        int GerarIdHistoricoAtualizacao(int hitorico, string data, string nomeDbCompleto);
        List<ParcelasPdvDTO> spParcelasPdv(string data, Acesso acesso);
    }
}
