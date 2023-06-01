using Application.Abstractions;
using Application.Features.Commands.AppUser.GoogleLogin;
using Application.Features.Commands.AppUser.Login;
using Application.Features.Commands.AppUser.PasswordReset;
using Application.Features.Commands.AppUser.Register;
using Application.Features.Commands.AppUser.UpdatePassword;
using Application.Features.Commands.AppUser.VeriftPasswordResetToken;
using Application.Features.Commands.AppUser.VerifyEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> Reqister(RegisterUserCommandRequest registerUserCommandRequest)
    {
        RegisterUserCommandResponse response = await _mediator.Send(registerUserCommandRequest);
        return Ok(response);
    }

    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery]VerifyEmailCommandRequest verifyEmailCommandRequest)
    {
        VerifyEmailCommandResponse response = await _mediator.Send(verifyEmailCommandRequest);
        return Ok(response);
    }

    [HttpPost("password-reset")]
    public async Task<IActionResult> ResetPassword([FromQuery]PasswordResetCommandRequest passwordResetCommandRequest)
    {
        PasswordResetCommandResponse response = await _mediator.Send(passwordResetCommandRequest);
        return Ok(response);
    }

    [HttpPost("verify-password-reset-token")]
    public async Task<IActionResult> VerifyPasswordResetToken([FromQuery] VerifyPasswordResetTokenCommandRequest verifyPasswordResetTokenCommandRequest)
    {
        VerifyPasswordResetTokenCommandResponse response = await _mediator.Send(verifyPasswordResetTokenCommandRequest);
        return Ok(response);
    }

    [HttpPost("update-password")]
    public async Task<IActionResult> UpdatePassword(
        [FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
    {
        UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
        return Ok(response);
    }
}