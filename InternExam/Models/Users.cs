namespace InternExam.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string UserBirthday { get; set; } = string.Empty;
        public string UserGender { get; set; } = string.Empty;
        public string UserCreatedAt { get; set; } = string.Empty;
    }
}
