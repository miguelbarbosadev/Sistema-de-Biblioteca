using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexaLibrary.Data;
using NexaLibrary.Models;

namespace NexaLibrary.Controllers
{
    [ApiController]
    [Route("emprestimos")]
    public class EmprestimosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmprestimosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimos()
        {
            return await _context.Emprestimos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Emprestimo>> GetEmprestimo(int id)
        {
            var emprestimo =
                await _context.Emprestimos.FindAsync(id);

            if (emprestimo == null)
                return NotFound();

            return emprestimo;
        }

        [HttpPost]
        public async Task<ActionResult<Emprestimo>> PostEmprestimo(
            Emprestimo emprestimo)
        {
            emprestimo.DataEmprestimo = DateTime.Now;
            emprestimo.Devolvido = false;

            _context.Emprestimos.Add(emprestimo);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEmprestimo),
                new { id = emprestimo.Id },
                emprestimo
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmprestimo(
            int id,
            Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
                return BadRequest();

            _context.Entry(emprestimo).State =
                EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmprestimo(int id)
        {
            var emprestimo =
                await _context.Emprestimos.FindAsync(id);

            if (emprestimo == null)
                return NotFound();

            _context.Emprestimos.Remove(emprestimo);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/devolucao")]
        public async Task<IActionResult> DevolverLivro(int id)
        {
            var emprestimo =
                await _context.Emprestimos.FindAsync(id);

            if (emprestimo == null)
                return NotFound();

            emprestimo.Devolvido = true;
            emprestimo.DataDevolucao = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(emprestimo);
        }
    }
}
