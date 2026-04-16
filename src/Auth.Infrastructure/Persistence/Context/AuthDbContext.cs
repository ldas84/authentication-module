using System;
using System.Collections.Generic;
using System.Text;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence.Context
{
    public class AuthDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    }
}
