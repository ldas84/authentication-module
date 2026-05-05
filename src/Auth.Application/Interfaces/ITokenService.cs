using Auth.Domain.Entities;
using Auth.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(string email, IEnumerable<string> roles);
        RefreshToken GenerateRefreshToken();
    }
}
