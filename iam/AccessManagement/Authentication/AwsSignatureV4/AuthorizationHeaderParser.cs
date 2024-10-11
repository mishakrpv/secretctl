using Microsoft.Extensions.Primitives;

namespace AccessManagement.Authentication.AwsSignatureV4;

public static class AuthorizationHeaderParser
{
    public static (string accessKeyId, string region, string service, string signature) Parse(StringValues authorizationHeader)
    {
        var authHeader = authorizationHeader.FirstOrDefault();
        if (string.IsNullOrEmpty(authHeader))
        {
            throw new ArgumentException("Authorization header is missing.");
        }
        
        var parts = authHeader.Split([' '], StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 4 || parts[0] != "AWS4-HMAC-SHA256")
        {
            throw new ArgumentException("Invalid Authorization header format.");
        }
        
        var credentialPart = parts[1];
        
        var credentialComponents = credentialPart.Split([','], StringSplitOptions.RemoveEmptyEntries);
        var credentialComponent = credentialComponents.FirstOrDefault(c => c.Trim().StartsWith("Credential="));
        if (credentialComponent == null)
        {
            throw new ArgumentException("Credential not found in Authorization header.");
        }
        
        var credentialValue = credentialComponent["Credential=".Length..].Trim();
        
        var credentialValues = credentialValue.Split('/');
        if (credentialValues.Length < 4)
        {
            throw new ArgumentException("Invalid Credential format.");
        }
        
        var signaturePart = parts.ElementAt(3);
        
        var accessKeyId = credentialValues[0];
        var region = credentialValues[2];
        var service = credentialValues[3];
        var signature = signaturePart["Credential=".Length..].Trim();

        return (accessKeyId, region, service, signature);
    }
}