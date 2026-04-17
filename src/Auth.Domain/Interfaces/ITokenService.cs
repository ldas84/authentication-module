using Auth.Domain.Entities;
using Auth.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken();
    }
}
