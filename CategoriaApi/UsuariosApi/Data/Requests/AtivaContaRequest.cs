using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Requests
{
    public class AtivaContaRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string CodigoDeAtivacao { get; set; }
    }
}
