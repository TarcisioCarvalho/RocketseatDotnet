using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace CashFlow.Infrastructure.Services.LoggedUser;
internal class LoggedUser : ILoggedUser
{
    private readonly CashFlowDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;
    public LoggedUser(CashFlowDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> Get()
    {
        var token = _tokenProvider.TokenOnRequest();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        var indetifier = jwtSecurityToken.Claims.FirstOrDefault(claim =>  claim.Type == "sid")?.Value;
        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(u => u.UserIndetifier == Guid.Parse(indetifier));
    }
}
