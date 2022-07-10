using AutoMapper;
using ColoritSummer.Data.MySQL.Entities;
using ColoritSummer.Interfaces.Repositories;
using ColoritSummer.Models.DTO;
using ColoritSummer.WebAPI.Infrastructure.Auth;
using ColoritSummer.WebAPI.Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ColoritSummer.WebAPI.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository<User> _repository;
        private readonly IOptions<JwtConfig> _jwtConfig;

        public AuthController(IMapper mapper, IUserRepository<User> repository, IOptions<JwtConfig> jwtConfig)
        {
            _mapper = mapper;
            _repository = repository;
            _jwtConfig = jwtConfig;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(string))]
        public async Task<IActionResult> Login(LoginInfo loginInfo)
        {
            var user = await _repository.GetByEmail(loginInfo.Login).ConfigureAwait(false);

            if (user == null)
                return NotFound($"Пользователь с логином \"{loginInfo.Login}\" не найден");

            if(user.IsBanned)
                return Forbid("Доступ к системе был ограничен");

            if(PasswordHelper.VerifyHashedPassword(user.Password, loginInfo.Password) == false)
                return Unauthorized("Не верный пароль");

            return Ok(CreateToken(user.Id, "Я забыл добавить роль"));
        }

        [HttpPost("registration")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registration(RegistrationInfo info)
        {
            if (await _repository.ExistEmail(info.Login).ConfigureAwait(false))
                return Conflict($"Пользователь с логином {info.Login} уже существует");


            var user = _mapper.Map<User>(info);
            user.Password = PasswordHelper.HashPassword(user.Password);
            var res = await _repository.Add(user).ConfigureAwait(false);

            if(res != null)
                return Ok("Успешная регистрация");

            return StatusCode(500);
        }
        private string CreateToken(int userId, string userRole)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, userId.ToString()),
                new Claim(ClaimTypes.Role, userRole)
            };
            var token = new JwtSecurityToken
                (
                    issuer: _jwtConfig.Value.Issuer,
                    audience: _jwtConfig.Value.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.Key)),
                        SecurityAlgorithms.HmacSha256)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
    }
}
