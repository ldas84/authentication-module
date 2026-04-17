using System;
using System.Collections.Generic;
using System.Text;
using Auth.Application.DTOs;

namespace Auth.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterUserDto dto);
    }
}
