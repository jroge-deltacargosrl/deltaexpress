using DeltaXpress.Models;
using DeltaXpress.Models.DTO;
using DeltaXpress.Models.Infraestructure;
using DeltaXpress.Models.Types;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeltaXpress.Utils
{
    public class UtilsApp
    {

        // Obtener estos datos desde origines externos para la proxima actualizacion
        public static readonly string HOST = "smtp.office365.com";
        public static readonly int PUERTO = 587;
        public static readonly bool HABILITAR_SSL = true;
        public static readonly bool USAR_CREDENCIALES_DEFAULT = false;
        public static readonly string USER = "comercial@deltacargosrl.com";
        public static readonly string PASS = "1025565029Cdc.";


        public static readonly string EMAIL_EMISOR = "comercial@deltacargosrl.com";
        public static readonly string EMAIL_RECEPTOR = "comercial@deltacargosrl.com";
        public static readonly string EMAIL_SUBJECT = "LEAD DELTAX";


        public const int STATE_MAIL_OK = 200;
        public static readonly string MESSAGE_SUCCESS_EMAIL = "Solicitud de Cotización realizada con éxito, pronto nos pondremos en contacto contigo.";
        public static readonly string MESSAGE_FAILED_EMAIL = "Algo extraño sucedio, envia nuevamente tu solicitud de cotización";

        public static string NO_APLICA = "N/A";


        public static string URL_BASE { get; private set; }

        public static void initVarEnviroment(DevelopmentEnvironment environment = DevelopmentEnvironment.IISServer)
        {
            switch (environment)
            {
                case DevelopmentEnvironment.IISExpressServer:
                    URL_BASE = "https://localhost:44333/api/v1/";
                    break;
                case DevelopmentEnvironment.AzureServer:
                    URL_BASE = "https://deltacargoapi.azurewebsites.net/api/v1";
                    break;
                case DevelopmentEnvironment.IISServer:
                    //URL_BASE = "https://deltacargoapi.azurewebsites.net/api/v1";
                    break;
                default:
                    break;
            }
            /*switch (environment)
            {
                case "developing":
                    URL_BASE = "https://localhost:44333/api/v1/";
                    break;
                case "local production":
                    //URL_BASE = "http://deltacargoapi.azurewebsites.net/api/v1";
                    break;
                case "cloud production":
                    URL_BASE = "http://deltacargoapi.azurewebsites.net/api/v1";
                    break;
            }*/
        }

        // Definir un template con bootstrap para dar estilos personalizados a los mensajes
        // REDEFINIR EL METODO DE ENVIO DEL MAIL 
        public static string formatMessageForQuotation(QuotationDetailsViewDTO quotationDetails)
        {

            StringBuilder sb = new StringBuilder();
            string textHeader = "INFORMACION DEL LEAD GENERADO POR LA WEB DELTA X :";
            return sb.AppendLine("<html>")
                .AppendLine("<body>")
                .AppendLine($"<strong>{textHeader}</strong>")
                .AppendLine("</br></br>")
                .AppendLine("<table border=\"1\">")
                .AppendLine(dataBody(quotationDetails))
                .AppendLine("</table>")
                .AppendLine("</body>")
                .AppendLine("</html>")
                .ToString();


            /*List<string> fieldsHeader = new List<string>() { "Empresa", "Nombre Completo", "Celular", "Correo Electronico", "Servicio", "Origen", "Destino" };
            return @"<html>
                    <body>
                        <p>Informacion del lead contactado por la web DELTAX:</p></br></br>
                            <table border =\" + "1" + @"\>" +
                            dataBody(fieldsHeader, fieldsContact(quotation)) +
                        @"</table>
                        </br></br></br>
                    </body>
                    </html>";*/
        }

        // Refactorizar el metodo a un metodo generico
        private static string dataBody(QuotationDetailsViewDTO quotationDetails)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, object> headerText = new Dictionary<string, object>()
            {
                ["Nombre Completo"] = quotationDetails.name,
                ["Empresa"] = quotationDetails.company,
                ["Celular"] = quotationDetails.phone,
                ["Correo Electrónico"] = quotationDetails.email,
                ["Tipo de Servicio"] = quotationDetails.serviceType
            };
            foreach (var itemHeader in headerText)
            {
                sb.AppendLine("<tr>")
                    .AppendLine($"<td><strong>{itemHeader.Key}</strong></td>")
                    .AppendLine($"<td><i>{itemHeader.Value}</i></td>")
                    .AppendLine("</tr>");
            }
            // logica para agregar a la tabla los campos que se requieren de acuerdo al tipo de servicio
            switch (quotationDetails.serviceType)
            {
                case "Transporte Urbano en SCZ":
                    // sin campo en particular
                    break;
                case "Almacenamiento de Carga en SCZ":
                    sb.AppendLine($"<td><strong>Capacidad de Almacenamiento</strong></td>")
                        .AppendLine($"<td><i>{quotationDetails.storageCapacity}</td></i>")
                        .AppendLine($"<td><strong>Tiempo de Almacenamiento</strong></td>")
                        .AppendLine($"<td><i>{quotationDetails.storageTime}</td></i>");
                    break;
                case "Transporte Nacional":
                    sb.AppendLine($"<td><strong>Ciudad Origen </strong></td>")
                        .AppendLine($"<td><i>{quotationDetails.cityDestination}</i></td>")
                        .AppendLine($"<td><strong>Ciudad Destino </strong></td>").
                        AppendLine($"<td><i>{quotationDetails.cityDestination}</i></td>");
                    break;
                case "Transporte Internacional":
                    sb.AppendLine($"<td><strong>Ruta Origen </strong></td>")
                        .AppendLine($"<td><i>{quotationDetails.countryOrigin}</i></td>")
                        .AppendLine($"<td><strong>Ciudad Origen </strong></td>")
                        .AppendLine($"<td><i>{quotationDetails.cityOrigin}</i></td>")
                        .AppendLine($"<td><strong>Ruta Destino </strong></td>")
                        .AppendLine($"<td><i>{quotationDetails.countryDestination}</i></td>")
                        .AppendLine($"<td><strong>Ciudad Destino </strong></td>")
                        .AppendLine($"<td><i>{quotationDetails.cityDestination}</i></td>");
                    break;
                case "Ruta Urbana SCZ":
                    sb.AppendLine($"<td><strong>Dirección </strong></td>")
                        .AppendLine($"<td><i>{quotationDetails.street}</i></td>");
                    break;
                default:
                    break;
            }
            if (!quotationDetails.serviceType.Equals("Almacenamiento de Carga en SCZ"))
            {
                sb.AppendLine($"<td><strong>Peso </strong></td>")
                    .AppendLine($"<td><i>{quotationDetails.loadWeight} Tn. </i></td>")
                    .AppendLine($"<td><strong>Volumen </strong></td>")
                    .AppendLine($"<td><i>{quotationDetails.loadVolume} m3 </i></td>");
            }
            return sb.AppendLine($"<td><strong>Coméntanos </strong></td>")
                .AppendLine($"<td><i>{quotationDetails.comment}</i></td>")
                .ToString();

        }

        public static string field(string value,string defaultValue="") => !isNull(value) ? value.Trim(): defaultValue;

        // Refactorizar este metodo
        private static List<string> fieldsContact(ContactModel contact)
        {
            string company =  field(contact.company);
            string fullname = field(contact.fullname);
            string phone = field(contact.phone);
            string email = field(contact.email, "sin correo de referencia");
            Load_Type serviceType = contact.serviceType;
            string origen = NO_APLICA, destino = NO_APLICA;
            if (serviceType.Equals(Load_Type.TRANSPORTE_NACIONAL))
            {
                origen = contact.transportRoute.loadingSource;
                destino = contact.transportRoute.loadingDestination;
            }
            return new List<string>() { company, fullname, phone, email, serviceType.ToString(), origen, destino };
        }

        private static string dataBody(List<string> fielsHeader, List<string> dataContact)
        {
            string data = string.Empty; int i = 0;
            dataContact.ForEach(val => data += $"<tr><td><b>{fielsHeader[i++]}</b></td><td><i>{val}<i></td></tr>");
            return data;
        }

        public static bool isNull<T>(T obj) => obj == null;



        /// <summary>
        /// Metodo que lee un documento .js 
        /// </summary>
        /*public static JavaScriptResult readJs(IHostingEnvironment hosting,string nameJs)
        {
            string webRootPath = hosting.WebRootPath;
            string result = System.IO.File.ReadAllText($"{webRootPath}/js/{nameJs}");
            return new JavaScriptResult(result);
        }*/

        #region "Métodos JSON"
        public static string serializeJSON<T>(T value)
            => JsonConvert.SerializeObject(value);

        public static T deserializeJSON<T>(string jsonValue)
            => JsonConvert.DeserializeObject<T>(jsonValue);
        #endregion



    }
}
