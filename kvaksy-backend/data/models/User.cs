using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
namespace kvaksy_backend.Data.Models
{
    public enum Role
    {
        [EnumMember(Value = "User")]
        User,
        [EnumMember(Value = "Admin")]
        Admin
    }
    public class User
    {
        public Role Role { get; set; } = Role.User;

        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
        public string? Password { get; set; }
    }
}
