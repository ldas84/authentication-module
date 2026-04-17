using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.ValueObjects
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public string? ReplacedToken { get; set; }
    }
}
