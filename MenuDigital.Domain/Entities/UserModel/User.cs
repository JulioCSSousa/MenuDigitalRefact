using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MenuDigital.Domain.Models.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string ActivedPlan { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public User() { }
        
        public User(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;

        }

        public void SetName(string name)
        {
            Name = name;
        }

    }
}
