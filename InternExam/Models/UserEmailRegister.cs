namespace InternExam.Models
{
    public class UserEmailRegister
    {
        public string UsersEmail { get; set; } = string.Empty;
        public byte[] UsersPasswordHash { get; set; }
        public byte[] UsersPasswordSalt { get; set; }
    }
}
