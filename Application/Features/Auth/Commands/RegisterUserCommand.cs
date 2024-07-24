using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Application.Contracts.Persistance;

namespace Application.Features.Auth.Commands
{
    public class RegisterUserCommand : IRequest<ErrorOr<BaseResponse<UserDto>>>
    {
        public RegisterDto registerDto;
    }

    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, ErrorOr<BaseResponse<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<UserDto>>> Handle(
            RegisterUserCommand command,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.Register(command.registerDto);
            if (user.IsError) return user.Errors;

            return new BaseResponse<UserDto>(){
                Message="User created successfully",
                Value=_mapper.Map<UserDto>(user.Value)
            };
        }
    }
}
