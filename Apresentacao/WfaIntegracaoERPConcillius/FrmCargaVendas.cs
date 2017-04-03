using IntegracaoERPConcillius.Infraestrutura.Repositorio;
using Microsoft.Win32;
using System;
using System.Configuration;
using System.Windows.Forms;
using ApiIntegracaoERPConcillius.Classes;
using System.Net.Http;
using System.Collections.Generic;
using IntegracaoERPConcillius.Dominio.Acesso;

namespace WfaIntegracaoERPConcillius
{
    public partial class FrmCargaVendas : Form
    {
        private string CNPJ;
        private string UrlBase;
        private Acesso acesso;
        
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

                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
