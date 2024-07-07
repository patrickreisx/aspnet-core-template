namespace AspNetCoreTemplate.Domain.Entities;

public class Article
{
	public int ArticleId { get; init; }
	public string Title { get; init; }
	public string Content { get; init; }
	public string DateTime { get; init; }
	public DateTime CreatedAt { get; init; }
	public DateTime? UpdatedAt { get; init; }

	public User User { get; init; }
	public int UserId { get; init; }
}
