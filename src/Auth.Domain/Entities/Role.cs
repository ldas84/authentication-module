using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}
