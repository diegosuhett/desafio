using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pagador.Models
{
    public class Payment
    {
        [StringLength(15)]
        public string Provider { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        [Display(Name = "Valor da compra")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Amount { get; set; }

        [Display(Name = "Número de parcelas")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Installments { get; set; }

        [Display(Name = "Comprovante de venda")]
        public string ProofOfSale { get; set; }

        [Display(Name = "Código da transação no provedor")]
        public string AcquirerTransactionId { get; set; }

        [Display(Name = "Código de autorização")]
        public string AuthorizationCode { get; set; }

        [Display(Name = "Número do pedido")]
        public string PaymentId { get; set; }

        [Display(Name = "Data de recebimento do pedido")]
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "Código de retorno da Operação")]
        public byte ReasonCode { get; set; }

        [Display(Name = "Mensagem de retorno da Operação")]
        public string ReasonMessage { get; set; }

        [Display(Name = "Status do pedido")]
        public byte Status { get; set; }

        [Display(Name = "Código retornado pelo provedor")]
        public string ProviderReturnCode { get; set; }

        [Display(Name = "Mensagem retornada pelo provedor")]
        public string ProviderReturnMessage { get; set; }

        public CreditCard CreditCard { get; set; }

        public List<Link> Links { get; set; }

        public List<Payment> Payments { get; set; }        
    }
}