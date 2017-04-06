using IntegracaoERPConcillius.Infraestrutura.Repositorio;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.Windows.Forms;
using ApiIntegracaoERPConcillius.Classes;
using System.Net.Http;
using System.Collections.Generic;
using IntegracaoERPConcillius.Dominio.Acesso;
using IntegracaoERPConcillius.Dominio.GravaVenda;

namespace WfaIntegracaoERPConcillius
{
    public partial class FrmCargaVendas : Form
    {
        private string CNPJ;
        private string UrlBase;
        private Acesso acesso;
        private List<VendasPdvDTO> vendas;

        public FrmCargaVendas()
        {
            InitializeComponent();
            IniciarFormulario();
        }

        private void IniciarFormulario()
        {
            FormatarData();
            this.CNPJ = LerCNPJRegedit();
            UrlBase = ConfigurationManager.AppSettings["UrlServicoBase"];
        }

        private string LerCNPJRegedit()
        {
            try
            {
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                using (var key = hklm.OpenSubKey(ConfigurationManager.AppSettings["CaminhoRegedit"]))
                {
                    return key.GetValue("CNPJ").ToString();
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private void FormatarData()
        {
            dthDataVenda.Format = DateTimePickerFormat.Custom;
            dthDataVenda.CustomFormat = "dd/MM/yyyy";
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
             
            if (!ValidarDataValida())
            {
                MessageBox.Show("Data informada deve ser menor que a data atual!","Atenção",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.CNPJ == string.Empty)
            {
                MessageBox.Show("Erro ao ler chave CNPJ no regedit!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                acesso = ConsultarAcesso();

                if (acesso.NomeDbCliente == null)
                {
                    MessageBox.Show("Banco do cliente não existe!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int historico = Verificar();

                if (historico != 0)
                {
                    if (MessageBox.Show("Carga já efetuada, deseja sobrepor?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Gravar
                        this.vendas = RetornaListaVendasPDV();

                        Gravar(historico);
                        

                        MessageBox.Show("Operação efetuada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        dthDataVenda.Value = DateTime.Now;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Gravar(int historico)
        {
            try
            {
                var param = "GravaVenda/Gravar?historico=" + historico ;
                var resposta = RequisicaoHttp.Post(UrlBase,param, this.vendas);
                var retorno = resposta.Content.ReadAsAsync<int>().Result;
                return;
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        private List<VendasPdvDTO> RetornaListaVendasPDV()
        {
            try
            {
                var data = dthDataVenda.Value.ToShortDateString();
                var param = "GravaVenda/spVendasPdv?dataVenda=" + data;
                var resposta = RequisicaoHttp.Get(UrlBase, param);
                return resposta.Content.ReadAsAsync<List<VendasPdvDTO>>().Result;
            }
            catch (Exception)
            {
                throw new Exception("Erro no método spVendasPdv api grava venda!");
            }
        }

        private int Verificar()
        {
            try
            {
                var data = dthDataVenda.Value.ToShortDateString();
                var param = "GravaVenda/Verificar?dataVenda=" + data + "&nomeDbCompleto=" + this.acesso.NomeDbCompleto;
                var resposta = RequisicaoHttp.Get(UrlBase, param);
                return resposta.Content.ReadAsAsync<int>().Result;
            }
            catch (Exception)
            {
                throw new Exception("Erro no método verficar api grava venda!");
            }
        }

        private Acesso ConsultarAcesso()
        {
            try
            {
                var param = "Acesso/RetornaAcesso?cnpj=" + this.CNPJ;
                var resposta = RequisicaoHttp.Get(UrlBase, param);
                return resposta.Content.ReadAsAsync<Acesso>().Result;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao consultar api acesso!");
            }
            
        }

        private bool ValidarDataValida()
        {
            if (dthDataVenda.Value.Date >= DateTime.Now.Date) return false;

            return true;
        }
    }
}
