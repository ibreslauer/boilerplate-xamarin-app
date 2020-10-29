using System;

namespace Boilerplate.Common.Models
{
    public class TokenModel
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public string DeviceId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
