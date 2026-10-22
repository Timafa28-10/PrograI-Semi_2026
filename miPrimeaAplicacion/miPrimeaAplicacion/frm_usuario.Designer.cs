using System;

namespace miPrimeaAplicacion
{
    partial class frm_usuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbEdicion = new System.Windows.Forms.GroupBox();
            this.btnEliminarusuario = new System.Windows.Forms.Button();
            this.btnModificarusuario = new System.Windows.Forms.Button();
            this.btnAgregarusuario = new System.Windows.Forms.Button();
            this.grbNavegacion = new System.Windows.Forms.GroupBox();
            this.lblRegistrosusuario = new System.Windows.Forms.Label();
            this.btnUltimousuario = new System.Windows.Forms.Button();
            this.btnSiguienteusuario = new System.Windows.Forms.Button();
            this.btnAnteriorusuario = new System.Windows.Forms.Button();
            this.btnPrimerousuario = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grbDatos = new System.Windows.Forms.GroupBox();
            this.idUsuario = new System.Windows.Forms.Label();
            this.lblIdusuario = new System.Windows.Forms.Label();
            this.txtClaveusuario = new System.Windows.Forms.TextBox();
            this.lblClaveusuario = new System.Windows.Forms.Label();
            this.txtTelefonousuario = new System.Windows.Forms.TextBox();
            this.lblTelefonousuario = new System.Windows.Forms.Label();
            this.txtDireccionusuario = new System.Windows.Forms.TextBox();
            this.lblDireccionusuario = new System.Windows.Forms.Label();
            this.txtNombreusuario = new System.Windows.Forms.TextBox();
            this.lblNombreusuario = new System.Windows.Forms.Label();
            this.txtUsuariousuario = new System.Windows.Forms.TextBox();
            this.lblCodigousuario = new System.Windows.Forms.Label();
            this.txtConfirmarusuario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grbEdicion.SuspendLayout();
            this.grbNavegacion.SuspendLayout();
            this.grbDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbEdicion
            // 
            this.grbEdicion.Controls.Add(this.btnEliminarusuario);
            this.grbEdicion.Controls.Add(this.btnModificarusuario);
            this.grbEdicion.Controls.Add(this.btnAgregarusuario);
            this.grbEdicion.Location = new System.Drawing.Point(229, 331);
            this.grbEdicion.Name = "grbEdicion";
            this.grbEdicion.Size = new System.Drawing.Size(317, 61);
            this.grbEdicion.TabIndex = 15;
            this.grbEdicion.TabStop = false;
            this.grbEdicion.Text = "Edicion";
            // 
            // btnEliminarusuario
            // 
            this.btnEliminarusuario.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarusuario.Location = new System.Drawing.Point(190, 20);
            this.btnEliminarusuario.Name = "btnEliminarusuario";
            this.btnEliminarusuario.Size = new System.Drawing.Size(95, 36);
            this.btnEliminarusuario.TabIndex = 9;
            this.btnEliminarusuario.Text = "Eliminar";
            this.btnEliminarusuario.UseVisualStyleBackColor = true;
            // 
            // btnModificarusuario
            // 
            this.btnModificarusuario.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarusuario.Location = new System.Drawing.Point(89, 19);
            this.btnModificarusuario.Name = "btnModificarusuario";
            this.btnModificarusuario.Size = new System.Drawing.Size(95, 36);
            this.btnModificarusuario.TabIndex = 8;
            this.btnModificarusuario.Text = "Modificar";
            this.btnModificarusuario.UseVisualStyleBackColor = true;
            // 
            // btnAgregarusuario
            // 
            this.btnAgregarusuario.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarusuario.Location = new System.Drawing.Point(3, 19);
            this.btnAgregarusuario.Name = "btnAgregarusuario";
            this.btnAgregarusuario.Size = new System.Drawing.Size(84, 36);
            this.btnAgregarusuario.TabIndex = 7;
            this.btnAgregarusuario.Text = "Agregar";
            this.btnAgregarusuario.UseVisualStyleBackColor = true;
            this.btnAgregarusuario.Click += new System.EventHandler(this.btnAgregarusuario_Click);
            // 
            // grbNavegacion
            // 
            this.grbNavegacion.Controls.Add(this.lblRegistrosusuario);
            this.grbNavegacion.Controls.Add(this.btnUltimousuario);
            this.grbNavegacion.Controls.Add(this.btnSiguienteusuario);
            this.grbNavegacion.Controls.Add(this.btnAnteriorusuario);
            this.grbNavegacion.Controls.Add(this.btnPrimerousuario);
            this.grbNavegacion.Controls.Add(this.groupBox3);
            this.grbNavegacion.Location = new System.Drawing.Point(9, 331);
            this.grbNavegacion.Name = "grbNavegacion";
            this.grbNavegacion.Size = new System.Drawing.Size(208, 61);
            this.grbNavegacion.TabIndex = 14;
            this.grbNavegacion.TabStop = false;
            this.grbNavegacion.Text = "Navegacion";
            // 
            // lblRegistrosusuario
            // 
            this.lblRegistrosusuario.AutoSize = true;
            this.lblRegistrosusuario.Location = new System.Drawing.Point(83, 37);
            this.lblRegistrosusuario.Name = "lblRegistrosusuario";
            this.lblRegistrosusuario.Size = new System.Drawing.Size(36, 13);
            this.lblRegistrosusuario.TabIndex = 10;
            this.lblRegistrosusuario.Text = "x de n";
            // 
            // btnUltimousuario
            // 
            this.btnUltimousuario.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUltimousuario.Location = new System.Drawing.Point(169, 20);
            this.btnUltimousuario.Name = "btnUltimousuario";
            this.btnUltimousuario.Size = new System.Drawing.Size(39, 41);
            this.btnUltimousuario.TabIndex = 6;
            this.btnUltimousuario.Text = ">|";
            this.btnUltimousuario.UseVisualStyleBackColor = true;
            // 
            // btnSiguienteusuario
            // 
            this.btnSiguienteusuario.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiguienteusuario.Location = new System.Drawing.Point(133, 20);
            this.btnSiguienteusuario.Name = "btnSiguienteusuario";
            this.btnSiguienteusuario.Size = new System.Drawing.Size(39, 41);
            this.btnSiguienteusuario.TabIndex = 5;
            this.btnSiguienteusuario.Text = ">";
            this.btnSiguienteusuario.UseVisualStyleBackColor = true;
            // 
            // btnAnteriorusuario
            // 
            this.btnAnteriorusuario.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnteriorusuario.Location = new System.Drawing.Point(38, 20);
            this.btnAnteriorusuario.Name = "btnAnteriorusuario";
            this.btnAnteriorusuario.Size = new System.Drawing.Size(39, 41);
            this.btnAnteriorusuario.TabIndex = 4;
            this.btnAnteriorusuario.Text = "<";
            this.btnAnteriorusuario.UseVisualStyleBackColor = true;
            // 
            // btnPrimerousuario
            // 
            this.btnPrimerousuario.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrimerousuario.Location = new System.Drawing.Point(2, 20);
            this.btnPrimerousuario.Name = "btnPrimerousuario";
            this.btnPrimerousuario.Size = new System.Drawing.Size(39, 41);
            this.btnPrimerousuario.TabIndex = 3;
            this.btnPrimerousuario.Text = "|<";
            this.btnPrimerousuario.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(214, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 61);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // grbDatos
            // 
            this.grbDatos.Controls.Add(this.txtConfirmarusuario);
            this.grbDatos.Controls.Add(this.label1);
            this.grbDatos.Controls.Add(this.idUsuario);
            this.grbDatos.Controls.Add(this.lblIdusuario);
            this.grbDatos.Controls.Add(this.txtClaveusuario);
            this.grbDatos.Controls.Add(this.lblClaveusuario);
            this.grbDatos.Controls.Add(this.txtTelefonousuario);
            this.grbDatos.Controls.Add(this.lblTelefonousuario);
            this.grbDatos.Controls.Add(this.txtDireccionusuario);
            this.grbDatos.Controls.Add(this.lblDireccionusuario);
            this.grbDatos.Controls.Add(this.txtNombreusuario);
            this.grbDatos.Controls.Add(this.lblNombreusuario);
            this.grbDatos.Controls.Add(this.txtUsuariousuario);
            this.grbDatos.Controls.Add(this.lblCodigousuario);
            this.grbDatos.Location = new System.Drawing.Point(9, 24);
            this.grbDatos.Name = "grbDatos";
            this.grbDatos.Size = new System.Drawing.Size(451, 290);
            this.grbDatos.TabIndex = 13;
            this.grbDatos.TabStop = false;
            this.grbDatos.Text = "DATOS";
            this.grbDatos.Enter += new System.EventHandler(this.grbDatos_Enter);
            // 
            // idUsuario
            // 
            this.idUsuario.AutoSize = true;
            this.idUsuario.Location = new System.Drawing.Point(98, 16);
            this.idUsuario.Name = "idUsuario";
            this.idUsuario.Size = new System.Drawing.Size(13, 13);
            this.idUsuario.TabIndex = 11;
            this.idUsuario.Text = "0";
            // 
            // lblIdusuario
            // 
            this.lblIdusuario.AutoSize = true;
            this.lblIdusuario.Location = new System.Drawing.Point(19, 16);
            this.lblIdusuario.Name = "lblIdusuario";
            this.lblIdusuario.Size = new System.Drawing.Size(54, 13);
            this.lblIdusuario.TabIndex = 10;
            this.lblIdusuario.Text = "idUsuario:";
            // 
            // txtClaveusuario
            // 
            this.txtClaveusuario.Location = new System.Drawing.Point(93, 79);
            this.txtClaveusuario.Name = "txtClaveusuario";
            this.txtClaveusuario.Size = new System.Drawing.Size(149, 20);
            this.txtClaveusuario.TabIndex = 9;
            // 
            // lblClaveusuario
            // 
            this.lblClaveusuario.AutoSize = true;
            this.lblClaveusuario.Location = new System.Drawing.Point(19, 82);
            this.lblClaveusuario.Name = "lblClaveusuario";
            this.lblClaveusuario.Size = new System.Drawing.Size(44, 13);
            this.lblClaveusuario.TabIndex = 8;
            this.lblClaveusuario.Text = "CLAVE:";
            this.lblClaveusuario.Click += new System.EventHandler(this.lblEmailAlumno_Click);
            // 
            // txtTelefonousuario
            // 
            this.txtTelefonousuario.Location = new System.Drawing.Point(86, 259);
            this.txtTelefonousuario.Name = "txtTelefonousuario";
            this.txtTelefonousuario.Size = new System.Drawing.Size(149, 20);
            this.txtTelefonousuario.TabIndex = 7;
            // 
            // lblTelefonousuario
            // 
            this.lblTelefonousuario.AutoSize = true;
            this.lblTelefonousuario.Location = new System.Drawing.Point(12, 262);
            this.lblTelefonousuario.Name = "lblTelefonousuario";
            this.lblTelefonousuario.Size = new System.Drawing.Size(67, 13);
            this.lblTelefonousuario.TabIndex = 6;
            this.lblTelefonousuario.Text = "TELEFONO:";
            // 
            // txtDireccionusuario
            // 
            this.txtDireccionusuario.Location = new System.Drawing.Point(86, 192);
            this.txtDireccionusuario.Multiline = true;
            this.txtDireccionusuario.Name = "txtDireccionusuario";
            this.txtDireccionusuario.Size = new System.Drawing.Size(272, 48);
            this.txtDireccionusuario.TabIndex = 5;
            // 
            // lblDireccionusuario
            // 
            this.lblDireccionusuario.AutoSize = true;
            this.lblDireccionusuario.Location = new System.Drawing.Point(12, 192);
            this.lblDireccionusuario.Name = "lblDireccionusuario";
            this.lblDireccionusuario.Size = new System.Drawing.Size(69, 13);
            this.lblDireccionusuario.TabIndex = 4;
            this.lblDireccionusuario.Text = "DIRECCION:";
            // 
            // txtNombreusuario
            // 
            this.txtNombreusuario.Location = new System.Drawing.Point(86, 154);
            this.txtNombreusuario.Name = "txtNombreusuario";
            this.txtNombreusuario.Size = new System.Drawing.Size(272, 20);
            this.txtNombreusuario.TabIndex = 3;
            // 
            // lblNombreusuario
            // 
            this.lblNombreusuario.AutoSize = true;
            this.lblNombreusuario.Location = new System.Drawing.Point(13, 157);
            this.lblNombreusuario.Name = "lblNombreusuario";
            this.lblNombreusuario.Size = new System.Drawing.Size(57, 13);
            this.lblNombreusuario.TabIndex = 2;
            this.lblNombreusuario.Text = "NOMBRE:";
            this.lblNombreusuario.Click += new System.EventHandler(this.lblNombreAlumno_Click);
            // 
            // txtUsuariousuario
            // 
            this.txtUsuariousuario.Location = new System.Drawing.Point(93, 43);
            this.txtUsuariousuario.Name = "txtUsuariousuario";
            this.txtUsuariousuario.Size = new System.Drawing.Size(149, 20);
            this.txtUsuariousuario.TabIndex = 1;
            // 
            // lblCodigousuario
            // 
            this.lblCodigousuario.AutoSize = true;
            this.lblCodigousuario.Location = new System.Drawing.Point(19, 46);
            this.lblCodigousuario.Name = "lblCodigousuario";
            this.lblCodigousuario.Size = new System.Drawing.Size(59, 13);
            this.lblCodigousuario.TabIndex = 0;
            this.lblCodigousuario.Text = "USUARIO:";
            // 
            // txtConfirmarusuario
            // 
            this.txtConfirmarusuario.Location = new System.Drawing.Point(101, 118);
            this.txtConfirmarusuario.Name = "txtConfirmarusuario";
            this.txtConfirmarusuario.Size = new System.Drawing.Size(149, 20);
            this.txtConfirmarusuario.TabIndex = 13;
            this.txtConfirmarusuario.TextChanged += new System.EventHandler(this.txtConfirmarusuario_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "CONFIRMAR:";
            // 
            // frm_usuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 450);
            this.Controls.Add(this.grbEdicion);
            this.Controls.Add(this.grbNavegacion);
            this.Controls.Add(this.grbDatos);
            this.Name = "frm_usuario";
            this.Text = "frm_usuario";
            this.grbEdicion.ResumeLayout(false);
            this.grbNavegacion.ResumeLayout(false);
            this.grbNavegacion.PerformLayout();
            this.grbDatos.ResumeLayout(false);
            this.grbDatos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grbEdicion;
        private System.Windows.Forms.Button btnEliminarusuario;
        private System.Windows.Forms.Button btnModificarusuario;
        private System.Windows.Forms.Button btnAgregarusuario;
        private System.Windows.Forms.GroupBox grbNavegacion;
        private System.Windows.Forms.Label lblRegistrosusuario;
        private System.Windows.Forms.Button btnUltimousuario;
        private System.Windows.Forms.Button btnSiguienteusuario;
        private System.Windows.Forms.Button btnAnteriorusuario;
        private System.Windows.Forms.Button btnPrimerousuario;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grbDatos;
        private System.Windows.Forms.Label idUsuario;
        private System.Windows.Forms.Label lblIdusuario;
        private System.Windows.Forms.TextBox txtClaveusuario;
        private System.Windows.Forms.Label lblClaveusuario;
        private System.Windows.Forms.TextBox txtTelefonousuario;
        private System.Windows.Forms.Label lblTelefonousuario;
        private System.Windows.Forms.TextBox txtDireccionusuario;
        private System.Windows.Forms.Label lblDireccionusuario;
        private System.Windows.Forms.TextBox txtNombreusuario;
        private System.Windows.Forms.Label lblNombreusuario;
        private System.Windows.Forms.TextBox txtUsuariousuario;
        private System.Windows.Forms.Label lblCodigousuario;
        private System.Windows.Forms.TextBox txtConfirmarusuario;
        private System.Windows.Forms.Label label1;
    }
}