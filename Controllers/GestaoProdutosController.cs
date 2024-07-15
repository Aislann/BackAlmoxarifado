using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlmoxarifadoAPI.Models;
using CrawlerDados.Utils;

namespace AlmoxarifadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestaoProdutosController : ControllerBase
    {
        private readonly AlmoxarifadoAPIContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public GestaoProdutosController(AlmoxarifadoAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GestaoProduto>>> GetGestaoProdutos()
        {
            if (_context.GestaoProdutos == null)
            {
                return NotFound();
            }
            return await _context.GestaoProdutos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GestaoProduto>> GetGestaoProduto(int id)
        {
            if (_context.GestaoProdutos == null)
            {
                return NotFound();
            }
            var gestaoProduto = await _context.GestaoProdutos.FindAsync(id);

            if (gestaoProduto == null)
            {
                return NotFound();
            }

            return gestaoProduto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGestaoProduto(int id, GestaoProduto gestaoProduto)
        {
            if (id != gestaoProduto.IdProduto)
            {
                return BadRequest();
            }

            _context.Entry(gestaoProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GestaoProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<GestaoProduto>> PostGestaoProduto(GestaoProduto gestaoProduto)
        {
            if (_context.GestaoProdutos == null)
            {
                return Problem("Entity set 'AlmoxarifadoAPIContext.GestaoProdutos'  is null.");
            }
            _context.GestaoProdutos.Add(gestaoProduto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GestaoProdutoExists(gestaoProduto.IdProduto))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGestaoProduto", new { id = gestaoProduto.IdProduto }, gestaoProduto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGestaoProduto(int id)
        {
            if (_context.GestaoProdutos == null)
            {
                return NotFound();
            }
            var gestaoProduto = await _context.GestaoProdutos.FindAsync(id);
            if (gestaoProduto == null)
            {
                return NotFound();
            }

            _context.GestaoProdutos.Remove(gestaoProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GestaoProdutoExists(int id)
        {
            return (_context.GestaoProdutos?.Any(e => e.IdProduto == id)).GetValueOrDefault();
        }

        [HttpGet("VerificarNovoProduto/{id}")]
        public async Task<ActionResult<GestaoProduto>> VerificarNovoProduto(int id)
        {
            if (_context.GestaoProdutos == null)
            {
                return NotFound();
            }

            var gestaoProduto = await _context.GestaoProdutos.FindAsync(id);

            if (gestaoProduto == null)
            {
                return NotFound();
            }

            VerificaProduto.VerificarNovoProduto(gestaoProduto, "true");

            return gestaoProduto;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<GestaoProduto>> UpdateProduto(int id, [FromBody] GestaoProduto produtoPatch)
        {
            var produto = await _context.GestaoProdutos.FindAsync(id);
            VerificaProduto.VerificarNovoProduto(produto, "false");

            if (produto == null)
            {
                return NotFound();
            }
            if (produtoPatch.Descricao != null)
            {
                produto.Descricao = produtoPatch.Descricao;
            }
            if (produtoPatch.Preco.HasValue)
            {
                produto.Preco = Benchmarking.PrecoEscolhido;
                Console.WriteLine(produto.Preco);
            }
            if (produtoPatch.EstoqueAtual.HasValue)
            {
                produto.EstoqueAtual = produtoPatch.EstoqueAtual.Value;
            }
            if (produtoPatch.EstoqueMinimo.HasValue)
            {
                produto.EstoqueMinimo = produtoPatch.EstoqueMinimo.Value;
            }
            produto.Estado = "executado";

            _context.Entry(produto).State = EntityState.Modified;
            var updateProduto = await _context.SaveChangesAsync();

            return Ok(updateProduto);


        }

    }
}