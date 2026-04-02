using Herois.WebApi.BdContextHerois;
using Herois.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/[controller]")]
public class HeroisController : ControllerBase
{
    private readonly HeroisContext _context;

    public HeroisController(HeroisContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get() => Ok(_context.Herois);

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var heroi = _context.Herois.Find(id);
        if (heroi == null) return NotFound();
        return Ok(heroi);
    }

    [HttpPost]
    public IActionResult Post(Heroi heroi)
    {
        if (string.IsNullOrEmpty(heroi.Nome)) return BadRequest();

        _context.Herois.Add(heroi);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = heroi.IdHeroi }, heroi);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Heroi heroi)
    {
        var existente = _context.Herois.Find(id);
        if (existente == null) return NotFound();

        existente.Nome = heroi.Nome;
        existente.Poder = heroi.Poder;
        existente.EquipeId = heroi.EquipeId;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var heroi = _context.Herois.Find(id);
        if (heroi == null) return NotFound();

        _context.Herois.Remove(heroi);
        _context.SaveChanges();

        return NoContent();
    }
}