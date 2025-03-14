using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private const string AuthorizationHeader = "Authorization";
    private const string BasicPrefix = "Basic ";

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        System.Text.Encodings.Web.UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(AuthorizationHeader))
        {
            return Task.FromResult(AuthenticateResult.Fail("Authorization header missing"));
        }

        var authorizationHeader = Request.Headers[AuthorizationHeader].ToString();
        if (!authorizationHeader.StartsWith(BasicPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid authorization scheme"));
        }

        var encodedCredentials = authorizationHeader.Substring(BasicPrefix.Length).Trim();
        var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
        var credentials = decodedCredentials.Split(':');

        if (credentials.Length != 2)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid credentials format"));
        }

        var username = credentials[0];
        var password = credentials[1];

        
        if (IsValidUser(username, password))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                
            };

            var identity = new ClaimsIdentity(claims, "Basic");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Basic");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        else
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
        }
    }

    private bool IsValidUser(string username, string password)
    {
        return username == "admin" && password == "password123";
    }
}
