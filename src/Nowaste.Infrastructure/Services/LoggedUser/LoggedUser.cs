using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Security.Tokens;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Nowaste.Infrastructure.Services.LoggedUser;

public class LoggedUser(NowasteDbContext dbContext, ITokenProvider tokenProvider) : ILoggedUser {
    private readonly NowasteDbContext _dbContext = dbContext;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public async Task<UserEntity> Get() {
        var token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type is ClaimTypes.Sid).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.Id == Guid.Parse(identifier));
    }
}
