using AlmoxarifadoAPI.Services.Utils;
using CrawlerDados.Models;
using HtmlAgilityPack;

public class MercadoLivreScraper
{
    public ProdutoScraper ObterPreco(string descricaoProduto, int idProduto)
    {
        string url = $"https://lista.mercadolivre.com.br/{descricaoProduto}";
        try
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            HtmlNode firstProductPriceNode = document.DocumentNode.SelectSingleNode("//span[@class='andes-money-amount__fraction']");
            HtmlNode firstProductTitleNode = document.DocumentNode.SelectSingleNode("//h2[@class='ui-search-item__title']");
            HtmlNode firstProductUrlNode = document.DocumentNode.SelectSingleNode("//a[contains(@class, 'ui-search-link__title-card')]");

            if (firstProductPriceNode != null)
            {
                string firstProductPrice = firstProductPriceNode.InnerText.Trim();
                string firstProductTitle = firstProductTitleNode.InnerText.Trim();
                string firstProductUrl = firstProductUrlNode.GetAttributeValue("href", "");

                ProdutoScraper produto = new ProdutoScraper();

                produto.Preco = firstProductPrice;
                produto.Titulo = firstProductTitle;
                produto.Url = firstProductUrl;

                Console.WriteLine(produto.Preco, firstProductPrice);
                LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Mercado Livre", "Sucesso", idProduto);
                return produto;
            }

            else
            {
                Console.WriteLine("Preço não encontrado.");
                LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Mercado Livre", "Preço não encontrado", idProduto);
                return null;
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar a página: {ex.Message}");
            LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Mercado Livre", $"Erro: {ex.Message}", idProduto);
            return null;
        }

    }
}