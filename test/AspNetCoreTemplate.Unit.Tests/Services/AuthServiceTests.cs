using AspNetCoreTemplate.API.Configuration;
using AspNetCoreTemplate.API.Models.DTOs.Auth;
using AspNetCoreTemplate.API.Models.DTOs.User;
using AspNetCoreTemplate.API.Services;
using AspNetCoreTemplate.API.Services.Interfaces;
using AspNetCoreTemplate.Domain.Entities;
using AspNetCoreTemplate.Infra.Repository.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;

namespace AspNetCoreTemplate.Unit.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IHashService> _hashServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            Mock<IOptions<JwtSettings>> jwtSettingsMock = new();
            jwtSettingsMock.Setup(x => x.Value).Returns(new JwtSettings { SecretKey = "c3204acae919fb225640ecad6227582a" });

            _userRepositoryMock = new Mock<IUserRepository>();
            _hashServiceMock = new Mock<IHashService>();
            _tokenServiceMock = new Mock<ITokenService>();
            _mapperMock = new Mock<IMapper>();
            _authService = new AuthService(_userRepositoryMock.Object, _hashServiceMock.Object, _tokenServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            _userRepositoryMock.Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            var authRequest = new AuthRequestDto { Username = "nonexistentuser", Password = "password" };

            var result = await _authService.Authenticate(authRequest);

            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult?.Value.Should().BeEquivalentTo(new { message = "User not found" });
        }

        [Fact]
        public async Task Authenticate_ShouldReturnUnauthorized_WhenPasswordIsInvalid()
        {
            var user = new User { UserName = "existinguser", Password = "hashedpassword" };
            _userRepositoryMock.Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                .ReturnsAsync(user);

            _hashServiceMock.Setup(hashService => hashService.Compare(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Failed);

            var authRequest = new AuthRequestDto { Username = "existinguser", Password = "wrongpassword" };

            var result = await _authService.Authenticate(authRequest);

            result.Should().BeOfType<UnauthorizedObjectResult>();
            var unauthorizedResult = result as UnauthorizedObjectResult;
            unauthorizedResult?.Value.Should().BeEquivalentTo(new { message = "Invalid password" });
        }

        [Fact]
        public async Task Authenticate_ShouldReturnOk_WithUserAndToken_WhenCredentialsAreValid()
        {
            var user = new User { UserName = "existinguser", Password = "hashedpassword" };
            var userReadDto = new UserReadDto { UserName = "existinguser" };
            _userRepositoryMock.Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                .ReturnsAsync(user);

            _hashServiceMock.Setup(hashService => hashService.Compare(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);

            _mapperMock.Setup(mapper => mapper.Map<UserReadDto>(user))
                .Returns(userReadDto);

            var authRequest = new AuthRequestDto { Username = "existinguser", Password = "correctpassword" };

            var result = await _authService.Authenticate(authRequest);

            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().BeEquivalentTo(new
            {
                user = new { UserName = "existinguser" },
                token = okResult.Value?.GetType().GetProperty("token")?.GetValue(okResult.Value)?.ToString()
            });

            var token = okResult?.Value?.GetType().GetProperty("token")?.GetValue(okResult.Value)?.ToString();
            token.Should().NotBeNullOrEmpty();
        }
    }
}
