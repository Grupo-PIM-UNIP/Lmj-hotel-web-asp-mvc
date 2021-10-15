using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LmjHotelWebApplication.Models
{
    [Table("Tb_Pagamento")]
    public class Pagamento
    {
        [Key]
        public long Id { get; set; }
        public DateTime Instante { get; set; }
        public int QtdParcelas { get; set; }
        public CartaoCredito Cartao { get; set; }
    }
}
