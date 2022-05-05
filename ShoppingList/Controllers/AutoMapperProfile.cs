using AutoMapper;
using ShoppingList.Models;
using ShoppingList.ViewModels;
using ShoppingList.ViewModels.Basket;

namespace ShoppingList.Controllers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BasketTableViewModel, Basket>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BasketId));

            CreateMap<Basket, BasketTableViewModel>()
                .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(
                    src => src.Products == null ? 0 : src.Products.Count));
            
            CreateMap<BasketCreateViewModel, Basket>();

            CreateMap<BasketEditViewModel, Basket>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BasketId));

            CreateMap<Basket, BasketEditViewModel>()
                .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
