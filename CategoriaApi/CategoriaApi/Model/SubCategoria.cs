﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Model
{
    public class SubCategoria
    {
        [Key]
        [Required]
       public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '\s]{1,20}", ErrorMessage=  "O campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        public bool Status { get; set; } 
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

        
    }
}