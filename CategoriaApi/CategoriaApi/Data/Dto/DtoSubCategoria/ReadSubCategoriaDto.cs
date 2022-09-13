using CategoriaApi.Model;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.DtoSubCategoria
{
    public class ReadSubCategoriaDto
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; }
        public string DataCriacao { get; set; }
        public string DataAtualizacao { get; set; }
        public Categoria Categoria { get; set; }


    }
}
