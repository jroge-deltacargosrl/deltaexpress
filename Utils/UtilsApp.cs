using DeltaXpress.Models;
using DeltaXpress.Models.Types;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static readonly string MESSAGE_SUCCESS_EMAIL = "El Formulario ha sido enviado correctamente";
        public static readonly string MESSAGE_FAILED_EMAIL = "Rellene el Formulario Correctamente";

        public static string NO_APLICA = "N/A";


        // Definir un template con bootstrap para dar estilos personalizados a los mensajes
        public static string messageContact(ContactModel contact)
        {
            List<string> fieldsHeader = new List<string>() { "Empresa", "Nombre Completo", "Celular", "Correo Electronico", "Servicio", "Origen", "Destino" };
            return @"<html>
                    <body>
                        <p>Informacion del lead contactado por la web DELTA XPRESS:</p></br></br>
                            <table border =\" + "1" + @"\>" +
                            dataBody(fieldsHeader, fieldsContact(contact)) +
                        @"</table>
                        </br></br></br>
                    </body>
                    </html>";
        }

        private static List<string> fieldsContact(ContactModel contact)
        {
            string company = contact.company.Trim();
            string fullname = contact.fullname.Trim();
            string phone = contact.phone.Trim();
            string email = "sin correo de referencia";
            if (!string.IsNullOrEmpty(contact.email))
            {
                email = contact.email.Trim();
            }
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
        

        /// <summary>
        /// Metodo que lee un documento .js 
        /// </summary>
        /*public static JavaScriptResult readJs(IHostingEnvironment hosting,string nameJs)
        {
            string webRootPath = hosting.WebRootPath;
            string result = System.IO.File.ReadAllText($"{webRootPath}/js/{nameJs}");
            return new JavaScriptResult(result);
        }*/


    }
}
