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
    public partial class MDIIntegracaoConcillius : Form
    {
        public MDIIntegracaoConcillius()
        {
            InitializeComponent();
        }

        private void MDIIntegracaoConcillius_Load(object sender, EventArgs e)
        {

        }

        private void vendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmCargaVendas();
            form.ShowDialog();
        }
    }
}
