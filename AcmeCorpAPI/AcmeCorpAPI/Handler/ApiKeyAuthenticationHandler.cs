using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace AcmeCorpAPI.Handler
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("X-API-Key", out var apiKey))
            {
                return AuthenticateResult.Fail("Missing API key.");
            }

            // Replace this logic with your actual API key validation logic.
            // In a real-world scenario, you'd validate the API key against a database or some other data source.
            // For demonstration purposes, we'll use a simple hardcoded API key here.
            string validApiKey = "samplekey";

            if (apiKey != validApiKey)
            {
                return AuthenticateResult.Fail("Invalid API key.");
            }

            var claims = new[] { new Claim(ClaimTypes.Name, "ApiKeyUser") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }

}