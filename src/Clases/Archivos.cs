using System;
using System.Web;
using System.Text;
using System.Collections;
using System.IO;
using System.IO.Compression;

using System.Configuration;
using System.Diagnostics;

namespace SECFDI.KitDesarrollo.Clases
{
    /// <summary>
    /// Clase que permite realizar operaciones basicas y comunes 
    /// del manejo de archivos como son: salvar, eliminar 
    /// abrir archivos a una ruta especifica
    /// </summary>
    public class Archivos
    {
        public bool resultado { get; set; }
        /// <summary>
        /// Crea una instancia de Modulo
        /// </summary>
        public Archivos()
        {
        }

        /// <summary>
        /// Libera los recursos de la memoria
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Es el destructor de la clase
        /// </summary>
        ~Archivos()
        {
            this.Dispose();
        }

        #region "Metodos Privados" 

        #endregion "Metodos Privados" 

        #region "Metodos Publicos" 
        
        /// <summary>
        /// Valida si existe un archivo en una ruta especifica
        /// </summary>
        /// <param name="ruta">Es la ruta de los archivos </param>
        /// <returns></returns>
        public bool Existe(string ruta)
        {
            return File.Exists(ruta);
        }

        /// <summary>
        /// Eliminar un archivo de una ruta especifica 
        /// </summary>
        /// <param name="ruta">Es la ruta de los archivos </param>
        /// <returns></returns>
        public bool Eliminar(string ruta)
        {
            try
            {                
                if (File.Exists(ruta))
                    File.Delete(ruta);

                resultado = true; 
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.ManejoArchivos, enumActividad.Eliminar, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }

            return resultado;
        }

        ///// <summary>
        ///// Devuelve la lista de archivos que estan en un directorio filtrando por extensiones
        ///// </summary>
        ///// <param name="RutaDirectorio">Ruta de directorio donde se empieza la busqueda</param>
        ///// <param name="extensiones">Nombre de las extensiones de los archivos que se van a buscar</param>
        public void EliminarArchivosDirectorio(string ruta)
        {
            try
            {
                string[] directorios = Directory.GetDirectories(ruta);

                foreach (string directorio in directorios)
                {
                    DirectoryInfo tmp = new DirectoryInfo(directorio);
                    FileInfo[] archivos = tmp.GetFiles();

                    foreach (FileInfo archivo in archivos)
                    {
                        archivo.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
        }

        /// <summary>
        /// Eliminar todos los archivos que se encuentran en un directorio
        /// </summary>
        /// <param name="ruta">Es la ruta de los archivos </param>
        /// <returns></returns>
        public bool EliminarArchivoDirectorio(string ruta)
        {
            bool resultado = false;
            string [] archivos;

            try
            {
                archivos = Directory.GetFiles(ruta);

                foreach (string archivo in archivos)
                {
                    if (File.Exists(archivo))
                        File.Delete(archivo);
                }

                resultado = true;

            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.ManejoArchivos, enumActividad.Eliminar, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }

        /// <summary>
        /// Eliminar todos los archivos que se encuentran en un directorio
        /// </summary>
        /// <param name="ruta">Es la ruta de los archivos </param>
        /// <returns></returns>
        public bool CreaDirectorio(string ruta)
        {
            bool resultado = false;

            //Create a new subfolder under the current active folder            
            try
            {
                if (Directory.Exists(ruta) == false)
                {
                    Directory.CreateDirectory(ruta);
                }

                resultado = true;
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.ManejoArchivos, enumActividad.Eliminar, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }


        /// <summary>
        /// Eliminar todos los archivos que se encuentran en un directorio
        /// </summary>
        /// <param name="ruta">Es la ruta de los archivos </param>
        /// <returns></returns>
        public bool CreaDirectorio(string ruta, string nombreDirectorio)
        {
            bool resultado = false;
            
            //Create a new subfolder under the current active folder
            ruta = System.IO.Path.Combine(ruta, nombreDirectorio);

            try
            {
                if (Directory.Exists(ruta) == false)
                {
                    Directory.CreateDirectory(ruta);
                }

                resultado = true;
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.ManejoArchivos, enumActividad.Eliminar, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }


        /// <summary>
        /// Abrir un archivo de una ruta especifica en forma de Strea,
        /// </summary>
        /// <param name="ruta">Es la ruta de los archivos </param>
        /// <returns></returns>
        public FileStream Abrir(string ruta)
        {
            FileStream resultado = null;
            
            try
            {
                resultado = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Delete);
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }

        public String AbrirBase64(string ruta)
        {
            String file = string.Empty; 

            try
            {
                file = Convert.ToBase64String(File.ReadAllBytes(ruta));                
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return file;
        }


        /// <summary>
        /// Abrir un archivo y devolver un string con el contenido
        /// </summary>
        /// <param name="ruta">Es la ruta de los archivos </param>
        /// <returns></returns>
        public string AbrirModoTexto(string ruta)
        {
            string cadena = string.Empty;
            FileStream resultado = null;

            try
            {

                resultado = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Read);
                using (StreamReader contenidoArchivo = new StreamReader(resultado))
                {
                    cadena = contenidoArchivo.ReadToEnd();
                    contenidoArchivo.Close();
                }
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            finally 
            {
                resultado = null;
            }
            return cadena;
        }

        /// Guarda un archivo en una ubicacion que se especifica como parametro
        /// La condicion es que debe de tener permisos el usuario de asp net para poderlo realizar
        /// </summary>
        public bool Guardar(string contenido, string ruta)
        {
            bool resultado = false;

            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete))
                {
                    StreamWriter tmpArchivo = new StreamWriter(fs);

                    tmpArchivo.Write(contenido);
                    tmpArchivo.Flush();
                    tmpArchivo.Close();
                }

                resultado = true;
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }


        /// <summary>
        /// Guarda un archivo en una ubicacion que se especifica como parametro
        /// La condicion es que debe de tener permisos el usuario de asp net para poderlo realizar
        /// </summary>
        public bool Guardar(Stream archivo, string ruta)
        {
            bool resultado = false;

            try
            {
                long tamaño = archivo.Length;
                byte[] archivoBytes = new byte[tamaño];
                
                using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete))
                {
                    archivo.Read(archivoBytes, 0, archivoBytes.Length);
                    fs.Write(archivoBytes, 0, archivoBytes.Length);
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }


        /// <summary>
        /// Guarda un archivo en una ubicacion que se especifica como parametro
        /// La condicion es que debe de tener permisos el usuario de asp net para poderlo realizar
        /// </summary>
        public bool Guardar(byte[] archivo, string ruta)
        {
            bool resultado = false;

            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete))
                {
                    fs.Write(archivo, 0, archivo.Length);
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }

        /// <summary>
        /// Guarda un archivo en una ubicacion que se especifica como parametro
        /// La condicion es que debe de tener permisos el usuario de asp net para poderlo realizar
        /// </summary>
        public byte[] ConvertirStreamToByte(Stream archivo)
        {
            byte[] archivoBytes = null;

            try
            {
                archivoBytes = new byte[archivo.Length];
                archivo.Read(archivoBytes, 0, archivoBytes.Length);
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return archivoBytes;
        }

        public byte[] ConvertirBase64ToByte(String file)
        {
            byte[] archivoBytes = null;

            try
            {
                archivoBytes = Convert.FromBase64String(file);                
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return archivoBytes;
        }

        public String ConvertirStringToBase64(string valor)
        {
            String file = string.Empty;

            try
            {
                file = Convert.ToBase64String(Encoding.UTF8.GetBytes(valor));
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return file;
        }

        public String ConvertirBase64ToString(string valor)
        {
            String file = string.Empty;

            try
            {
                byte[] data = Convert.FromBase64String(valor);
                file = Encoding.UTF8.GetString(data);
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return file;
        }
        
        public String ConvertirByteToBase64(byte[] valor)
        {
            String file = string.Empty;

            try
            {
                file = Convert.ToBase64String(valor);
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return file;
        }

        public string ConvertirStreamToStringUTF8(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public Stream ConvertirStringToStreamUTF8(string fuente)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(fuente);
            return new MemoryStream(byteArray);
        }


        /// <summary>
        /// Convierte un archivo Unix a Widnows
        /// </summary>
        /// <param name="contenido"></param>
        /// <returns></returns>
        public string UnixWindows(String contenido)
        {
            if (contenido.IndexOf('\r') != -1) return contenido;

            Char[] retornoCarro = { '\n' };
            String[] lineas = contenido.Split(retornoCarro, StringSplitOptions.None);
            return String.Join("\r\n", lineas);
        }

        /// <summary>
        /// Mueve un archivo de una ubicacion a otroa 
        /// </summary>
        /// <returns></returns>
        public bool MoverArchivo(string fuente, string destino)
        {
            bool resultado = false;

            try
            {
                try
                {
                   if (Existe(destino))
                   {
                        Eliminar(destino);
                   }
                }
                catch
                {
                }

                System.IO.File.Move(fuente, destino);
                resultado = true;
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }

        public byte[] ObtieneBytes(string fileName)
        {
            byte[] buffer = null;

            try
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, System.IO.FileAccess.Read);
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            
            return buffer;
        }

        public bool GeneraPFXSSL(string certificado, string llavePrivada, string contraseña, string pfx)
        {
            bool resultado = false;

            string rutaEjecutable = ConfigurationManager.AppSettings["PathEXEPFX"];
            string parametros = string.Empty;

            Process process = new Process();
           

            try
            {
                parametros = " x509 -inform DER -outform PEM -in \"" + certificado + "\" -out \"" + certificado + ".pem\""; 

                process = new Process
                {
                    StartInfo = { FileName = rutaEjecutable, Arguments = parametros, RedirectStandardInput = process.StartInfo.RedirectStandardOutput = true, CreateNoWindow = true, UseShellExecute = false }
                };

                process.Start();
                process.WaitForExit();
                process.Close();

                parametros = " pkcs8 -inform DER -in \"" + llavePrivada + "\" -passin pass:" + contraseña + " -out \"" + llavePrivada + ".pem\""; 
                              
                process = new Process
                {
                    StartInfo = { FileName = rutaEjecutable, Arguments = parametros, RedirectStandardInput = process.StartInfo.RedirectStandardOutput = true, CreateNoWindow = true, UseShellExecute = false }
                };
                process.Start();
                process.WaitForExit();
                process.Close();

                parametros = " pkcs12 -export -in \"" + certificado + ".pem\"" + " -inkey \"" + llavePrivada + ".pem\"" + " -out \"" + pfx + "\" -passout pass:" + contraseña;
                
                process = new Process
                {
                    StartInfo = { FileName = rutaEjecutable, Arguments = parametros, RedirectStandardInput = process.StartInfo.RedirectStandardOutput = true, CreateNoWindow = true, UseShellExecute = false }
                };
                process.Start();
                process.WaitForExit();

                resultado = true;

            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.Sello, enumServicios.Certificados, enumActividad.Agregar, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            finally
            {
                process.Dispose();
                process.Close();
            }

            return resultado;
        }

        public bool Comprimir(string NombreArchivo, string RutaArchivoOrigen, string RutaArchivoDestino)
        {
            bool resultado = false;

            try
            {
                using (ZipArchive archivoComprimido = ZipFile.Open(RutaArchivoDestino, ZipArchiveMode.Create ))
                {
                    archivoComprimido.CreateEntryFromFile(RutaArchivoOrigen, NombreArchivo, CompressionLevel.Optimal);
                    archivoComprimido.Dispose();
                }                
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Alta, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }

            return resultado;
        }

        public bool Renombrar(string ArchivoOrigen, string ArchivoDestino)
        {
            bool resultado = false;

            try
            {
                System.IO.File.Move(ArchivoOrigen, ArchivoDestino);
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Alta, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }

            return resultado;
        }


        #endregion
    }
}
