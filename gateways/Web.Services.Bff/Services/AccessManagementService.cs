using Microsoft.AspNetCore.Http.Extensions;
using Ocelot.Infrastructure.Extensions;

namespace Web.Services.Bff.Services;

public class AccessManagementService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    private readonly string _apiV1Prefix = "api/v1/access";
    
    public async Task<int> ShouldBeGranted(HttpContext context)
    {
        var url = context.Request.GetDisplayUrl();
        var authorization = context.Request.Headers.Authorization.GetValue();
        if (string.IsNullOrEmpty(authorization))
        {
            return 400;
        }
        _httpClient.DefaultRequestHeaders.Add("Authorization", authorization);
        _httpClient.DefaultRequestHeaders.Add("X-Resource-Url", url);
        var response = await _httpClient.GetAsync($"{_apiV1Prefix}/authorize");
        return (int)response.StatusCode;
    }
}