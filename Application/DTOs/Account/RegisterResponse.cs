namespace Application.DTOs.Account
{
    public class RegisterResponse
    {
        public bool UserAlreadyExists { get; set; }
        public string Message { get; set; }
    }
}
