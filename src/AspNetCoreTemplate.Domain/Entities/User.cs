namespace AspNetCoreTemplate.Domain.Entities;

public sealed class User
{
	public int UserId { get; init; }

	public Guid UserUuid { get; init; }
	public string Email { get; init; } = null!;
	public string UserName { get; init; } = null!;
	public string Password { get; set; } = null!;
	public ICollection<Article>? Articles { get; init; }
}
