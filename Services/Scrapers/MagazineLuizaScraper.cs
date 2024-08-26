using AlmoxarifadoAPI.Services.Utils;
using CrawlerDados.Models;
using HtmlAgilityPack;

public class MagazineLuizaScraper
{
    public ProdutoScraper ObterPreco(string descricaoProduto, int idProduto)
    {
        string url = $"https://www.magazineluiza.com.br/busca/{descricaoProduto}";
        try
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
            HtmlNode firstProductPriceNode = document.DocumentNode.SelectSingleNode(".//p[@data-testid=\"price-value\"]");
            HtmlNode firstProductTitleNode = document.DocumentNode.SelectSingleNode(".//h2[@data-testid=\"product-title\"]");
            //HtmlNode firstProductUrlNode = document.DocumentNode.SelectSingleNode("//a[contains(@class, 'ui-search-link__title-card')]");

            if (firstProductPriceNode != null)
            {
                string firstProductPrice = firstProductPriceNode.InnerText.Trim();
                string firstProductTitle = firstProductTitleNode.InnerText.Trim();
                //string firstProductUrl = firstProductUrlNode.GetAttributeValue("href", "");

                ProdutoScraper produto = new ProdutoScraper();
                produto.Preco = firstProductPrice;
                produto.Titulo = firstProductTitle;
                produto.Url = "firstProductUrl";
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
            LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Magazine Luiza", $"Erro: {ex.Message}", idProduto);
            return null;
        }
    }
}

