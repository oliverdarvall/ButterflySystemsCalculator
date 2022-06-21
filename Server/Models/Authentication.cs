using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Server.Domain;
using Server.Interfaces;

namespace Server.Models
{
    /// <summary>
    /// BasicAuthentication is used to perform Basic authentication on the provided credentials via headers
    /// </summary>
    public class BasicAuthentication
    {
        public static async Task<AuthenticateResult> AuthenticateAsync(string schemeName, IDictionary<string, StringValues> headers, IUsersService usersService)
        {
            return await Task<AuthenticateResult>.Run(async () =>
            {
                if (string.IsNullOrWhiteSpace(schemeName) || (headers == null) || (headers.Count < 1) || (usersService == null))
                    return AuthenticateResult.Fail("Invalid Authentication Parameters");

                if (!headers.ContainsKey(Constants.AuthorizationHeader))
                    return AuthenticateResult.Fail("Missing Authorization Header");

                try
                {
                    var authorizationHeader = headers[Constants.AuthorizationHeader];

                    var authorizationHeaderValue = authorizationHeader.ToString();

                    if (!authorizationHeaderValue.Contains(Constants.BasicAuthentication, StringComparison.InvariantCultureIgnoreCase))
                        return AuthenticateResult.Fail("Invalid Authorization Type");

                    var base64 = authorizationHeaderValue.Split(' ').Last();

                    var credentialBytes = Convert.FromBase64String(base64);
                    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' });

                    var username = credentials.First();
                    var passwordHash = credentials.Last();

                    var user = await usersService.ValidateAsync(username, passwordHash);

                    if (user?.ID > 0)
                    {
                        var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()), new Claim(ClaimTypes.Name, user.FullName) };
                        var identity = new ClaimsIdentity(claims, schemeName);
                        var principal = new ClaimsPrincipal(identity);
                        var ticket = new AuthenticationTicket(principal, schemeName);

                        return AuthenticateResult.Success(ticket);
                    }

                    return AuthenticateResult.Fail("Invalid Username or Password");
                }
                catch
                {
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                }
            });
        }
    }

    public class Authentication : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        readonly IUsersService _usersService;

        public Authentication(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUsersService usersService) : base(options, logger, encoder, clock)
        {
            _usersService = usersService;
        }

        // This function is called by the Authentication pipeline with the HTTP headers
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var result = await BasicAuthentication.AuthenticateAsync(Scheme.Name, Request.Headers, _usersService);

            if (result.Succeeded)
                Logger.LogInformation("{name} logged in successfully.", result.Principal?.Identity?.Name);
            else
                Logger.LogWarning("{warning}", result.Failure?.ToString() ?? "");

            return result;
        }
    }
}