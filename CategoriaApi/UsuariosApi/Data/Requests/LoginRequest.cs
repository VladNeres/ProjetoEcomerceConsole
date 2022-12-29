using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "O campo UserName é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="O campo password é obrigatório")]
        public string Password { get; set; }

    }
}
