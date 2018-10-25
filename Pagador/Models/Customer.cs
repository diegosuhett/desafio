using System.ComponentModel.DataAnnotations;

namespace Pagador.Models
{
    public class Customer
    {
        [StringLength(255)]
        [Display(Name = "Nome do cliente")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public string Name { get; set; }
    }
}