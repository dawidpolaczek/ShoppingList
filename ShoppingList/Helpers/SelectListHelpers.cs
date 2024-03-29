﻿using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Models;

namespace ShoppingList.Helpers
{
    public static class SelectListHelpers
    {
        public static MultiSelectList ToMultiSelectList<TEntity>(this IEnumerable<TEntity> entities,
            IEnumerable<TEntity>? selectedEntities = null, Func<TEntity, string>? additionaInfo = null)
            where TEntity : EntityBase
        {
            var entitySelectListItems = entities.Select(e => new SelectListItem()
            {
                Text = e.Name + (additionaInfo != null ? additionaInfo(e) : ""),
                Value = e.Id.ToString(),
                Selected = true
            });

            var selectedEntitySelectListItems = selectedEntities?.Select(e => e.Id).ToArray();

            return new MultiSelectList(entitySelectListItems, "Value", "Text", selectedEntitySelectListItems);
        }

        public static SelectList ToSelectList<TEntity>(this IEnumerable<TEntity> entities,
            TEntity? selectedEntity = null, Func<TEntity, string>? additionaInfo = null)
            where TEntity : EntityBase
        {
            var entitySelectListItems = entities.Select(s => new SelectListItem()
            {
                Text = s.Name + (additionaInfo != null ? additionaInfo(s) : ""),
                Value = s.Id.ToString(),
            });

            int? selectedValue = selectedEntity?.Id;

            return new SelectList(entitySelectListItems, "Value", "Text", selectedValue);
        }

        public static SelectList GetSelectListOfEnum<TEnum>(TEnum? selectedItem = default)
            where TEnum : struct, Enum
        {
            var enums = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            Type underlyingType = Enum.GetUnderlyingType(typeof(TEnum));

            var selectListItems = enums
                .Select(en => new SelectListItem()
                {
                    Text = en.ToString(),
                    Value = Convert.ChangeType(en, underlyingType).ToString(),
                    Selected = en.Equals(selectedItem)
                });

            return new SelectList(
                selectListItems,
                "Value", "Text");
        }
    }
}
