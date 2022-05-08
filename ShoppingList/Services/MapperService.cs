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
            CreateMap<Basket, BasketTableViewModel>()
                .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(
                    src => src.Products == null ? 0 : src.Products.Count))
                .ForMember(dest => dest.NextShoppingDate, opt => opt.MapFrom(
                    src => GetNextShoppingDate(src)));

            CreateMap<Basket, BasketEditViewModel>()
                .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Basket, BasketDetailsViewModel>()
                .ForMember(dest => dest.ShopName, opt => opt.MapFrom(
                    src => src.Shop != null ? src.Shop.Name : "undefined"))
                .ForMember(dest => dest.NextShoppingDate, opt => opt.MapFrom(
                    src => GetNextShoppingDate(src)))
                .ForMember(dest => dest.DayEveryWeek, opt => opt.MapFrom(
                    src => src.DayEveryWeek != null ? src.DayEveryWeek.ToString() : "undefined"))
                .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.Id));
        }

        public static DateTime? GetNextShoppingDate(Basket basket)
        {
            DateTime today = DateTime.Today;
            if (basket.DayEveryWeek != null)
            {
                int daysUntilDay = ((int)basket.DayEveryWeek - (int)today.DayOfWeek + 7) % 7;
                DateTime nextDateOfDay = today.AddDays(daysUntilDay);
                
                if (basket.SpecificDate == null)
                    return nextDateOfDay;

                return basket.SpecificDate < nextDateOfDay && basket.SpecificDate >= today ?
                    basket.SpecificDate : nextDateOfDay;
            }

            return basket.SpecificDate < today ? null : basket.SpecificDate;
        }
    }
}
