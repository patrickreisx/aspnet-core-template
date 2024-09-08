using AspNetCoreTemplate.API.UseCases.Interfaces;
using AspNetCoreTemplate.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTemplate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ArticleController(IArticleUseCase articleUseCase) : ControllerBase
{
	[HttpGet]
	public ActionResult Get()
	{
		var articles = articleUseCase.GetArticles();

		return Ok(articles);
	}

	[HttpGet("{id:int}")]
	public ActionResult Get(int id)
	{
		return Ok("Hello World");
	}
}
