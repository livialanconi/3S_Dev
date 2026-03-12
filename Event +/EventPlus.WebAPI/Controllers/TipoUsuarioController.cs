using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus_.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoUsuarioController : ControllerBase
{
    private ITipoUsuarioRepository _tipoUsuarioRepository;

    public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    //<sumary>
    //Endpoint da api que faz chamada para o metodo de listar os tipos de evento
    //</summary>
    //<returns>Status code 200 e a lista de tios de evento</returns>

    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoUsuarioRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    //<sumary>
    //Endpoint da api que faz chamada para o metodo de buscar um tipo de evento especifico
    //</summary>
    //<param name="id"> Id do tipo de evento buscado</param>
    //<returns>Status code 200 e tipo de evento buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoUsuarioRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    //<sumary>
    //Endpoint da api que faz chamada para o metodo de cadastrar um tipo de evento
    //</summary>
    //<param name="tipoEvento">tipo de evento a ser cadastrado</param>
    //<returns>Status code 201 e o tipo de evento cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var novoTipoUsuario = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };

            _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);

            return StatusCode(201, novoTipoUsuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    //<sumary>
    //Endpoint da api que faz chamada para o metodo de atualizar um tipo de evento
    //</summary>
    //<param name="id">Id do tipo de evento a ser atualizado</param>
    //<param name="tipoEvento">tipo de evento com dados atualizados</param>
    //<returns>Status code 204 e o tipo de evento atualizado</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoUsuarioDTO tipoUsuario)
    {
        try
        {
            var tipoUsuarioAtualizado = new TipoUsuario
            {
                Titulo = tipoUsuario.Titulo!
            };

            _tipoUsuarioRepository.Atualizar(id, tipoUsuarioAtualizado);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da Api que faz a chamada para um metodo de deletar um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo de evento a ser excluido</param>
    /// <returns>Status code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoUsuarioRepository.Deletar(id);

            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}