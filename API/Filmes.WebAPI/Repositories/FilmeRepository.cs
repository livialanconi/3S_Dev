using FIlmes.WebAPI.BdContextFilme;
using FIlmes.WebAPI.Controllers;
using FIlmes.WebAPI.Models;
using FILmes.WebAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FIlmes.WebAPI.Repositories;

public class FilmeRepository : IFilmeRepository
{
    private readonly FilmeContext _context;

    public FilmeRepository(FilmeContext context)
    {
        _context = context;
    }

    public void AtualizarIdCorpo(Filme filmeAtualizado)
    {
        try
        {
            Filme filmeBuscado = _context.Filmes.Find(filmeAtualizado.IdFilme)!;
            if (filmeBuscado != null) 
            {
                filmeBuscado.Titulo = filmeAtualizado.Titulo;
                filmeBuscado.IdGenero = filmeAtualizado.IdGenero;
            }
            _context.Filmes.Update(filmeBuscado!);
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void AtualizarIdUrl(Guid id, Filme filmeAtualizado)
    {
        try
        {
            Filme filmeBuscado = _context.Filmes.Find(id.ToString())!;

            if (filmeBuscado != null) 
            {
                filmeBuscado.Titulo = filmeAtualizado.Titulo;
                filmeBuscado.IdGenero = filmeAtualizado.IdGenero;
            }

            _context.Filmes.Update(filmeBuscado!);
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public Filme BuscarPorId(Guid id)
    {
        try
        {
            Filme filmeBuscado = _context.Filmes.Find(id.ToString())!;

            return filmeBuscado;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Cadastrar(Filme novoFilme)
    {
        try
        {
            novoFilme.IdFilme = Guid.NewGuid().ToString();

            _context.Filmes.Add(novoFilme);
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Cadastrar(FilmeController novoFilme)
    {
        throw new NotImplementedException();
    }

    public void Deletar(Guid id)
    {
        try
        {
            Filme filmeBuscado = _context.Filmes.Find(id.ToString())!;
            if (filmeBuscado != null) 
            { 
                _context.Filmes.Remove(filmeBuscado);
            }
            _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public List<Filme> Listar()
    {
        try
        {
            List<Filme> listaFilmes = _context.Filmes.Include(f => f.IdGeneroNavigation).ToList();

            return listaFilmes;
        }
        catch (Exception)
        {

            throw;
        }
    }

    
}
