using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Quetzal.Application.DTOs;
using Quetzal.Domain.Entidades;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Quetzal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        //gerar token jwt (token usado na autenticação das apis)
        private JwtSecurityToken GerarToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Chave"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Emissor"],
                audience: _configuration["Jwt:Audiencia"],
                expires: DateTime.Now.AddHours(8),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }


        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUserDto dto)
        {
            var userExists = await _userManager.FindByEmailAsync(dto.Email);
            if (userExists != null)
                return BadRequest(ApiResposta<object>.Falha("Ja existe um usuario com este e-mail."));

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                NomeCompleto = dto.NomeCompleto,
                Ativo = true,
                DataCadastro = DateTime.UtcNow
            };

            //cria o usuario no banco
            var result = await _userManager.CreateAsync(user, dto.Senha);
            if (!result.Succeeded)
            {
                var erros = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(ApiResposta<object>.FalhaValidacao(erros, "Erro ao criar usuario."));
            }

            //defini o "perfil" cliente para o usuario
            await _userManager.AddToRoleAsync(user, "Cliente");

            return StatusCode(201, ApiResposta<object>.Ok(null!, "Usuario registrado com sucesso."));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            //verifica se o usuario existe
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !user.Ativo)
                return Unauthorized(ApiResposta<LoginRespostaDto>.Falha("Usuario invalido ou inativo."));

            //verifica se a senha valida
            var senhaValida = await _userManager.CheckPasswordAsync(user, dto.Senha);
            if (!senhaValida)
                return Unauthorized(ApiResposta<LoginRespostaDto>.Falha("Senha incorreta."));

            //buscar as roles do usuario (perfis)
            var roles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.NomeCompleto),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = GerarToken(authClaims);

            var resposta = new LoginRespostaDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracao = token.ValidTo,
                NomeUsuario = user.NomeCompleto,
                Email = user.Email!,
                Perfis = roles.ToList()
            };

            return Ok(ApiResposta<LoginRespostaDto>.Ok(resposta, "Login realizado com sucesso."));
        }

    }
}
