using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Models;


namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly WebApiContext _context;
        public ProdutoController(WebApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produtos>>> GetProduto()
        {
            return await _context.Produtos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produtos>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<Produtos>> PostProduto(Produtos produto)
        {
            // Ensure that the category exists
            if (produto.IdCategoria.HasValue)
            {
                var categoriaExists = await _context.Categorias.AnyAsync(c => c.IdCategoria == produto.IdCategoria);
                if (!categoriaExists)
                {
                    return BadRequest("Categoria não encontrada.");
                }
            }

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduto), new { id = produto.IdProduto }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produtos produto)
        {
            if (id != produto.IdProduto)
            {
                return BadRequest();
            }

            // Ensure that the category exists
            if (produto.IdCategoria.HasValue)
            {
                var categoriaExists = await _context.Categorias.AnyAsync(c => c.IdCategoria == produto.IdCategoria);
                if (!categoriaExists)
                {
                    return BadRequest("Categoria não encontrada.");
                }
            }

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.IdProduto == id);
        }
    }
}

