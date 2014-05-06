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
        //
        // GET: /Contato/

        public ActionResult Contato(ContatoModel form)
        {

            if (ModelState.IsValid)
            {
                var node = umbraco.uQuery.GetCurrentNode();

                MailAddress endereco = new MailAddress(form.Destinatario);
                SmtpClient smtp = new SmtpClient();

                smtp.EnableSsl = true;

                MailMessage msg = new MailMessage("beatriz.carreiro@inspira.com.br", endereco.Address);
                msg.Subject = uQuery.GetCurrentNode().Name;


                foreach (var prop in form.GetType().GetProperties())
                {
                    if (prop.PropertyType != typeof(SelectList) && prop.Name != "Cep2")
                    {
                        if (prop.PropertyType == typeof(Boolean))
                        {
                            msg.Body += prop.Name + " : " + (prop.GetValue(form, null).ToString() == "True" ? "Verdadeiro" : "Falso") + "<br/>";
                        }
                        else if (prop.Name == "Cep")
                        {
                            msg.Body += prop.Name + " : " + prop.GetValue(form, null) + "-" + form.GetType().GetProperty("Cep2").GetValue(form, null) + "<br/>";
                        }
                        else
                        {
                            msg.Body += prop.Name + " : " + prop.GetValue(form, null) + "<br/>";
                        }
                    }
                }

                msg.IsBodyHtml = true;
                msg.BodyEncoding = System.Text.Encoding.UTF8;

                smtp.Send(msg);
            }

            return View();
        }

    }
}
