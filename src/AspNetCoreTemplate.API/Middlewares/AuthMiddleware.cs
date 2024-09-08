using AspNetCoreTemplate.API.Services.Interfaces;
using AspNetCoreTemplate.Infra.Context;

namespace AspNetCoreTemplate.API.Middlewares;

public class AuthMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
{

	public async Task Invoke(HttpContext context)
	{
		var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

		if (token != null)
		{
			using (var scope = scopeFactory.CreateScope())
			{
				var dbContext = scope.ServiceProvider.GetRequiredService<EntityContext>();
				var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();

				AttachUserToContext(context, dbContext, tokenService, token);
			}
		}
		await next(context);
	}

	private void AttachUserToContext(HttpContext context, EntityContext dbContext, ITokenService tokenService, string token)
	{
		var claimsPrincipal = tokenService.ValidateToken(token);


		var userId = Guid.Parse(claimsPrincipal.Claims.First(x => x.Type == "uuid").Value);

		context.Items["User"] = dbContext.User.FirstOrDefault(u => u.UserUuid == userId);
	}
}
