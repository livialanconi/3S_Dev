using Filmes.WebAPI.DTO;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Filmes.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    public IActionResult Login(LoginDTO loginDto)
    {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDto.Email!, loginDto.Senha!);

            if (usuarioBuscado == null)
            {
                return NotFound("Email ou Senha inválidos!");
            }

            //caso encontre o usuario, prosseguir para criação do token 
            //1 - Definir as informações(claims) que serão fornecidas no token (Payload)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario),

                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),

                //existe a possibilidade de criar uma claim personalizada
                //new Claim("Claim Personalizada", "Valor da claim personalizada")
            };

            //2 - definir a chave de acesso token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-webapi-autentificacao-webapi-dev"));

            //3 - Definir as credenciais do token(Header)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4 - Gerar o token
            var token = new JwtSecurityToken
                (
                 issuer: "api_filmes",

                 audience: "api_filmes",

                 claims: claims,

                 expires: DateTime.Now.AddMinutes(5),

                 signingCredentials: creds
                );

            //5 - Retornar o token criado
            return Ok(new
            {
              token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
        catch (Exception)
        {

            throw;
        }
    }
}
