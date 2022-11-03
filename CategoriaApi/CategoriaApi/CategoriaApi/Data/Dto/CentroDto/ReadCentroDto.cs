using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.CentroDto
{
    public class ReadCentroDto
    {   
        [Required]
        [Key]
        public int Id { get; set; }

        [RegularExpression(@"[a-zA-Zá-úÁ-Ú[0-9]' '", ErrorMessage = "O campo nome não deve conter caracteres especiais")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(128, ErrorMessage = "Tamanho máximo de 128 caracteres excedido")]
        public string Nome { get; set; }

        [RegularExpression(@"[a-zA-Zá-úÁ-Ú[0-9]' '", ErrorMessage = "O campo logradouro não deve conter caracteres especiais")]
        [StringLength(258, ErrorMessage = "Tamanho máximo de 258 caracteres excedido")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo número é obrigatório")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo complemento é obrigatório")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo bairro  é obrigatório")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo cidade é obrigatório")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O campo UF é obrigatório")]
        public string UF { get; set; }
        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "O campo Status é obrigatório")]
        public bool Status { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }

}
