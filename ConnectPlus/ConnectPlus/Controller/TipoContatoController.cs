using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TipoContatoController : ControllerBase
{
    private ITipoContatoRepository _tipoContatoRepository;
    public TipoContatoController(ITipoContatoRepository tipoContatoRepository)
    {
        _tipoContatoRepository = tipoContatoRepository;
    }
    /// <summary>
    /// Endpoint para listar os tipos de contato cadastrados no banco de dados
    /// </summary>
    /// <returns>Status Code 200 e lista de tipos de contato cadastrados no banco de dados</returns>
    [HttpGet]
    public IActionResult Listar()
    {
        try
        {
            return Ok(_tipoContatoRepository.Listar());
        }
        catch (Exception erro)
        {

            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endepoint para buscar um tipo de contato pelo seu id
    /// </summary>
    /// <param name="id">id do tipo de contato buscado</param>
    /// <returns>Status Code 200 e tipo de contato buscado</returns>

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_tipoContatoRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API para cadastrar um novo tipo de contato no banco de dados
    /// </summary>
    /// <param name="tipoContato">Tipo de contato cadastrado</param>
    /// <returns>Status Code 201 e tipo de contato cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(TipoContatoDTO tipoContato)
    {
        try
        {
            var novoTipoContato = new TipoContato
            {
                Titulo = tipoContato.Titulo!
            };
            _tipoContatoRepository.Cadastrar(novoTipoContato);
            return StatusCode(201, novoTipoContato);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API para atualizar um tipo de contato já cadastrado no banco de dados
    /// </summary>
    /// <param name="id">id do tipo de contato a ser atualizado</param>
    /// <param name="tipoContato">dados do tipo de contato atualizado</param>
    /// <returns>Status Code 204 e tipo de contato com os dados atualizados</returns>
    [HttpPut("{id}")]
    public IActionResult Atualizar(Guid id, TipoContatoDTO tipoContato)
    {
        try
        {
            var tipoContatoAtualizado = new TipoContato
            {
                Titulo = tipoContato.Titulo!
            };
            _tipoContatoRepository.Atualizar(id, tipoContatoAtualizado);
            return StatusCode(204, tipoContatoAtualizado);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
    /// <summary>
    /// Endpoint da API para deletar um tipo de contato cadastrado no banco de dados
    /// </summary>
    /// <param name="id">id do tipo de contato a ser deletado</param>
    /// <returns>Status Code 204</returns>
    [HttpDelete("{id}")]
    public IActionResult Deletar(Guid id)
    {
        try
        {
            _tipoContatoRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }
}