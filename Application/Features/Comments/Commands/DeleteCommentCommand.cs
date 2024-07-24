using ErrorOr;
using MediatR;
using Application.Common.Responses;
using Application.Contracts.Persistance;
using Application.Common.Errors;

namespace Application.Features.Auth.Commands
{
    public class DeleteCommentCommand : IRequest<ErrorOr<BaseResponse<Unit>>>
    {
        public long Id { get; set; }
    }


    public class DeleteCommentCommandHandler
        : IRequestHandler<DeleteCommentCommand, ErrorOr<BaseResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommentCommandHandler(
            
            IUnitOfWork unitOfWork
            )
        {
  
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<BaseResponse<Unit>>> Handle(
            DeleteCommentCommand command,
            CancellationToken cancellationToken
        )
        {
            var Comment = await _unitOfWork.CommentRepository.GetByIdAsync(command.Id);
            if (Comment == null) return ErrorFactory.NotFound("Comment","Comment not found");

            _unitOfWork.CommentRepository.DeleteAsync(Comment);

            await _unitOfWork.SaveAsync();

            return new BaseResponse<Unit>(){
                Message="Comment Deletedsuccessfully",
                Value= Unit.Value
            };

        }
    }
}
