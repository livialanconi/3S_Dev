using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus_.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventPlus_.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly IEventoRepository _eventoRepository;

    public EventoController(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o metodo de listar eventos filtrado pelo id do usuário
    /// </summary>
    /// <param name="IdUsuario">Id do usuario para filtragem</param>
    /// <returns>Status Code 200 e uma lista de eventos</returns>
    [HttpGet("Usuario/{IdUsuario}")]
    public IActionResult ListarPorId(Guid IdUsuario)
    {
        try
        {

            return Ok(_eventoRepository.ListarPorId(IdUsuario));

        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);

        }
    }


    /// <summary>
    /// Endpoint da API que faz a chamada para o método de listar os próximos eventos
    /// </summary>
    /// <returns>Status code 200 e a lista dos próximos eventos</returns>
    [HttpGet("ListarProximos")]
    public IActionResult BuscarProximosEventos()
    {
        try
        {
            return Ok(_eventoRepository.ProximosEventos());
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
            return Ok(_eventoRepository.Listar());
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
    public IActionResult Cadastrar(Evento evento)
    {
        try
        {
            var novoEvento = new Evento
            {
                Nome = evento.Nome!,

                Descricao = evento.Descricao!,

                DataEvento = evento.DataEvento!


            };

            _eventoRepository.Cadastrar(novoEvento);


            return StatusCode(201, novoEvento);
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
    public IActionResult Atualizar(Guid id, Evento evento)
    {
        try
        {
            var eventoAtualizado = new Evento
            {
                Nome = evento.Nome!,

                Descricao = evento.Descricao!,

                DataEvento = evento.DataEvento!
            };
            _eventoRepository.Atualizar(id, eventoAtualizado);
            return StatusCode(204, evento);

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
    public IActionResult Delete(Guid id)
    {
        try
        {
            _eventoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }



}
