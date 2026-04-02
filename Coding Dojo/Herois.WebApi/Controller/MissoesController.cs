using Herois.WebApi.Interfaces;
using Herois.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MissoesController : ControllerBase
{
    private readonly IMissaoRepository _missaoRepository;

    public MissoesController(IMissaoRepository missaoRepository)
    {
        _missaoRepository = missaoRepository;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_missaoRepository.Listar());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var missao = _missaoRepository.BuscarPorId(id);
        if (missao == null) return NotFound();
        return Ok(missao);
    }

    [HttpPost]
    public IActionResult Post(MissaoDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var missao = new Missao
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            HeroiId = dto.HeroiId,
            EquipeId = dto.EquipeId
        };

        _missaoRepository.CadastrarMissao(missao);
        return CreatedAtAction(nameof(GetById), new { id = missao.IdMissao }, missao);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, MissaoDTO dto)
    {
        var existente = _missaoRepository.BuscarPorId(id);
        if (existente == null) return NotFound();

        var atualizada = new Missao
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            HeroiId = dto.HeroiId,
            EquipeId = dto.EquipeId
        };

        _missaoRepository.AtualizarMissao(id, atualizada);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existente = _missaoRepository.BuscarPorId(id);
        if (existente == null) return NotFound();

        _missaoRepository.DeletarMissao(id);
        return NoContent();
    }
}