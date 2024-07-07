using AspNetCoreTemplate.API.Models.DTOs.User;
using AspNetCoreTemplate.Domain.Entities;
using AutoMapper;

namespace AspNetCoreTemplate.API.AutoMapper;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		#region User
			CreateMap<User, UserCreateDto>();
			CreateMap<UserCreateDto, User>();

			CreateMap<User, UserReadDto>();
			CreateMap<UserReadDto, User>();
		#endregion
	}
}
