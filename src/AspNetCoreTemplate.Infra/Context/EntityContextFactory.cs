using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AspNetCoreTemplate.Infra.Context
{
	public class EntityContextFactory : IDesignTimeDbContextFactory<EntityContext>
	{
		public EntityContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<EntityContext>();
			optionsBuilder.UseNpgsql("Host=localhost:5432;Database=aspnetcoretemplate;Username=aspnetcoretemplate;Password=aspnetcoretemplate");

			return new EntityContext(optionsBuilder.Options);
		}
	}
}
