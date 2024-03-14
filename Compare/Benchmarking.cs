using AlmoxarifadoAPI.Models;
using CrawlerDados.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class Benchmarking
{
    public static decimal? PrecoEscolhido { get; private set; }
    public static decimal? CompararValores(ProdutoScraper precoMagazineLuiza, ProdutoScraper precoMercadoLivre, int idProduto, string NomeProduto)
    {
        char[] charRemove = { 'R', '$', ' ' };
        // Converte as strings para decimal
        var precoMagaluvar = precoMagazineLuiza.Preco.Trim(charRemove);
        var precoMercadovar = precoMercadoLivre.Preco.Trim(charRemove);

        decimal precoMagalu;
        decimal precoMercado;

        decimal.TryParse(precoMagaluvar, out precoMagalu);
        decimal.TryParse(precoMercadovar, out precoMercado);

        // Valores convertidos com sucesso
        Console.WriteLine($"Valor Magazine Luiza: {precoMagalu}");
        Console.WriteLine($"Valor Mercado Livre: {precoMercado}\n");

        if (precoMagalu < precoMercado)
        {
            PrecoEscolhido = precoMagalu;
            //LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Menor Valor - Magazine Luiza", "Sucesso", idProduto);
            //SendEmail.EnviarEmail(precoMagazineLuiza.Titulo, precoMercadoLivre.Titulo, precoMercado, precoMagalu, "Magazine Luiza", precoMagazineLuiza.Url, idProduto, NomeProduto);
            
        }
        else if (precoMercado < precoMagalu)
        {
            PrecoEscolhido = precoMercado;
            //LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Menor Valor - Mercado Livre", "Sucesso", idProduto);
            //SendEmail.EnviarEmail(precoMagazineLuiza.Titulo, precoMercadoLivre.Titulo, precoMercado, precoMagalu, "Mercado Livre", precoMercadoLivre.Url, idProduto, NomeProduto);

        }
        else
        {
            return null;
        }
        return PrecoEscolhido;
    }

}
