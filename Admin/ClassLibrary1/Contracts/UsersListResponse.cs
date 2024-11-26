namespace Library
{
    public class UserListResponse
    {
        public List<User> Users { get; set; }

        public UserListResponse()
        {
            Users = new List<User>();
        }
    }
}