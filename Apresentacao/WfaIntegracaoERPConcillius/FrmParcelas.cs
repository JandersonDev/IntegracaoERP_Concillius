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
using IntegracaoERPConcillius.Dominio.Parcelas;

namespace WfaIntegracaoERPConcillius
{
    public partial class FrmParcelas : Form
    {
        private string CNPJ;
        private string UrlBase;
        private Acesso acesso;
        private List<ParcelasPdvDTO> parcelas;
        private string Layout;

        public FrmParcelas()
        {
            InitializeComponent();
            IniciarFormulario();
        }

        private void IniciarFormulario()
        {
            FormatarData();
            this.CNPJ = LerCNPJRegedit();
            this.Layout = LerLayoutRegedit();
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

        private string LerLayoutRegedit()
        {
            try
            {
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                using (var key = hklm.OpenSubKey(ConfigurationManager.AppSettings["CaminhoRegedit"]))
                {
                    return key.GetValue("LAYOUT").ToString();
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

            if (this.Layout == string.Empty)
            {
                MessageBox.Show("Erro ao ler chave LAYOUT no regedit!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        this.parcelas = RetornaListaParcelasPDV();
                        Gravar(historico);
                    }
                    else
                    {
                        dthDataVenda.Value = DateTime.Now;
                        return;
                    }
                }
                else
                {
                    this.parcelas = RetornaListaParcelasPDV();

                    if (this.parcelas.Count == 0)
                    {
                        MessageBox.Show("Não existem parcelas nessa data!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Gravar(historico);
                }

                MessageBox.Show("Operação efetuada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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
                var data = dthDataVenda.Value.ToShortDateString();
                var param = "Parcelas/Gravar?historico=" + historico + "&data=" + data + "&nomeDbCompleto=" + this.acesso.NomeDbCompleto + "&layout=" + this.Layout;
                var resposta = RequisicaoHttp.Post(UrlBase, param, this.parcelas);
                var retorno = resposta.Content.ReadAsAsync<int>().Result;
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ParcelasPdvDTO> RetornaListaParcelasPDV()
        {
            try
            {
                var data = dthDataVenda.Value.ToShortDateString();
                var param = "Parcelas/spParcelasPdv?data=" + data;
                var resposta = RequisicaoHttp.Post(UrlBase, param, this.acesso);
                var retorno = resposta.Content.ReadAsAsync<List<ParcelasPdvDTO>>().Result;
                return retorno;
            }
            catch (Exception)
            {
                throw new Exception("Erro no método spParcelasPdv api grava venda!");
            }
        }

        private int Verificar()
        {
            try
            {
                var data = dthDataVenda.Value.ToShortDateString();
                var param = "Parcelas/Verificar?data=" + data + "&nomeDbCompleto=" + this.acesso.NomeDbCompleto;
                var resposta = RequisicaoHttp.Get(UrlBase, param);
                return resposta.Content.ReadAsAsync<int>().Result;
            }
            catch (Exception)
            {
                throw new Exception("Erro no método verficar api parcelas!");
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
