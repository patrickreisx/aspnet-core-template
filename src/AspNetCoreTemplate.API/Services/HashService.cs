using AspNetCoreTemplate.API.Services.Interfaces;
using AspNetCoreTemplate.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTemplate.API.Services;

public class HashService : IHashService
{
	public PasswordVerificationResult Compare(string password, string hash)
	{
		var user = new User();
		var hasher = new PasswordHasher<User>();
		var result = hasher.VerifyHashedPassword(user, hash, password);

		return result;
	}

	public string Hash(string password)
	{
		var user = new User();
		var hasher = new PasswordHasher<User>();
		var result = hasher.HashPassword(user, password);

		return result;
	}
}
