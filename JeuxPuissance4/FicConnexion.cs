using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JeuxPuissance4
{
    public partial class EcranConnexion : Form
    {
        public EcranConnexion()
        {
            InitializeComponent();
        }

        private void btnEcouter_Click(object sender, EventArgs e)
        {
            Hide();
            EcranAcceuil f = new EcranAcceuil(false, tbServeur.Text);
            f.ShowDialog();
            Close();
        }

        private void btnConnecter_Click(object sender, EventArgs e)
        {
            Hide();
            EcranAcceuil f = new EcranAcceuil(true, tbServeur.Text);
            f.ShowDialog();
            Close();
        }
    }
}
