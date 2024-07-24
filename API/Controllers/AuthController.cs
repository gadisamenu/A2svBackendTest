using Microsoft.AspNetCore.Mvc;
using Application.Features.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Auth.Dtos;
using Application.Contracts;
using Application.Features.Auth.Queries;


namespace API.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        public AuthController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            return HandleResult(
                await Mediator.Send(
                    new RegisterUserCommand()
                    {
                        registerDto = registerDto
                    }
                )
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return HandleResult(
                await Mediator.Send(
                    new AuthenticateUserCommand()
                    {
                        loginDto = loginDto
                    }
                )
            );
        }


        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserDetails()
        {
            var userId = _userAccessor.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            return HandleResult(
                await Mediator.Send(
                    new GetUserDetailQuery
                    {
                        Id = userId
                    }
                )
            );
        }

        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
        {
            var userId = _userAccessor.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            return HandleResult(
                await Mediator.Send(
                    new UpdateUserCommand
                    {
                        updateUserDto = updateUserDto,
                        userId = userId
                    }
                )
            );
        }

       
    }
}