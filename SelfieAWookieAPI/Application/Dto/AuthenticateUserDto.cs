namespace SelfieAWookieAPI.Application.Dto
{
    public class AuthenticateUserDto
    {
        #region Properties

        public string Login { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        #endregion
    }
}