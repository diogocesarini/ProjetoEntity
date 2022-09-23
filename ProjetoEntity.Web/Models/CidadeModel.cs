using System.ComponentModel.DataAnnotations;

namespace ProvaCandidato.Models
{
    public class CidadeModel
    {
        public int Codigo { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Minimo de caracteres é de 3 e o máximo é 50")]
        [Display(Name = "Nome da Cidade")]
        public string Nome { get; set; }
    }
}