using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Buffers.Text;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Nhs.Test.Api.Filters
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder) : base(options, logger, encoder) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("Authorization",
                out var authHeaderValue))
            {
                return Task.FromResult(AuthenticateResult.Fail(
                    "request header missing 'Authorization'")
                );
            }

            if (!AuthenticationHeaderValue.TryParse(authHeaderValue.ToString(), out var authenticationHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail(
                    "failed to convert to authentication header value"));
            }

            if (!authenticationHeader.Scheme.Equals("Basic",
                StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail(
                    "Authentication scheme is not 'Basic'"));
            }

            if (!Base64.IsValid(authenticationHeader.Parameter!))
            {
                return Task.FromResult(AuthenticateResult.Fail(
                    "'Authorization' header value isn't valid")
                );
            }

            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationHeader.Parameter!));
            var credentials = decoded.Split(':', 2);

            if (credentials.Length != 2)
            {
                return Task.FromResult(AuthenticateResult.Fail(
                      "'Authorization' header value isn't valid")
                );
            }

            var username = credentials[0];
            var password = credentials[1];

            if (username != Constants.Username || password != Constants.Password)
            {
                return Task.FromResult(AuthenticateResult.Fail(
                    "Invalid username or password")
                );
            }

            var identity = new ClaimsIdentity([new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.AuthenticationMethod, authenticationHeader.Scheme)], Constants.SchemeName);

            return Task.FromResult(AuthenticateResult.Success(
                new AuthenticationTicket(
                    new ClaimsPrincipal(identity),
                    Constants.SchemeName
                )));
        }
    }
}
