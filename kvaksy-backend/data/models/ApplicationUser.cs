using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
namespace kvaksy_backend.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public override string Id { get; set; }
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }
        [PersonalData]
        public string Password { get; set; }

        // Ignoring unwanted columns from IdentityUser

        [JsonIgnore]
        public override int AccessFailedCount { get; set; }
        [JsonIgnore]
        public override bool LockoutEnabled { get; set; }
        [JsonIgnore]
        public override string? NormalizedUserName { get; set; }
        [JsonIgnore]
        public override string? NormalizedEmail { get; set; }
        [JsonIgnore]
        public override bool EmailConfirmed { get; set; }
        [JsonIgnore]
        public override string? ConcurrencyStamp { get; set; }
        [JsonIgnore]
        public override string? SecurityStamp { get; set; }
        [JsonIgnore]
        public override bool TwoFactorEnabled { get; set; }
        [JsonIgnore]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [JsonIgnore]
        public override bool PhoneNumberConfirmed { get; set; }
        [JsonIgnore]
        public override string? PasswordHash { get; set; }
    }
}
