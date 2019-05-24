using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SECFDI.KitDesarrollo.Genericos;
using SECFDI.KitDesarrollo.WCFCertificar;
using SECFDI.KitDesarrollo.Clases; 

namespace SECFDI.KitDesarrollo
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.ofdAbrirArchivo.ShowDialog();
            txtArchivoXML.Text = this.ofdAbrirArchivo.FileName;
            txtXMLTimbrado.Text = "";
        }

        private void btnTimbrar_Click(object sender, EventArgs e)
        {
            txtFechaInicio.Text = "";
            txtFechaTermino.Text = "";
            txtXMLTimbrado.Text = "";

            if (txtArchivoXML.Text.Trim().Length == 0)
            {
                MessageBox.Show("Seleccione un archivo antes de continuar.", "Error al Timbrar Documento");
            }
            else
            {
                txtFechaInicio.Text = DateTime.Now.ToString();
                
                Cursor.Show();
                Cursor.Current = Cursors.WaitCursor;

                CertificarClient certificar = new CertificarClient();                
                AutenticarPeticion autenticar = new AutenticarPeticion();
                TimbrarCFDIPeticion xml = new TimbrarCFDIPeticion();

                // Asigna los parametros de configuracion de conexion 
                autenticar.Usuario = "PruebasTimbrado";
                autenticar.Contrasenia = "@Notiene1";

                // abre el archivo XMl que fue seleccionado
                FileStream resultado = null;
                resultado = new FileStream(txtArchivoXML.Text, FileMode.Open, FileAccess.Read, FileShare.Read);
                using (StreamReader contenidoArchivo = new StreamReader(resultado))
                {
                    xml.XMLEntrada = contenidoArchivo.ReadToEnd();
                    contenidoArchivo.Close();
                }
                
                TimbrarCFDIRespuesta respuesta = certificar.TimbradoMultiEmpresas(autenticar, xml); 

                certificar.Close();

                txtFechaTermino.Text = DateTime.Now.ToString();

                if (respuesta.Estatus.Codigo == "000")
                {
                    txtXMLTimbrado.Text = respuesta.Timbrado.TimbreXML;
                }
                
                MessageBox.Show(respuesta.Estatus.Descripcion, "Generación CFDI");

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (txtFolioFiscal1.Text.Trim().Length == 0 && txtFolioFiscal2.Text.Trim().Length == 0)
            {
                MessageBox.Show("Ingrese al menos un Folio Fiscal antes de continuar.", "Error al cancelar el CFDI");
            }
            else
            {
                // Genera XMl con los Folios a enviar se pueden enviar uno o mas CFDI para cancelacion 
                // Solo se podran enviar CFDI timbrados con nuestro servicio no hay el limite de CFDI que se puean cancelar

                Cursor.Show();
                Cursor.Current = Cursors.WaitCursor;
              
                CertificarClient certificar = new CertificarClient();
                AutenticarPeticion autenticar = new AutenticarPeticion();
                CancelarCFDIPACPeticion cancelar = new CancelarCFDIPACPeticion();

                // Asigna los parametros de configuracion de conexion 
                autenticar.Usuario = "PruebasTimbrado";
                autenticar.Contrasenia = "@Notiene1";

                cancelar.RFC = "AAA010101AAA"; // RFC del emisor del CFDI;
                cancelar.Password = "12345678a";

                Archivos archivo = new Archivos();                
                cancelar.PFX = archivo.ConvertirStreamToByte(archivo.Abrir(ObtieneDirectorioAplicacion() + @"\Certificado\AAA010101AAA.pfx"));  //"Se debe de enviar el certificado en formato PFX para realizar la cancelacion";

                List<string> folios = new List<string>();

                if (!string.IsNullOrEmpty(txtFolioFiscal1.Text))
                    folios.Add(txtFolioFiscal1.Text.Trim());

                if (!string.IsNullOrEmpty(txtFolioFiscal2.Text))
                    folios.Add(txtFolioFiscal2.Text.Trim());

                cancelar.UUID = folios.ToArray(); 

                CancelarCFDIRespuesta respuesta =  certificar.CancelarCFDICualquierPAC(autenticar, cancelar);

                certificar.Close();

                txtFechaTermino.Text = DateTime.Now.ToString();

                Cursor.Current = Cursors.Default;

                // No hay limite para el envio de cancelaciones en el ejemplo solo en envian 3 pero pueden ser todos los que el usuario 
                // desee cancelar al mismo tiempo
                for (int contador = 0; contador < respuesta.FoliosRespuesta.Length; contador++)
			    {
                    if (respuesta.FoliosRespuesta[contador].Estatus != null)
                        MessageBox.Show(respuesta.FoliosRespuesta[contador].Estatus.Descripcion, "Generación CFDI");
			    }

            }
        }

        private static string ObtieneDirectorioAplicacion()
        {
            string cmdLine = Environment.CommandLine;
            string DirectorioInstalacionAplicacion = string.Empty;

            try
            {
                cmdLine = cmdLine.Replace("console", " ");
                cmdLine = cmdLine.Replace("\"", "");
                DirectorioInstalacionAplicacion = Path.GetDirectoryName(cmdLine);

                int indice = DirectorioInstalacionAplicacion.ToUpper().IndexOf("BIN");
                DirectorioInstalacionAplicacion = DirectorioInstalacionAplicacion.Substring(0, indice - 1);
            }
            catch (Exception)
            {
                cmdLine = cmdLine.Replace("console", " ");
                cmdLine = cmdLine.Replace("\"", "");
                DirectorioInstalacionAplicacion = Path.GetDirectoryName(cmdLine);
            }

            return DirectorioInstalacionAplicacion;
        }

        private void lblInicio_Click(object sender, EventArgs e)
        {

        }

        private void txtFechaInicio_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbltermino_Click(object sender, EventArgs e)
        {

        }

        private void txtFechaTermino_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtArchivoXML_TextChanged(object sender, EventArgs e)
        {

        }
    }
}



