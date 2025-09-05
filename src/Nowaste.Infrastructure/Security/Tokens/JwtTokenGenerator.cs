using Microsoft.IdentityModel.Tokens;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Nowaste.Infrastructure.Security.Tokens;

public class JwtTokenGenerator(uint expirationTimeInMinutes, string signingKey) : IAccessTokenGenerator {
    private readonly uint _expirationTimeInMinutes = expirationTimeInMinutes;
    private readonly string _signingKey = signingKey;

    public string Generate(UserEntity user) {
        var claims = new List<Claim>() {
            new(ClaimTypes.Sid, user.Id.ToString()),
            new(ClaimTypes.Name, user.Person.FullName.Split(" ").First()),
            new(ClaimTypes.Role, user.Role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeInMinutes),
            SigningCredentials = new SigningCredentials(
                SecurityKey(),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Subject = new ClaimsIdentity(claims)
        };


        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey() {
        var key = System.Text.Encoding.UTF8.GetBytes(_signingKey);

        return new SymmetricSecurityKey(key);
    }
}
