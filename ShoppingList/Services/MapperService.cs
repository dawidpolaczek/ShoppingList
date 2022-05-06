using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Models;
using ShoppingList.ViewModels;
using ShoppingList.ViewModels.Basket;

namespace ShoppingList.Services
{
    public class MapperService : Profile
    {
        public MapperService()
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

            CreateMap<DayOfWeek?, SelectList>().ConvertUsing(src => MapDaysOfWeekToSelectList(src));

            CreateMap<Basket, BasketEditViewModel>()
                .ForMember(dest => dest.BasketId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DaysOfWeek, opt => opt.MapFrom(src => src.DayEveryWeek));
        }

/*        private static DayOfWeek? MapSelectListToDayOfWeek(SelectList? list)
        {
            var selectedItemText = list?.FirstOrDefault(item => item.Selected)?.Text;
            if (Enum.TryParse(selectedItemText, out DayOfWeek dayOfWeek))
            {
                return dayOfWeek;
            }

            return null;
        }*/

        private static SelectList MapDaysOfWeekToSelectList(DayOfWeek? selectedDayOfWeek)
        {
            var days = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
            var selectListItems = days
                .Select(day => new SelectListItem()
                {
                    Text = day.ToString(),
                    Value = ((int)day).ToString(),
                    Selected = selectedDayOfWeek == day
                });

            return new SelectList(
                selectListItems,
                "Value", "Text");
        }
    }
}
