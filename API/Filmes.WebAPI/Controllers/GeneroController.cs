using FIlmes.WebAPI.DTO;
using FIlmes.WebAPI.Models;
using FILmes.WebAPI.Interface;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FIlmes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeneroController : ControllerBase
{
    private readonly IGeneroRepository _generoRepository;

    public GeneroController(IGeneroRepository generoRepository)
    {
        _generoRepository = generoRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_generoRepository.Listar());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_generoRepository.BuscarPorId(id));

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
        public IActionResult Post(GeneroDTO generoDto)
    {
        try
        {
            Genero novoGenero = new Genero();

            novoGenero.Nome = generoDto.Nome!;

            _generoRepository.Cadastrar(novoGenero);

            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, GeneroDTO novoGenero)
    {
        try
        {
            var genero = new Genero
            {
                Nome = novoGenero.Nome!,
                IdGenero = id.ToString()
            };
            _generoRepository.AtualizarIdUrl(id, genero);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public IActionResult PutBody(Genero generoAtualizado)
    {
        try
        {
            _generoRepository.AtualizarIdCorpo(generoAtualizado);
            return NoContent();

        }
        catch (Exception ex)
        {
            return BadRequest();
        }

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id) 
    {
        try 
        { 
            _generoRepository.Deletar(id);
            return NoContent();
            
        }catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    
    }
}


