using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SECFDI.KitDesarrollo.Clases;
using System.IO;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace SECFDI.KitDesarrollo
{
    public partial class frmGeneraCFDI : Form
    {
        private string PDFGenerado { get; set; }

        public frmGeneraCFDI()
        {
            InitializeComponent();
            GeneraListaCFDIs();
        }

        private void GeneraListaCFDIs()
        {
            List<Generica> listaTipoCFDI = new List<Generica>();
            listaTipoCFDI.Add( new Generica { Clave = "1", Descripcion = "Factura"});
            listaTipoCFDI.Add(new Generica { Clave = "2", Descripcion = "Nota de Cargo" });
            listaTipoCFDI.Add(new Generica { Clave = "3", Descripcion = "Nota de Crédito" });
            listaTipoCFDI.Add(new Generica { Clave = "4", Descripcion = "Recibo de Honorarios" });
            listaTipoCFDI.Add(new Generica { Clave = "5", Descripcion = "Recibo de Arrendamientos" });
            listaTipoCFDI.Add(new Generica { Clave = "6", Descripcion = "Recibo de Donativos" });

            listaTipoCFDI.Add(new Generica { Clave = "7", Descripcion = "Recibo de Nómina Simple " });
            listaTipoCFDI.Add(new Generica { Clave = "8", Descripcion = "Recibo de Nómina + Otros Pagos" });
            listaTipoCFDI.Add(new Generica { Clave = "9", Descripcion = "Recibo de Nómina + Horas Extras" });
            listaTipoCFDI.Add(new Generica { Clave = "10", Descripcion = "Recibo de Nómina + Incapacidad" });
            listaTipoCFDI.Add(new Generica { Clave = "11", Descripcion = "Recibo de Nómina + Separación o Indemnización" });
            listaTipoCFDI.Add(new Generica { Clave = "12", Descripcion = "Recibo de Nómina + Jubilacion, Pension, Retiro" });
            listaTipoCFDI.Add(new Generica { Clave = "13", Descripcion = "Recibo de Nómina + Todas Anteriores" });

            listaTipoCFDI.Add(new Generica { Clave = "14", Descripcion = "Carta Porte" });
            listaTipoCFDI.Add(new Generica { Clave = "15", Descripcion = "Complemento de Pagos" });
            
            cmbOpcion.DisplayMember = "Descripcion";
            cmbOpcion.ValueMember = "Clave";
            cmbOpcion.DataSource = listaTipoCFDI;
            cmbOpcion.SelectedItem = "1";
        }

        private string ObtieneCFDI()
        {
            string resultado = string.Empty;
            
            switch (cmbOpcion.SelectedValue.ToString())
            {
                case "1":

                    resultado = "Factura";
                    break;

                case "2":
                    resultado = "NotaCargo";
                    break;

                case "3":
                    resultado = "NotaCredito";
                    break;

                case "4":
                    resultado = "ReciboHonorarios";                    
                    break;

                case "5":
                    resultado = "ReciboArrendamiento";
                    
                    break;

                case "6":
                    resultado = "ReciboDonativo";

                    break;

                case "7":
                case "8":
                case "9":
                case "10":
                case "11":
                case "12":
                case "13":
                    resultado = "Nomina";
                    break;

                case "14":
                    resultado = "CartaPorte";                    
                    break;

                case "15":
                    resultado = "Pago";
                    break;
            }

            return resultado;
        }

        private string ObtieneTipoCFDI()
        {
            string resultado = string.Empty;

            switch (cmbOpcion.SelectedValue.ToString())
            {
                case "1":

                    resultado = "Ingreso";
                    break;

                case "2":
                    resultado = "Ingreso";
                    break;

                case "3":
                    resultado = "Egreso";
                    break;

                case "4":
                    resultado = "Ingreso";
                    break;

                case "5":
                    resultado = "Ingreso";

                    break;

                case "6":
                    resultado = "Ingreso";

                    break;

                case "7":
                case "8":
                case "9":
                case "10":
                case "11":
                case "12":
                case "13":
                    resultado = "Nomina";
                    break;

                case "14":
                    resultado = "Traslado";
                    break;

                case "15":
                    resultado = "Pago";
                    break;
            }

            return resultado;
        }

        private void btnTimbrar_Click(object sender, EventArgs e)
        {
            GeneraCFDI();
        }

        private void GeneraCFDI()
        {
            Archivos admnistrador = new Archivos();
            
            WCFCertificar.CertificarClient cliente = new WCFCertificar.CertificarClient();
            WCFCertificar.GeneraCFDIPeticion parametros = new WCFCertificar.GeneraCFDIPeticion();

            //wbVisorTimbre.DocumentText = string.Empty;
            //wbVisorCFDI.DocumentText = string.Empty;
            btnPDF.Visible = false;

            Cursor.Show();
            Cursor.Current = Cursors.WaitCursor;
            txtFechaInicio.Text = DateTime.Now.ToString();
            parametros = GeneraDatosCFDI();
            
            var cadena = new JavaScriptSerializer().Serialize(parametros);
            var respuesta = cliente.GeneraCFDI(parametros);
            txtFechaTermino.Text = DateTime.Now.ToString();

            //Estos son los valores que retorna el servicio 
            //IdVersionTimbrado
            //Fecha
            //CadenaOriginal
            //CadenaOriginalCFD
            //NoCertificado
            //RfcProvCertif
            //SelloCFD
            //SelloSAT
            //TimbreXML -- -- Se envía la respuesta en basBase 64
            //UUID 
            //CFDIXML -- Se envía la respuesta en basBase 64
            //PDF --- Se envía solo en caso de que se marco que se generara el PDF el archivo esta en base 64
            //EmailEnviado -- Solo si se timbro y generó el PDF

            if (respuesta.Estatus.Codigo == "000")
            {
                wbVisorTimbre.DocumentText = admnistrador.ConvertirBase64ToString(ObtieneAtributo(respuesta.CFDITimbrado.Respuesta.Timbre.ToList(), "TimbreXML"));
                wbVisorCFDI.DocumentText = admnistrador.ConvertirBase64ToString(ObtieneAtributo(respuesta.CFDITimbrado.Respuesta.Timbre.ToList(), "CFDIXML"));

                if (!string.IsNullOrEmpty(ObtieneAtributo(respuesta.CFDITimbrado.Respuesta.Timbre.ToList(), "PDF")))
                {
                    byte[] PDF = admnistrador.ConvertirBase64ToByte(ObtieneAtributo(respuesta.CFDITimbrado.Respuesta.Timbre.ToList(), "PDF"));
                    PDFGenerado = ObtieneDirectorioAplicacion() + @"\PDF\" + DateTime.Now.ToString("yyyy-MM-dd") + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";
                    admnistrador.Guardar(PDF, PDFGenerado);
                    btnPDF.Visible = true;
                }

                MessageBox.Show("Archivos Generados con Éxito", "Atención");
            }
            else
            {
                MessageBox.Show(respuesta.Estatus.Descripcion, "Atención");
            }

            Cursor.Current = Cursors.Default;            
        }

        private WCFCertificar.GeneraCFDIPeticion GeneraDatosCFDI()
        {
            WCFCertificar.GeneraCFDIPeticion respuesta = new WCFCertificar.GeneraCFDIPeticion();

            switch (cmbOpcion.SelectedValue.ToString())
            {
                case "1":
                case "2":
                case "3":
                    respuesta = LlenaCFDI();
                    break;
                case "4":                    
                    respuesta = LlenaHonorarios();                    
                    break;
                case "5":
                    respuesta = LlenaCFDI();
                    break;

                case "6":
                    respuesta = LlenaDonativo();                    
                    break;
                case "7":
                case "8":
                case "9":
                case "10":
                case "11":
                case "12":
                case "13":
                    respuesta = LlenaNomina();                    
                    break;

                case "14":
                    respuesta = LlenaCartaPorte();
                    break;

                case "15":
                    respuesta = LlenaComplementoPagos();
                    break;
            }            
            return respuesta;
        }
        
        private WCFCertificar.GeneraCFDIPeticion LlenaCFDI()
        {
            WCFCertificar.GeneraCFDIPeticion parametro = new WCFCertificar.GeneraCFDIPeticion();
            parametro.DatosGenerales =  new WCFCertificar.DatosGenerales();
            Archivos archivo = new Archivos();

            #region "Datos Generales"

            parametro.DatosGenerales.Configuracion = new Dictionary<string, string>();

            parametro.DatosGenerales.Configuracion.Add("Version", "3.3");
            parametro.DatosGenerales.Configuracion.Add("Usuario", "PruebasTimbrado");
            parametro.DatosGenerales.Configuracion.Add("Password", "@Notiene1");
            parametro.DatosGenerales.Configuracion.Add("SellaCFDI", "true");
            parametro.DatosGenerales.Configuracion.Add("TimbraCFDI", "true");

            parametro.DatosGenerales.Configuracion.Add("CSD", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Certificado\AAA010101AAA.cer"));
            parametro.DatosGenerales.Configuracion.Add("LlavePrivada", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Certificado\AAA010101AAA.key"));

            //parametro.DatosGenerales.Configuracion.Add("CSD", "1111");
            //parametro.DatosGenerales.Configuracion.Add("LlavePrivada", "2222");

            parametro.DatosGenerales.Configuracion.Add("CSDPassword", "12345678a");
            parametro.DatosGenerales.Configuracion.Add("GeneraPDF", "true");
            parametro.DatosGenerales.Configuracion.Add("Logotipo", "3333");
            parametro.DatosGenerales.Configuracion.Add("FormatoImpresion", "");

            parametro.DatosGenerales.Configuracion.Add("CFDI", ObtieneCFDI());
            parametro.DatosGenerales.Configuracion.Add("OpcionDecimales", "1");
            parametro.DatosGenerales.Configuracion.Add("NumeroDecimales", "2");
            parametro.DatosGenerales.Configuracion.Add("TipoCFDI", ObtieneTipoCFDI());

            parametro.DatosGenerales.Configuracion.Add("EnviaEmail", "false");
            parametro.DatosGenerales.Configuracion.Add("ReceptorEmail", "correo@dominio.com");            
            parametro.DatosGenerales.Configuracion.Add("EmailMensaje", "CFDI de Prueba enviado desde facturoporti.com.mx");

            #endregion "Datos Generales"

            #region "Encabezado"

            parametro.Encabezado = new Dictionary<string, string>();

            parametro.Encabezado.Add("EmisorRFC", "AAA010101AAA");
            parametro.Encabezado.Add("EmisorNombreRazonSocial", "Empresa Patito");
            parametro.Encabezado.Add("RegimenFiscal", "601 - General Ley de las Personas Morales");

            parametro.Encabezado.Add("EmisorDireccionCalle", "Serapio Rendon");
            parametro.Encabezado.Add("EmisorDireccionNumeroExterior", "122");
            parametro.Encabezado.Add("EmisorDireccionNumeroInterior", "5");
            parametro.Encabezado.Add("EmisorDireccionColonia", "San Rafael");
            parametro.Encabezado.Add("EmisorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("EmisorDireccionMunicipio", "Cuauhtemoc");
            parametro.Encabezado.Add("EmisorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("EmisorDireccionPais", "México");
            parametro.Encabezado.Add("EmisorDireccionCodigoPostal", "06470");

            parametro.Encabezado.Add("ReceptorRFC", "SSF1103037F1");
            parametro.Encabezado.Add("ReceptorNombreRazonSocial", "Scafandra Software Factory SA de CV");
            parametro.Encabezado.Add("ReceptorUsoCFDI", "P01 - Por Definir");
    
            parametro.Encabezado.Add("ReceptorDireccionCalle", "Reforma");
            parametro.Encabezado.Add("ReceptorDireccionNumeroExterior", "6283");
            parametro.Encabezado.Add("ReceptorDireccionNumeroInterior", "A");
            parametro.Encabezado.Add("ReceptorDireccionColonia", "Centro");
            parametro.Encabezado.Add("ReceptorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("ReceptorDireccionMunicipio", "Benito Juarez");
            parametro.Encabezado.Add("ReceptorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("ReceptorDireccionPais", "México");
            parametro.Encabezado.Add("ReceptorDireccionCodigoPostal", "62774");

            parametro.Encabezado.Add("Fecha", DateTime.Now.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss"));
            parametro.Encabezado.Add("Serie", "AB");
            parametro.Encabezado.Add("Folio", "1");
            parametro.Encabezado.Add("MetodoPago", "PUE - Pago en una sola exhibición");
            parametro.Encabezado.Add("FormaPago", "99-Por Definir");

            parametro.Encabezado.Add("CondicionesPago", "Crédito");
            parametro.Encabezado.Add("Moneda", "MXN");
            parametro.Encabezado.Add("TipoCambio", "1");
            parametro.Encabezado.Add("LugarExpedicion", "06470");
            parametro.Encabezado.Add("SubTotal", "100.00");
            parametro.Encabezado.Add("Total", "116");
            parametro.Encabezado.Add("Descuento", "0");
            parametro.Encabezado.Add("CFDIsRelacionados", "52B4E070-CD29-4584-BF0D-5CE501532791;342C6218-0A94-4F1E-AC38-1ED54F4171BB;");
            parametro.Encabezado.Add("TipoRelacion", "04 Sustitución de los CFDI previos");

            #endregion "Encabezado"

            #region "Detalle"

            parametro.Detalle = new Dictionary<string, string>();

            parametro.Detalle.Add("ConceptoCantidad_1", "1");
            parametro.Detalle.Add("ConceptoCodigoUnidad_1", "E48");
            parametro.Detalle.Add("ConceptoUnidad_1", "Servicio");
            parametro.Detalle.Add("ConceptoSerie_1", "1234ABC");
            parametro.Detalle.Add("ConceptoCodigoProducto_1", "84111506");
            parametro.Detalle.Add("ConceptoProducto_1", "Timbres de Facturación");
            parametro.Detalle.Add("ConceptoPrecioUnitario_1", "100");
            parametro.Detalle.Add("ConceptoImporte_1", "100");
            parametro.Detalle.Add("ConceptoDescuento_1", "");
            parametro.Detalle.Add("ConceptoCuentaPredial_1", "12039412");

            parametro.Detalle.Add("ConceptoCampo_1_Adicional_1", "Campo 1");
            parametro.Detalle.Add("ConceptoCampo_1_Adicional_2", "Campo 2");
            parametro.Detalle.Add("ConceptoCampo_1_Adicional_3", "Campo 3");
            parametro.Detalle.Add("ConceptoCampo_1_Adicional_4", "Campo 4");
            parametro.Detalle.Add("ConceptoCampo_1_Adicional_5", "Campo 5");
            parametro.Detalle.Add("ConceptoCampo_1_Adicional_6", "Campo 6");
            parametro.Detalle.Add("ConceptoCampo_1_Adicional_7", "Campo 7");
            parametro.Detalle.Add("ConceptoCampo_1_Adicional_8", "Campo 8");
            parametro.Detalle.Add("ConceptoCampo_1_Adicional_9", "Campo 9");
            parametro.Detalle.Add("ConceptoCampo_1_Adicional_10", "Campo 10");

            parametro.Detalle.Add("ConceptoImpuestos_1_TipoImpuesto_1", "1");
            parametro.Detalle.Add("ConceptoImpuestos_1_Impuesto_1", "2");
            parametro.Detalle.Add("ConceptoImpuestos_1_Factor_1", "1");
            parametro.Detalle.Add("ConceptoImpuestos_1_Base_1", "100");
            parametro.Detalle.Add("ConceptoImpuestos_1_Tasa_1", "0.160000");
            parametro.Detalle.Add("ConceptoImpuestos_1_ImpuestoImporte_1", "16");

            #endregion "Detalle"

            return parametro;
        }

        private WCFCertificar.GeneraCFDIPeticion LlenaHonorarios()
        {
            WCFCertificar.GeneraCFDIPeticion parametro = new WCFCertificar.GeneraCFDIPeticion();
            parametro.DatosGenerales = new WCFCertificar.DatosGenerales();
            Archivos archivo = new Archivos();

            #region "Datos Generales"

            parametro.DatosGenerales.Configuracion = new Dictionary<string, string>();

            parametro.DatosGenerales.Configuracion.Add("Version", "3.3");
            parametro.DatosGenerales.Configuracion.Add("Usuario", "PruebasTimbrado");
            parametro.DatosGenerales.Configuracion.Add("Password", "@Notiene1");
            parametro.DatosGenerales.Configuracion.Add("SellaCFDI", "true");
            parametro.DatosGenerales.Configuracion.Add("TimbraCFDI", "true");

            parametro.DatosGenerales.Configuracion.Add("CSD", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Certificado\AAA010101AAA.cer"));
            parametro.DatosGenerales.Configuracion.Add("LlavePrivada", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Certificado\AAA010101AAA.key"));

            parametro.DatosGenerales.Configuracion.Add("CSDPassword", "12345678a");
            parametro.DatosGenerales.Configuracion.Add("GeneraPDF", "true");
            parametro.DatosGenerales.Configuracion.Add("Logotipo", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Logotipos\Logotipo.png"));
            parametro.DatosGenerales.Configuracion.Add("FormatoImpresion", "");

            parametro.DatosGenerales.Configuracion.Add("CFDI", ObtieneCFDI());
            parametro.DatosGenerales.Configuracion.Add("OpcionDecimales", "1");
            parametro.DatosGenerales.Configuracion.Add("NumeroDecimales", "2");
            parametro.DatosGenerales.Configuracion.Add("TipoCFDI", ObtieneTipoCFDI());

            parametro.DatosGenerales.Configuracion.Add("EnviaEmail", "false");
            parametro.DatosGenerales.Configuracion.Add("ReceptorEmail", "correo@dominio.com");            
            parametro.DatosGenerales.Configuracion.Add("EmailMensaje", "CFDI de Prueba enviado desde facturoporti.com.mx");

            #endregion "Datos Generales"

            #region "Encabezado"

            parametro.Encabezado = new Dictionary<string, string>();

            parametro.Encabezado.Add("EmisorRFC", "AAA010101AAA");
            parametro.Encabezado.Add("EmisorNombreRazonSocial", "Empresa Patito");
            parametro.Encabezado.Add("RegimenFiscal", "601 - General Ley de las Personas Morales");

            parametro.Encabezado.Add("EmisorDireccionCalle", "Serapio Rendon");
            parametro.Encabezado.Add("EmisorDireccionNumeroExterior", "122");
            parametro.Encabezado.Add("EmisorDireccionNumeroInterior", "5");
            parametro.Encabezado.Add("EmisorDireccionColonia", "San Rafael");
            parametro.Encabezado.Add("EmisorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("EmisorDireccionMunicipio", "Cuauhtemoc");
            parametro.Encabezado.Add("EmisorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("EmisorDireccionPais", "México");
            parametro.Encabezado.Add("EmisorDireccionCodigoPostal", "06470");

            parametro.Encabezado.Add("ReceptorRFC", "SSF1103037F1");
            parametro.Encabezado.Add("ReceptorNombreRazonSocial", "Scafandra Software Factory SA de CV");
            parametro.Encabezado.Add("ReceptorUsoCFDI", "P01 - Por Definir");

            parametro.Encabezado.Add("ReceptorDireccionCalle", "Reforma");
            parametro.Encabezado.Add("ReceptorDireccionNumeroExterior", "6283");
            parametro.Encabezado.Add("ReceptorDireccionNumeroInterior", "A");
            parametro.Encabezado.Add("ReceptorDireccionColonia", "Centro");
            parametro.Encabezado.Add("ReceptorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("ReceptorDireccionMunicipio", "Benito Juarez");
            parametro.Encabezado.Add("ReceptorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("ReceptorDireccionPais", "México");
            parametro.Encabezado.Add("ReceptorDireccionCodigoPostal", "62774");

            parametro.Encabezado.Add("Fecha", DateTime.Now.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss"));
            parametro.Encabezado.Add("Serie", "AB");
            parametro.Encabezado.Add("Folio", "1");
            parametro.Encabezado.Add("MetodoPago", "PUE - Pago en una sola exhibición");
            parametro.Encabezado.Add("FormaPago", "99-Por Definir");

            parametro.Encabezado.Add("CondicionesPago", "Crédito");
            parametro.Encabezado.Add("Moneda", "MXN");
            parametro.Encabezado.Add("TipoCambio", "1");
            parametro.Encabezado.Add("LugarExpedicion", "06470");
            parametro.Encabezado.Add("SubTotal", "1000.00");
            parametro.Encabezado.Add("Total", "953.34");

            parametro.Encabezado.Add("CFDIsRelacionados", "52B4E070-CD29-4584-BF0D-5CE501532791;342C6218-0A94-4F1E-AC38-1ED54F4171BB;");
            parametro.Encabezado.Add("TipoRelacion", "04 Sustitución de los CFDI previos");

            #endregion "Encabezado"

            #region "Detalle"

            parametro.Detalle = new Dictionary<string, string>();

            parametro.Detalle.Add("ConceptoCantidad_1", "1");
            parametro.Detalle.Add("ConceptoCodigoUnidad_1", "E48");
            parametro.Detalle.Add("ConceptoUnidad_1", "Servicio");
            parametro.Detalle.Add("ConceptoSerie_1", "1234ABC");
            parametro.Detalle.Add("ConceptoCodigoProducto_1", "84111506");
            parametro.Detalle.Add("ConceptoProducto_1", "Servicios Profesionales y consultoria");
            parametro.Detalle.Add("ConceptoPrecioUnitario_1", "1000");
            parametro.Detalle.Add("ConceptoImporte_1", "1000");
            parametro.Detalle.Add("ConceptoDescuento_1", "");


            // Se agrega el IVA
            parametro.Detalle.Add("ConceptoImpuestos_1_TipoImpuesto_1", "1");
            parametro.Detalle.Add("ConceptoImpuestos_1_Impuesto_1", "2");
            parametro.Detalle.Add("ConceptoImpuestos_1_Factor_1", "1");
            parametro.Detalle.Add("ConceptoImpuestos_1_Base_1", "1000");
            parametro.Detalle.Add("ConceptoImpuestos_1_Tasa_1", "0.160000");
            parametro.Detalle.Add("ConceptoImpuestos_1_ImpuestoImporte_1", "160");

            // Se agrega la retencion del 10 %
            parametro.Detalle.Add("ConceptoImpuestos_1_TipoImpuesto_2", "2");
            parametro.Detalle.Add("ConceptoImpuestos_1_Impuesto_2", "1"); 
            parametro.Detalle.Add("ConceptoImpuestos_1_Factor_2", "1");
            parametro.Detalle.Add("ConceptoImpuestos_1_Base_2", "1000");
            parametro.Detalle.Add("ConceptoImpuestos_1_Tasa_2", "0.100000");
            parametro.Detalle.Add("ConceptoImpuestos_1_ImpuestoImporte_2", "100");

            // Se agrega la retencion del 2/3 del IVA al 16 %
            parametro.Detalle.Add("ConceptoImpuestos_1_TipoImpuesto_3", "2");
            parametro.Detalle.Add("ConceptoImpuestos_1_Impuesto_3", "2");
            parametro.Detalle.Add("ConceptoImpuestos_1_Factor_3", "1");
            parametro.Detalle.Add("ConceptoImpuestos_1_Base_3", "1000");
            parametro.Detalle.Add("ConceptoImpuestos_1_Tasa_3", "0.106667");
            parametro.Detalle.Add("ConceptoImpuestos_1_ImpuestoImporte_3", "106.66");

            #endregion "Detalle"

            return parametro;
        }

        private WCFCertificar.GeneraCFDIPeticion LlenaDonativo()
        {
            WCFCertificar.GeneraCFDIPeticion parametro = new WCFCertificar.GeneraCFDIPeticion();
            parametro.DatosGenerales = new WCFCertificar.DatosGenerales();
            Archivos archivo = new Archivos();

            #region "Datos Generales"

            parametro.DatosGenerales.Configuracion = new Dictionary<string, string>();

            parametro.DatosGenerales.Configuracion.Add("Version", "3.3");
            parametro.DatosGenerales.Configuracion.Add("Usuario", "PruebasTimbrado");
            parametro.DatosGenerales.Configuracion.Add("Password", "@Notiene1");
            parametro.DatosGenerales.Configuracion.Add("SellaCFDI", "true");
            parametro.DatosGenerales.Configuracion.Add("TimbraCFDI", "true");

            parametro.DatosGenerales.Configuracion.Add("CSD", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Certificado\AAA010101AAA.cer"));
            parametro.DatosGenerales.Configuracion.Add("LlavePrivada", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Certificado\AAA010101AAA.key"));

            parametro.DatosGenerales.Configuracion.Add("CSDPassword", "12345678a");
            parametro.DatosGenerales.Configuracion.Add("GeneraPDF", "true");
            parametro.DatosGenerales.Configuracion.Add("Logotipo", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Logotipos\Logotipo.png"));
            parametro.DatosGenerales.Configuracion.Add("FormatoImpresion", "");

            parametro.DatosGenerales.Configuracion.Add("CFDI", ObtieneCFDI());
            parametro.DatosGenerales.Configuracion.Add("OpcionDecimales", "1");
            parametro.DatosGenerales.Configuracion.Add("NumeroDecimales", "2");
            parametro.DatosGenerales.Configuracion.Add("TipoCFDI", ObtieneTipoCFDI());

            parametro.DatosGenerales.Configuracion.Add("EnviaEmail", "false");
            parametro.DatosGenerales.Configuracion.Add("ReceptorEmail", "correo@dominio.com");            
            parametro.DatosGenerales.Configuracion.Add("EmailMensaje", "CFDI de Prueba enviado desde facturoporti.com.mx");

            #endregion "Datos Generales"

            #region "Encabezado"

            parametro.Encabezado = new Dictionary<string, string>();

            parametro.Encabezado.Add("EmisorRFC", "AAA010101AAA");
            parametro.Encabezado.Add("EmisorNombreRazonSocial", "Empresa Patito");
            parametro.Encabezado.Add("RegimenFiscal", "601 - General Ley de las Personas Morales");

            parametro.Encabezado.Add("EmisorDireccionCalle", "Serapio Rendon");
            parametro.Encabezado.Add("EmisorDireccionNumeroExterior", "122");
            parametro.Encabezado.Add("EmisorDireccionNumeroInterior", "5");
            parametro.Encabezado.Add("EmisorDireccionColonia", "San Rafael");
            parametro.Encabezado.Add("EmisorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("EmisorDireccionMunicipio", "Cuauhtemoc");
            parametro.Encabezado.Add("EmisorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("EmisorDireccionPais", "México");
            parametro.Encabezado.Add("EmisorDireccionCodigoPostal", "06470");

            parametro.Encabezado.Add("ReceptorRFC", "SSF1103037F1");
            parametro.Encabezado.Add("ReceptorNombreRazonSocial", "Scafandra Software Factory SA de CV");
            parametro.Encabezado.Add("ReceptorUsoCFDI", "P01 - Por Definir");

            parametro.Encabezado.Add("ReceptorDireccionCalle", "Reforma");
            parametro.Encabezado.Add("ReceptorDireccionNumeroExterior", "6283");
            parametro.Encabezado.Add("ReceptorDireccionNumeroInterior", "A");
            parametro.Encabezado.Add("ReceptorDireccionColonia", "Centro");
            parametro.Encabezado.Add("ReceptorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("ReceptorDireccionMunicipio", "Benito Juarez");
            parametro.Encabezado.Add("ReceptorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("ReceptorDireccionPais", "México");
            parametro.Encabezado.Add("ReceptorDireccionCodigoPostal", "62774");

            parametro.Encabezado.Add("Fecha", DateTime.Now.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss"));
            parametro.Encabezado.Add("Serie", "AB");
            parametro.Encabezado.Add("Folio", "1");
            parametro.Encabezado.Add("MetodoPago", "PUE - Pago en una sola exhibición");
            parametro.Encabezado.Add("FormaPago", "99-Por Definir");

            parametro.Encabezado.Add("CondicionesPago", "Crédito");
            parametro.Encabezado.Add("Moneda", "MXN");
            parametro.Encabezado.Add("TipoCambio", "1");
            parametro.Encabezado.Add("LugarExpedicion", "06470");
            parametro.Encabezado.Add("SubTotal", "200.00");
            parametro.Encabezado.Add("Total", "200");
            
            parametro.Encabezado.Add("CFDIsRelacionados", "52B4E070-CD29-4584-BF0D-5CE501532791;342C6218-0A94-4F1E-AC38-1ED54F4171BB;");
            parametro.Encabezado.Add("TipoRelacion", "04 Sustitución de los CFDI previos");

            #endregion "Encabezado"

            #region "Detalle"

            parametro.Detalle = new Dictionary<string, string>();

            parametro.Detalle.Add("ConceptoCantidad_1", "1");
            parametro.Detalle.Add("ConceptoCodigoUnidad_1", "E48");
            parametro.Detalle.Add("ConceptoUnidad_1", "Servicio");
            parametro.Detalle.Add("ConceptoSerie_1", "1234ABC");
            parametro.Detalle.Add("ConceptoCodigoProducto_1", "84111506");
            parametro.Detalle.Add("ConceptoProducto_1", "Donativo de una ambulancia 2018");
            parametro.Detalle.Add("ConceptoPrecioUnitario_1", "200");
            parametro.Detalle.Add("ConceptoImporte_1", "200");
            parametro.Detalle.Add("ConceptoDescuento_1", "");
            
            parametro.Detalle.Add("ConceptoImpuestos_1_TipoImpuesto_1", "1");
            parametro.Detalle.Add("ConceptoImpuestos_1_Impuesto_1", "2");
            parametro.Detalle.Add("ConceptoImpuestos_1_Factor_1", "3");
            parametro.Detalle.Add("ConceptoImpuestos_1_Base_1", "100");
            parametro.Detalle.Add("ConceptoImpuestos_1_Tasa_1", "0.000000");
            parametro.Detalle.Add("ConceptoImpuestos_1_ImpuestoImporte_1", "0");

            #endregion "Detalle"

            return parametro;
        }

        private WCFCertificar.GeneraCFDIPeticion LlenaCartaPorte()
        {
            WCFCertificar.GeneraCFDIPeticion parametro = new WCFCertificar.GeneraCFDIPeticion();
            parametro.DatosGenerales = new WCFCertificar.DatosGenerales();
            Archivos archivo = new Archivos();

            #region "Datos Generales"

            parametro.DatosGenerales.Configuracion = new Dictionary<string, string>();

            parametro.DatosGenerales.Configuracion.Add("Version", "3.3");
            parametro.DatosGenerales.Configuracion.Add("Usuario", "PruebasTimbrado");
            parametro.DatosGenerales.Configuracion.Add("Password", "@Notiene1");
            parametro.DatosGenerales.Configuracion.Add("SellaCFDI", "true");
            parametro.DatosGenerales.Configuracion.Add("TimbraCFDI", "true");

            parametro.DatosGenerales.Configuracion.Add("CSD", archivo.AbrirBase64(ObtieneDirectorioAplicacion() +  @"\Certificado\AAA010101AAA.cer"));
            parametro.DatosGenerales.Configuracion.Add("LlavePrivada", archivo.AbrirBase64(ObtieneDirectorioAplicacion() +  @"\Certificado\AAA010101AAA.key"));

            parametro.DatosGenerales.Configuracion.Add("CSDPassword", "12345678a");
            parametro.DatosGenerales.Configuracion.Add("GeneraPDF", "true");
            parametro.DatosGenerales.Configuracion.Add("Logotipo", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Logotipos\Logotipo.png"));
            parametro.DatosGenerales.Configuracion.Add("FormatoImpresion", "");

            parametro.DatosGenerales.Configuracion.Add("CFDI", "CartaPorte");
            parametro.DatosGenerales.Configuracion.Add("OpcionDecimales", "1");
            parametro.DatosGenerales.Configuracion.Add("NumeroDecimales", "2");
            parametro.DatosGenerales.Configuracion.Add("TipoCFDI", "Traslado");

            parametro.DatosGenerales.Configuracion.Add("EnviaEmail", "false");
            parametro.DatosGenerales.Configuracion.Add("ReceptorEmail", "correo@dominio.com");
            parametro.DatosGenerales.Configuracion.Add("EmailMensaje", "CFDI de Prueba enviado desde facturoporti.com.mx");

            #endregion "Datos Generales"

            #region "Encabezado"

            parametro.Encabezado = new Dictionary<string, string>();

            parametro.Encabezado.Add("EmisorRFC", "AAA010101AAA");
            parametro.Encabezado.Add("EmisorNombreRazonSocial", "Empresa Patito");
            parametro.Encabezado.Add("RegimenFiscal", "601 - General Ley de las Personas Morales");

            parametro.Encabezado.Add("EmisorDireccionCalle", "Serapio Rendon");
            parametro.Encabezado.Add("EmisorDireccionNumeroExterior", "122");
            parametro.Encabezado.Add("EmisorDireccionNumeroInterior", "5");
            parametro.Encabezado.Add("EmisorDireccionColonia", "San Rafael");
            parametro.Encabezado.Add("EmisorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("EmisorDireccionMunicipio", "Cuauhtemoc");
            parametro.Encabezado.Add("EmisorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("EmisorDireccionPais", "México");
            parametro.Encabezado.Add("EmisorDireccionCodigoPostal", "06470");

            parametro.Encabezado.Add("ReceptorRFC", "SSF1103037F1");
            parametro.Encabezado.Add("ReceptorNombreRazonSocial", "Scafandra Software Factory SA de CV");

            parametro.Encabezado.Add("ReceptorDireccionCalle", "Reforma");
            parametro.Encabezado.Add("ReceptorDireccionNumeroExterior", "6283");
            parametro.Encabezado.Add("ReceptorDireccionNumeroInterior", "A");
            parametro.Encabezado.Add("ReceptorDireccionColonia", "Centro");
            parametro.Encabezado.Add("ReceptorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("ReceptorDireccionMunicipio", "Benito Juarez");
            parametro.Encabezado.Add("ReceptorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("ReceptorDireccionPais", "México");
            parametro.Encabezado.Add("ReceptorDireccionCodigoPostal", "62774");

            parametro.Encabezado.Add("Fecha", DateTime.Now.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss"));
            parametro.Encabezado.Add("Serie", "AB");
            parametro.Encabezado.Add("Folio", "1");
            parametro.Encabezado.Add("Moneda", "MXN");
            parametro.Encabezado.Add("TipoCambio", "1");
            parametro.Encabezado.Add("LugarExpedicion", "06470");

            parametro.Encabezado.Add("CFDIsRelacionados", "52B4E070-CD29-4584-BF0D-5CE501532791;342C6218-0A94-4F1E-AC38-1ED54F4171BB;");
            parametro.Encabezado.Add("TipoRelacion", "04 Sustitución de los CFDI previos");

            parametro.Encabezado.Add("FechaEntrega", "22 de Febrero del 2018");
            parametro.Encabezado.Add("Remitente", "Rogelio Romo");
            parametro.Encabezado.Add("RemitenteDomicilio", "Reforma 1295 int 67 Colonia Cuauhtemoc Delegación Cuauhtemoc Ciudad de México");
            parametro.Encabezado.Add("RemitenteLugarRecoger", "Paseo de los Jardines 3 Colonia Paseos de Taxqueña, Coyoacan Ciudad de México");
            parametro.Encabezado.Add("Destinatario", "Miguel Rios");
            parametro.Encabezado.Add("DestinatarioDomicilio", "Ignacio Zaragosa 30 Colonia Centro Cuernavaca Morelos México");
            parametro.Encabezado.Add("DestinatarioLugarEntrega", "Insurgente 90 Colonia Gustavo Diaz Ordaz, Cuautla Morelos");
            parametro.Encabezado.Add("DescripcionMercancia", "Abarrotes en generar y costales de Azucar premium");
            parametro.Encabezado.Add("Peso", "15000");
            parametro.Encabezado.Add("MetrosCubicos", "N/A");
            parametro.Encabezado.Add("Litros", "N/A");
            parametro.Encabezado.Add("MaterialPeligroso", "No");
            parametro.Encabezado.Add("ValorDeclarado", "150 000 pesos");
            parametro.Encabezado.Add("Indemnizacion", "No aplica");
            parametro.Encabezado.Add("Conductor", "Mario Camacho");
            parametro.Encabezado.Add("Vehiculo", "Ford 125");
            parametro.Encabezado.Add("Placas", "PYU-8674");
            parametro.Encabezado.Add("Kilometros", "12000");

            #endregion "Encabezado"

            #region "Detalle"

            parametro.Detalle = new Dictionary<string, string>();

            parametro.Detalle.Add("ConceptoCantidad_1", "25");
            parametro.Detalle.Add("ConceptoCodigoUnidad_1", "KGM");
            parametro.Detalle.Add("ConceptoUnidad_1", "Kilogramo");
            parametro.Detalle.Add("ConceptoCodigoProducto_1", "10121604");
            parametro.Detalle.Add("ConceptoProducto_1", "Alimento para avez NutriAlimentos");

            parametro.Detalle.Add("ConceptoCantidad_2", "11");
            parametro.Detalle.Add("ConceptoCodigoUnidad_2", "H87");
            parametro.Detalle.Add("ConceptoUnidad_2", "Piezas");
            parametro.Detalle.Add("ConceptoCodigoProducto_2", "24122000");
            parametro.Detalle.Add("ConceptoProducto_2", "Botella de Pet");

            parametro.Detalle.Add("ConceptoCantidad_3", "40");
            parametro.Detalle.Add("ConceptoCodigoUnidad_3", "BB");
            parametro.Detalle.Add("ConceptoUnidad_3", "Cajas");
            parametro.Detalle.Add("ConceptoCodigoProducto_3", "50161509");
            parametro.Detalle.Add("ConceptoProducto_3", "Caja de 5 KG de Azucar la Dulzura");

            #endregion "Detalle"

            return parametro;
        }

        private WCFCertificar.GeneraCFDIPeticion LlenaComplementoPagos()
        {
            WCFCertificar.GeneraCFDIPeticion parametro = new WCFCertificar.GeneraCFDIPeticion();
            Archivos archivo = new Archivos();
            parametro.DatosGenerales = new WCFCertificar.DatosGenerales();

            #region "Datos Generales"

            parametro.DatosGenerales.Configuracion = new Dictionary<string, string>();

            parametro.DatosGenerales.Configuracion.Add("Version", "3.3");
            parametro.DatosGenerales.Configuracion.Add("Usuario", "PruebasTimbrado");
            parametro.DatosGenerales.Configuracion.Add("Password", "@Notiene1");
            parametro.DatosGenerales.Configuracion.Add("SellaCFDI", "true");
            parametro.DatosGenerales.Configuracion.Add("TimbraCFDI", "true");

            parametro.DatosGenerales.Configuracion.Add("CSD", archivo.AbrirBase64(ObtieneDirectorioAplicacion() +  @"\Certificado\AAA010101AAA.cer"));
            parametro.DatosGenerales.Configuracion.Add("LlavePrivada", archivo.AbrirBase64(ObtieneDirectorioAplicacion() +  @"\Certificado\AAA010101AAA.key"));

            parametro.DatosGenerales.Configuracion.Add("CSDPassword", "12345678a");
            parametro.DatosGenerales.Configuracion.Add("GeneraPDF", "true");
            parametro.DatosGenerales.Configuracion.Add("Logotipo", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Logotipos\Logotipo.png"));
            parametro.DatosGenerales.Configuracion.Add("FormatoImpresion", "");

            parametro.DatosGenerales.Configuracion.Add("CFDI", "ComplementoPagos");
            parametro.DatosGenerales.Configuracion.Add("OpcionDecimales", "1");
            parametro.DatosGenerales.Configuracion.Add("NumeroDecimales", "2");
            parametro.DatosGenerales.Configuracion.Add("TipoCFDI", "Pago");

            parametro.DatosGenerales.Configuracion.Add("EnviaEmail", "false");
            parametro.DatosGenerales.Configuracion.Add("ReceptorEmail", "correo@dominio.com");
            parametro.DatosGenerales.Configuracion.Add("EmailMensaje", "CFDI de Prueba enviado desde facturoporti.com.mx");

            #endregion "Datos Generales"

            #region "Encabezado"

            parametro.Encabezado = new Dictionary<string, string>();

            parametro.Encabezado.Add("EmisorRFC", "AAA010101AAA");
            parametro.Encabezado.Add("EmisorNombreRazonSocial", "Empresa Patito");
            parametro.Encabezado.Add("RegimenFiscal", "601 - General Ley de las Personas Morales");

            parametro.Encabezado.Add("EmisorDireccionCalle", "Serapio Rendon");
            parametro.Encabezado.Add("EmisorDireccionNumeroExterior", "122");
            parametro.Encabezado.Add("EmisorDireccionNumeroInterior", "5");
            parametro.Encabezado.Add("EmisorDireccionColonia", "San Rafael");
            parametro.Encabezado.Add("EmisorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("EmisorDireccionMunicipio", "Cuauhtemoc");
            parametro.Encabezado.Add("EmisorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("EmisorDireccionPais", "México");
            parametro.Encabezado.Add("EmisorDireccionCodigoPostal", "06470");

            parametro.Encabezado.Add("ReceptorRFC", "SSF1103037F1");
            parametro.Encabezado.Add("ReceptorNombreRazonSocial", "Scafandra Software Factory SA de CV");

            parametro.Encabezado.Add("ReceptorDireccionCalle", "Reforma");
            parametro.Encabezado.Add("ReceptorDireccionNumeroExterior", "6283");
            parametro.Encabezado.Add("ReceptorDireccionNumeroInterior", "A");
            parametro.Encabezado.Add("ReceptorDireccionColonia", "Centro");
            parametro.Encabezado.Add("ReceptorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("ReceptorDireccionMunicipio", "Benito Juarez");
            parametro.Encabezado.Add("ReceptorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("ReceptorDireccionPais", "México");
            parametro.Encabezado.Add("ReceptorDireccionCodigoPostal", "62774");

            parametro.Encabezado.Add("Fecha", DateTime.Now.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss"));
            parametro.Encabezado.Add("Serie", "AB");
            parametro.Encabezado.Add("Folio", "1");
            parametro.Encabezado.Add("LugarExpedicion", "06470");
            parametro.Encabezado.Add("CFDIsRelacionados", "52B4E070-CD29-4584-BF0D-5CE501532791;342C6218-0A94-4F1E-AC38-1ED54F4171BB;");
            parametro.Encabezado.Add("TipoRelacion", "04 Sustitución de los CFDI previos");

            #endregion "Encabezado"

            #region "Complemento"
            
            parametro.Complemento = new Dictionary<string, string>();

            parametro.Complemento.Add("Pago_FechaPago_1", "2018-02-13T00:00:00");
            parametro.Complemento.Add("Pago_FormaPago_1", "01 - Efectivo");
            parametro.Complemento.Add("Pago_Moneda_1", "MXN");
            parametro.Complemento.Add("Pago_TipoCambio_1", "");
            parametro.Complemento.Add("Pago_NumeroOperacion_1", "84111506");
            parametro.Complemento.Add("Pago_BancoExtranjero_1", "Bank Of America");

            // Documento relacionado 1 del pago 1
            parametro.Complemento.Add("Pago_1_Documento_UUID_1", "35BC4A91-C8AA-4C32-88A9-0F7FBD19FD12");
            parametro.Complemento.Add("Pago_1_Documento_Serie_1", "PAG");
            parametro.Complemento.Add("Pago_1_Documento_Folio_1", "1");
            parametro.Complemento.Add("Pago_1_Documento_Moneda_1", "MXN");
            parametro.Complemento.Add("Pago_1_Documento_TipoCambio_1", "");
            parametro.Complemento.Add("Pago_1_Documento_MetodoPago_1", " PUE - Pago en una sola exhibicion");
            parametro.Complemento.Add("Pago_1_Documento_NumeroParcialidad_1", "");
            parametro.Complemento.Add("Pago_1_Documento_ImporteSaldoAnterior_1", "1000");
            parametro.Complemento.Add("Pago_1_Documento_ImportePagado_1", "400");
            parametro.Complemento.Add("Pago_1_Documento_ImporteSaldoInsoluto_1", "600");

            // Documento relacionado 2 del pago 1
            parametro.Complemento.Add("Pago_1_Documento_UUID_2", "4A7D7D6B-D16E-4198-9816-D49A5548BECC");
            parametro.Complemento.Add("Pago_1_Documento_Serie_2", "PAG");
            parametro.Complemento.Add("Pago_1_Documento_Folio_2", "2");
            parametro.Complemento.Add("Pago_1_Documento_Moneda_2", "MXN");
            parametro.Complemento.Add("Pago_1_Documento_TipoCambio_2", "");
            parametro.Complemento.Add("Pago_1_Documento_MetodoPago_2", "PPD - Pago en parcialidades o diferido");
            parametro.Complemento.Add("Pago_1_Documento_NumeroParcialidad_2", "1");
            parametro.Complemento.Add("Pago_1_Documento_ImporteSaldoAnterior_2", "2000");
            parametro.Complemento.Add("Pago_1_Documento_ImportePagado_2", "1500");
            parametro.Complemento.Add("Pago_1_Documento_ImporteSaldoInsoluto_2", "500");

            // Documento relacionado 3 del pago 1
            parametro.Complemento.Add("Pago_1_Documento_UUID_3", "75F5E25E-6E11-4C03-A11E-4DE68C0ACC25");
            parametro.Complemento.Add("Pago_1_Documento_Serie_3", "PAG");
            parametro.Complemento.Add("Pago_1_Documento_Folio_3", "3");
            parametro.Complemento.Add("Pago_1_Documento_Moneda_3", "MXN");
            parametro.Complemento.Add("Pago_1_Documento_TipoCambio_3", "");
            parametro.Complemento.Add("Pago_1_Documento_MetodoPago_3", "PPD - Pago en parcialidades o diferido");
            parametro.Complemento.Add("Pago_1_Documento_NumeroParcialidad_3", "2");
            parametro.Complemento.Add("Pago_1_Documento_ImporteSaldoAnterior_3", "500");
            parametro.Complemento.Add("Pago_1_Documento_ImportePagado_3", "500");
            parametro.Complemento.Add("Pago_1_Documento_ImporteSaldoInsoluto_3", "0");

            #endregion "Complemento"

            return parametro;
        }

        decimal Deducciones = 0;
        decimal Importe = 0;
        decimal SubTotal = 0;
        decimal Total = 0;

        private WCFCertificar.GeneraCFDIPeticion LlenaNomina()
        {
            WCFCertificar.GeneraCFDIPeticion parametro = new WCFCertificar.GeneraCFDIPeticion();
            parametro.DatosGenerales = new WCFCertificar.DatosGenerales();
            Archivos archivo = new Archivos();

            #region "Datos Generales"

            parametro.DatosGenerales.Configuracion = new Dictionary<string, string>();

            parametro.DatosGenerales.Configuracion.Add("Version", "3.3");
            parametro.DatosGenerales.Configuracion.Add("Usuario", "PruebasTimbrado");
            parametro.DatosGenerales.Configuracion.Add("Password", "@Notiene1");
            parametro.DatosGenerales.Configuracion.Add("SellaCFDI", "true");
            parametro.DatosGenerales.Configuracion.Add("TimbraCFDI", "true");

            parametro.DatosGenerales.Configuracion.Add("CSD", archivo.AbrirBase64(@"C:\CFD\5.-CSD\AAA010101AAA\Certificado.cer"));
            parametro.DatosGenerales.Configuracion.Add("LlavePrivada", archivo.AbrirBase64(@"C:\CFD\5.-CSD\AAA010101AAA\LlavePrivada.key"));

            parametro.DatosGenerales.Configuracion.Add("CSDPassword", "12345678a");
            parametro.DatosGenerales.Configuracion.Add("GeneraPDF", "true");
            //parametro.DatosGenerales.Configuracion.Add("Logotipo", archivo.AbrirBase64(ObtieneDirectorioAplicacion() + @"\Logotipos\Logotipo.png"));
            parametro.DatosGenerales.Configuracion.Add("FormatoImpresion", "");

            parametro.DatosGenerales.Configuracion.Add("CFDI", "ReciboNomina");
            parametro.DatosGenerales.Configuracion.Add("OpcionDecimales", "1");
            parametro.DatosGenerales.Configuracion.Add("NumeroDecimales", "2");
            parametro.DatosGenerales.Configuracion.Add("TipoCFDI", "ReciboNomina");

            parametro.DatosGenerales.Configuracion.Add("EnviaEmail", "false");
            parametro.DatosGenerales.Configuracion.Add("ReceptorEmail", "correo@dominio.com");
            parametro.DatosGenerales.Configuracion.Add("EmailMensaje", "CFDI de Prueba enviado desde facturoporti.com.mx");

            #endregion "Datos Generales"

            #region "Encabezado"

            parametro.Encabezado = new Dictionary<string, string>();

            parametro.Encabezado.Add("EmisorRFC", "AAA010101AAA");
            parametro.Encabezado.Add("EmisorNombreRazonSocial", "Empresa Patito");
            parametro.Encabezado.Add("RegimenFiscal", "601 - General Ley de las Personas Morales");

            parametro.Encabezado.Add("EmisorDireccionCalle", "Serapio Rendon");
            parametro.Encabezado.Add("EmisorDireccionNumeroExterior", "122");
            parametro.Encabezado.Add("EmisorDireccionNumeroInterior", "5");
            parametro.Encabezado.Add("EmisorDireccionColonia", "San Rafael");
            parametro.Encabezado.Add("EmisorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("EmisorDireccionMunicipio", "Cuauhtemoc");
            parametro.Encabezado.Add("EmisorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("EmisorDireccionPais", "México");
            parametro.Encabezado.Add("EmisorDireccionCodigoPostal", "06470");

            parametro.Encabezado.Add("ReceptorRFC", "OOSI790616HL2");
            parametro.Encabezado.Add("ReceptorNombreRazonSocial", "Juan Perez");

            parametro.Encabezado.Add("ReceptorDireccionCalle", "Reforma");
            parametro.Encabezado.Add("ReceptorDireccionNumeroExterior", "6283");
            parametro.Encabezado.Add("ReceptorDireccionNumeroInterior", "A");
            parametro.Encabezado.Add("ReceptorDireccionColonia", "Centro");
            parametro.Encabezado.Add("ReceptorDireccionLocalidad", "CDMX");
            parametro.Encabezado.Add("ReceptorDireccionMunicipio", "Benito Juarez");
            parametro.Encabezado.Add("ReceptorDireccionEstado", "Ciudad de México");
            parametro.Encabezado.Add("ReceptorDireccionPais", "México");
            parametro.Encabezado.Add("ReceptorDireccionCodigoPostal", "62774");

            parametro.Encabezado.Add("Fecha", DateTime.Now.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ss"));
            parametro.Encabezado.Add("Serie", "AB");
            parametro.Encabezado.Add("Folio", "150");
            parametro.Encabezado.Add("LugarExpedicion", "06470");

            SubTotal = 3373;
            Total = Convert.ToDecimal(2689.82);

            parametro.Encabezado.Add("SubTotal", SubTotal.ToString());
            parametro.Encabezado.Add("Total", Total.ToString());

            #endregion "Encabezado"

            #region "Complemento"

            parametro.Complemento = new Dictionary<string, string>();

            #region "Datos Generales Recibo de Nómina"

            parametro.Complemento.Add("Version", "1.2");
            parametro.Complemento.Add("RiesgoPuestoTrabajo", "1");
            parametro.Complemento.Add("RegistroPatronal", "C6165389200");
            parametro.Complemento.Add("ReceptorCURP", "HEML830415MOCRRS07");
            //parametro.Complemento.Add("ReceptorCURP", "1111");

            parametro.Complemento.Add("NumeroEmpleado", "12345");
            parametro.Complemento.Add("NumeroSeguroSocial", "123456789");
            parametro.Complemento.Add("Banco", "12 - BBVA BANCOMER");
            parametro.Complemento.Add("CuentaBancaria", "002180700008152233");
            parametro.Complemento.Add("Departamento", "Contabilidad");
            parametro.Complemento.Add("Puesto", "Auxiliar Contable");
            parametro.Complemento.Add("FechaInicioRelacionLaboral", "2017-06-01T12:00:00");
            parametro.Complemento.Add("Sindicalizado", "No");
            parametro.Complemento.Add("TipoRegimen", "2 - Sueldos");
            parametro.Complemento.Add("TipoContrato", "1 - Contrato de trabajo por tiempo indeterminado");
            parametro.Complemento.Add("PeriodicidadPago", "4 - Quincenal");
            parametro.Complemento.Add("TipoJornada", "3 - Mixta");
            parametro.Complemento.Add("SalarioBaseCotizacion", "221.22");
            parametro.Complemento.Add("SalarioDiarioIntegrado", "260.38");
            parametro.Complemento.Add("TipoNomina", "O - Ordinaria");
            parametro.Complemento.Add("FechaInicialPago", "2018-02-15T12:00:00");
            parametro.Complemento.Add("FechaFinalPago", "2018-02-28T12:00:00");
            parametro.Complemento.Add("FechaPago", "2018-02-27T12:00:00");
            parametro.Complemento.Add("NumDiasPagados", "14");
            parametro.Complemento.Add("Estado", "DIF - Ciudad de México");

            Importe = 3373;
            parametro.Complemento.Add("ConceptoPrecioUnitario_1", Importe.ToString());
            parametro.Complemento.Add("ConceptoImporte_1", Importe.ToString());

            #endregion "Datos Generales Recibo de Nómina"

            #region "Percepciones"

            parametro.Complemento.Add("PercepcionTipo_1", "1 - Sueldos, Salarios  Rayas y Jornales");
            parametro.Complemento.Add("PercepcionClave_1", "P001");
            parametro.Complemento.Add("PercepcionDescripcion_1", "Sueldos ");
            parametro.Complemento.Add("PercepcionImporteGravado_1", "3318");
            parametro.Complemento.Add("PercepcionImporteExento_1", "0");

            parametro.Complemento.Add("PercepcionTipo_2", "20 - Prima dominical");
            parametro.Complemento.Add("PercepcionClave_2", "P002");
            parametro.Complemento.Add("PercepcionDescripcion_2", "Prima Dominical");
            parametro.Complemento.Add("PercepcionImporteGravado_2", "");
            parametro.Complemento.Add("PercepcionImporteExento_2", "55");

            #endregion "Percepciones"

            #region "Deducciones"

            parametro.Complemento.Add("DeduccionTipo_1", "1 - Seguridad social");
            parametro.Complemento.Add("DeduccionClave_1", "D001");
            parametro.Complemento.Add("DeduccionDescripcion_1", "IMSS");
            parametro.Complemento.Add("DeduccionImporte_1", "95.09");

            parametro.Complemento.Add("DeduccionTipo_2", "2 - ISR");
            parametro.Complemento.Add("DeduccionClave_2", "D002");
            parametro.Complemento.Add("DeduccionDescripcion_2", "Impuesto Sobre la Renta");
            parametro.Complemento.Add("DeduccionImporte_2", "379.09");

            parametro.Complemento.Add("DeduccionTipo_3", "4 - Otros");
            parametro.Complemento.Add("DeduccionClave_3", "D003");
            parametro.Complemento.Add("DeduccionDescripcion_3", "Comedor");
            parametro.Complemento.Add("DeduccionImporte_3", "209");

            Deducciones = 683.18M;

            #endregion "Deducciones"            

            if (cmbOpcion.SelectedValue.ToString() == "8" || cmbOpcion.SelectedValue.ToString() == "13")
                LlenaOtrosPagos(parametro);

            if (cmbOpcion.SelectedValue.ToString() == "9" || cmbOpcion.SelectedValue.ToString() == "13")
                LlenaHorasExtras(parametro);

            if (cmbOpcion.SelectedValue.ToString() == "10" || cmbOpcion.SelectedValue.ToString() == "13")
                LlenaIncapacidad(parametro);

            if (cmbOpcion.SelectedValue.ToString() == "11" || cmbOpcion.SelectedValue.ToString() == "13")
                LlenaSeparacionIndemnizacion(parametro);

            if (cmbOpcion.SelectedValue.ToString() == "12" || cmbOpcion.SelectedValue.ToString() == "13")
                LlenaJubilacionPensionRetiro(parametro);

            #endregion "Complemento"

            return parametro;
        }

        private void LlenaOtrosPagos(WCFCertificar.GeneraCFDIPeticion parametro)
        {
            decimal importeSubsidio = 100M;

            parametro.Complemento.Add("OtrosPagosTipo_1", "2 - Subsidio para el empleo (efectivamente entregado al trabajador).");
            parametro.Complemento.Add("OtrosPagosClave_1", "OP01");
            parametro.Complemento.Add("OtrosPagosDescripcion_1", "Subsidio al empleo");
            parametro.Complemento.Add("OtrosPagosImporte_1", importeSubsidio.ToString());

            SubTotal = SubTotal + importeSubsidio;
            parametro.Complemento["ConceptoPrecioUnitario_1"] = SubTotal.ToString();
            parametro.Complemento["ConceptoImporte_1"] = SubTotal.ToString();
            parametro.Encabezado["SubTotal"] = SubTotal.ToString();

            Total = SubTotal - Deducciones;
            parametro.Encabezado["Total"] = Total.ToString();
        }

        private void LlenaHorasExtras(WCFCertificar.GeneraCFDIPeticion parametro)
        {
            decimal importeHorasExtras = 437.5M;

            parametro.Complemento.Add("PercepcionTipo_3", "19 - Horas extra");
            parametro.Complemento.Add("PercepcionClave_3", "P003");
            parametro.Complemento.Add("PercepcionDescripcion_3", "Horas Extras");
            parametro.Complemento.Add("PercepcionImporteGravado_3", importeHorasExtras.ToString());
            parametro.Complemento.Add("PercepcionImporteExento_3", "");

            parametro.Complemento.Add("HorasExtrasTipo_1", " 3 - Simples");
            parametro.Complemento.Add("HorasExtrasDias_1", "3");
            parametro.Complemento.Add("HorasExtrasNumeroHoras_1", "12");
            parametro.Complemento.Add("HorasExtrasImporte_1", importeHorasExtras.ToString());

            SubTotal = SubTotal + importeHorasExtras;
            parametro.Complemento["ConceptoPrecioUnitario_1"] = SubTotal.ToString();
            parametro.Complemento["ConceptoImporte_1"] = SubTotal.ToString();
            parametro.Encabezado["SubTotal"] = SubTotal.ToString();

            Total = SubTotal - Deducciones;
            parametro.Encabezado["Total"] = Total.ToString();
        }

        private void LlenaIncapacidad(WCFCertificar.GeneraCFDIPeticion parametro)
        {
            decimal importeIncapacidad = 200M;

            parametro.Complemento.Add("DeduccionTipo_4", "6 - Descuento por incapacidad");
            parametro.Complemento.Add("DeduccionClave_4", "D004");
            parametro.Complemento.Add("DeduccionDescripcion_4", "Incapacidad");
            parametro.Complemento.Add("DeduccionImporte_4", importeIncapacidad.ToString());

            parametro.Complemento.Add("IncapacidadTipo_1", "2 - Enfermedad en general");
            parametro.Complemento.Add("IncapacidadNumeroDias_1", "1");
            parametro.Complemento.Add("IncapacidadImporte_1", importeIncapacidad.ToString());

            Total = SubTotal - Deducciones - importeIncapacidad;
            parametro.Encabezado["Total"] = Total.ToString();
        }

        private void LlenaSeparacionIndemnizacion(WCFCertificar.GeneraCFDIPeticion parametro)
        {
            parametro.Complemento.Add("SeparacionIndemnizacionTotalPagado", Total.ToString());
            parametro.Complemento.Add("SeparacionIndemnizacionNumerioAñosServicio", "25");
            parametro.Complemento.Add("SeparacionIndemnizacionUltimoSueldoMensualOrdinario", "3127");
            parametro.Complemento.Add("SeparacionIndemnizacionIngresoAcumulable", "680000");
            parametro.Complemento.Add("SeparacionIndemnizacionIngresoNoAcumulable", "20000");
        }

        private void LlenaJubilacionPensionRetiro(WCFCertificar.GeneraCFDIPeticion parametro)
        {
            parametro.Complemento.Add("JubilacionPensionRetiroTotalUnaExhibicion", "500000");
            //parametro.Complemento.Add("JubilacionPensionRetiroTotalParcialidad", "1500000");
            //parametro.Complemento.Add("JubilacionPensionRetiroMontoDiario", "6000");
            parametro.Complemento.Add("JubilacionPensionRetiroIngresoAcumulable", "1800000");
            parametro.Complemento.Add("JubilacionPensionRetiroIngresoNoAcumulable", "200000");
        }

        private string ObtieneDirectorioAplicacion()
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
        private string ObtieneAtributo(List<KeyValuePair<string, string>> lista, string key)
        {
            dynamic valor;

            valor = lista.FirstOrDefault(con => con.Key.ToUpper().Trim() == key.ToUpper().Trim()).Value;

            if (string.IsNullOrEmpty(valor) == true)
                valor = "";

            return valor;
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                Process.Start("explorer.exe", PDFGenerado);
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.LogUI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Alta, ex.Message, enumCategoria.Debug);
                MessageBox.Show("No se puede visualizar el archivo PDF probablemente no tiene instaladao Adobe Acrobat Reader algun otro lector de PDF.");
            }
        }
    }
}
