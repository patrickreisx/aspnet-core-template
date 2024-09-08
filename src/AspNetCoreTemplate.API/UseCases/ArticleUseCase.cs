using AspNetCoreTemplate.API.UseCases.Interfaces;
using AspNetCoreTemplate.Domain.Entities;
using AspNetCoreTemplate.Infra.Context;
using Microsoft.EntityFrameworkCore;


namespace AspNetCoreTemplate.API.UseCases;
public class ArticleUseCase(EntityContext context, IHttpContextAccessor httpContextAccessor) : IArticleUseCase
{
	public IEnumerable<Article> GetArticles()
	{
		var user = httpContextAccessor.HttpContext?.Items["User"] as User;

		var articles = context.Article.Include(x => x.User)
			.Where(x => x.UserId == user!.UserId)
			.ToList();

		return articles;
	}
}
