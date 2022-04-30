using ShoppingList.Services.Interfaces;
using System.Security.Claims;

namespace ShoppingList.Services.ConcreteServices
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string? GetId()
        {
            return _contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
