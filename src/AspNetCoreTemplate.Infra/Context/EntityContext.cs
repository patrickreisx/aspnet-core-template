using AspNetCoreTemplate.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace AspNetCoreTemplate.Infra.Context;

public class EntityContext : DbContext
{
	public EntityContext(DbContextOptions<EntityContext> options)
		: base(options)
	{
	}

	public DbSet<Article> Article { get; init; } = null!;
	public DbSet<User> User { get; init; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Article>(ob =>
		{
			ob.ToTable("articles");
			ob.HasKey(o => o.ArticleId);
			ob.Property(o => o.ArticleId).HasColumnName("id").ValueGeneratedOnAdd();
			ob.Property(o => o.ArticleUuid).HasColumnName("uuid").HasValueGenerator<GuidValueGenerator>();
			ob.Property(o => o.Title).HasColumnName("title").IsRequired();
			ob.Property(o => o.Content).HasColumnName("content").IsRequired();
			ob.Property(o => o.CreatedAt).HasColumnName("created_at");
			ob.Property(o => o.UpdatedAt).HasColumnName("updated_at");
			ob.Property(o => o.UserId).HasColumnName("user_id");
			ob.HasOne(o => o.User).WithMany(u => u.Articles).HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<User>(ob =>
		{
			ob.ToTable("users");
			ob.HasKey(o => o.UserId);
			ob.Property(o => o.UserId).HasColumnName("id").ValueGeneratedOnAdd();
			ob.Property(o => o.UserUuid).HasColumnName("uuid").HasValueGenerator<GuidValueGenerator>();
			ob.Property(o => o.Email).HasColumnName("email").IsRequired();
			ob.Property(o => o.UserName).HasColumnName("username").IsRequired();
			ob.Property(o => o.Password).HasColumnName("password").IsRequired();
		});
	}
}
