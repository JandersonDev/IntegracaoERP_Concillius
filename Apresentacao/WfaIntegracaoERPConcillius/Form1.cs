using IntegracaoERPConcillius.Infraestrutura.Repositorio;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfaIntegracaoERPConcillius
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //string PATH = @"Computador\HKEY_LOCAL_MACHINE\SOFTWARE\Concillius";

            //string PATH = @"SOFTWARE\Concillius";

            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(@"SOFTWARE\TESTE"))
            {
                var t = key.GetValue("Teste1");
            }

            //RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\gitforwindows");

            //RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Concillius\\CNPJ");

            //Console.WriteLine(registryKey.GetValue("Padão"));
        }
    }
}
