using AlmoxarifadoAPI.Models;
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
                RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Mercado Livre", "Sucesso", idProduto);
                return produto;
            }

            else
            {
                Console.WriteLine("Preço não encontrado.");
                RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Mercado Livre", "Preço não encontrado", idProduto);
                return null;
            }
        }
            
        //try
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = client.GetAsync($"https://www.magazineluiza.com.br/busca/{descricaoProduto}").Result;

            //        string content = response.Content.ReadAsStringAsync().Result;

            //        var docHtml = new HtmlDocument();

            //        docHtml.LoadHtml(content);

            //        var produtos = docHtml.DocumentNode.SelectNodes("//a");
            //        ProdutoScraper produtoRetorno = new ProdutoScraper();
            //        foreach (var item in produtos)
            //        {
            //            if (item.OuterHtml.Contains("data-testid=\"product-card-container\""))
            //            {

            //                var card = item;
            //                var linkproduto = card.Attributes["href"].Value;
            //                var elePrecoValue = card.SelectSingleNode(".//p[@data-testid=\"price-value\"]");
            //                var firstProductTitle = card.SelectSingleNode(".//h2[@data-testid=\"product-title\"]");
            //                var tdd = "sda";



            //                produtoRetorno.Preco = elePrecoValue.InnerText;
            //                produtoRetorno.Titulo = firstProductTitle.InnerText;
            //                produtoRetorno.Url = linkproduto;

            //                Console.WriteLine(produtoRetorno.Preco, elePrecoValue.InnerText);
            //            }

            //        }

            //            // Registra o log com o ID do produto
            //            RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Magazine Luiza", "Sucesso", idProduto);
            //        return produtoRetorno;
            //    }
            //}
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar a página: {ex.Message}");
            RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Magazine Luiza", $"Erro: {ex.Message}", idProduto);

            return null;
        }
    }

    private static void RegistrarLog(string codRob, string usuRob, DateTime dateLog, string processo, string infLog, int idProd)
    {

        using (var context = new AlmoxarifadoAPIContext())
        {
            var log = new Logrobo
            {
                CodigoRobo = codRob,
                UsuarioRobo = usuRob,
                DateLog = dateLog,
                Etapa = processo,
                InformacaoLog = infLog,
                IdProdutoAPI = idProd
            };
            context.LOGROBO.Add(log);
            context.SaveChanges();
        }

    }
}

