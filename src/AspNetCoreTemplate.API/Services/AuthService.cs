using AspNetCoreTemplate.API.Models.DTOs.Auth;
using AspNetCoreTemplate.API.Models.DTOs.User;
using AspNetCoreTemplate.API.Services.Interfaces;using AspNetCoreTemplate.Infra.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTemplate.API.Services;

public class AuthService(IUserRepository userRepository, IHashService hashService, ITokenService tokenService, IMapper mapper) : IAuthService
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

		var token = tokenService.GenerateToken(userEntity);

		return new OkObjectResult(new
		{
			user,
			token
		});
	}
}

