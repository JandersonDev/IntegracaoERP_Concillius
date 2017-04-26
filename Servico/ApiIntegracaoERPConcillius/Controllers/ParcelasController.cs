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
using IntegracaoERPConcillius.Dominio.Parcelas;

namespace ApiIntegracaoERPConcillius.Controllers
{
    public class ParcelasController : ApiController
    {
        // GET: Acesso

        private IParcelasRepositoiro repositorio;

        //AcessoRepositorio repositorio = new AcessoRepositorio();

        public ParcelasController()
        {
            repositorio = new ParcelasRepositoiro();
        }
        
        [HttpGet]
        public IHttpActionResult Verificar(string data, string nomeDbCompleto)
        {
            int retorno = repositorio.Verificar(data, nomeDbCompleto);
            return Ok(retorno);
        }

        [HttpPost]
        public IHttpActionResult spParcelasPdv(string data, Acesso acesso)
        {
            List<ParcelasPdvDTO> retorno = repositorio.spParcelasPdv(data, acesso);

            return Ok(retorno);
        }

        [HttpPost]
        public IHttpActionResult Gravar(int historico, List<ParcelasPdvDTO> parcelas, string data, string nomeDbCompleto, string layout)
        {
            int idHistoricoAtualizacao = repositorio.GerarIdHistoricoAtualizacao(historico, data, nomeDbCompleto);

            //foreach (var parcela in parcelas)
            //{
            //    string retorno = repositorio.Gravar(parcela, idHistoricoAtualizacao, nomeDbCompleto, layout);

            //    if (retorno != string.Empty)
            //    {
            //        return Ok(retorno);
            //    }
            //}

            return Ok(idHistoricoAtualizacao.ToString());
        }
    }
}