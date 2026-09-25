using Microsoft.EntityFrameworkCore;
using NexaLibrary.Models;

namespace NexaLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<Emprestimo> Emprestimos { get; set; }
    }
}
