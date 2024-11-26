using System.Data;

namespace Server.Contracts.DataTransferObjects
{
    public class UserDto
    {
        public UserDto(string username, string role)
        {
            this.Username = username;
            this.Role = role;
        }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
