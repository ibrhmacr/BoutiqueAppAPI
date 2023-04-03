namespace Application.DTOs.User;

public class RegisterUserDTO
{
    public string UserName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string PasswordConfirm { get; set; }
}