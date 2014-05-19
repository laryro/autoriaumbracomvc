using System.Web.Mvc;
using System.Net.Mail;
using AutoriaUmbracoMvc.Models;


namespace AutoriaUmbracoMvc.Controllers
{
    public class ContatoController : Umbraco.Web.Mvc.SurfaceController  
    {
        
        [HttpPost]
        public ActionResult Contato(ContatoModel form)
        {

            if (ModelState.IsValid)
            {
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

                msg.Subject = "[Site Autoria]" + form.Assunto;

                msg.IsBodyHtml = true;
                msg.BodyEncoding = System.Text.Encoding.UTF8;

                smtp.Send(msg);
            }

            return View();
        }

        [HttpPost]
        public ActionResult EnviaRedacao(EnvieRedacaoModel model) {

            if (ModelState.IsValid) {

                var arquivo = Request.Files.Get("anexo");

                MailAddress endereco = new MailAddress("beatriz.carreiro@inspira.com.br");
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;


                MailMessage msg = new MailMessage("beatriz.carreiro@inspira.com.br", endereco.Address);
                msg.Body = string.Format("<h3>Redação</h3>");
                msg.Subject = "[Site Autoria] Redação";

                msg.IsBodyHtml = true;
                msg.BodyEncoding = System.Text.Encoding.UTF8;

                smtp.Send(msg);
            }

            return View();
        }
     
    }
}
