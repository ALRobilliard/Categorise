using AutoMapper;
using CategoriseApi.Dtos;
using CategoriseApi.Models;

namespace CategoriseApi.Helpers
{
  /// <summary>
  /// Profile for AutoMapper.
  /// </summary>
  public class AutoMapperProfile : Profile
  {
    /// <summary>
    /// AutoMapperProfile constructor. Defines AutoMapper mappings.
    /// </summary>
    public AutoMapperProfile()
    {
      CreateMap<CategoryDto, Category>();
      CreateMap<UserRegisterDto, User>();
    }
  }
}