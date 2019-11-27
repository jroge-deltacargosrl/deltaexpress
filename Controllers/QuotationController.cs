using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaXpress.Models;
using DeltaXpress.Models.DAL;
using DeltaXpress.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DeltaXpress.Controllers
{
    public class QuotationController : Controller
    {

        private readonly MailServicesDAL mailServices;

        public QuotationController(MailServicesDAL mailServices)
        {
            this.mailServices = mailServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("SendMail")]
        public JsonResult SendMailQuotation(QuotationDetailsViewDTO quotationDetails)
        {
            var quotationResponseMail = mailServices.sendMail(quotationDetails);
            return new JsonResult(quotationResponseMail);
        }


      
    }
}