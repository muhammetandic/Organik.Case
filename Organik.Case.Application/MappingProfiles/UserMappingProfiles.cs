using AutoMapper;
using Organik.Case.Application.Dtos;
using Organik.Case.Domain.Entities;

namespace Organik.Case.Application.MappingProfiles
{
	public class UserMappingProfiles : Profile
	{
		public UserMappingProfiles()
		{
			CreateMap<RegisterRequest, User>();
			CreateMap<User, RegisterResponse>();
		}
	}
}

