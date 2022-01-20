using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LeagueOfLegends_API.Data;
using LeagueOfLegends_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LeagueOfLegends_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")] //Versão legada - Versão sem Suporte

    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public UsuarioController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpPost("Registro")]
        //api/v1/usuarios/Registro
        public IActionResult Registro([FromBody] UsuarioTemp usuario)
        {
            var usuRegistrado = _database.Usuarios.FirstOrDefault(x => x.Email == usuario.Email);
            if (usuRegistrado == null)
            {
                Usuario usuarioBd = new Usuario();
                usuarioBd.Email = usuario.Email;
                usuarioBd.Senha = usuario.Senha;
                _database.Usuarios.Add(usuarioBd);
                _database.SaveChanges();
                return Ok(new { msg = "Usuário cadastrado com sucesso!" });
            }
            else
            {
                Response.StatusCode = 400;
                return new ObjectResult(new { msg = "Email já registrado." });
            }

        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioTemp credenciais)
        {
            try
            {
                var usuario = _database.Usuarios.FirstOrDefault(x => x.Email.Equals(credenciais.Email));

                if (usuario != null)
                {
                    if (usuario.Senha.Equals(credenciais.Senha))
                    {
                        var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Secret));
                        var credenciaisAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);

                        var claims = new List<Claim>();
                        claims.Add(new Claim("id", usuario.Id.ToString()));
                        claims.Add(new Claim("email", usuario.Email));
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));

                        var JWT = new JwtSecurityToken(
                            issuer: "Admin",
                            expires: DateTime.Now.AddHours(1),
                            audience: "User",
                            signingCredentials: credenciaisAcesso,
                            claims: claims
                        );

                        return Ok(new JwtSecurityTokenHandler().WriteToken(JWT));
                    }

                    Response.StatusCode = 401;
                    return new ObjectResult("Login Inválido");
                }
                else
                {
                    Response.StatusCode = 401;
                    return new ObjectResult("");
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 401;
                return new ObjectResult("");
            }
        }
    }

    public class UsuarioTemp
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}