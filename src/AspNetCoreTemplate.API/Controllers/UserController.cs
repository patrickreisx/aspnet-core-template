using AspNetCoreTemplate.API.Models.DTOs.User;
using AspNetCoreTemplate.API.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTemplate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserUseCase userUseCase)
{
	[HttpGet]
	public async Task<IActionResult> Show(Guid uuid)
	{
		var user = await userUseCase.Show(uuid);

		return new OkObjectResult(user);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] UserCreateDto user)
	{
		var newUser = await userUseCase.Create(user);

		return new CreatedResult("api/user/1", newUser);
	}
}
