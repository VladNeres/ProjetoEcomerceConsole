using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto
{
    public class CreateCategoriaDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '/s]{1,20}", ErrorMessage = "O campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Status é obrigatório")]
        public bool Status { get; set; }

    }
}
