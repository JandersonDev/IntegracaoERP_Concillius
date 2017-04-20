using IntegracaoERPConcillius.Dominio.GravaVenda;
using IntegracaoERPConcillius.Infraestrutura.Interface;
using IntegracaoERPConcillius.Infraestrutura.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;
using System.Net.Http;
//using System.Web.Mvc;

namespace ApiIntegracaoERPConcillius.Controllers
{
    public class GravaVendaController : ApiController
    {
        // GET: GravaVenda

        private IGravaVendaRepositoiro repositorio;

        public GravaVendaController()
        {
            repositorio = new GravaVendaRepositoiro();
        }
        [HttpGet]
        public IHttpActionResult Verificar(string dataVenda, string nomeDbCompleto)
        {
            int retorno = repositorio.Verificar(dataVenda, nomeDbCompleto);
            return Ok(retorno);
        }
        [HttpGet]
        public IHttpActionResult spVendasPdv(string dataVenda, string nomeDbCompleto)
        {
            List<VendasPdvDTO> retorno = repositorio.spVendasPdv(dataVenda, nomeDbCompleto);

            return Ok(retorno);

        }
        [HttpPost]
        public IHttpActionResult Gravar(int historico, List<VendasPdvDTO> vendas, string dataVenda, string nomeDbCompleto)
        {
            int idHistoricoAtualizacao = repositorio.GerarIdHistoricoAtualizacao(historico, dataVenda, nomeDbCompleto);

            foreach (var venda in vendas)
            {
                int retorno = repositorio.Gravar(historico, venda);
            }

            return Ok(0);
        }
    }
}