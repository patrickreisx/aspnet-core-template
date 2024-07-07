using AspNetCoreTemplate.API.Models.DTOs.User;

namespace AspNetCoreTemplate.API.UseCases.Interfaces;

public interface IUserUseCase
{
	Task<UserReadDto> Show(Guid uuid);
	Task<UserCreateDto> Create(UserCreateDto user);
}
