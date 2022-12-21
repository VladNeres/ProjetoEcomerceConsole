using System.ComponentModel.DataAnnotations;
using UsuariosApi.Models;

namespace UsuariosApi.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required(ErrorMessage ="O campor CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo username é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "o campo e-mail é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage= "O campo password é obrigatório")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage= "O campo repassword é obrigatório para confirmar a senha")]
        [Compare("Password")]
        public string Repassword { get; set; }

        [Required(ErrorMessage = "O campo endereco é obrigatório")]
        public Endereco Endereco { get; set; }
    }
}
