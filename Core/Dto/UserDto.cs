using Storage.Tables;

namespace Core.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public UserDto(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Name = user.Name;
        }
    }
}
