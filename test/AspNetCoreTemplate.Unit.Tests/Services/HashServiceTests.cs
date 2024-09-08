using AspNetCoreTemplate.API.Services;
using AspNetCoreTemplate.API.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTemplate.Unit.Tests.Services;

public class HashServiceTests
{
	private readonly IHashService _hashService = new HashService();

	[Fact]
	public void Hash_ShouldReturnOK_WhenValidPassword()
	{
		const string password = "my_password";
		var hashPassword = _hashService.Hash(password);

		var result = _hashService.Compare(password, hashPassword);

		result.Should().Be(PasswordVerificationResult.Success);
	}

	[Fact]
	public void Hash_ShouldReturnHashedPassword()
	{
		const string password = "my_password";

		var result = _hashService.Hash(password);

		result.Should().NotBeNullOrEmpty();
		result.Should().NotBe(password);
	}
}
