using AlmoxarifadoAPI.Models;
using CrawlerDados.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class MagazineLuizaScraper
{
    public ProdutoScraper ObterPreco(string descricaoProduto, int idProduto)
    {
        try
        {
            // Inicializa o ChromeDriver
            using (IWebDriver driver = new ChromeDriver())
            {
                // Abre a página
                driver.Navigate().GoToUrl($"https://www.magazineluiza.com.br/busca/{descricaoProduto}");

                // Aguarda um tempo fixo para permitir que a página seja carregada (você pode ajustar conforme necessário)
                System.Threading.Thread.Sleep(5000);

                // Encontra o elemento que possui o atributo data-testid
                IWebElement priceElement = driver.FindElement(By.CssSelector("[data-testid='price-value']"));
                IWebElement titleElement = driver.FindElement(By.CssSelector("[data-testid='product-title']"));
                IWebElement urlElement = driver.FindElement(By.CssSelector("[data-testid='product-card-container']"));

                // Verifica se o elemento foi encontrado
                if (priceElement != null)
                {
                    // Obtém o preço do primeiro produto
                    string firstProductPrice = priceElement.Text.Trim();
                    string firstProductTitle = titleElement.Text.Trim();
                    string firstProductUrl = urlElement.GetAttribute("href");

                    ProdutoScraper produto = new ProdutoScraper();

                    produto.Preco = firstProductPrice;
                    produto.Titulo = firstProductTitle;
                    produto.Url = firstProductUrl;


                    // Registra o log com o ID do produto
                    RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Magazine Luiza", "Sucesso", idProduto);

                    // Retorna o preço
                    return produto;
                }
                else
                {
                    Console.WriteLine("Preço não encontrado.");

                    // Registra o log com o ID do produto
                    RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Consultar Dados - Magazine Luiza", "Preço não encontrado", idProduto);

                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao acessar a página: {ex.Message}");

            // Registra o log com o ID do produto
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
                IdProdutoApi = idProd
            };
            context.Logrobos.Add(log);
            context.SaveChanges();
        }

    }
}

