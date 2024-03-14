using AlmoxarifadoAPI.Models;

namespace CrawlerDados.Utils
{
    class LogManager
    {
        // Método para registrar um log no banco de dados
        public static void RegistrarLog(string codRob, string usuRob, DateTime dateLog, string processo, string infLog, int idProd)
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
}
