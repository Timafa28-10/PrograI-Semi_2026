using System;
using System.Windows.Forms;

namespace Conversores {
    public partial class Form1 : Form {

        // Array con las 6 categorias (Moneda, Masa, Volumen, Longitud, Almacenamiento, Tiempo)
        private readonly Categoria[] categorias;

        public Form1() {
            InitializeComponent();
            categorias = DatosConversores.Construir();
            CargarCategorias();
        }

        /// <summary>
        /// Llena el ComboBox de categorias recorriendo el array "categorias" con un bucle.
        /// </summary>
        private void CargarCategorias() {
            cbCategoria.Items.Clear();
            for (int i = 0; i < categorias.Length; i++) {
                cbCategoria.Items.Add(categorias[i].Nombre);
            }

            if (cbCategoria.Items.Count > 0)
                cbCategoria.SelectedIndex = 0;
        }

        /// <summary>
        /// Llena "Desde" y "Hacia" recorriendo el array de unidades de la categoria seleccionada.
        /// </summary>
        private void CargarUnidades(int indiceCategoria) {
            cbDesde.Items.Clear();
            cbHacia.Items.Clear();

            string[] unidades = categorias[indiceCategoria].Unidades;

            for (int i = 0; i < unidades.Length; i++) {
                cbDesde.Items.Add(unidades[i]);
                cbHacia.Items.Add(unidades[i]);
            }

            if (cbDesde.Items.Count > 0) cbDesde.SelectedIndex = 0;
            if (cbHacia.Items.Count > 1) cbHacia.SelectedIndex = 1;
            else if (cbHacia.Items.Count > 0) cbHacia.SelectedIndex = 0;
        }

        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e) {
            if (cbCategoria.SelectedIndex >= 0)
                CargarUnidades(cbCategoria.SelectedIndex);
        }

        private void btnInvertir_Click(object sender, EventArgs e) {
            int desde = cbDesde.SelectedIndex;
            int hacia = cbHacia.SelectedIndex;
            cbDesde.SelectedIndex = hacia;
            cbHacia.SelectedIndex = desde;
        }

        private void btnConvertir_Click(object sender, EventArgs e) {
            if (cbCategoria.SelectedIndex < 0 || cbDesde.SelectedIndex < 0 || cbHacia.SelectedIndex < 0) {
                MessageBox.Show("Selecciona categoria, unidad de origen y unidad destino.");
                return;
            }

            double valor;
            if (!double.TryParse(txtValor.Text, out valor)) {
                MessageBox.Show("Ingresa un valor numerico valido.");
                return;
            }

            Categoria categoria = categorias[cbCategoria.SelectedIndex];
            double resultado = categoria.Convertir(valor, cbDesde.SelectedIndex, cbHacia.SelectedIndex);

            lblResultado.Text = string.Format("Resultado: {0} {1} = {2:0.####} {3}",
                valor,
                categoria.Unidades[cbDesde.SelectedIndex],
                resultado,
                categoria.Unidades[cbHacia.SelectedIndex]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
