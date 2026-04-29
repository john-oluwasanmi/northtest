using Microsoft.AspNetCore.Authorization;
using Nhs.Test.Api.Filters;

namespace Nhs.Test.Api
{
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        public BasicAuthorizationAttribute()
        {
            AuthenticationSchemes = BasicAuthenticationHandler._schemeName;
        }
    }
}
