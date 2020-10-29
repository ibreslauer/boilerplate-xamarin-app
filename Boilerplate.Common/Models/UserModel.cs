namespace Boilerplate.Common.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Gender { get; set; }
        public string IpAddress { get; set; }
        public bool? IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
    }
}
