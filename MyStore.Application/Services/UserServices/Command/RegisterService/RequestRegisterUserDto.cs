using MyStore.Common.Enums.RoleNamesEnum;

namespace MyStore.Application.Services.UserServices.Command.RegisterService
{
    public class RequestRegisterUserDto
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = RoleName.User.ToString();
        public bool AdminPannelRegisteration { get; set; } = false;

    }
}
