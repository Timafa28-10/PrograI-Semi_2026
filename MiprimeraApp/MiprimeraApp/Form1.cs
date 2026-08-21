using System;
using System.Linq;
using System.Windows.Forms;

namespace MiprimeraApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            String[] serie = txtSerie.Text.Split(',');

            ltsRespuesta.DataSource = serie.Select(n => int.Parse(n)).Where(n => n % 2 == 0)
                .OrderBy(n => n)
                .ToList();
        }
    }
}
