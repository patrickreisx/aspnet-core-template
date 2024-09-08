using System.Security.Claims;
using AspNetCoreTemplate.Domain.Entities;

namespace AspNetCoreTemplate.API.Services.Interfaces;

public interface ITokenService
{
	string GenerateToken(User user);
	ClaimsPrincipal ValidateToken(string token);
}
