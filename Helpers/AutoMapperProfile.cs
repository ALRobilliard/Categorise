using AutoMapper;
using Categorise.Dtos;
using Categorise.Models;

namespace Categorise.Helpers
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
            CreateMap<AccountDto, Account>();
            CreateMap<CategoryDto, Category>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}