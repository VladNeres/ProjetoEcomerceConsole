using CategoriaApi.Data.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.DtoCategoria
{
    public class ReaderCategoriaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
        [Required]
        public bool Status { get; set; }
        public string DataCriacao { get; set; }
        public string DataAtualizacao { get; set; }


    }
}

