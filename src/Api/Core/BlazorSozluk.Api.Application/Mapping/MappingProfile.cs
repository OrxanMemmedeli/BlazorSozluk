using AutoMapper;
using BlazorSozluk.Api.Domain.Entities;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;

namespace BlazorSozluk.Api.Application.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<User, LoginUserViewModel>().ReverseMap();
		CreateMap<User, CreateUserCommand>().ReverseMap();
		CreateMap<User, UpdateUserCommand>().ReverseMap();
	}
}
