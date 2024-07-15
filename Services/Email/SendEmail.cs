using AlmoxarifadoAPI.Models;
using AlmoxarifadoAPI.Services.Utils;
using System.Net;
using System.Net.Mail;

public static class SendEmail
{
    private static readonly AlmoxarifadoAPIContext _context = new AlmoxarifadoAPIContext();

    public static void EnviarEmail(string nomeProdutoMagalu, string nomeProdutoMercado, decimal precoProdutoMercadoLivre, decimal precoProdutoMagazineLuiza, string melhorCompra, string urlProduto, int idProduto, string NomeProduto)
    {
        var destinatario = _context.Emails.FirstOrDefault(); 

        if (destinatario == null)
        {
            Console.WriteLine("Endereço de e-mail do remetente não encontrado.");
            return;
        }

        // Configurações do servidor SMTP do Outlook
        string smtpServer = "smtp-mail.outlook.com"; 
        int porta = 587; 
        string remetente = "aislanfake@outlook.com"; 
        string senha = "senhateste1"; 

        using (SmtpClient client = new SmtpClient(smtpServer, porta))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(remetente, senha);
            client.EnableSsl = true; 
            Email email = new Email();

            MailMessage mensagem = new MailMessage(remetente, destinatario.EmailUsuario)
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
            LogManager.RegistrarLog("AO24", "AislanOliveira", DateTime.Now, "Envio - Email", "Sucesso", idProduto);
            Console.WriteLine($"Email enviado com sucesso! ");
        }
    }
}