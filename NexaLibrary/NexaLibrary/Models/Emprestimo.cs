namespace NexaLibrary.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int LivroId { get; set; }

        public int FuncionarioId { get; set; }

        public DateTime DataEmprestimo { get; set; }

        public DateTime? DataDevolucao { get; set; }

        public bool Devolvido { get; set; }
    }
}

