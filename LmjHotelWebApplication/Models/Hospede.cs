using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmjHotelWebApplication.Models
{
    [Table("Tb_Hospede")]
    public class Hospede
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [Display(Name = "CPF")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Informe os 11 dígitos do CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "Informe o número de seu telefone com 9 ou 8 dígitos após o DDD")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [Display(Name = "Número cartão de crédito")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Informe os 16 números do seu cartão de crédito")]
        [DataType(DataType.CreditCard)]
        public string CartaoCredito { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um email válido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public ICollection<Reserva> Reservas { get; } = new List<Reserva>();
    }
}
