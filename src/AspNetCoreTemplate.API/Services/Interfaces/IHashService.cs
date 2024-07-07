using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTemplate.API.Services.Interfaces;

public interface IHashService
{
	PasswordVerificationResult Compare(string password, string hash);
	string Hash(string password);
}
