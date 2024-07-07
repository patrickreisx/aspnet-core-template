using AspNetCoreTemplate.API.UseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTemplate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ArticleController(IArticleUseCase articleUseCase) : ControllerBase
{
	private readonly IArticleUseCase _articleUseCase = articleUseCase;

	[HttpGet]
	public ActionResult Get()
	{
		var articles = _articleUseCase.GetArticles();

		return Ok(articles);
	}

	[HttpGet("{id}")]
	public ActionResult Get(int id)
	{
		return Ok("Hello World");
	}
}
