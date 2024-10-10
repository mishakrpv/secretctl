using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using AccessManagement.Data;
using AccessManagement.Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace AccessManagement.Authentication;

public class AuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    AppDbContext context,
    IPolicyRepository repository)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private readonly AppDbContext _context = context;
    private readonly IPolicyRepository _repository = repository;
    
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var request = Context.Request;

        try
        {
            var claims = new[] { new Claim(ClaimTypes.Name, "") };
            var identity = new ClaimsIdentity(claims, nameof(AuthenticationHandler));
            var principal = new ClaimsPrincipal(identity);
            
            return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex);
        }
    }
    
    private static string ComputeHash(string input)
    {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }
}