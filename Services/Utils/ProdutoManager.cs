using AlmoxarifadoAPI.Models;
using Newtonsoft.Json;

namespace AlmoxarifadoAPI.Services.Utils
{
    class ProdutoManager
    {
        public static List<GestaoProduto> ObterNovosProdutos(string responseData)
        {
            List<GestaoProduto> produtos = JsonConvert.DeserializeObject<List<GestaoProduto>>(responseData);
            return produtos;
        }
        public static bool ProdutoJaRegistrado(int idProduto)
        {
            using (var context = new AlmoxarifadoAPIContext())
            {
                return context.LOGROBO.Any(log => log.IdProdutoAPI == idProduto && log.CodigoRobo == "AO24");
            }
        }
    }
}
