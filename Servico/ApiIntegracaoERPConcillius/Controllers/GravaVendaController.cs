﻿using IntegracaoERPConcillius.Dominio.GravaVenda;
using IntegracaoERPConcillius.Infraestrutura.Interface;
using IntegracaoERPConcillius.Infraestrutura.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using IntegracaoERPConcillius.Dominio.Acesso;
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
        [HttpPost]
        public IHttpActionResult spVendasPdv(string dataVenda, Acesso acesso)
        { 
            List<VendasPdvDTO> retorno = repositorio.spVendasPdv(dataVenda, acesso);

            return Ok(retorno);
        }
        [HttpPost]
        public IHttpActionResult Gravar(int historico, List<VendasPdvDTO> vendas, string dataVenda, string nomeDbCompleto, string layout)
        {
            int idHistoricoAtualizacao = repositorio.GerarIdHistoricoAtualizacao(historico, dataVenda, nomeDbCompleto);

            foreach (var venda in vendas)
            {
                string retorno = repositorio.Gravar(venda, idHistoricoAtualizacao, nomeDbCompleto, layout);

                if (retorno != string.Empty)
                {
                    return Ok(retorno);
                }
            }

            return Ok(idHistoricoAtualizacao.ToString());
        }
    }
}