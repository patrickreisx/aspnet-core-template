namespace AspNetCoreTemplate.API.Models.DTOs.Auth;

public class AuthRequestDto
{
	public string Username { get; init; } = null!;
	public string Password { get; init; } = null!;
}
