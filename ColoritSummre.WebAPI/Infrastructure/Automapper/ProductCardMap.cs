using AutoMapper;
using ColoritSummer.Data.MySQL.Entities;
using ColoritSummer.Models.Entities;

namespace ColoritSummer.WebAPI.Infrastructure.Automapper
{
    public class ProductCardMap : Profile
    {
        public ProductCardMap()
        {
            CreateMap<ProductCard, ProductCardInfo>().ReverseMap();
        }
    }
}
