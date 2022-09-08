using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.DtoSubCategoria
{
    public class UpdateSubCategoriaDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "O Campo é obrigatório")]
     
        public string Nome { get; set; }
        public bool Status { get; set; }
        public string DataCriacao { get; set; } 

        public string DataAtualizacao { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
    }
}
