using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using Petalaka.Payment.API.ModelViews.RequestModels;
using Petalaka.Payment.Core.Utils;

namespace Petalaka.Payment.API.CustomMiddleware;

public class ValidateJwtTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpClientFactory _httpClient;    
    private readonly IConfiguration _configuration;
    public ValidateJwtTokenMiddleware(RequestDelegate next, IHttpClientFactory httpClient, IConfiguration configuration)
    {
        _next = next;
        _httpClient = httpClient;
        _configuration = configuration;
    }
    
    public async Task Invoke(HttpContext context)
    {
        var urlEndPoint = _configuration.GetSection("ApiEndPoint:Url").Value;
        var client = _httpClient.CreateClient();
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrWhiteSpace(token))
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (!jwtHandler.CanReadToken(token))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
        
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var payload = jwtToken.Claims.ToList();
        
            string userId = StringConverterHelper.CapitalizeString(payload.FirstOrDefault(p => p.Type == "UserId")?.Value);
            string tokenHash = payload.FirstOrDefault(p => p.Type == "TokenHash")?.Value;
            string deviceId = context.Request.Headers["User-Agent"].ToString();
            var validationRequest = new ValidateTokenRequestModel()
            {
                UserId = userId,
                DevideId = deviceId,
                TokenHashed = tokenHash,
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.RawData);
            client.DefaultRequestHeaders.UserAgent.ParseAdd(deviceId);
            var response = await client.PostAsJsonAsync($"{urlEndPoint}/account-service/authentications/v1/token/validation", validationRequest);
            if (!response.IsSuccessStatusCode)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            
            /*var userToken = await _unitOfWork.ApplicationUserTokenRepository.GetUserAsync(p =>
                p.UserId.ToString() == userId &&
                p.LoginProvider == deviceId);
            if(userToken == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            if (String.CompareOrdinal(userToken.ExpiryTime, TimeStampHelper.GenerateUnixTimeStamp()) < 0)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            // Validate the token hash with the input string
            string inputString = $"{userId}{deviceId}{userToken.Value}";
            if (!tokenService.ValidTokenHash(tokenHash, inputString))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }*/
        }
        await _next(context); // Call the next middleware

    }
}