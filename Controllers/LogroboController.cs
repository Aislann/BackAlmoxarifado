﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlmoxarifadoAPI.Models;

namespace AlmoxarifadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogroboController : ControllerBase
    {
        private readonly AlmoxarifadoAPIContext _context;

        public LogroboController(AlmoxarifadoAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logrobo>>> GetLogrobos()
        {
          if (_context.LOGROBO == null)
          {
              return NotFound();
          }
            return await _context.LOGROBO.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Logrobo>> GetLogrobo(int id)
        {
          if (_context.LOGROBO == null)
          {
              return NotFound();
          }
            var logrobo = await _context.LOGROBO.FindAsync(id);

            if (logrobo == null)
            {
                return NotFound();
            }

            return logrobo;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogrobo(int id, Logrobo logrobo)
        {
            if (id != logrobo.IDlOg)
            {
                return BadRequest();
            }

            _context.Entry(logrobo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogroboExists(id))
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
        public async Task<ActionResult<Logrobo>> PostLogrobo(Logrobo logrobo)
        {
          if (_context.LOGROBO == null)
          {
              return Problem("Entity set 'AlmoxarifadoAPIContext.Logrobos'  is null.");
          }
            _context.LOGROBO.Add(logrobo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogrobo", new { id = logrobo.IDlOg }, logrobo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogrobo(int id)
        {
            if (_context.LOGROBO == null)
            {
                return NotFound();
            }
            var logrobo = await _context.LOGROBO.FindAsync(id);
            if (logrobo == null)
            {
                return NotFound();
            }

            _context.LOGROBO.Remove(logrobo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogroboExists(int id)
        {
            return (_context.LOGROBO?.Any(e => e.IDlOg == id)).GetValueOrDefault();
        }
    }
}
