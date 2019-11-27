using DeltaXpress.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DeltaXpress.Utils.UtilsApp;

namespace DeltaXpress.Models.DAL
{
    public class MailServicesDAL
    {

        public ResponseToRequest<QuotationDetailsViewDTO> sendMail(QuotationDetailsViewDTO quotation)
        {
            var response = new ResponseToRequest<QuotationDetailsViewDTO>
            {
                code = STATE_MAIL_OK, 
                dateResponse = DateTime.UtcNow,
                modelResponse = quotation,
                message = MESSAGE_SUCCESS_EMAIL
            };
            var mailResponse = new MessageEmail()
                .addFrom(EMAIL_EMISOR)
                .addToList(EMAIL_RECEPTOR)
                .addSubject(EMAIL_SUBJECT)
                .addBodyMessage(formatMessageForQuotation(quotation))
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
