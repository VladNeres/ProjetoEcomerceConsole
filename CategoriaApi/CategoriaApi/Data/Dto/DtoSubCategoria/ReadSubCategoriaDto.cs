using CategoriaApi.Model;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.DtoSubCategoria
{
    public class ReadSubCategoriaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
        public bool Status { get; set; }
        public string DataCriacao { get; set; }
        public string DataAtualizacao { get; set; }
        public Categoria Categoria { get; set; }


    }
}
