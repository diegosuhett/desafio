using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pagador.Controllers.MetodoHelper;
using Pagador.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Pagador.Controllers
{
    public class TransacaoController : Controller
    {
        private IConfiguration config;
        private IMetodosBase metodo;

        public TransacaoController(IConfiguration configuracao, IMetodosBase metodosBase)
        {
            config = configuracao;
            metodo = metodosBase;
        }

        [HttpGet, HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InfoPedido(string payId)
        {
            var pedido = await metodo.BuscaPedidoId(payId);
            return View(pedido);
        }

        [HttpGet]
        public IActionResult Incluir()
        {
            ViewBag.MerchantOrderId = config["SandBox:MerchantOrderId"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Incluir(Pedido novoPedido)
        {
            if (ModelState.IsValid)
            {
                var pedido = await metodo.Incluir(novoPedido);
                if (pedido == null)
                    return View(novoPedido);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.MerchantOrderId = novoPedido.MerchantOrderId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Capturar(Pedido pedido)
        {
            var pedidoCapturado = await metodo.Capturar(pedido.Payment.PaymentId.ToString());
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(Pedido pedido)
        {
            var pedidoCancelado = await metodo.Cancelar(pedido.Payment.PaymentId.ToString());
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}