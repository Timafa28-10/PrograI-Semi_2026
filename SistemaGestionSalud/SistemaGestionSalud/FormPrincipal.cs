using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaGestionSalud {
    public partial class FormPrincipal : Form {

        // Todos los datos se manejan en memoria (no se usa base de datos).
        private readonly List<Turno> turnos = new List<Turno>();
        private readonly List<Cita> citas = new List<Cita>();
        private int contadorTurnos = 0;

        private static readonly string[] unidadesSalud = new string[] {
            "Medicina General",
            "Pediatría",
            "Emergencias",
            "Laboratorio",
            "Odontología",
            "Ginecología"
        };

        public FormPrincipal() {
            InitializeComponent();
            ConfigurarGrids();
            CargarUnidades();
            RefreshTurnosGrid();
            RefreshEstadoCola();
            RefreshCitasGrid();
            ActualizarDashboard();
        }

        // ------------------------------------------------------------------
        // Configuración inicial
        // ------------------------------------------------------------------

        private void CargarUnidades() {
            cboUnidadTurno.Items.AddRange(unidadesSalud);
            cboUnidadCita.Items.AddRange(unidadesSalud);
            if (cboUnidadTurno.Items.Count > 0) cboUnidadTurno.SelectedIndex = 0;
            if (cboUnidadCita.Items.Count > 0) cboUnidadCita.SelectedIndex = 0;

            cboGravedadTurno.Items.Add("Crítico");
            cboGravedadTurno.Items.Add("Urgente");
            cboGravedadTurno.Items.Add("Leve");
            cboGravedadTurno.SelectedIndex = 2; // Leve por defecto

            dtpFechaCita.Value = DateTime.Today;
            dtpHoraCita.Value = DateTime.Today.AddHours(8);
        }

        private void ConfigurarGrids() {
            dgvTurnos.Columns.Add("colNumero", "Turno #");
            dgvTurnos.Columns.Add("colPaciente", "Paciente");
            dgvTurnos.Columns.Add("colUnidad", "Unidad");
            dgvTurnos.Columns.Add("colGravedad", "Gravedad");
            dgvTurnos.Columns.Add("colLlegada", "Hora Llegada");
            dgvTurnos.Columns.Add("colAtencion", "Hora Atención");
            dgvTurnos.Columns.Add("colEspera", "Tiempo de Espera");
            dgvTurnos.Columns.Add("colEstadoTurno", "Estado");
            dgvTurnos.Columns["colPaciente"].Width = 150;
            dgvTurnos.Columns["colUnidad"].Width = 140;
            dgvTurnos.Columns["colGravedad"].Width = 80;
            dgvTurnos.Columns["colEstadoTurno"].Width = 90;

            dgvCitas.Columns.Add("colPacienteCita", "Paciente");
            dgvCitas.Columns.Add("colUnidadCita", "Unidad");
            dgvCitas.Columns.Add("colFechaCita", "Fecha");
            dgvCitas.Columns.Add("colHoraCita", "Hora");
            dgvCitas.Columns.Add("colMotivoCita", "Motivo");
            dgvCitas.Columns.Add("colEstadoCitaCol", "Estado");
            dgvCitas.Columns["colPacienteCita"].Width = 150;
            dgvCitas.Columns["colMotivoCita"].Width = 220;
        }

        // ------------------------------------------------------------------
        // Módulo de Turnos y Tiempos de Espera
        // ------------------------------------------------------------------

        private void btnGenerarTurno_Click(object sender, EventArgs e) {
            if (!ValidarCampoTexto(txtNombreTurno, "el nombre del paciente")) return;
            if (cboUnidadTurno.SelectedItem == null) {
                MessageBox.Show("Seleccione una unidad de salud.", "Dato requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboGravedadTurno.SelectedItem == null) {
                MessageBox.Show("Seleccione el nivel de gravedad del paciente.", "Dato requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            contadorTurnos++;
            Turno turno = new Turno {
                Numero = contadorTurnos,
                Paciente = txtNombreTurno.Text.Trim(),
                Unidad = cboUnidadTurno.SelectedItem.ToString(),
                Gravedad = ObtenerGravedadSeleccionada(),
                HoraLlegada = DateTime.Now,
                Estado = EstadoTurno.EnEspera
            };
            turnos.Add(turno);

            txtNombreTurno.Clear();
            cboGravedadTurno.SelectedIndex = 2; // vuelve a "Leve" por defecto
            txtNombreTurno.Focus();

            RefreshTurnosGrid();
            RefreshEstadoCola();
        }

        private NivelGravedad ObtenerGravedadSeleccionada() {
            switch (cboGravedadTurno.SelectedIndex) {
                case 0: return NivelGravedad.Critico;
                case 1: return NivelGravedad.Urgente;
                default: return NivelGravedad.Leve;
            }
        }

        private void btnAtenderSiguiente_Click(object sender, EventArgs e) {
            // Prioridad: primero el nivel de gravedad más alto (Crítico > Urgente > Leve);
            // dentro del mismo nivel, el que llegó primero.
            Turno siguiente = turnos
                .Where(t => t.Estado == EstadoTurno.EnEspera)
                .OrderBy(t => (int)t.Gravedad)
                .ThenBy(t => t.HoraLlegada)
                .FirstOrDefault();

            if (siguiente == null) {
                MessageBox.Show("No hay pacientes en espera.", "Cola vacía", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            siguiente.HoraAtencion = DateTime.Now;
            siguiente.Estado = EstadoTurno.Atendido;

            RefreshTurnosGrid();
            RefreshEstadoCola();
        }

        private void RefreshTurnosGrid() {
            dgvTurnos.Rows.Clear();
            // Se muestran primero los que están en espera (ordenados por prioridad),
            // y luego los ya atendidos (ordenados por hora de atención).
            var enEspera = turnos.Where(t => t.Estado == EstadoTurno.EnEspera)
                .OrderBy(t => (int)t.Gravedad).ThenBy(t => t.HoraLlegada);
            var atendidos = turnos.Where(t => t.Estado == EstadoTurno.Atendido)
                .OrderBy(t => t.HoraAtencion);

            foreach (Turno t in enEspera.Concat(atendidos)) {
                int indiceFila = dgvTurnos.Rows.Add(
                    t.Numero,
                    t.Paciente,
                    t.Unidad,
                    t.GravedadTexto,
                    t.HoraLlegada.ToString("HH:mm:ss"),
                    t.HoraAtencion.HasValue ? t.HoraAtencion.Value.ToString("HH:mm:ss") : "--",
                    FormatearDuracion(t.TiempoEspera),
                    t.Estado == EstadoTurno.EnEspera ? "En espera" : "Atendido"
                );
                dgvTurnos.Rows[indiceFila].DefaultCellStyle.BackColor = ColorParaGravedad(t.Gravedad);
            }
        }

        private static Color ColorParaGravedad(NivelGravedad gravedad) {
            switch (gravedad) {
                case NivelGravedad.Critico: return Color.FromArgb(255, 205, 205);
                case NivelGravedad.Urgente: return Color.FromArgb(255, 240, 190);
                default: return Color.FromArgb(215, 245, 215);
            }
        }

        private void RefreshEstadoCola() {
            int enEspera = turnos.Count(t => t.Estado == EstadoTurno.EnEspera);
            Turno proximo = turnos
                .Where(t => t.Estado == EstadoTurno.EnEspera)
                .OrderBy(t => (int)t.Gravedad)
                .ThenBy(t => t.HoraLlegada)
                .FirstOrDefault();
            int atendidosHoy = turnos.Count(t => t.Estado == EstadoTurno.Atendido
                && t.HoraAtencion.HasValue
                && t.HoraAtencion.Value.Date == DateTime.Today);

            lblEstadoCola.Text = string.Format(
                "En espera: {0}   |   Próximo turno: {1}   |   Atendidos hoy: {2}",
                enEspera,
                proximo != null ? proximo.Paciente + " (" + proximo.GravedadTexto + ")" : "--",
                atendidosHoy
            );
        }

        // ------------------------------------------------------------------
        // Módulo de Agenda de Citas
        // ------------------------------------------------------------------

        private void btnAgendarCita_Click(object sender, EventArgs e) {
            if (!ValidarCampoTexto(txtNombreCita, "el nombre del paciente")) return;
            if (cboUnidadCita.SelectedItem == null) {
                MessageBox.Show("Seleccione una unidad de salud.", "Dato requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cita cita = new Cita {
                Paciente = txtNombreCita.Text.Trim(),
                Unidad = cboUnidadCita.SelectedItem.ToString(),
                Fecha = dtpFechaCita.Value.Date,
                Hora = dtpHoraCita.Value,
                Motivo = txtMotivoCita.Text.Trim(),
                Estado = EstadoCita.Programada
            };
            citas.Add(cita);

            txtNombreCita.Clear();
            txtMotivoCita.Clear();
            txtNombreCita.Focus();

            RefreshCitasGrid();
        }

        private void btnCancelarCita_Click(object sender, EventArgs e) {
            if (dgvCitas.SelectedRows.Count == 0) {
                MessageBox.Show("Seleccione una cita de la lista para cancelarla.", "Ninguna cita seleccionada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int indiceSeleccionado = dgvCitas.SelectedRows[0].Index;
            List<Cita> citasOrdenadas = citas.OrderBy(c => c.FechaHora).ToList();

            if (indiceSeleccionado >= 0 && indiceSeleccionado < citasOrdenadas.Count) {
                citasOrdenadas[indiceSeleccionado].Estado = EstadoCita.Cancelada;
            }

            RefreshCitasGrid();
        }

        private void RefreshCitasGrid() {
            dgvCitas.Rows.Clear();
            foreach (Cita c in citas.OrderBy(c => c.FechaHora)) {
                dgvCitas.Rows.Add(
                    c.Paciente,
                    c.Unidad,
                    c.Fecha.ToString("dd/MM/yyyy"),
                    c.Hora.ToString("HH:mm"),
                    c.Motivo,
                    TraducirEstadoCita(c.Estado)
                );
            }
        }

        private static string TraducirEstadoCita(EstadoCita estado) {
            switch (estado) {
                case EstadoCita.Programada: return "Programada";
                case EstadoCita.Cancelada: return "Cancelada";
                case EstadoCita.Atendida: return "Atendida";
                default: return estado.ToString();
            }
        }

        // ------------------------------------------------------------------
        // Módulo de Dashboard de Demanda
        // ------------------------------------------------------------------

        private void btnActualizarDashboard_Click(object sender, EventArgs e) {
            ActualizarDashboard();
        }

        private void ActualizarDashboard() {
            int enEsperaAhora = turnos.Count(t => t.Estado == EstadoTurno.EnEspera);
            int atendidosHoy = turnos.Count(t => t.Estado == EstadoTurno.Atendido
                && t.HoraAtencion.HasValue
                && t.HoraAtencion.Value.Date == DateTime.Today);

            List<Turno> atendidos = turnos.Where(t => t.Estado == EstadoTurno.Atendido).ToList();
            double promedioMinutos = atendidos.Count > 0 ? atendidos.Average(t => t.TiempoEspera.TotalMinutes) : 0;

            int citasProgramadas = citas.Count(c => c.Estado == EstadoCita.Programada);

            int enEsperaCriticos = turnos.Count(t => t.Estado == EstadoTurno.EnEspera && t.Gravedad == NivelGravedad.Critico);
            int enEsperaUrgentes = turnos.Count(t => t.Estado == EstadoTurno.EnEspera && t.Gravedad == NivelGravedad.Urgente);
            int enEsperaLeves = turnos.Count(t => t.Estado == EstadoTurno.EnEspera && t.Gravedad == NivelGravedad.Leve);

            lblEnColaDash.Text = "En espera ahora: " + enEsperaAhora;
            lblAtendidosDash.Text = "Atendidos hoy: " + atendidosHoy;
            lblPromedioDash.Text = string.Format("Tiempo promedio de espera: {0:0.0} min", promedioMinutos);
            lblCitasDash.Text = "Citas programadas: " + citasProgramadas;
            lblGravedadDash.Text = string.Format(
                "En espera por gravedad — Crítico: {0}   Urgente: {1}   Leve: {2}",
                enEsperaCriticos, enEsperaUrgentes, enEsperaLeves);

            pnlGrafico.Invalidate();
        }

        private void pnlGrafico_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var datos = unidadesSalud.Select(u => new {
                Unidad = u,
                Cantidad = turnos.Count(t => t.Unidad == u)
                    + citas.Count(c => c.Unidad == u && c.Estado != EstadoCita.Cancelada)
            }).ToList();

            int maximo = Math.Max(1, datos.Max(d => d.Cantidad));

            int margenInferior = 45;
            int margenSuperior = 30;
            int margenLateral = 15;

            int anchoDisponible = pnlGrafico.Width - (margenLateral * 2);
            int altoDisponible = pnlGrafico.Height - margenInferior - margenSuperior;
            if (anchoDisponible <= 0 || altoDisponible <= 0 || datos.Count == 0) return;

            int anchoBarra = anchoDisponible / datos.Count;

            using (Font fuenteEtiqueta = new Font("Segoe UI", 8F))
            using (Font fuenteValor = new Font("Segoe UI", 8.5F, FontStyle.Bold))
            using (Brush pincelBarra = new SolidBrush(Color.SteelBlue))
            using (Brush pincelTexto = new SolidBrush(Color.Black))
            using (Pen lapizEje = new Pen(Color.Gray)) {

                for (int i = 0; i < datos.Count; i++) {
                    var d = datos[i];
                    int alturaBarra = (int)Math.Round((d.Cantidad / (double)maximo) * altoDisponible);
                    int x = margenLateral + (i * anchoBarra);
                    int y = pnlGrafico.Height - margenInferior - alturaBarra;
                    int anchoRelleno = Math.Max(10, anchoBarra - 16);

                    g.FillRectangle(pincelBarra, x + 8, y, anchoRelleno, Math.Max(alturaBarra, 1));

                    string valorTexto = d.Cantidad.ToString();
                    SizeF tamValor = g.MeasureString(valorTexto, fuenteValor);
                    g.DrawString(valorTexto, fuenteValor, pincelTexto, x + (anchoBarra - tamValor.Width) / 2, y - 18);

                    SizeF tamEtiqueta = g.MeasureString(d.Unidad, fuenteEtiqueta);
                    g.DrawString(d.Unidad, fuenteEtiqueta, pincelTexto,
                        x + (anchoBarra - tamEtiqueta.Width) / 2,
                        pnlGrafico.Height - margenInferior + 8);
                }

                g.DrawLine(lapizEje,
                    margenLateral, pnlGrafico.Height - margenInferior,
                    pnlGrafico.Width - margenLateral, pnlGrafico.Height - margenInferior);
            }
        }

        // ------------------------------------------------------------------
        // Utilidades
        // ------------------------------------------------------------------

        private bool ValidarCampoTexto(TextBox campo, string nombreCampo) {
            if (string.IsNullOrWhiteSpace(campo.Text)) {
                MessageBox.Show("Por favor ingrese " + nombreCampo + ".", "Dato requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                campo.Focus();
                return false;
            }
            return true;
        }

        private static string FormatearDuracion(TimeSpan ts) {
            if (ts.TotalHours >= 1) {
                return string.Format("{0}h {1}min", (int)ts.TotalHours, ts.Minutes);
            }
            return string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }

        private void timerReloj_Tick(object sender, EventArgs e) {
            // Actualiza en vivo los tiempos de espera de la cola.
            RefreshTurnosGrid();
            RefreshEstadoCola();

            if (tabControlPrincipal.SelectedTab == tabDashboard) {
                ActualizarDashboard();
            }
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void dgvTurnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cboUnidadTurno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
