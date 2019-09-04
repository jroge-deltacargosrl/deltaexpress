using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeltaXpress.Models;
using DeltaXpress.Models.DAL;
using DeltaXpress.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;

namespace DeltaXpress.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment; // no utilizado

        public ContactController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public JsonResult ContactForm(ContactModel contactRequest)
        {
            ContactResponse response = ContactDAL.sendEmail(contactRequest);
            return new JsonResult(response);
            // aqui obtengo la respuesta de la llamada al codigo de node
            /*string messageNodeJs = await nodeServices.InvokeAsync<string>("./wwwroot/js/example1/",new ContactResponse { code = 200});

            await Response.WriteAsync("<script>alert('" +messageNodeJs+ "');</script>");*/
            //return RedirectToAction("Index", "Home");
        }

        

    }
}