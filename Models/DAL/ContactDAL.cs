using DeltaXpress.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DeltaXpress.Utils.UtilsApp;

namespace DeltaXpress.Models.DAL
{
    public class ContactDAL
    {

        public static ContactResponse sendEmail(ContactModel contact)
        {
            // ver la forma de validar este envio de datos en el mismo formulario
            var response = new ContactResponse
            {
                code = 200, // cambiar a los codigos http (ok)
                dateResponse = DateTime.UtcNow,
                message = MESSAGE_SUCCESS_EMAIL
            };
            var mailResponse =new MessageEmail()
                .addFrom(EMAIL_EMISOR)
                .addToList(EMAIL_RECEPTOR)
                .addSubject(EMAIL_SUBJECT)
                .addBodyMessage(formatMessageForQuotation(contact))
                .enableBodyHtml()
                .send();
            if (!mailResponse)
            {
                response.code = 400; // bad request
                response.message = MESSAGE_FAILED_EMAIL;
            }
            return response; 

        }
    }
}
