namespace InternExam.Models
{
    public class UserPhoneRegister
    {
        public string UsersPhone { get; set; } = string.Empty;
        public byte[] UsersPasswordHash { get; set; }
        public byte[] UsersPasswordSalt { get; set; }
    }
}
