using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus_.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstituicaoController : ControllerBase
{
    private IInstituicaoRepository _instituicaoRepository;

    public InstituicaoController(IInstituicaoRepository instituicaoRepository)
    {
        _instituicaoRepository = instituicaoRepository;
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
            return Ok(_instituicaoRepository.Listar());
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
            return Ok(_instituicaoRepository.BuscarPorId(id));
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
    public IActionResult Cadastrar(InstituicaoDTO instituicao)
    {
        try
        {
            var novoInstituicao = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!
            };

            _instituicaoRepository.Cadastrar(novoInstituicao);

            return StatusCode(201, novoInstituicao);
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
    public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
    {
        try
        {
            var instituicaoAtualizada = new Instituicao
            {
                NomeFantasia = instituicao.NomeFantasia!,
                Cnpj = instituicao.Cnpj!,
                Endereco = instituicao.Endereco!
            };

            _instituicaoRepository.Atualizar(id, instituicaoAtualizada);
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
    /// 
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _instituicaoRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}