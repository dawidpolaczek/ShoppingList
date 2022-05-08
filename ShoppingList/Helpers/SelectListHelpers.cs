using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingList.Models;

namespace ShoppingList.Helpers
{
    public static class SelectListHelpers
    {
        public static SelectList ToSelectList<TEntity>(this IEnumerable<TEntity> entities,
            IEnumerable<TEntity>? selectedEntities = null, Func<TEntity, string>? additionaInfo = null)
            where TEntity : EntityBase
        {
            return new SelectList(entities.Select(s => new SelectListItem()
            {
                Text = s.Name + (additionaInfo != null ? additionaInfo(s) : ""),
                Value = s.Id.ToString(),
                Selected = selectedEntities != null && selectedEntities.Contains(s)
            }), "Value", "Text");
        }

        public static SelectList ToSelectList<TEntity>(this IEnumerable<TEntity> entities,
            TEntity? selectedEntity, Func<TEntity, string>? additionaInfo = null)
            where TEntity : EntityBase
        {
            return entities.ToSelectList(selectedEntity != null ? new List<TEntity>() { selectedEntity } : null,
                additionaInfo);
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
