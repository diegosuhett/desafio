using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pagador.Models.Decricao;
using System.ComponentModel.DataAnnotations;

namespace Pagador.Models
{
    public class CreditCard
    {
        [StringLength(16)]
        [Display(Name = "Número do Cartão")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string CardNumber { get; set; }

        [StringLength(25)]
        [Display(Name = "Nome impresso no cartão")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Holder { get; set; }

        [StringLength(7)]
        [Display(Name = "Data de validade")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string ExpirationDate { get; set; }

        [StringLength(4)]
        [Display(Name = "Código de segurança")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string SecurityCode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [Display(Name = "Bandeira")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public BandeiraCartao Brand { get; set; }
    }
}