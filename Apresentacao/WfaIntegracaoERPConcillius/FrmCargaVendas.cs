using IntegracaoERPConcillius.Infraestrutura.Repositorio;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfaIntegracaoERPConcillius
{
    public partial class FrmCargaVendas : Form
    {
        private string CNPJ;
        private string UrlBase;
        
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
            }

            if (this.CNPJ == string.Empty)
            {
                MessageBox.Show("Erro ao ler chave CNPJ no regedit!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //var param = "CentroDistribuicao/ConsultaParametroAplicativoAutomatico?codigoDeposito=" + codigoDeposito;
            //var resposta = Http.Get(UrlBase, param);
            //var result = resposta.Content.ReadAsAsync<string>().Result;
            //return !result.Equals("D");

        }

        private bool ValidarDataValida()
        {
            if (dthDataVenda.Value.Date >= DateTime.Now.Date) return false;

            return true;
        }
    }
}
