using System.ComponentModel.DataAnnotations;

namespace LmjHotelWebApplication.Models.ViewModels
{
    public class ResetaSenhaViewModel
    {
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [Display(Name = "Nova Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Compare("Senha")]
        [Display(Name = "Confirmar Senha")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }
    }
}
