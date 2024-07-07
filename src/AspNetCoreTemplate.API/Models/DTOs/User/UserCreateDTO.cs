namespace AspNetCoreTemplate.API.Models.DTOs.User;

public class UserCreateDto
{
	public string Email { get; init; } = null!;
	public string UserName { get; init; } = null!;
	public string Password { get; init; } = null!;
}

