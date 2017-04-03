using IntegracaoERPConcillius.Infraestrutura.Interface;
using IntegracaoERPConcillius.Infraestrutura.Repositorio;
using IntegracaoERPConcillius.Dominio.Acesso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Net.Http;


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
        public IHttpActionResult RetornaAcesso(string cnpj)
        {
            Acesso retorno = repositorio.RetornaAcesso(cnpj);
            return Ok(retorno);
        }
    }
}