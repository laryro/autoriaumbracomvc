using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using AutoriaUmbracoMvc.Models;
using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using umbraco.DataLayer;
using umbraco.NodeFactory;
using umbraco;
using System.Net.Mime;


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

                ViewBag.sucesso = "Sua mensagem foi enviada com sucesso!";
            }

            return View();
        }

        [HttpPost]
        public ActionResult EnvieRedacao(EnvieRedacaoModel model) {

            if (ModelState.IsValid) {


                var arquivo = Request.Files.Get("anexo");

                var caminhoArquivo = "";
                caminhoArquivo = SalvarArquivo("~/UserUploads/", arquivo);

                Attachment data = new Attachment(
                         caminhoArquivo,
                         MediaTypeNames.Application.Octet);

                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;

                MailAddress endereco = new MailAddress("beatriz.carreiro@inspira.com.br");
                MailMessage msg = new MailMessage("beatriz.carreiro@inspira.com.br", endereco.Address);

                msg.Body = string.Format(
                    "<h3>Redação</h3>" + "<br>" +
                    "Nome: " + model.Nome + "<br>" +
                    "E-mail: " + model.Email
                    );

                msg.Subject = "[Site Autoria] Redação";
                
                msg.IsBodyHtml = true;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.Attachments.Add(data);


                smtp.Send(msg);

                ViewBag.sucesso = "Sua mensagem foi enviada com sucesso!";

            }

            return View();
        }


        public string SalvarArquivo(String caminho, HttpPostedFileBase arquivo)
        {
            var path = Server.MapPath(caminho);

            var nome = arquivo.FileName;
            var extensao = nome.Split(new[] { '.' }).Last().ToLower();
            nome = nome + "_" + DateTime.Now.Ticks + "." + extensao;

            var pathArquivo = String.Format("{0}\\{1}", path, nome);

            if (System.IO.File.Exists(pathArquivo))
            {
                System.IO.File.Delete(pathArquivo);
            }
            else
            {
                System.IO.Directory.CreateDirectory(path);
            }

            arquivo.SaveAs(pathArquivo);
            return pathArquivo;
        }
     
    }
}
