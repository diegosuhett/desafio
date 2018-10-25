using Pagador.Models;
using System.Threading.Tasks;

namespace Pagador.Controllers.MetodoHelper
{
    public interface IMetodosBase
    {
        Task<Pedido> Incluir(Pedido pedido);
        Task<Pedido> BuscaPedidoId(string payId);
        Task<Pedido> Capturar(string payId);
        Task<Pedido> Cancelar(string payId);
    }
}
