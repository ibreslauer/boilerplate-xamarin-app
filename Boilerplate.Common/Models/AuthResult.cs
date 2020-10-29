using System;

namespace Boilerplate.Common.Models
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public UserModel User { get; set; }
        public string AccessToken { get; set; }
        public DateTime? Expiration { get; set; }
    }
}
