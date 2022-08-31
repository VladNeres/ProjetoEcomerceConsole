using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Data
{
    public class UpdateCategoriaDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "O campo nomme é obrigatório")]
        [RegularExpression( @"[a-zA-Zá-úÁ-Ú' '/s]{1,20}", ErrorMessage = "O campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        [Required ( ErrorMessage= "O campo status é obrigatório")]
        public bool Status { get; set; }
        [JsonInclude]
        public string DataAtualizacao { get; set; } = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
    }
}
