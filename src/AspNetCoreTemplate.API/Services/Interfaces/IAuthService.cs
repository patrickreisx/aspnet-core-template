using AspNetCoreTemplate.API.Models.DTOs.Auth;
using AspNetCoreTemplate.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTemplate.API.Services.Interfaces;

public interface IAuthService
{
	Task<ActionResult<dynamic>?> Authenticate(AuthRequestDto auth);
	string GenerateToken(User user);
}
