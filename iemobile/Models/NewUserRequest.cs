using System;
namespace iemobile.Models
{
    public class NewUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; } = true;
        public int Role { get; set; } = 0;

    }
}
