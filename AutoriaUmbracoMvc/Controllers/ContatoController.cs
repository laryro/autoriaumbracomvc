using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using AutoriaUmbracoMvc.Models;
using AutoriaUmbracoMvc.Code;
using Umbraco.Web.Mvc;
using umbraco;
using umbraco.DataLayer;

namespace AutoriaUmbracoMvc.Controllers
{
    public class ContatoController : Umbraco.Web.Mvc.SurfaceController  
    {
        
        [HttpPost]
        public ActionResult Contato(ContatoModel form)
        {

            if (ModelState.IsValid)
            {
                var node = umbraco.uQuery.GetCurrentNode();

                MailAddress endereco = new MailAddress("beatriz.carreiro@inspira.com.br");
                SmtpClient smtp = new SmtpClient();
                

                smtp.EnableSsl = true;
                

                MailMessage msg = new MailMessage("beatriz.carreiro@inspira.com.br", endereco.Address);
                msg.Body = string.Format(
                                "<h3>Contato Autoria</h3>" +
                                "<p>Nome: " + form.Nome + "<br/>" +
                                "Telefone: " + form.Telefone + "<br/>" +
                                "Celular: " + form.Celular + "</p>" +
                                "<p>" + form.Mensagem + "</p>");

                msg.Subject = form.Assunto;

                msg.IsBodyHtml = true;
                msg.BodyEncoding = System.Text.Encoding.UTF8;

                smtp.Send(msg);
            }

            return View();
        }
     
    }
}
