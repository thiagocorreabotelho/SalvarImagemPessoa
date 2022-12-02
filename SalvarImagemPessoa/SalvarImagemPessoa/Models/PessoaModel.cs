using System.ComponentModel.DataAnnotations;

namespace SalvarImagemPessoa.Models
{
    public class PessoaModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Nome { get; set; }

        public string Foto { get; set; }
    }
}
