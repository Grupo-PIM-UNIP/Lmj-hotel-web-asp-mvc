using System.ComponentModel.DataAnnotations;

namespace LmjHotelWebApplication.Models.ViewModels
{
    public class LoginHospedeViewModel
    {
        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
