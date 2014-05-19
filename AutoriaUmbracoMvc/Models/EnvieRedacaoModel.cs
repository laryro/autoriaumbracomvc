using System.ComponentModel.DataAnnotations;

namespace AutoriaUmbracoMvc.Models
{
    public class EnvieRedacaoModel
    {
        public EnvieRedacaoModel()
        {

        }

        [Required(ErrorMessage = "Anexe um arquivo com sua redação")]
        public string Anexo { get; set; }

        [Required(ErrorMessage = "Campo Nome obrigatório"), RegularExpression(@"^[^0-9]{1,}", ErrorMessage = "Nome inválido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo E-mail obrigatório"),
        RegularExpression(@"[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,4}", ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }


    }
}