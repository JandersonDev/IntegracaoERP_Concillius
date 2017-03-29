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
        public IHttpActionResult Verificar(string dataVenda)
        {
            List<int> retorno = repositorio.Verificar(dataVenda);
            return Ok(retorno);
        }
        [HttpGet]
        public IHttpActionResult spVendasPdv(string dataVenda)
        {
            List<VendasPdvDTO> retorno = repositorio.spVendasPdv(dataVenda);

            return Ok(retorno);

        }
        [HttpGet]
        public IHttpActionResult Movimentacao(string hitorico)
        {
            repositorio.Movimentacao(hitorico);
            return Ok();
        }
    }
}