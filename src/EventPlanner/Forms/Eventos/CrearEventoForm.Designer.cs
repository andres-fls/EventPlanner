namespace EventPlanner
{
    partial class CrearEventoForm
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
            this.panelBase = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpHora = new System.Windows.Forms.DateTimePicker();
            this.lblHora = new System.Windows.Forms.Label();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblCupo = new System.Windows.Forms.Label();
            this.numCupo = new System.Windows.Forms.NumericUpDown();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblLugar = new System.Windows.Forms.Label();
            this.txtLugar = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBase.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCupo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBase
            // 
            this.panelBase.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelBase.Controls.Add(this.groupBox2);
            this.panelBase.Controls.Add(this.groupBox1);
            this.panelBase.Controls.Add(this.btnCancelar);
            this.panelBase.Controls.Add(this.btnGuardar);
            this.panelBase.Controls.Add(this.label1);
            this.panelBase.Location = new System.Drawing.Point(0, 0);
            this.panelBase.Name = "panelBase";
            this.panelBase.Size = new System.Drawing.Size(800, 450);
            this.panelBase.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpHora);
            this.groupBox2.Controls.Add(this.lblHora);
            this.groupBox2.Controls.Add(this.chkActivo);
            this.groupBox2.Controls.Add(this.dtpFecha);
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.lblFecha);
            this.groupBox2.Controls.Add(this.lblCupo);
            this.groupBox2.Controls.Add(this.numCupo);
            this.groupBox2.Controls.Add(this.cmbTipo);
            this.groupBox2.Controls.Add(this.lblTipo);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(460, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 270);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            // 
            // dtpHora
            // 
            this.dtpHora.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHora.Location = new System.Drawing.Point(148, 70);
            this.dtpHora.Name = "dtpHora";
            this.dtpHora.ShowUpDown = true;
            this.dtpHora.Size = new System.Drawing.Size(137, 26);
            this.dtpHora.TabIndex = 12;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Location = new System.Drawing.Point(6, 75);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(44, 20);
            this.lblHora.TabIndex = 5;
            this.lblHora.Text = "Hora";
            // 
            // chkActivo
            // 
            this.chkActivo.AutoSize = true;
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivo.Location = new System.Drawing.Point(160, 230);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(125, 24);
            this.chkActivo.TabIndex = 18;
            this.chkActivo.Text = "Evento Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(148, 17);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(137, 26);
            this.dtpFecha.TabIndex = 10;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(6, 234);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(60, 20);
            this.lblEstado.TabIndex = 17;
            this.lblEstado.Text = "Estado";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(6, 22);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(54, 20);
            this.lblFecha.TabIndex = 4;
            this.lblFecha.Text = "Fecha";
            // 
            // lblCupo
            // 
            this.lblCupo.AutoSize = true;
            this.lblCupo.Location = new System.Drawing.Point(6, 131);
            this.lblCupo.Name = "lblCupo";
            this.lblCupo.Size = new System.Drawing.Size(105, 20);
            this.lblCupo.TabIndex = 6;
            this.lblCupo.Text = "Cupo maximo";
            // 
            // numCupo
            // 
            this.numCupo.Location = new System.Drawing.Point(184, 125);
            this.numCupo.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numCupo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCupo.Name = "numCupo";
            this.numCupo.Size = new System.Drawing.Size(101, 26);
            this.numCupo.TabIndex = 13;
            this.numCupo.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(164, 180);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(121, 28);
            this.cmbTipo.TabIndex = 14;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(6, 188);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(117, 20);
            this.lblTipo.TabIndex = 7;
            this.lblTipo.Text = "Tipo inscripcion";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblNombre);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.lblDescripcion);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Controls.Add(this.lblLugar);
            this.groupBox1.Controls.Add(this.txtLugar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(38, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 322);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(6, 31);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(144, 20);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre del Evento";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(156, 28);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(241, 26);
            this.txtNombre.TabIndex = 8;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(6, 98);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(92, 20);
            this.lblDescripcion.TabIndex = 2;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(156, 95);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(241, 137);
            this.txtDescripcion.TabIndex = 9;
            // 
            // lblLugar
            // 
            this.lblLugar.AutoSize = true;
            this.lblLugar.Location = new System.Drawing.Point(6, 275);
            this.lblLugar.Name = "lblLugar";
            this.lblLugar.Size = new System.Drawing.Size(50, 20);
            this.lblLugar.TabIndex = 3;
            this.lblLugar.Text = "Lugar";
            // 
            // txtLugar
            // 
            this.txtLugar.Location = new System.Drawing.Point(156, 272);
            this.txtLugar.Name = "txtLugar";
            this.txtLugar.Size = new System.Drawing.Size(241, 26);
            this.txtLugar.TabIndex = 11;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(584, 350);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 35);
            this.btnCancelar.TabIndex = 16;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(470, 350);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(80, 35);
            this.btnGuardar.TabIndex = 15;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(321, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "CREAR EVENTO";
            // 
            // CrearEventoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.panelBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "CrearEventoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EventPlanner";
            this.Load += new System.EventHandler(this.CrearEventoForm_Load);
            this.panelBase.ResumeLayout(false);
            this.panelBase.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCupo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBase;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblCupo;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblLugar;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtLugar;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.DateTimePicker dtpHora;
        private System.Windows.Forms.NumericUpDown numCupo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}