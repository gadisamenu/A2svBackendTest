using ErrorOr;
using MediatR;
using Application.Common.Responses;
using Application.Contracts.Persistance;

namespace Application.Features.Auth.Commands
{
    public class DeleteUserCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public string Email { get; set; }
    }


    public class DeleteUserCommandHandler
        : IRequestHandler<DeleteUserCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserCommandHandler(
            
            IUnitOfWork unitOfWork
            )
        {
  
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            DeleteUserCommand command,
            CancellationToken cancellationToken
        )
        {
            var result = await _unitOfWork.UserRepository.DeleteUserAsync(command.Email);


            if (result.IsError) return result.Errors;

            return new BaseResponse<Unit>(){
                Message="User Deletedsuccessfully",
                Value= Unit.Value
            };

        }
    }
}
