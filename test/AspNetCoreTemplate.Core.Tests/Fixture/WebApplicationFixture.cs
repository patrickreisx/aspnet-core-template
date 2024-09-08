using System.Data.Common;
using AspNetCoreTemplate.Infra.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace AspNetCoreTemplate.Core.Tests.Fixture;

public class WebApplicationFixture<TProgram>
	: WebApplicationFactory<TProgram> where TProgram : class
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			var dbContextDescriptor = services.SingleOrDefault(
				d => d.ServiceType ==
				     typeof(DbContextOptions<EntityContext>));

			services.Remove(dbContextDescriptor);

			var dbConnectionDescriptor = services.SingleOrDefault(
				d => d.ServiceType ==
				     typeof(DbConnection));

			services.Remove(dbConnectionDescriptor);

			// Create open SqliteConnection so EF won't automatically close it.
			services.AddSingleton<DbConnection>(container =>
			{
				var connection = new NpgsqlConnection("DataSource=:memory:");
				connection.Open();

				return connection;
			});

			services.AddDbContext<EntityContext>((container, options) =>
			{
				var connection = container.GetRequiredService<DbConnection>();
				options.UseNpgsql(connection);
			});
		});

		builder.UseEnvironment("Development");
	}
}
