using System;
using System.Linq;
using System.Security.Claims;

namespace Categorise.Extensions
{
    /// <summary>
    /// Extension class for ClaimsIdentity.
    /// </summary>
    public static class IdentityExtension
    {
        /// <summary>
        /// Returns the unique identifier for the user contained in the ClaimsIdentity.
        /// </summary>
        /// <param name="claimsIdentity">The current ClaimsIdentity object.</param>
        public static Guid? GetUserId(this ClaimsIdentity claimsIdentity)
        {
            Claim userIdClaim = claimsIdentity.Claims.Where(c => c.Type.Equals(ClaimTypes.NameIdentifier)).FirstOrDefault();
            Guid? userId = null;
            if (userIdClaim != null)
            {
                userId = new Guid(userIdClaim.Value);
            }

            return userId;
        }
    }
}