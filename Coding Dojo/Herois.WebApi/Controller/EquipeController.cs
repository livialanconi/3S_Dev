using Herois.WebApi.DTO;
using Herois.WebApi.Interfaces;
using Herois.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Herois.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipeController : ControllerBase
{
    private readonly IEquipeRepository _equipeRepository;

    public EquipeController(IEquipeRepository equipeRepository)
    {
        _equipeRepository = equipeRepository;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_equipeRepository.Listar());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var equipe = _equipeRepository.BuscarPorId(id);
        if (equipe == null) return NotFound();
        return Ok(equipe);
    }

    [HttpPost]
    public IActionResult Post(EquipeDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var equipe = new Equipe
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao ?? string.Empty
        };

        _equipeRepository.CadastrarEquipe(equipe);

        return CreatedAtAction(nameof(GetById), new { id = equipe.IdEquipe }, equipe);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, EquipeDTO dto)
    {
        var existente = _equipeRepository.BuscarPorId(id);
        if (existente == null) return NotFound();

        var atualizada = new Equipe
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao ?? existente.Descricao
        };

        _equipeRepository.AtualizarEquipe(id, atualizada);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existente = _equipeRepository.BuscarPorId(id);
        if (existente == null) return NotFound();

        _equipeRepository.DeletarEquipe(id);
        return NoContent();
    }
}