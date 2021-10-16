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
        [StringLength(50, MinimumLength = 8, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [Display(Name = "CPF")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Informe o CPF no formato XXX.XXX.XXX-XX")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(13, MinimumLength = 12, ErrorMessage = "Informe o Telefone no formato XX XXXX-XXXX com 9 ou 8 digitos após o DDD")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório")]
        [Display(Name = "Número do cartão de crédito")]
        [StringLength(19, MinimumLength = 19, ErrorMessage = "Informe o número do cartão de crédito formato XXXX-XXXX-XXXX-XXXX")]
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
