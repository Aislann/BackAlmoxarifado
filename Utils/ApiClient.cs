using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerDados.Utils
{
    class ApiClient
    {
        private readonly string username;
        private readonly string senha;

        public ApiClient(string username, string senha)
        {
            this.username = username;
            this.senha = senha;
        }

        public async Task<string> GetApiResponse(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                // Adicionar as credenciais de autenticação básica
                var byteArray = Encoding.ASCII.GetBytes($"{username}:{senha}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                // Fazer a requisição GET à API
                HttpResponseMessage response = await client.GetAsync(url);

                // Verificar se a requisição foi bem-sucedida (código de status 200)
                if (response.IsSuccessStatusCode)
                {
                    // Ler o conteúdo da resposta como uma string
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Imprimir mensagem de erro caso a requisição falhe
                    Console.WriteLine($"Erro: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
}
