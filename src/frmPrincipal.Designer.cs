namespace SECFDI.KitDesarrollo
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtXMLTimbrado = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFechaTermino = new System.Windows.Forms.TextBox();
            this.txtFechaInicio = new System.Windows.Forms.TextBox();
            this.lbltermino = new System.Windows.Forms.Label();
            this.lblInicio = new System.Windows.Forms.Label();
            this.btnTimbrar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtArchivoXML = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ofdAbrirArchivo = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFolioFiscal2 = new System.Windows.Forms.TextBox();
            this.txtFolioFiscal1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtXMLTimbrado);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtFechaTermino);
            this.groupBox1.Controls.Add(this.txtFechaInicio);
            this.groupBox1.Controls.Add(this.lbltermino);
            this.groupBox1.Controls.Add(this.lblInicio);
            this.groupBox1.Controls.Add(this.btnTimbrar);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtArchivoXML);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtXMLTimbrado
            // 
            resources.ApplyResources(this.txtXMLTimbrado, "txtXMLTimbrado");
            this.txtXMLTimbrado.Name = "txtXMLTimbrado";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtFechaTermino
            // 
            resources.ApplyResources(this.txtFechaTermino, "txtFechaTermino");
            this.txtFechaTermino.Name = "txtFechaTermino";
            this.txtFechaTermino.TextChanged += new System.EventHandler(this.txtFechaTermino_TextChanged);
            // 
            // txtFechaInicio
            // 
            resources.ApplyResources(this.txtFechaInicio, "txtFechaInicio");
            this.txtFechaInicio.Name = "txtFechaInicio";
            this.txtFechaInicio.TextChanged += new System.EventHandler(this.txtFechaInicio_TextChanged);
            // 
            // lbltermino
            // 
            resources.ApplyResources(this.lbltermino, "lbltermino");
            this.lbltermino.Name = "lbltermino";
            this.lbltermino.Click += new System.EventHandler(this.lbltermino_Click);
            // 
            // lblInicio
            // 
            resources.ApplyResources(this.lblInicio, "lblInicio");
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Click += new System.EventHandler(this.lblInicio_Click);
            // 
            // btnTimbrar
            // 
            resources.ApplyResources(this.btnTimbrar, "btnTimbrar");
            this.btnTimbrar.Name = "btnTimbrar";
            this.btnTimbrar.UseVisualStyleBackColor = true;
            this.btnTimbrar.Click += new System.EventHandler(this.btnTimbrar_Click);
            // 
            // btnBuscar
            // 
            resources.ApplyResources(this.btnBuscar, "btnBuscar");
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtArchivoXML
            // 
            resources.ApplyResources(this.txtArchivoXML, "txtArchivoXML");
            this.txtArchivoXML.Name = "txtArchivoXML";
            this.txtArchivoXML.TextChanged += new System.EventHandler(this.txtArchivoXML_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ofdAbrirArchivo
            // 
            this.ofdAbrirArchivo.FileName = "openFileDialog1";
            resources.ApplyResources(this.ofdAbrirArchivo, "ofdAbrirArchivo");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtFolioFiscal2);
            this.groupBox2.Controls.Add(this.txtFolioFiscal1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnCancelar);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtFolioFiscal2
            // 
            resources.ApplyResources(this.txtFolioFiscal2, "txtFolioFiscal2");
            this.txtFolioFiscal2.Name = "txtFolioFiscal2";
            // 
            // txtFolioFiscal1
            // 
            resources.ApplyResources(this.txtFolioFiscal1, "txtFolioFiscal1");
            this.txtFolioFiscal1.Name = "txtFolioFiscal1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnCancelar
            // 
            resources.ApplyResources(this.btnCancelar, "btnCancelar");
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmPrincipal
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.IsMdiContainer = true;
            this.Name = "frmPrincipal";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtArchivoXML;
        private System.Windows.Forms.OpenFileDialog ofdAbrirArchivo;
        private System.Windows.Forms.Button btnTimbrar;
        private System.Windows.Forms.TextBox txtFechaTermino;
        private System.Windows.Forms.TextBox txtFechaInicio;
        private System.Windows.Forms.Label lbltermino;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtFolioFiscal2;
        private System.Windows.Forms.TextBox txtFolioFiscal1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox txtXMLTimbrado;
    }
}