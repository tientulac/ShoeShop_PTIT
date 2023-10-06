using AutoMapper;
using ShoeShopAPI.Models;
using ShoeShopAPI.Models.DTO;

namespace ShoeShopAPI.Mappings
{
    public class ToMapping : Profile
    {
        public ToMapping()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}