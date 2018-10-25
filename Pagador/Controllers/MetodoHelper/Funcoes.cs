using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pagador.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pagador.Controllers.MetodoHelper
{
    public class Funcoes : IMetodosBase
    {
        private HttpClient BaseUrl;
        private readonly IConfiguration config;

        public Funcoes(IConfiguration Configuracao)
        {
            config = Configuracao;

            BaseUrl = new HttpClient();
            BaseUrl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BaseUrl.DefaultRequestHeaders.Add("MerchantId", config["SandBox:MerchantId"]);
            BaseUrl.DefaultRequestHeaders.Add("MerchantKey", config["SandBox:MerchantKey"]);
        }

        public async Task<Pedido> BuscaPedidoId(string payId)
        {
            if(BaseUrl.BaseAddress == null)
                BaseUrl.BaseAddress = new Uri(config["EndPoint:Query"]);

            HttpResponseMessage pedidoPaymentId = await BaseUrl.GetAsync("v2/sales/" + payId);

            if (pedidoPaymentId.IsSuccessStatusCode)
            {
                var retornoSucess = await pedidoPaymentId.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pedido>(retornoSucess);
            }

            return new Pedido();
        }

        public async Task<Pedido> Incluir(Pedido pedido)
        {
            BaseUrl.BaseAddress = new Uri(config["EndPoint:Transacional"]);

            var pedidoSerialize = JsonConvert.SerializeObject(pedido);
            var pedidoUnicode = new StringContent(pedidoSerialize, UnicodeEncoding.UTF8, "application/json");

            HttpResponseMessage incluirPedido = await BaseUrl.PostAsync("v2/sales/", pedidoUnicode);

            if (incluirPedido.IsSuccessStatusCode)
            {
                var retornoSucess = await incluirPedido.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pedido>(retornoSucess);
            }

            return new Pedido();
        }

        public async Task<Pedido> Capturar(string payId)
        {
            BaseUrl.BaseAddress = new Uri(config["EndPoint:Transacional"]);

            HttpResponseMessage capturarPedido = await BaseUrl.PutAsync("v2/sales/" + payId + "/capture", null);
            if (capturarPedido.IsSuccessStatusCode)
            {
                var retornoSucess = await capturarPedido.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pedido>(retornoSucess);
            }

            return new Pedido();
        }

        public async Task<Pedido> Cancelar(string payId)
        {
            BaseUrl.BaseAddress = new Uri(config["EndPoint:Transacional"]);

            HttpResponseMessage cancelarPedido = await BaseUrl.PutAsync("v2/sales/" + payId + "/void", null);
            if (cancelarPedido.IsSuccessStatusCode)
            {
                var retornoSucess = await cancelarPedido.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Pedido>(retornoSucess);
            }

            return new Pedido();
        }
    }
}
