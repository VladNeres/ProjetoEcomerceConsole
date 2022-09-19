using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Model
{
    public class SubCategoria
    {
        [Key]
        [Required]
       public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '\s]{1,1000}", ErrorMessage=  "O campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        public bool Status { get; set; } 
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        [JsonIgnore]
        public int CategoriaId { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }

        
    }
}
