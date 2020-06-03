using AutoMapper;
using CategoriseApi.Dtos;
using CategoriseApi.Models;

namespace CategoriseApi.Helpers
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<UserDto, User>();
    }
  }
}