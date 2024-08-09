using BusinessObjects.Models;
using Microsoft.Extensions.Configuration;
using Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BusinessObjects.Dto.ResponseDto;

namespace Services.Implementation;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private IConfiguration Configuration { get; } = configuration;

    public Task<TokenResponseDto> CreateToken(User user)
    {
        var issuer = Configuration["Jwt:Issuer"];
        var audience = Configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"] ?? throw new InvalidOperationException());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.ToUniversalTime().AddMinutes(60),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        
        var tokenResponse = new TokenResponseDto()
        {
            Token = jwtToken,
            Expiration = tokenDescriptor.Expires ?? DateTime.UtcNow
        };
        return Task.FromResult(tokenResponse);
    }

    public Task<TokenResponseDto> CreateToken(Customer customer)
    {
        var issuer = Configuration["Jwt:Issuer"];
        var audience = Configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"] ?? throw new InvalidOperationException());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, customer.CustomerId ?? string.Empty),
                new Claim(ClaimTypes.Role, "Customer"),
                new Claim(JwtRegisteredClaimNames.Sub, customer.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, customer.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.ToUniversalTime().AddMinutes(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        
        var tokenResponse = new TokenResponseDto()
        {
            Token = jwtToken,
            Expiration = tokenDescriptor.Expires ?? DateTime.UtcNow
        };
        return Task.FromResult(tokenResponse);
    }
}