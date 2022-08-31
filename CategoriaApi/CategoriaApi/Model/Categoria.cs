﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Model
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "O campo nome é obrigatório")]
        [StringLength (50, ErrorMessage = "O campo nome deve conter no maximo 50 caracteres")]
        [RegularExpression( @"[a-zA-Zá-úÁ-Ú' '\s]{1,20}", ErrorMessage = "O campo nome deve conter apenas letras")]
        public string Nome { get; set; }
        [Required]  
        public bool Status { get; set; }
        public string DataCriacao { get; set; } = DateTime.Now.ToString("dd-MM-yyyy  HH:mm:ss");
        
        public string DataAlteracao { get; set; } 
    }
}
 