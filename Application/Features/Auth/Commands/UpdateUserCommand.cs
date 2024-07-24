using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Application.Contracts.Persistance;

namespace Application.Features.Auth.Commands
{
    public class UpdateUserCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public UpdateUserDto updateUserDto;
        public string userId;
    }

    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            UpdateUserCommand command,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.UpdateUser(command.updateUserDto,command.userId);
            if (user.IsError) return user.Errors;

            return new BaseResponse<Unit>(){
                Message="User created successfully",
                Value=_mapper.Map<Unit>(user.Value)
            };
        }
    }
}
