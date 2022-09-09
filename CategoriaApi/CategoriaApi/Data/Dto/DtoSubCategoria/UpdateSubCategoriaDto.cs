using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.DtoSubCategoria
{
    public class UpdateSubCategoriaDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú ' '\s]{1,20}", ErrorMessage = "o Campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        public bool Status { get; set; }
        public string DataCriacao { get; set; } 

        public string DataAtualizacao { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
}
