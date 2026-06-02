using FIlmes.WebAPI.DTO;
using FIlmes.WebAPI.Interface;
using FIlmes.WebAPI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FIlmes.WebAPI.Controllers;

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

            //caso encontre o usuário, ´prosseguir para criação do token

            //1 - Definir as informações(Claims) que serão fornecidas no token(Payload)

            var claims = new[]
            {
                //formato da claim
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario),
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),

                //existe a possibilidade de criar uma claim personalizada
                //new Claim("Criam Personalizam", "Valor da claim personalizada")

            };

            //2 - definir a chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));
            // 3 - Definir as credenciais do token (HEADER)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4- Gerar o token
            var token = new JwtSecurityToken
            (
                //emissor do token
                issuer: "api_filmes",

                //destinatário do token
                audience: "api_filmes",

                //dados definidos nas claims(informações)
                claims: claims,

                //tempo de expiração do token
                expires: DateTime.Now.AddMinutes(5),

                //credenciais do token
                signingCredentials: creds
            );

            //5- Retornar o token criado
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            }



              

            );
        }
        catch (Exception)
        {

            throw;
        }

    }
}
