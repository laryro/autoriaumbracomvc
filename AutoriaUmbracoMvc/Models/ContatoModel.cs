using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoriaUmbracoMvc.Models
{
    public class ContatoModel
    {

        public ContatoModel() {
            var assunto = new Dictionary<string, string>();

            assunto.Add("Dúvidas", "Dúvidas");
            assunto.Add("Reclamações", "Reclamações");
            assunto.Add("Sugestões", "Sugestões");

            this.Assuntos = new SelectList(
            assunto.Select(x => new { value = x.Key, text = x.Value }), "value", "text"); 
        }
       
        [Required(ErrorMessage = "Campo Nome obrigatório"), RegularExpression(@"^[^0-9]{1,}", ErrorMessage = "Nome inválido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo E-mail obrigatório"),
        RegularExpression(@"[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}", ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Campo Telefone obrigatório"), RegularExpression(@"^\([0-9]{2}\)[\s][0-9]{4}-[0-9]{4,5}",
        ErrorMessage = "Telefone invalido")]
        public string Telefone { get; set; }

        [RegularExpression(@"^\([0-9]{2}\)[\s][0-9]{4}-[0-9]{4,5}", ErrorMessage = "Celular invalido")]
        public string Celular { get; set; }

        public SelectList Assuntos { get; set; }

        [Required(ErrorMessage = "Campo Assunto obrigatório")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "Campo Mensagem obrigatório")]
        public string Mensagem { get; set; }
    }
}