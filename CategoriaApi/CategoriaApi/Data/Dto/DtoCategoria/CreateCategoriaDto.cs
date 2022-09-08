using CategoriaApi.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Data.Dto.DtoCategoria
{
    public class CreateCategoriaDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '/s]{1,20}", ErrorMessage = "O campo nome deve conter apenas letras")]
        public string Nome { get; set; }

        public bool Status { get; set; } = true;
        public string DataCriacao { get; set; } = DateTime.Now.ToString("dd-MM-yyyy  HH:mm:ss");

    }
}
