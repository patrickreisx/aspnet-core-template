using AspNetCoreTemplate.Domain.Entities;

namespace AspNetCoreTemplate.Infra.Repository.Interfaces;

public interface IUserRepository
{
	Task<User?> GetByUuid(Guid uuid);
	Task<User?> GetByUsername(string username);
	Task InsertUser(User user);
	void DeleteUser(User user);
	void UpdateUser(User user);
	Task SaveAsync();
}
