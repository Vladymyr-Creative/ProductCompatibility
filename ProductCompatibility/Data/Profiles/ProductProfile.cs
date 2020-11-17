using AutoMapper;
using ProductCompatibility.Data.Models;
using ProductCompatibility.ViewModels;

namespace ProductCompatibility.Data.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>()
                .ForMember("Img", opt => opt.Ignore())
                .ForMember("Description", opt => opt.MapFrom(c => c.Desc))
                .ForMember("CategoryId", opt => opt.MapFrom(c => c.Cat));
        }
    }
}
