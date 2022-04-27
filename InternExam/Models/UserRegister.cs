namespace InternExam.Models
{
    public class UserRegister
    {
        public string UserPhone { get; set; } = string.Empty;
        public string UsersEmail { get; set; } = string.Empty;
        public byte[] UsersPasswordHash { get; set; }
        public byte[] UsersPasswordSalt { get; set; }
    }
}
