namespace NexaLibrary.Models
{
    public class Livro
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Autor { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public int ano_publicacao { get; set; }

        public bool Disponivel { get; set; }
    }
}
