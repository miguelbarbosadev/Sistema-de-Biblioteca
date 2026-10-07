using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexaLibrary.Data;
using NexaLibrary.Models;

namespace NexaLibrary.Controllers
{
    [ApiController]
    [Route("funcionarios")]
    public class FuncionariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            return await _context.Funcionarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);

            if (funcionario == null)
                return NotFound();

            return funcionario;
        }

        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(
            Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetFuncionario),
                new { id = funcionario.Id },
                funcionario
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(
            int id,
            Funcionario funcionario)
        {
            if (id != funcionario.Id)
                return BadRequest();

            _context.Entry(funcionario).State =
                EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var funcionario =
                await _context.Funcionarios.FindAsync(id);

            if (funcionario == null)
                return NotFound();

            _context.Funcionarios.Remove(funcionario);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
