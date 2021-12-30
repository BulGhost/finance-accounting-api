namespace FinanceAccounting.Application.Common.DataTransferObjects.UserDto
{
    public class UserAuthenticationResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
