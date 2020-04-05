using System;
using System.Linq;
using System.Security.Claims;

namespace Extensions
{
    public static class IdentityExtensions
    {
        public static TKey UserId<TKey>(this ClaimsPrincipal user)
        {
            var stringId = user.Claims
                .Single(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (typeof(TKey) == typeof(string))
            {
                return (TKey) Convert.ChangeType(stringId, typeof(TKey));
            }
            else if (typeof(TKey) == typeof(int) || typeof(TKey) == typeof(long))
            {
                return stringId != null
                    ? (TKey) Convert.ChangeType(stringId, typeof(TKey))
                    : (TKey) Convert.ChangeType(0, typeof(TKey));
            }
            else if (typeof(TKey) == typeof(Guid))
            {
                return (TKey) Convert.ChangeType(new Guid(stringId), typeof(TKey));
            }
            else

            {
                throw new Exception("Invalid type provided");
            }
        }

        public static Guid UserGuidId(this ClaimsPrincipal user)
        {
            return user.UserId<Guid>();
        }

    }
}