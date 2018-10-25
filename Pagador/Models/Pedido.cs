using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Pagador.Models
{
    public class Pedido
    {
        [StringLength(36)]
        public string MerchantOrderId { get; set; }
        public Customer Customer { get; set; }
        public Payment Payment { get; set; }
    }
}