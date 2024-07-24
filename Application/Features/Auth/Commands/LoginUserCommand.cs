using ErrorOr;
using MediatR;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Domain;
using Microsoft.AspNetCore.Identity;
using Application.Contracts.Services;
using Application.Common.Errors;
using Application.Contracts.Persistance;
namespace Application.Features.Auth.Commands
{
    public class AuthenticateUserCommand : IRequest<ErrorOr<BaseResponse<LoginResponseDto>>>
    {
        public LoginDto loginDto;
    }


    public class AuthenticateUserCommandHandler
        : IRequestHandler<AuthenticateUserCommand, ErrorOr<BaseResponse<LoginResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticateUserCommandHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtService jwtService,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<LoginResponseDto>>> Handle(
            AuthenticateUserCommand command,
            CancellationToken cancellationToken
        )
        {
            var response = await _unitOfWork.UserRepository.Login(command.loginDto);

            if (response.IsError) return response.Errors;


        
            return new BaseResponse<LoginResponseDto>(){
                Message="Logged in successfully",
                Value= response.Value
            };

        }
    }
}
