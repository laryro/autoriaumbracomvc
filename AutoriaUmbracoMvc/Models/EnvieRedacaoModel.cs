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

    }
}