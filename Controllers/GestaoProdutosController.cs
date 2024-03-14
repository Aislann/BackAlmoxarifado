using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public GestaoProdutosController(AlmoxarifadoAPIContext context)
        {
            _context = context;
        }

        // GET: api/GestaoProdutos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GestaoProduto>>> GetGestaoProdutos()
        {
          if (_context.GestaoProdutos == null)
          {
              return NotFound();
          }
            return await _context.GestaoProdutos.ToListAsync();
        }

        // GET: api/GestaoProdutos/5
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

        // PUT: api/GestaoProdutos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/GestaoProdutos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // DELETE: api/GestaoProdutos/5
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

        // GET: api/GestaoProdutos/VerificarNovoProduto/5

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

            // Chamar a função VerificarNovoProduto da classe VerificaProduto
            VerificaProduto.VerificarNovoProduto(gestaoProduto);

            return gestaoProduto;
        }


    }
}
