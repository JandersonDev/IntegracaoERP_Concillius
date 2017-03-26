using IntegracaoERPConcillius.Infraestrutura.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using IntegracaoERPConcillius.Infraestrutura.Repositorio;
using IntegracaoERPConcillius.Dominio.Acesso;

namespace ApiIntegracaoERPConcillius.Controllers
{
    public class AcessoController : ApiController
    {
        // GET: Acesso

        private IAcessoRepositorio repositorio;

        //AcessoRepositorio repositorio = new AcessoRepositorio();

        public AcessoController()
        {
            repositorio = new AcessoRepositorio();
        }
        
        [HttpGet]
        public IHttpActionResult LocalizaNumeroHistorico()
        {
            List<int> retorno = repositorio.LocalizaNumeroHistorico();
            return Ok(retorno);
        }
        
        [HttpGet]
        public IHttpActionResult RetornaAcesso(string Cnpj)
        {
            Acesso retorno = repositorio.RetornaAcesso(Cnpj);
            return Ok(retorno);
        }
    }
}