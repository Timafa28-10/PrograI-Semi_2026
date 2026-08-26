namespace SistemaGestionSalud {
    partial class FormPrincipal {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControlPrincipal = new System.Windows.Forms.TabControl();
            this.tabTurnos = new System.Windows.Forms.TabPage();
            this.lblNombreTurno = new System.Windows.Forms.Label();
            this.txtNombreTurno = new System.Windows.Forms.TextBox();
            this.lblUnidadTurno = new System.Windows.Forms.Label();
            this.cboUnidadTurno = new System.Windows.Forms.ComboBox();
            this.lblGravedadTurno = new System.Windows.Forms.Label();
            this.cboGravedadTurno = new System.Windows.Forms.ComboBox();
            this.btnGenerarTurno = new System.Windows.Forms.Button();
            this.btnAtenderSiguiente = new System.Windows.Forms.Button();
            this.lblEstadoCola = new System.Windows.Forms.Label();
            this.dgvTurnos = new System.Windows.Forms.DataGridView();
            this.tabCitas = new System.Windows.Forms.TabPage();
            this.lblNombreCita = new System.Windows.Forms.Label();
            this.txtNombreCita = new System.Windows.Forms.TextBox();
            this.lblUnidadCita = new System.Windows.Forms.Label();
            this.cboUnidadCita = new System.Windows.Forms.ComboBox();
            this.lblFechaCita = new System.Windows.Forms.Label();
            this.dtpFechaCita = new System.Windows.Forms.DateTimePicker();
            this.lblHoraCita = new System.Windows.Forms.Label();
            this.dtpHoraCita = new System.Windows.Forms.DateTimePicker();
            this.lblMotivoCita = new System.Windows.Forms.Label();
            this.txtMotivoCita = new System.Windows.Forms.TextBox();
            this.btnAgendarCita = new System.Windows.Forms.Button();
            this.btnCancelarCita = new System.Windows.Forms.Button();
            this.dgvCitas = new System.Windows.Forms.DataGridView();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.btnActualizarDashboard = new System.Windows.Forms.Button();
            this.lblEnColaDash = new System.Windows.Forms.Label();
            this.lblAtendidosDash = new System.Windows.Forms.Label();
            this.lblPromedioDash = new System.Windows.Forms.Label();
            this.lblCitasDash = new System.Windows.Forms.Label();
            this.lblGravedadDash = new System.Windows.Forms.Label();
            this.pnlGrafico = new System.Windows.Forms.Panel();
            this.timerReloj = new System.Windows.Forms.Timer(this.components);
            this.tabControlPrincipal.SuspendLayout();
            this.tabTurnos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).BeginInit();
            this.tabCitas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).BeginInit();
            this.tabDashboard.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(976, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Sistema de Gestión del Tiempo y la Demanda en las Unidades de Salud";
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Controls.Add(this.tabTurnos);
            this.tabControlPrincipal.Controls.Add(this.tabCitas);
            this.tabControlPrincipal.Controls.Add(this.tabDashboard);
            this.tabControlPrincipal.Location = new System.Drawing.Point(12, 45);
            this.tabControlPrincipal.Name = "tabControlPrincipal";
            this.tabControlPrincipal.SelectedIndex = 0;
            this.tabControlPrincipal.Size = new System.Drawing.Size(976, 560);
            this.tabControlPrincipal.TabIndex = 1;
            // 
            // tabTurnos
            // 
            this.tabTurnos.Controls.Add(this.lblNombreTurno);
            this.tabTurnos.Controls.Add(this.txtNombreTurno);
            this.tabTurnos.Controls.Add(this.lblUnidadTurno);
            this.tabTurnos.Controls.Add(this.cboUnidadTurno);
            this.tabTurnos.Controls.Add(this.lblGravedadTurno);
            this.tabTurnos.Controls.Add(this.cboGravedadTurno);
            this.tabTurnos.Controls.Add(this.btnGenerarTurno);
            this.tabTurnos.Controls.Add(this.btnAtenderSiguiente);
            this.tabTurnos.Controls.Add(this.lblEstadoCola);
            this.tabTurnos.Controls.Add(this.dgvTurnos);
            this.tabTurnos.Location = new System.Drawing.Point(4, 22);
            this.tabTurnos.Name = "tabTurnos";
            this.tabTurnos.Padding = new System.Windows.Forms.Padding(3);
            this.tabTurnos.Size = new System.Drawing.Size(968, 534);
            this.tabTurnos.TabIndex = 0;
            this.tabTurnos.Text = "Turnos y Tiempos de Espera";
            this.tabTurnos.UseVisualStyleBackColor = true;
            // 
            // lblNombreTurno
            // 
            this.lblNombreTurno.AutoSize = true;
            this.lblNombreTurno.Location = new System.Drawing.Point(17, 23);
            this.lblNombreTurno.Name = "lblNombreTurno";
            this.lblNombreTurno.Size = new System.Drawing.Size(52, 13);
            this.lblNombreTurno.TabIndex = 0;
            this.lblNombreTurno.Text = "Paciente:";
            // 
            // txtNombreTurno
            // 
            this.txtNombreTurno.Location = new System.Drawing.Point(100, 20);
            this.txtNombreTurno.Name = "txtNombreTurno";
            this.txtNombreTurno.Size = new System.Drawing.Size(200, 20);
            this.txtNombreTurno.TabIndex = 1;
            // 
            // lblUnidadTurno
            // 
            this.lblUnidadTurno.AutoSize = true;
            this.lblUnidadTurno.Location = new System.Drawing.Point(320, 23);
            this.lblUnidadTurno.Name = "lblUnidadTurno";
            this.lblUnidadTurno.Size = new System.Drawing.Size(87, 13);
            this.lblUnidadTurno.TabIndex = 2;
            this.lblUnidadTurno.Text = "Unidad de salud:";
            // 
            // cboUnidadTurno
            // 
            this.cboUnidadTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnidadTurno.FormattingEnabled = true;
            this.cboUnidadTurno.Location = new System.Drawing.Point(420, 20);
            this.cboUnidadTurno.Name = "cboUnidadTurno";
            this.cboUnidadTurno.Size = new System.Drawing.Size(190, 21);
            this.cboUnidadTurno.TabIndex = 3;
            this.cboUnidadTurno.SelectedIndexChanged += new System.EventHandler(this.cboUnidadTurno_SelectedIndexChanged);
            // 
            // lblGravedadTurno
            // 
            this.lblGravedadTurno.AutoSize = true;
            this.lblGravedadTurno.Location = new System.Drawing.Point(628, 23);
            this.lblGravedadTurno.Name = "lblGravedadTurno";
            this.lblGravedadTurno.Size = new System.Drawing.Size(57, 13);
            this.lblGravedadTurno.TabIndex = 4;
            this.lblGravedadTurno.Text = "Gravedad:";
            // 
            // cboGravedadTurno
            // 
            this.cboGravedadTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGravedadTurno.FormattingEnabled = true;
            this.cboGravedadTurno.Location = new System.Drawing.Point(695, 20);
            this.cboGravedadTurno.Name = "cboGravedadTurno";
            this.cboGravedadTurno.Size = new System.Drawing.Size(120, 21);
            this.cboGravedadTurno.TabIndex = 5;
            // 
            // btnGenerarTurno
            // 
            this.btnGenerarTurno.Location = new System.Drawing.Point(17, 55);
            this.btnGenerarTurno.Name = "btnGenerarTurno";
            this.btnGenerarTurno.Size = new System.Drawing.Size(140, 27);
            this.btnGenerarTurno.TabIndex = 6;
            this.btnGenerarTurno.Text = "Generar turno";
            this.btnGenerarTurno.UseVisualStyleBackColor = true;
            this.btnGenerarTurno.Click += new System.EventHandler(this.btnGenerarTurno_Click);
            // 
            // btnAtenderSiguiente
            // 
            this.btnAtenderSiguiente.Location = new System.Drawing.Point(165, 55);
            this.btnAtenderSiguiente.Name = "btnAtenderSiguiente";
            this.btnAtenderSiguiente.Size = new System.Drawing.Size(240, 27);
            this.btnAtenderSiguiente.TabIndex = 7;
            this.btnAtenderSiguiente.Text = "Atender siguiente (mayor gravedad)";
            this.btnAtenderSiguiente.UseVisualStyleBackColor = true;
            this.btnAtenderSiguiente.Click += new System.EventHandler(this.btnAtenderSiguiente_Click);
            // 
            // lblEstadoCola
            // 
            this.lblEstadoCola.AutoSize = true;
            this.lblEstadoCola.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoCola.Location = new System.Drawing.Point(17, 92);
            this.lblEstadoCola.Name = "lblEstadoCola";
            this.lblEstadoCola.Size = new System.Drawing.Size(306, 15);
            this.lblEstadoCola.TabIndex = 8;
            this.lblEstadoCola.Text = "En espera: 0   |   Próximo turno: --   |   Atendidos hoy: 0";
            // 
            // dgvTurnos
            // 
            this.dgvTurnos.AllowUserToAddRows = false;
            this.dgvTurnos.AllowUserToDeleteRows = false;
            this.dgvTurnos.Location = new System.Drawing.Point(17, 120);
            this.dgvTurnos.Name = "dgvTurnos";
            this.dgvTurnos.ReadOnly = true;
            this.dgvTurnos.RowHeadersWidth = 25;
            this.dgvTurnos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTurnos.Size = new System.Drawing.Size(936, 395);
            this.dgvTurnos.TabIndex = 9;
            this.dgvTurnos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTurnos_CellContentClick);
            // 
            // tabCitas
            // 
            this.tabCitas.Controls.Add(this.lblNombreCita);
            this.tabCitas.Controls.Add(this.txtNombreCita);
            this.tabCitas.Controls.Add(this.lblUnidadCita);
            this.tabCitas.Controls.Add(this.cboUnidadCita);
            this.tabCitas.Controls.Add(this.lblFechaCita);
            this.tabCitas.Controls.Add(this.dtpFechaCita);
            this.tabCitas.Controls.Add(this.lblHoraCita);
            this.tabCitas.Controls.Add(this.dtpHoraCita);
            this.tabCitas.Controls.Add(this.lblMotivoCita);
            this.tabCitas.Controls.Add(this.txtMotivoCita);
            this.tabCitas.Controls.Add(this.btnAgendarCita);
            this.tabCitas.Controls.Add(this.btnCancelarCita);
            this.tabCitas.Controls.Add(this.dgvCitas);
            this.tabCitas.Location = new System.Drawing.Point(4, 22);
            this.tabCitas.Name = "tabCitas";
            this.tabCitas.Padding = new System.Windows.Forms.Padding(3);
            this.tabCitas.Size = new System.Drawing.Size(968, 534);
            this.tabCitas.TabIndex = 1;
            this.tabCitas.Text = "Agenda de Citas";
            this.tabCitas.UseVisualStyleBackColor = true;
            // 
            // lblNombreCita
            // 
            this.lblNombreCita.AutoSize = true;
            this.lblNombreCita.Location = new System.Drawing.Point(17, 23);
            this.lblNombreCita.Name = "lblNombreCita";
            this.lblNombreCita.Size = new System.Drawing.Size(52, 13);
            this.lblNombreCita.TabIndex = 0;
            this.lblNombreCita.Text = "Paciente:";
            // 
            // txtNombreCita
            // 
            this.txtNombreCita.Location = new System.Drawing.Point(100, 20);
            this.txtNombreCita.Name = "txtNombreCita";
            this.txtNombreCita.Size = new System.Drawing.Size(200, 20);
            this.txtNombreCita.TabIndex = 1;
            // 
            // lblUnidadCita
            // 
            this.lblUnidadCita.AutoSize = true;
            this.lblUnidadCita.Location = new System.Drawing.Point(320, 23);
            this.lblUnidadCita.Name = "lblUnidadCita";
            this.lblUnidadCita.Size = new System.Drawing.Size(44, 13);
            this.lblUnidadCita.TabIndex = 2;
            this.lblUnidadCita.Text = "Unidad:";
            // 
            // cboUnidadCita
            // 
            this.cboUnidadCita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnidadCita.FormattingEnabled = true;
            this.cboUnidadCita.Location = new System.Drawing.Point(370, 20);
            this.cboUnidadCita.Name = "cboUnidadCita";
            this.cboUnidadCita.Size = new System.Drawing.Size(190, 21);
            this.cboUnidadCita.TabIndex = 3;
            // 
            // lblFechaCita
            // 
            this.lblFechaCita.AutoSize = true;
            this.lblFechaCita.Location = new System.Drawing.Point(578, 23);
            this.lblFechaCita.Name = "lblFechaCita";
            this.lblFechaCita.Size = new System.Drawing.Size(40, 13);
            this.lblFechaCita.TabIndex = 4;
            this.lblFechaCita.Text = "Fecha:";
            // 
            // dtpFechaCita
            // 
            this.dtpFechaCita.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaCita.Location = new System.Drawing.Point(620, 19);
            this.dtpFechaCita.Name = "dtpFechaCita";
            this.dtpFechaCita.Size = new System.Drawing.Size(110, 20);
            this.dtpFechaCita.TabIndex = 5;
            // 
            // lblHoraCita
            // 
            this.lblHoraCita.AutoSize = true;
            this.lblHoraCita.Location = new System.Drawing.Point(740, 23);
            this.lblHoraCita.Name = "lblHoraCita";
            this.lblHoraCita.Size = new System.Drawing.Size(33, 13);
            this.lblHoraCita.TabIndex = 6;
            this.lblHoraCita.Text = "Hora:";
            // 
            // dtpHoraCita
            // 
            this.dtpHoraCita.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraCita.Location = new System.Drawing.Point(778, 19);
            this.dtpHoraCita.Name = "dtpHoraCita";
            this.dtpHoraCita.ShowUpDown = true;
            this.dtpHoraCita.Size = new System.Drawing.Size(90, 20);
            this.dtpHoraCita.TabIndex = 7;
            // 
            // lblMotivoCita
            // 
            this.lblMotivoCita.AutoSize = true;
            this.lblMotivoCita.Location = new System.Drawing.Point(17, 58);
            this.lblMotivoCita.Name = "lblMotivoCita";
            this.lblMotivoCita.Size = new System.Drawing.Size(42, 13);
            this.lblMotivoCita.TabIndex = 8;
            this.lblMotivoCita.Text = "Motivo:";
            // 
            // txtMotivoCita
            // 
            this.txtMotivoCita.Location = new System.Drawing.Point(100, 55);
            this.txtMotivoCita.Name = "txtMotivoCita";
            this.txtMotivoCita.Size = new System.Drawing.Size(460, 20);
            this.txtMotivoCita.TabIndex = 9;
            // 
            // btnAgendarCita
            // 
            this.btnAgendarCita.Location = new System.Drawing.Point(578, 53);
            this.btnAgendarCita.Name = "btnAgendarCita";
            this.btnAgendarCita.Size = new System.Drawing.Size(120, 25);
            this.btnAgendarCita.TabIndex = 10;
            this.btnAgendarCita.Text = "Agendar cita";
            this.btnAgendarCita.UseVisualStyleBackColor = true;
            this.btnAgendarCita.Click += new System.EventHandler(this.btnAgendarCita_Click);
            // 
            // btnCancelarCita
            // 
            this.btnCancelarCita.Location = new System.Drawing.Point(708, 53);
            this.btnCancelarCita.Name = "btnCancelarCita";
            this.btnCancelarCita.Size = new System.Drawing.Size(160, 25);
            this.btnCancelarCita.TabIndex = 11;
            this.btnCancelarCita.Text = "Cancelar cita seleccionada";
            this.btnCancelarCita.UseVisualStyleBackColor = true;
            this.btnCancelarCita.Click += new System.EventHandler(this.btnCancelarCita_Click);
            // 
            // dgvCitas
            // 
            this.dgvCitas.AllowUserToAddRows = false;
            this.dgvCitas.AllowUserToDeleteRows = false;
            this.dgvCitas.Location = new System.Drawing.Point(17, 90);
            this.dgvCitas.Name = "dgvCitas";
            this.dgvCitas.ReadOnly = true;
            this.dgvCitas.RowHeadersWidth = 25;
            this.dgvCitas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCitas.Size = new System.Drawing.Size(936, 425);
            this.dgvCitas.TabIndex = 12;
            // 
            // tabDashboard
            // 
            this.tabDashboard.Controls.Add(this.btnActualizarDashboard);
            this.tabDashboard.Controls.Add(this.lblEnColaDash);
            this.tabDashboard.Controls.Add(this.lblAtendidosDash);
            this.tabDashboard.Controls.Add(this.lblPromedioDash);
            this.tabDashboard.Controls.Add(this.lblCitasDash);
            this.tabDashboard.Controls.Add(this.lblGravedadDash);
            this.tabDashboard.Controls.Add(this.pnlGrafico);
            this.tabDashboard.Location = new System.Drawing.Point(4, 22);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabDashboard.Size = new System.Drawing.Size(968, 534);
            this.tabDashboard.TabIndex = 2;
            this.tabDashboard.Text = "Dashboard de Demanda";
            this.tabDashboard.UseVisualStyleBackColor = true;
            // 
            // btnActualizarDashboard
            // 
            this.btnActualizarDashboard.Location = new System.Drawing.Point(17, 20);
            this.btnActualizarDashboard.Name = "btnActualizarDashboard";
            this.btnActualizarDashboard.Size = new System.Drawing.Size(130, 28);
            this.btnActualizarDashboard.TabIndex = 0;
            this.btnActualizarDashboard.Text = "Actualizar";
            this.btnActualizarDashboard.UseVisualStyleBackColor = true;
            this.btnActualizarDashboard.Click += new System.EventHandler(this.btnActualizarDashboard_Click);
            // 
            // lblEnColaDash
            // 
            this.lblEnColaDash.AutoSize = true;
            this.lblEnColaDash.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEnColaDash.Location = new System.Drawing.Point(170, 15);
            this.lblEnColaDash.Name = "lblEnColaDash";
            this.lblEnColaDash.Size = new System.Drawing.Size(132, 19);
            this.lblEnColaDash.TabIndex = 1;
            this.lblEnColaDash.Text = "En espera ahora: 0";
            // 
            // lblAtendidosDash
            // 
            this.lblAtendidosDash.AutoSize = true;
            this.lblAtendidosDash.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAtendidosDash.Location = new System.Drawing.Point(170, 42);
            this.lblAtendidosDash.Name = "lblAtendidosDash";
            this.lblAtendidosDash.Size = new System.Drawing.Size(122, 19);
            this.lblAtendidosDash.TabIndex = 2;
            this.lblAtendidosDash.Text = "Atendidos hoy: 0";
            // 
            // lblPromedioDash
            // 
            this.lblPromedioDash.AutoSize = true;
            this.lblPromedioDash.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPromedioDash.Location = new System.Drawing.Point(430, 15);
            this.lblPromedioDash.Name = "lblPromedioDash";
            this.lblPromedioDash.Size = new System.Drawing.Size(258, 19);
            this.lblPromedioDash.TabIndex = 3;
            this.lblPromedioDash.Text = "Tiempo promedio de espera: 0.0 min";
            // 
            // lblCitasDash
            // 
            this.lblCitasDash.AutoSize = true;
            this.lblCitasDash.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCitasDash.Location = new System.Drawing.Point(430, 42);
            this.lblCitasDash.Name = "lblCitasDash";
            this.lblCitasDash.Size = new System.Drawing.Size(152, 19);
            this.lblCitasDash.TabIndex = 4;
            this.lblCitasDash.Text = "Citas programadas: 0";
            // 
            // lblGravedadDash
            // 
            this.lblGravedadDash.AutoSize = true;
            this.lblGravedadDash.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblGravedadDash.Location = new System.Drawing.Point(17, 65);
            this.lblGravedadDash.Name = "lblGravedadDash";
            this.lblGravedadDash.Size = new System.Drawing.Size(360, 17);
            this.lblGravedadDash.TabIndex = 5;
            this.lblGravedadDash.Text = "En espera por gravedad — Crítico: 0   Urgente: 0   Leve: 0";
            // 
            // pnlGrafico
            // 
            this.pnlGrafico.BackColor = System.Drawing.Color.White;
            this.pnlGrafico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGrafico.Location = new System.Drawing.Point(17, 95);
            this.pnlGrafico.Name = "pnlGrafico";
            this.pnlGrafico.Size = new System.Drawing.Size(936, 420);
            this.pnlGrafico.TabIndex = 6;
            this.pnlGrafico.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGrafico_Paint);
            // 
            // timerReloj
            // 
            this.timerReloj.Enabled = true;
            this.timerReloj.Interval = 1000;
            this.timerReloj.Tick += new System.EventHandler(this.timerReloj_Tick);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.tabControlPrincipal);
            this.Controls.Add(this.lblTitulo);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "FormPrincipal";
            this.Text = "Sistema de Gestión del Tiempo y la Demanda en las Unidades de Salud";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabTurnos.ResumeLayout(false);
            this.tabTurnos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).EndInit();
            this.tabCitas.ResumeLayout(false);
            this.tabCitas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).EndInit();
            this.tabDashboard.ResumeLayout(false);
            this.tabDashboard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControlPrincipal;

        private System.Windows.Forms.TabPage tabTurnos;
        private System.Windows.Forms.Label lblNombreTurno;
        private System.Windows.Forms.TextBox txtNombreTurno;
        private System.Windows.Forms.Label lblUnidadTurno;
        private System.Windows.Forms.ComboBox cboUnidadTurno;
        private System.Windows.Forms.Label lblGravedadTurno;
        private System.Windows.Forms.ComboBox cboGravedadTurno;
        private System.Windows.Forms.Button btnGenerarTurno;
        private System.Windows.Forms.Button btnAtenderSiguiente;
        private System.Windows.Forms.Label lblEstadoCola;
        private System.Windows.Forms.DataGridView dgvTurnos;

        private System.Windows.Forms.TabPage tabCitas;
        private System.Windows.Forms.Label lblNombreCita;
        private System.Windows.Forms.TextBox txtNombreCita;
        private System.Windows.Forms.Label lblUnidadCita;
        private System.Windows.Forms.ComboBox cboUnidadCita;
        private System.Windows.Forms.Label lblFechaCita;
        private System.Windows.Forms.DateTimePicker dtpFechaCita;
        private System.Windows.Forms.Label lblHoraCita;
        private System.Windows.Forms.DateTimePicker dtpHoraCita;
        private System.Windows.Forms.Label lblMotivoCita;
        private System.Windows.Forms.TextBox txtMotivoCita;
        private System.Windows.Forms.Button btnAgendarCita;
        private System.Windows.Forms.Button btnCancelarCita;
        private System.Windows.Forms.DataGridView dgvCitas;

        private System.Windows.Forms.TabPage tabDashboard;
        private System.Windows.Forms.Button btnActualizarDashboard;
        private System.Windows.Forms.Label lblEnColaDash;
        private System.Windows.Forms.Label lblAtendidosDash;
        private System.Windows.Forms.Label lblPromedioDash;
        private System.Windows.Forms.Label lblCitasDash;
        private System.Windows.Forms.Label lblGravedadDash;
        private System.Windows.Forms.Panel pnlGrafico;

        private System.Windows.Forms.Timer timerReloj;
    }
}
