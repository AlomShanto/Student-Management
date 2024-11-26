namespace Library
{
    public class User
    {
        public User(string username, string role)
        {
            this.Username = username;
            this.Role = role;
        }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
