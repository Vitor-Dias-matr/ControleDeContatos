using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.ViewModels
{
    public class RedefinirSenhaViewModel
    {
        [Required(ErrorMessage = "Digite o login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }   
    }
}