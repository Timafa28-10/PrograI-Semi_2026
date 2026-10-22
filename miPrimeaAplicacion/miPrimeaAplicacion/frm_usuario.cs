using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace miPrimeaAplicacion
{
    public partial class frm_usuario : Form
    {
        Conexion objConexion = new Conexion();
        DataSet objDs;
        DataTable tblUsuarios;
        int posicion = 0;
        bool modoNuevo = false;

        public frm_usuario()
        {
            InitializeComponent();

            // Limites de caracteres y clave oculta
            txtUsuariousuario.MaxLength = 16;
            txtClaveusuario.MaxLength = 35;
            txtConfirmarusuario.MaxLength = 35;
            txtNombreusuario.MaxLength = 65;
            txtDireccionusuario.MaxLength = 85;
            txtTelefonousuario.MaxLength = 9;
            txtClaveusuario.UseSystemPasswordChar = true;
            txtConfirmarusuario.UseSystemPasswordChar = true;

            // Eventos de los botones
            // (btnAgregarusuario ya esta enlazado desde el diseñador)
            this.Load += frm_usuario_Load;
            btnPrimerousuario.Click += (s, e) => { posicion = 0; mostrarRegistro(); };
            btnAnteriorusuario.Click += (s, e) => { if (posicion > 0) posicion--; mostrarRegistro(); };
            btnSiguienteusuario.Click += (s, e) => { if (posicion < tblUsuarios.Rows.Count - 1) posicion++; mostrarRegistro(); };
            btnUltimousuario.Click += (s, e) => { posicion = tblUsuarios.Rows.Count - 1; mostrarRegistro(); };
            btnModificarusuario.Click += btnModificar_Click;
            btnEliminarusuario.Click += btnEliminar_Click;
        }

        // Metodos vacios que el diseñador tiene enlazados (no borrar)
        private void lblNombreAlumno_Click(object sender, EventArgs e) { }
        private void lblEmailAlumno_Click(object sender, EventArgs e) { }
        private void grbDatos_Enter(object sender, EventArgs e) { }
        private void txtConfirmarusuario_TextChanged(object sender, EventArgs e) { }

        private void frm_usuario_Load(object sender, EventArgs e)
        {
            cargarDatos();
            mostrarRegistro();
        }

        private void cargarDatos()
        {
            objDs = objConexion.obtenerDatos();
            tblUsuarios = objDs.Tables["usuarios"];
        }

        private void mostrarRegistro()
        {
            modoNuevo = false;
            btnAgregarusuario.Text = "Agregar";
            btnModificarusuario.Enabled = true;
            btnEliminarusuario.Enabled = true;

            if (tblUsuarios.Rows.Count == 0)
            {
                limpiarCampos();
                lblRegistrosusuario.Text = "0 de 0";
                return;
            }
            if (posicion >= tblUsuarios.Rows.Count) posicion = tblUsuarios.Rows.Count - 1;
            if (posicion < 0) posicion = 0;

            DataRow fila = tblUsuarios.Rows[posicion];
            idUsuario.Text = fila["idUsuario"].ToString();
            txtUsuariousuario.Text = fila["usuario"].ToString();
            txtClaveusuario.Text = fila["clave"].ToString();
            txtConfirmarusuario.Text = fila["clave"].ToString();
            txtNombreusuario.Text = fila["nombre"].ToString();
            txtDireccionusuario.Text = fila["dirección"].ToString();
            txtTelefonousuario.Text = fila["telefono"].ToString();
            lblRegistrosusuario.Text = (posicion + 1) + " de " + tblUsuarios.Rows.Count;
        }

        private void limpiarCampos()
        {
            idUsuario.Text = "0";
            txtUsuariousuario.Clear();
            txtClaveusuario.Clear();
            txtConfirmarusuario.Clear();
            txtNombreusuario.Clear();
            txtDireccionusuario.Clear();
            txtTelefonousuario.Clear();
        }

        private bool validar()
        {
            string usuario = txtUsuariousuario.Text.Trim();
            string clave = txtClaveusuario.Text;

            if (usuario.Length < 6 || usuario.Length > 16)
            {
                MessageBox.Show("El usuario debe tener entre 6 y 16 caracteres.", "Validacion");
                txtUsuariousuario.Focus();
                return false;
            }
            if (clave != txtConfirmarusuario.Text)
            {
                MessageBox.Show("La clave y su confirmacion no coinciden.", "Validacion");
                txtConfirmarusuario.Focus();
                return false;
            }
            if (!clave.Any(char.IsLetter) || !clave.Any(char.IsDigit))
            {
                MessageBox.Show("La clave debe llevar letras y numeros.", "Validacion");
                txtClaveusuario.Focus();
                return false;
            }
            return true;
        }

        private bool usuarioRepetido(string usuario, string idActual)
        {
            foreach (DataRow fila in tblUsuarios.Rows)
            {
                if (fila["usuario"].ToString() == usuario && fila["idUsuario"].ToString() != idActual)
                {
                    return true;
                }
            }
            return false;
        }

        private string t(string texto)
        {
            return texto.Trim().Replace("'", "''");
        }

        private string[] armarDatos()
        {
            return new string[] {
                idUsuario.Text, t(txtUsuariousuario.Text), t(txtClaveusuario.Text),
                t(txtNombreusuario.Text), t(txtDireccionusuario.Text), t(txtTelefonousuario.Text)
            };
        }

        private void btnAgregarusuario_Click(object sender, EventArgs e)
        {
            if (!modoNuevo)
            {
                // Primer clic: preparar para un registro nuevo
                modoNuevo = true;
                limpiarCampos();
                btnAgregarusuario.Text = "Guardar";
                btnModificarusuario.Enabled = false;
                btnEliminarusuario.Enabled = false;
                txtUsuariousuario.Focus();
                return;
            }
            // Segundo clic: guardar
            if (!validar()) return;
            if (usuarioRepetido(txtUsuariousuario.Text.Trim(), "0"))
            {
                MessageBox.Show("Ese usuario ya existe.", "Validacion");
                return;
            }
            string resp = objConexion.mantenimiento_usuarios_USSS089026(armarDatos(), "nuevo");
            if (resp == "1")
            {
                cargarDatos();
                posicion = tblUsuarios.Rows.Count - 1;
                mostrarRegistro();
                MessageBox.Show("Usuario agregado.", "Listo");
            }
            else
            {
                MessageBox.Show("Error: " + resp, "Error");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (tblUsuarios.Rows.Count == 0) return;
            if (!validar()) return;
            if (usuarioRepetido(txtUsuariousuario.Text.Trim(), idUsuario.Text))
            {
                MessageBox.Show("Ese usuario ya existe.", "Validacion");
                return;
            }
            string resp = objConexion.mantenimiento_usuarios_USSS089026(armarDatos(), "modificar");
            if (resp == "1")
            {
                cargarDatos();
                mostrarRegistro();
                MessageBox.Show("Usuario modificado.", "Listo");
            }
            else
            {
                MessageBox.Show("Error: " + resp, "Error");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (tblUsuarios.Rows.Count == 0) return;
            DialogResult r = MessageBox.Show("Eliminar a " + txtUsuariousuario.Text + "?", "Confirmar", MessageBoxButtons.YesNo);
            if (r != DialogResult.Yes) return;

            string resp = objConexion.mantenimiento_usuarios_USSS089026(armarDatos(), "eliminar");
            if (resp == "1")
            {
                cargarDatos();
                if (posicion > 0) posicion--;
                mostrarRegistro();
                MessageBox.Show("Usuario eliminado.", "Listo");
            }
            else
            {
                MessageBox.Show("Error: " + resp, "Error");
            }
        }
    }
}