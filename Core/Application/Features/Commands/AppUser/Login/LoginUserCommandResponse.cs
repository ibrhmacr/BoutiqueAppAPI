using Application.DTOs.Token;

namespace Application.Features.Commands.AppUser.Login;

public class LoginUserCommandResponse
{
    public string Message { get; set; }
}

public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
{
    public string Token { get; set; }
}

public class LoginUserErrorCommandResponse : LoginUserCommandResponse
{
    
}
//Polymorphism Ornek (Cok bicimlilik)