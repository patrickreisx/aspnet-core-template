using AspNetCoreTemplate.Domain.Entities;

namespace AspNetCoreTemplate.API.UseCases.Interfaces;

public interface IArticleUseCase
{
	IEnumerable<Article> GetArticles();
}
