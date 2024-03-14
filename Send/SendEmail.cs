using CrawlerDados.Models;
using CrawlerDados.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

public static class SendEmail
{

    public static void EnviarEmail(string nomeProdutoMagalu, string nomeProdutoMercado, decimal precoProdutoMercadoLivre, decimal precoProdutoMagazineLuiza, string melhorCompra, string urlProduto, int idProduto, string NomeProduto)
    {

        // Configurações do servidor SMTP do Outlook
        string smtpServer = "smtp-mail.outlook.com"; 
        int porta = 587; 
        string remetente = "aislanfake@outlook.com"; 
        string senha = "senhateste1"; 

        using (SmtpClient client = new SmtpClient(smtpServer, porta))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(remetente, senha);
            client.EnableSsl = true; // Habilitar SSL/TLS

            // Construir mensagem de e-mail
            MailMessage mensagem = new MailMessage(remetente, "aislanfake@outlook.com")
            {
                Subject = $"Benchmarking: {NomeProduto}",
                Body = $"Mercado Livre\n" +
                       $"Produto: {nomeProdutoMercado}\n" +
                       $"Preço: R${precoProdutoMercadoLivre}\n" +
                       $"\n" +
                       $"Magazine Luiza\n" +
                       $"Produto: {nomeProdutoMagalu}" +
                       $"\nPreço: R${precoProdutoMagazineLuiza}\n" +
                       $"\n" +
                       "Melhor compra:" +
                       $"\n{melhorCompra} - {urlProduto}\n" +
                       $"\n" +
                       $"AO24\n" +
                       $"Usuário: AislanOliveira"
            };

            client.Send(mensagem);
            //LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Envio - Email", "Sucesso", idProduto);
            Console.WriteLine($"Email enviado com sucesso! ");
        }
    }
}