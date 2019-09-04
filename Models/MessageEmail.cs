using DeltaXpress.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DeltaXpress.Models
{
    public class MessageEmail
    {
        public List<string> toList { get; set; } = new List<string>();
        public string from { get; set; }
        public string subject { get; set; }
        public string bodyMessage { get; set; }
        public bool isBodyHTML { get; set; } = false;

        public MessageEmail addToList(params string[] to)
        {
            to.ToList().ForEach((v) => toList.Add(v));
            //this.toList.AddRange(toList);
            return this;
        }

        public MessageEmail addFrom(string from)
        {
            this.from = from;
            return this;
        }

        public MessageEmail addSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public MessageEmail addBodyMessage(string bodyMessage)
        {
            this.bodyMessage = bodyMessage;
            return this;
        }

        public MessageEmail enableBodyHtml()
        {
            isBodyHTML = true;
            return this;
        }

        public bool send()
        {
            MailMessage mail = new MailMessage();
            // recorrer la lista de direcciones a las cuales se enviara el correo
            foreach (string dir in toList)
            {
                mail.To.Add(dir);
            }
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.SubjectEncoding = Encoding.UTF8; // codificacion UTF-8

            mail.Body = bodyMessage;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = isBodyHTML;


            mail.Priority = MailPriority.Normal;
            // crear la instancia para el servidor SMTP
            SmtpClient smtp = new SmtpClient();
            smtp.Host = UtilsApp.HOST;
            smtp.Port = UtilsApp.PUERTO;
            smtp.EnableSsl = UtilsApp.HABILITAR_SSL;
            smtp.UseDefaultCredentials = UtilsApp.USAR_CREDENCIALES_DEFAULT;
            smtp.Credentials = new NetworkCredential(UtilsApp.USER, UtilsApp.PASS);
            smtp.Timeout = 200000;
            try
            {
                smtp.Send(mail);
                mail.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /*Email email = new Email()
        {
            bodyMessage = UtilsLandingPage.messageContact(fieldsContact(contacto)),
            toList = new List<string>() { UtilsLandingPage.EMAIL_RECEPTOR },
            from = UtilsLandingPage.EMAIL_EMISOR,
            subject = UtilsLandingPage.EMAIL_SUBJECT
        };
            /*ViewBag.data = email.send()?
                    UtilsLandingPage.MESSAGE_SUCCESS_EMAIL:
                    UtilsLandingPage.MESSAGE_FAILED_EMAIL;

            return Json(
                new ResponseContacto()
        {
            date = DateTime.Now,
                    message = email.send() ?
                    UtilsLandingPage.MESSAGE_SUCCESS_EMAIL :
                    UtilsLandingPage.MESSAGE_FAILED_EMAIL
                }
            , JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", new { responseFormContact = ViewBag.data });
            //return View();*/
    }
}
