namespace AspNetCoreTemplate.Domain.Entities;

public class Article
{
	public int ArticleId { get; init; }
	public Guid ArticleUuid { get; init; }
	public string Title { get; init; } = null!;
	public string Content { get; init; } = null!;
	public DateTime CreatedAt { get; init; }
	public DateTime? UpdatedAt { get; init; }

	public User User { get; init; } = null!;
	public int UserId { get; init; }
}
