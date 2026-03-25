namespace EventPlanner
{
    partial class UsuariosForm
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
            this.btnVolver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCargar = new System.Windows.Forms.Button();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.btnDetalle = new System.Windows.Forms.Button();
            this.dgvInscripciones = new System.Windows.Forms.DataGridView();
            this.btnActivar = new System.Windows.Forms.Button();
            this.btnDesactivar = new System.Windows.Forms.Button();
            this.cmbEvento = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBase
            // 
            this.panelBase.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelBase.Controls.Add(this.groupBox1);
            this.panelBase.Controls.Add(this.label2);
            this.panelBase.Controls.Add(this.cmbEvento);
            this.panelBase.Controls.Add(this.btnVolver);
            this.panelBase.Controls.Add(this.label1);
            this.panelBase.Controls.Add(this.btnFiltrar);
            this.panelBase.Controls.Add(this.dgvInscripciones);
            this.panelBase.Location = new System.Drawing.Point(0, 0);
            this.panelBase.Name = "panelBase";
            this.panelBase.Size = new System.Drawing.Size(784, 411);
            this.panelBase.TabIndex = 0;
            // 
            // btnVolver
            // 
            this.btnVolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.Location = new System.Drawing.Point(697, 364);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(75, 35);
            this.btnVolver.TabIndex = 10;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(282, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Gestion de Aprendices";
            // 
            // btnCargar
            // 
            this.btnCargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargar.Location = new System.Drawing.Point(14, 72);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(102, 35);
            this.btnCargar.TabIndex = 8;
            this.btnCargar.Text = "Cargar";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(274, 76);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(80, 35);
            this.btnFiltrar.TabIndex = 7;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnDetalle
            // 
            this.btnDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetalle.Location = new System.Drawing.Point(14, 19);
            this.btnDetalle.Name = "btnDetalle";
            this.btnDetalle.Size = new System.Drawing.Size(102, 35);
            this.btnDetalle.TabIndex = 6;
            this.btnDetalle.Text = "Ver detalle";
            this.btnDetalle.UseVisualStyleBackColor = true;
            this.btnDetalle.Click += new System.EventHandler(this.btnDetalle_Click);
            // 
            // dgvInscripciones
            // 
            this.dgvInscripciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInscripciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInscripciones.Location = new System.Drawing.Point(12, 117);
            this.dgvInscripciones.Name = "dgvInscripciones";
            this.dgvInscripciones.Size = new System.Drawing.Size(594, 228);
            this.dgvInscripciones.TabIndex = 5;
            // 
            // btnActivar
            // 
            this.btnActivar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivar.Location = new System.Drawing.Point(14, 123);
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.Size = new System.Drawing.Size(102, 35);
            this.btnActivar.TabIndex = 11;
            this.btnActivar.Text = "Activar";
            this.btnActivar.UseVisualStyleBackColor = true;
            this.btnActivar.Click += new System.EventHandler(this.btnActivar_Click);
            // 
            // btnDesactivar
            // 
            this.btnDesactivar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesactivar.Location = new System.Drawing.Point(14, 176);
            this.btnDesactivar.Name = "btnDesactivar";
            this.btnDesactivar.Size = new System.Drawing.Size(102, 35);
            this.btnDesactivar.TabIndex = 12;
            this.btnDesactivar.Text = "Desactivar";
            this.btnDesactivar.UseVisualStyleBackColor = true;
            this.btnDesactivar.Click += new System.EventHandler(this.btnDesactivar_Click);
            // 
            // cmbEvento
            // 
            this.cmbEvento.FormattingEnabled = true;
            this.cmbEvento.Location = new System.Drawing.Point(116, 85);
            this.cmbEvento.Name = "cmbEvento";
            this.cmbEvento.Size = new System.Drawing.Size(121, 21);
            this.cmbEvento.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "Evento";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDetalle);
            this.groupBox1.Controls.Add(this.btnCargar);
            this.groupBox1.Controls.Add(this.btnActivar);
            this.groupBox1.Controls.Add(this.btnDesactivar);
            this.groupBox1.Location = new System.Drawing.Point(632, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(130, 228);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // UsuariosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.panelBase);
            this.Name = "UsuariosForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EventPlanner";
            this.Load += new System.EventHandler(this.UsuariosForm_Load);
            this.panelBase.ResumeLayout(false);
            this.panelBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnDetalle;
        private System.Windows.Forms.DataGridView dgvInscripciones;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.ComboBox cmbEvento;
        private System.Windows.Forms.Button btnDesactivar;
        private System.Windows.Forms.Button btnActivar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}