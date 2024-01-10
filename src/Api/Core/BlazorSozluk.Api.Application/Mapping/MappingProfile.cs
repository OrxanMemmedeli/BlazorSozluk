using AutoMapper;
using BlazorSozluk.Api.Domain.Entities;
using BlazorSozluk.Common.Models.Queries;

namespace BlazorSozluk.Api.Application.Mapping;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<User, LoginUserViewModel>().ReverseMap();
	}
}
