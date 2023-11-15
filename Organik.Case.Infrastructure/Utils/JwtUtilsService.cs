using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Organik.Case.Application.Helpers;
using Organik.Case.Application.Interfaces.Utils;
using Organik.Case.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Organik.Case.Infrastructure.Utils
{
	public class JwtUtilsService : IJwtUtilsService
	{
        private readonly AppSettings _appSettings;
		public JwtUtilsService(IOptions<AppSettings> options)
		{
            _appSettings = options.Value;
		}

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
            }),
                Expires = DateTime.UtcNow.AddMinutes((double)_appSettings.AccessTokenLifeInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

