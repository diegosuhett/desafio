using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pagador.Controllers.MetodoHelper;
using Pagador.Models;

namespace Pagador.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration config;
        private IMetodosBase metodo;

        public HomeController(IConfiguration configuracao, IMetodosBase metodosBase)
        {
            config = configuracao;
            metodo = metodosBase;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpClient BaseUrl = new HttpClient();
            List<Pedido> ListaDados = new List<Pedido>();

            BaseUrl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BaseUrl.DefaultRequestHeaders.Add("MerchantId", config["SandBox:MerchantId"]);
            BaseUrl.DefaultRequestHeaders.Add("MerchantKey", config["SandBox:MerchantKey"]);            
            BaseUrl.BaseAddress = new Uri(config["EndPoint:Query"]);

            HttpResponseMessage pedidosOrderId = await BaseUrl.GetAsync("v2/sales?merchantOrderId=" + config["SandBox:MerchantOrderId"]);            

            if (pedidosOrderId.IsSuccessStatusCode)
            {
                var retorno = await pedidosOrderId.Content.ReadAsStringAsync();
                var paymentJson = JsonConvert.DeserializeObject<Payment>(retorno);

                foreach (Payment pagamento in paymentJson.Payments)
                {
                    Pedido pedidoUnico = await metodo.BuscaPedidoId(pagamento.PaymentId.ToString());
                    ListaDados.Add(pedidoUnico);
                }
            }    

            return View(ListaDados);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
