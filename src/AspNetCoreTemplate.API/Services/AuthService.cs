using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCoreTemplate.API.Configuration;
using AspNetCoreTemplate.API.Models.DTOs.Auth;
using AspNetCoreTemplate.API.Models.DTOs.User;
using AspNetCoreTemplate.API.Services.Interfaces;
using AspNetCoreTemplate.Domain.Entities;
using AspNetCoreTemplate.Infra.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreTemplate.API.Services;

public class AuthService(IOptions<JwtSettings> jwtSettings, IUserRepository userRepository, IHashService hashService, IMapper mapper) : IAuthService
{
	public async Task<ActionResult> Authenticate(AuthRequestDto auth)
	{
		var userEntity = await userRepository.GetByUsername(auth.Username);
		var user = mapper.Map<UserReadDto>(userEntity);

		if (userEntity == null)
		{
			return new NotFoundObjectResult(new { message = "User not found" });
		}

		var verifyHash = hashService.Compare(auth.Password, userEntity.Password);
		if (verifyHash == PasswordVerificationResult.Failed)
		{
			return new UnauthorizedObjectResult(new { message = "Invalid password" });
		}

		var token = GenerateToken(userEntity);

		return new OkObjectResult(new
		{
			user,
			token
		});
	}

	public string GenerateToken(User user)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(jwtSettings.Value.SecretKey);

		// ReSharper disable once InconsistentNaming
		// Use PascalCase for constant names, both fields and local constants.

		const int ExpireTime = 2;

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Name, user.UserName)
			}),
			Expires = DateTime.UtcNow.AddHours(ExpireTime),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}
}

