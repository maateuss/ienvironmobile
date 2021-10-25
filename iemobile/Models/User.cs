using System;
using Newtonsoft.Json;

namespace iemobile.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Id { get; set; }
    }

    public class UserRequest
    {
        [JsonProperty("user")]
        public User UserData { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

}
