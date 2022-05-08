using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Helpers;
using ShoppingList.Models;
using ShoppingList.ViewModels;
using ShoppingList.ViewModels.Basket;

namespace ShoppingList.Services
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<Basket, BasketTableViewModel>();

            CreateMap<Basket, BasketTableViewModel>()
                .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(
                    src => src.Products == null ? 0 : src.Products.Count));

            CreateMap<Basket, BasketEditViewModel>()
                .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Basket, BasketDetailsViewModel>()
                .ForMember(dest => dest.ShopName, opt => opt.MapFrom(
                    src => src.Shop != null ? src.Shop.Name : null))
                .ForMember(dest => dest.SpecificDate, opt => opt.MapFrom(
                    src => src.SpecificDate != null ? src.SpecificDate.Value.ToString("yyyy-MM-dd") : null))
                .ForMember(dest => dest.DayEveryWeek, opt => opt.MapFrom(
                    src => src.DayEveryWeek != null ? src.DayEveryWeek.ToString() : null))
                .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
