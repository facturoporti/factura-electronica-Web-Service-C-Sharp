namespace SECFDI.KitDesarrollo
{
    partial class frmGeneraCFDI
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
            this.txtFechaTermino = new System.Windows.Forms.TextBox();
            this.txtFechaInicio = new System.Windows.Forms.TextBox();
            this.lbltermino = new System.Windows.Forms.Label();
            this.lblInicio = new System.Windows.Forms.Label();
            this.cmbOpcion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnTimbrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.wbVisorTimbre = new System.Windows.Forms.WebBrowser();
            this.wbVisorCFDI = new System.Windows.Forms.WebBrowser();
            this.btnPDF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFechaTermino
            // 
            this.txtFechaTermino.Location = new System.Drawing.Point(153, 55);
            this.txtFechaTermino.Margin = new System.Windows.Forms.Padding(2);
            this.txtFechaTermino.Name = "txtFechaTermino";
            this.txtFechaTermino.Size = new System.Drawing.Size(136, 20);
            this.txtFechaTermino.TabIndex = 11;
            // 
            // txtFechaInicio
            // 
            this.txtFechaInicio.Location = new System.Drawing.Point(153, 25);
            this.txtFechaInicio.Margin = new System.Windows.Forms.Padding(2);
            this.txtFechaInicio.Name = "txtFechaInicio";
            this.txtFechaInicio.Size = new System.Drawing.Size(136, 20);
            this.txtFechaInicio.TabIndex = 10;
            // 
            // lbltermino
            // 
            this.lbltermino.AutoSize = true;
            this.lbltermino.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbltermino.Location = new System.Drawing.Point(79, 60);
            this.lbltermino.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbltermino.Name = "lbltermino";
            this.lbltermino.Size = new System.Drawing.Size(71, 13);
            this.lbltermino.TabIndex = 9;
            this.lbltermino.Text = "Hora Termino";
            // 
            // lblInicio
            // 
            this.lblInicio.AutoSize = true;
            this.lblInicio.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblInicio.Location = new System.Drawing.Point(92, 27);
            this.lblInicio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(58, 13);
            this.lblInicio.TabIndex = 8;
            this.lblInicio.Text = "Hora Inicio";
            // 
            // cmbOpcion
            // 
            this.cmbOpcion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpcion.FormattingEnabled = true;
            this.cmbOpcion.Location = new System.Drawing.Point(153, 85);
            this.cmbOpcion.Name = "cmbOpcion";
            this.cmbOpcion.Size = new System.Drawing.Size(263, 21);
            this.cmbOpcion.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Tipo de CFDI 3.3";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(9, 295);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "XML Timbrado";
            // 
            // btnTimbrar
            // 
            this.btnTimbrar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTimbrar.Location = new System.Drawing.Point(528, 69);
            this.btnTimbrar.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimbrar.Name = "btnTimbrar";
            this.btnTimbrar.Size = new System.Drawing.Size(99, 37);
            this.btnTimbrar.TabIndex = 14;
            this.btnTimbrar.Text = "Generar CFDI";
            this.btnTimbrar.UseVisualStyleBackColor = true;
            this.btnTimbrar.Click += new System.EventHandler(this.btnTimbrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(12, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Timbre XML";
            // 
            // wbVisorTimbre
            // 
            this.wbVisorTimbre.Location = new System.Drawing.Point(15, 171);
            this.wbVisorTimbre.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbVisorTimbre.Name = "wbVisorTimbre";
            this.wbVisorTimbre.ScriptErrorsSuppressed = true;
            this.wbVisorTimbre.Size = new System.Drawing.Size(612, 111);
            this.wbVisorTimbre.TabIndex = 19;
            // 
            // wbVisorCFDI
            // 
            this.wbVisorCFDI.Location = new System.Drawing.Point(15, 311);
            this.wbVisorCFDI.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbVisorCFDI.Name = "wbVisorCFDI";
            this.wbVisorCFDI.ScriptErrorsSuppressed = true;
            this.wbVisorCFDI.Size = new System.Drawing.Size(612, 260);
            this.wbVisorCFDI.TabIndex = 20;
            // 
            // btnPDF
            // 
            this.btnPDF.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPDF.Location = new System.Drawing.Point(528, 129);
            this.btnPDF.Margin = new System.Windows.Forms.Padding(2);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(99, 37);
            this.btnPDF.TabIndex = 21;
            this.btnPDF.Text = "Ver PDF";
            this.btnPDF.UseVisualStyleBackColor = true;
            this.btnPDF.Visible = false;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // frmGeneraCFDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 583);
            this.Controls.Add(this.btnPDF);
            this.Controls.Add(this.wbVisorCFDI);
            this.Controls.Add(this.wbVisorTimbre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnTimbrar);
            this.Controls.Add(this.cmbOpcion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFechaTermino);
            this.Controls.Add(this.txtFechaInicio);
            this.Controls.Add(this.lbltermino);
            this.Controls.Add(this.lblInicio);
            this.Name = "frmGeneraCFDI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servicio Web para Generar el CFDI 3.3, Crear  PDF, Envia Correo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFechaTermino;
        private System.Windows.Forms.TextBox txtFechaInicio;
        private System.Windows.Forms.Label lbltermino;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.ComboBox cmbOpcion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnTimbrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser wbVisorTimbre;
        private System.Windows.Forms.WebBrowser wbVisorCFDI;
        private System.Windows.Forms.Button btnPDF;
    }
}