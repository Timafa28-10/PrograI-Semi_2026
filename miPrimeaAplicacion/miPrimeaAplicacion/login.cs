using System;
using System.Data;
using System.Windows.Forms;

namespace miPrimeaAplicacion
{
    public partial class login : Form
    {
        Conexion objConexion = new Conexion();
        int intentos = 0;

        public login()
        {
            InitializeComponent();

            txtUsuario.MaxLength = 16;
            txtClave.MaxLength = 35;
            txtClave.UseSystemPasswordChar = true;

            this.AcceptButton = btnIngresar;   // Enter = Ingresar
            btnIngresar.Click += btnIngresar_Click;
            btnSalir.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; };
        }

        // Metodos vacios que el diseñador tiene enlazados (no borrar)
        private void label1_Click(object sender, EventArgs e) { }
        private void txtUsuario_TextChanged(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text;

            if (usuario == "" || clave == "")
            {
                MessageBox.Show("Escriba el usuario y la clave.", "Login");
                return;
            }

            DataSet ds = objConexion.obtenerDatos();
            DataTable tbl = ds.Tables["usuarios"];

            bool existe = false;
            foreach (DataRow fila in tbl.Rows)
            {
                if (fila["usuario"].ToString() == usuario && fila["clave"].ToString() == clave)
                {
                    existe = true;
                    break;
                }
            }

            if (existe)
            {
                this.DialogResult = DialogResult.OK;   // acceso permitido
            }
            else
            {
                intentos++;
                MessageBox.Show("Acceso denegado: usuario o clave incorrectos.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClave.Clear();
                txtClave.Focus();
                if (intentos >= 3)
                {
                    MessageBox.Show("Demasiados intentos. El sistema se cerrará.", "Login");
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }
    }
}