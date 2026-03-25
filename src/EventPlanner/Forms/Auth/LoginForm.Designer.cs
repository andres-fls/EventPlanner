namespace EventPlanner
{
    partial class LoginForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelBase = new System.Windows.Forms.Panel();
            this.groupBoxlogin = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.linkRegistro = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInicio = new System.Windows.Forms.Label();
            this.panelBase.SuspendLayout();
            this.groupBoxlogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBase
            // 
            this.panelBase.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelBase.Controls.Add(this.groupBoxlogin);
            this.panelBase.Controls.Add(this.btnLogin);
            this.panelBase.Controls.Add(this.linkRegistro);
            this.panelBase.Controls.Add(this.label5);
            this.panelBase.Controls.Add(this.txtInicio);
            this.panelBase.Location = new System.Drawing.Point(0, 0);
            this.panelBase.Name = "panelBase";
            this.panelBase.Size = new System.Drawing.Size(800, 450);
            this.panelBase.TabIndex = 0;
            // 
            // groupBoxlogin
            // 
            this.groupBoxlogin.Controls.Add(this.label1);
            this.groupBoxlogin.Controls.Add(this.cmbRol);
            this.groupBoxlogin.Controls.Add(this.txtPassword);
            this.groupBoxlogin.Controls.Add(this.label2);
            this.groupBoxlogin.Controls.Add(this.txtUsuario);
            this.groupBoxlogin.Controls.Add(this.label3);
            this.groupBoxlogin.Location = new System.Drawing.Point(183, 92);
            this.groupBoxlogin.Name = "groupBoxlogin";
            this.groupBoxlogin.Size = new System.Drawing.Size(446, 194);
            this.groupBoxlogin.TabIndex = 20;
            this.groupBoxlogin.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Usuario o correo";
            // 
            // cmbRol
            // 
            this.cmbRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(173, 144);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(128, 28);
            this.cmbRol.TabIndex = 17;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(173, 82);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(257, 26);
            this.txtPassword.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(173, 25);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(257, 26);
            this.txtUsuario.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Tipo Usuario";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.SeaGreen;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(366, 314);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(80, 30);
            this.btnLogin.TabIndex = 16;
            this.btnLogin.Text = "Ingresar";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // linkRegistro
            // 
            this.linkRegistro.AutoSize = true;
            this.linkRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkRegistro.Location = new System.Drawing.Point(415, 400);
            this.linkRegistro.Name = "linkRegistro";
            this.linkRegistro.Size = new System.Drawing.Size(119, 20);
            this.linkRegistro.TabIndex = 15;
            this.linkRegistro.TabStop = true;
            this.linkRegistro.Text = "Registrate Aqui";
            this.linkRegistro.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRegistro_LinkClicked);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(262, 400);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "¿No tienes cuenta?";
            // 
            // txtInicio
            // 
            this.txtInicio.AutoSize = true;
            this.txtInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInicio.Location = new System.Drawing.Point(302, 33);
            this.txtInicio.Name = "txtInicio";
            this.txtInicio.Size = new System.Drawing.Size(209, 29);
            this.txtInicio.TabIndex = 10;
            this.txtInicio.Text = "INICIAR SESION";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelBase);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EventPlanner";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panelBase.ResumeLayout(false);
            this.panelBase.PerformLayout();
            this.groupBoxlogin.ResumeLayout(false);
            this.groupBoxlogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBase;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.LinkLabel linkRegistro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtInicio;
        private System.Windows.Forms.GroupBox groupBoxlogin;
    }
}

