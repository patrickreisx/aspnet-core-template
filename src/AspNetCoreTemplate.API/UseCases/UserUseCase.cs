using AspNetCoreTemplate.API.Models.DTOs.User;
using AspNetCoreTemplate.API.Services.Interfaces;
using AspNetCoreTemplate.API.UseCases.Interfaces;
using AspNetCoreTemplate.Domain.Entities;
using AspNetCoreTemplate.Infra.Repository.Interfaces;
using AutoMapper;

namespace AspNetCoreTemplate.API.UseCases;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
public class UserUseCase(IMapper mapper, IUserRepository userRepository, IHashService hashService) : IUserUseCase
{
	public async Task<UserReadDto> Show(Guid uuid)
	{
		var user = await userRepository.GetByUuid(uuid);

		var userVm = mapper.Map<UserReadDto>(user);

		return userVm;
	}

	public async Task<UserCreateDto> Create(UserCreateDto user)
	{
		var newUser = mapper.Map<User>(user);
		newUser.Password = hashService.Hash(user.Password);

		await userRepository.InsertUser(newUser);
		await userRepository.SaveAsync();

		return user;
	}
}
