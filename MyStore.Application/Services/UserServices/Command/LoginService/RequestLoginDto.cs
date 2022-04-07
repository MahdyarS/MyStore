namespace MyStore.Application.Services.UserServices.Command.LoginService
{
    public class RequestLoginDto
    {
        public RequestLoginDto(string userName, string password, bool isPersistent)
        {
            UserName = userName;
            Password = password;
            IsPersistent = isPersistent;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsPersistent { get; set; }


    }
}
