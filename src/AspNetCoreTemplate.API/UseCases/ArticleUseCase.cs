using AspNetCoreTemplate.API.UseCases.Interfaces;
using AspNetCoreTemplate.Domain.Entities;
using AspNetCoreTemplate.Infra.Context;
using Microsoft.EntityFrameworkCore;


namespace AspNetCoreTemplate.API.UseCases;
public class ArticleUseCase(EntityContext context) : IArticleUseCase
{
	public IEnumerable<Article> GetArticles()
	{
		var articles = context.Article.Include(x => x.User).ToList();

		return articles;
	}
}
