using Auth.Domain.Entities;
using Auth.Application.UseCases.Auth;
using Auth.Application.UseCases.RegisterUser;
using System;
using System.Collections.Generic;

namespace Auth.Tests.TestUtils;

public static class TestDataFactory
{

    // -------------------------------
    // Users
    // -------------------------------
    public static User CreateUser(
        string email = "test@mail.com",
        string passwordHash = "hashed-password",
        bool isActive = true
    )
    {
        return new User
        {
          Id = Guid.NewGuid(),
          Email = email,
          PasswordHash = passwordHash,
          IsActive = isActive  
        };
    }
}