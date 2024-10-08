﻿using CrawlerDados.Models;

public class BenchEmail
{

    public static decimal? CompararValores(ProdutoScraper precoMagazineLuiza, ProdutoScraper precoMercadoLivre, int idProduto, string NomeProduto)
    {
        char[] charRemove = { 'R', '$', ' ' };
        var precoMagaluvar = precoMagazineLuiza.Preco.Trim(charRemove);
        var precoMercadovar = precoMercadoLivre.Preco.Trim(charRemove);

        decimal precoMagalu = ConvertToBRL.StringToDecimal(precoMagaluvar);
        decimal precoMercado;

        decimal.TryParse(precoMercadovar, out precoMercado);

        Console.WriteLine($"Valor Magazine Luiza: {precoMagalu}");
        Console.WriteLine($"Valor Mercado Livre: {precoMercado}\n");

        if (precoMagalu <= precoMercado)
        {
            SendEmail.EnviarEmail(precoMagazineLuiza.Titulo, precoMercadoLivre.Titulo, precoMercado, precoMagalu, "Magazine Luiza", "https://www.magazineluiza.com.br"+precoMagazineLuiza.Url, idProduto, NomeProduto);
        }
        else if (precoMercado < precoMagalu)
        {
            SendEmail.EnviarEmail(precoMagazineLuiza.Titulo, precoMercadoLivre.Titulo, precoMercado, precoMagalu, "Mercado Livre", precoMercadoLivre.Url, idProduto, NomeProduto);
        }
        else
        {
            return null;
        }
        return 0;
    }

}
