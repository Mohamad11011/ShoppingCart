namespace Tutorial.Models
{
    public class UserToRole
    {
        public UserToRole()
        { }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
