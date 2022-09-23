using System;
using System.ComponentModel.DataAnnotations;

namespace ProvaCandidato.Models
{
    public class ClienteModel
    {
        [Display(Name ="Nome")]
        public string Nome { get; set; }
        
        [Display(Name = "Cidade")]
        public string CidadeNome { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        public int Codigo { get; set; }
        public int CidadeId { get; set; }
    }
}