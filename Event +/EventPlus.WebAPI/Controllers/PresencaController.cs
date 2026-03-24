using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private IPresencaRepository _presencaRepository;
    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// Endpoint da api que retorna uma presença por id
    /// </summary>
    /// <param name="id">id da presenca a ser buscada</param>
    /// <returns>Status code 200 e presenca buscada</returns>

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que retorna uma lista de presenças filtrada por usuário
    /// </summary>
    /// <param name="idUsuario">id do usuario para filtragem</param>
    /// <returns>uma lista de presencas filtra pelo usuario</returns>

    [HttpGet("ListarMinhas/{IdUsuario}")]
    public IActionResult BuscarPorUsuario(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz chamada para o metódo de listar o evento
    /// </summary>
    /// <returns>Status code 200 e a lista de tipos de evento</returns>
 
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_presencaRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o metódo de cadastrar um evento
    /// </summary>
    /// <param name="evento">Evento a ser cadastrado</param>
    /// <returns>Status code 201 e o tipo de evento cadastrado</returns>

    [HttpPost]
    public IActionResult Inscrever(PresencaDTO presencaDto)
    {
        try
        {
            var novaPresenca = new Presenca
            {
                IdUsuario = presencaDto.IdUsuario,
                IdEvento = presencaDto.IdEvento,
                Situacao = presencaDto.Situacao
            };

            _presencaRepository.Inscrever(novaPresenca);

            return StatusCode(201, novaPresenca);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o metódo de atualizar um evento
    /// </summary>
    /// <param name="id">Id do evento com dados atualizados</param>
    /// <param name="tipoEvento"></param>
    /// <returns>Status code 204 e o evento atualizado</returns>
   
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, PresencaDTO presencaDto)
    {
        try
        {
            var presencaAtualizada = new Presenca
            {
                IdPresenca = Guid.NewGuid(),
                IdUsuario = presencaDto.IdUsuario,
                IdEvento = presencaDto.IdEvento,
                Situacao = presencaDto.Situacao
            };

            _presencaRepository.Atualizar(id);

            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o metódo de deletar um evento
    /// </summary>
    /// <param name="id">Id do evento a ser excluído</param>
    /// <returns>Status Code 204</returns> 
    
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _presencaRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

}
