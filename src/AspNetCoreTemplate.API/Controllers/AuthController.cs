using AspNetCoreTemplate.API.Models.DTOs.Auth;
using AspNetCoreTemplate.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTemplate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<dynamic>?> Authenticate([FromBody] AuthRequestDto auth)
	{
		var user = await authService.Authenticate(auth);

		return user;
	}
}
