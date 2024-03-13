using AlmoxarifadoAPI.Models;

namespace CrawlerDados.Utils
{
    class VerificaProduto
    {
        // Lista para armazenar produtos já verificados
        static List<GestaoProduto> produtosVerificados = new List<GestaoProduto>();
        public static void VerificarNovoProduto(object state)
        {

            string username = "11164448";
            string senha = "60-dayfreetrial";
            string url = "http://regymatrix-001-site1.ktempurl.com/api/v1/produto/getall";

            try
            {
                ApiClient apiClient = new ApiClient(username, senha);
                string responseData = apiClient.GetApiResponse(url).Result;

                // Processar os dados da resposta
                List<GestaoProduto> novosProdutos = ProdutoManager.ObterNovosProdutos(responseData);

                foreach (GestaoProduto produto in novosProdutos)
                {     
                    if (!produtosVerificados.Exists(p => p.IdProduto == produto.IdProduto))
                    {

                        // Se é um novo produto, faça algo com ele
                        Console.WriteLine($"Novo produto encontrado: ID {produto.IdProduto}, Nome: {produto.Descricao}\n");
                        // Adicionar o produto à lista de produtos verificados
                        produtosVerificados.Add(produto);

                        

                        // Registra um log no banco de dados apenas se o produto for novo
                        if (!ProdutoManager.ProdutoJaRegistrado(produto.IdProduto))
                        {
                            LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Verificação", "Sucesso", produto.IdProduto);

                            MercadoLivreScraper mercadoLivreScraper = new MercadoLivreScraper();
                            MagazineLuizaScraper magazineLuizaScraper = new MagazineLuizaScraper();

                            // Obter preço da Magazine Luiza
                            var precoMagazineLuiza = magazineLuizaScraper.ObterPreco(produto.Descricao, produto.IdProduto);
                            // Obter preço do Mercado Livre
                            var precoMercadoLivre = mercadoLivreScraper.ObterPreco(produto.Descricao, produto.IdProduto);

                            Benchmarking.CompararValores(precoMagazineLuiza, precoMercadoLivre, produto.IdProduto, produto.Descricao);

                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                // Imprimir mensagem de erro caso ocorra uma exceção
                Console.WriteLine($"Erro ao fazer a requisição: {ex.Message}");
            }

            
        }
    }
}
