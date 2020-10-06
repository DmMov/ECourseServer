using ECourse.Domain.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace ECourse.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
        RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
