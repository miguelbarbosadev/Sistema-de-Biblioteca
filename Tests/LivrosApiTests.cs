
using System.Net;
using System.Net.Http.Json;
using NexaLibrary.Models;
using Xunit;

namespace NexaLibrary.Tests;

public class LivrosApiTests
{
    [Fact]
    public async Task GetLivros_DeveRetornarSucesso()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/livros");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetLivro_Inexistente_DeveRetornar404()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/livros/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PostLivro_DeveCadastrarLivro()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var livro = new Livro
        {
            Titulo = "Dom Casmurro",
            Autor = "Machado de Assis",
            ISBN = "9780000000001",
            ano_publicacao = 1899,
            Disponivel = true
        };

        var response = await client.PostAsJsonAsync("/livros", livro);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var cadastrado = await response.Content.ReadFromJsonAsync<Livro>();

        Assert.NotNull(cadastrado);
        Assert.True(cadastrado.Id > 0);
        Assert.Equal("Dom Casmurro", cadastrado.Titulo);
    }

    [Fact]
    public async Task PutLivro_DeveAtualizarLivro()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var livro = new Livro
        {
            Titulo = "Livro Original",
            Autor = "Autor Teste",
            ISBN = "9780000000002",
            ano_publicacao = 2020,
            Disponivel = true
        };

        var cadastro = await client.PostAsJsonAsync("/livros", livro);
        var cadastrado = await cadastro.Content.ReadFromJsonAsync<Livro>();

        Assert.NotNull(cadastrado);

        cadastrado.Titulo = "Livro Atualizado";

        var response = await client.PutAsJsonAsync(
            $"/livros/{cadastrado.Id}", cadastrado);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var consulta = await client.GetFromJsonAsync<Livro>(
            $"/livros/{cadastrado.Id}");

        Assert.NotNull(consulta);
        Assert.Equal("Livro Atualizado", consulta.Titulo);
    }

    [Fact]
    public async Task DeleteLivro_DeveExcluirLivro()
    {
        using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var livro = new Livro
        {
            Titulo = "Livro Para Excluir",
            Autor = "Autor Teste",
            ISBN = "9780000000003",
            ano_publicacao = 2021,
            Disponivel = true
        };

        var cadastro = await client.PostAsJsonAsync("/livros", livro);
        var cadastrado = await cadastro.Content.ReadFromJsonAsync<Livro>();

        Assert.NotNull(cadastrado);

        var exclusao = await client.DeleteAsync(
            $"/livros/{cadastrado.Id}");

        Assert.Equal(HttpStatusCode.NoContent, exclusao.StatusCode);

        var consulta = await client.GetAsync(
            $"/livros/{cadastrado.Id}");

        Assert.Equal(HttpStatusCode.NotFound, consulta.StatusCode);
    }
}
