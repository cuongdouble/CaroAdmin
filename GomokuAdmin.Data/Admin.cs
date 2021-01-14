using System;
using System.Collections.Generic;

#nullable disable

namespace GomokuAdmin.Data
{
    public partial class Admin
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordExpires { get; set; }
        public DateTime? BannedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
