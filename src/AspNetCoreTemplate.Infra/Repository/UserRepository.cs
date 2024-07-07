using AspNetCoreTemplate.Domain.Entities;
using AspNetCoreTemplate.Infra.Context;
using AspNetCoreTemplate.Infra.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTemplate.Infra.Repository;

public sealed class UserRepository(EntityContext context) : IUserRepository, IDisposable
{
	~UserRepository()
	{
		Dispose(false);
	}

	public Task<User?> GetByUuid(Guid uuid)
	{
		return context.User.FirstOrDefaultAsync(b => b.UserUuid == uuid);
	}

	public async Task<User?> GetByUsername(string username)
	{
		return await context.User.SingleOrDefaultAsync(user => user.UserName == username);
	}

	public async Task InsertUser(User user)
	{
		await context.User.AddAsync(user);
	}

	public void DeleteUser(User user)
	{
		context.User.Remove(user);
	}

	public void UpdateUser(User user)
	{
		context.User.Update(user);
	}

	public async Task SaveAsync()
	{
		await context.SaveChangesAsync();
	}

	private bool _disposed;

	private void Dispose(bool disposing)
	{
		if (!this._disposed)
		{
			if (disposing)
			{
				context.Dispose();
			}
		}
		this._disposed = true;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
