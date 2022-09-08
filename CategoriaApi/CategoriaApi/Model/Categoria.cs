using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Model
{
    public class Categoria
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required (ErrorMessage = "O campo nome é obrigatório")]
        [StringLength (50, ErrorMessage = "O campo nome deve conter no maximo 50 caracteres")]
        [RegularExpression( @"[a-zA-Zá-úÁ-Ú' '\s]{1,30}", ErrorMessage = "O campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        
        public bool Status { get; set; } 
        public DateTime DataCriacao { get; set; } 
        
        public DateTime DataAtualizacao{ get; set; }
        [JsonIgnore]
        public virtual SubCategoria SubCategoria { get; set; }

    }
}
 ;