using ErrorOr;
using MediatR;
using AutoMapper;
using Application.Common.Responses;
using Application.Features.Auth.Dtos;
using Application.Contracts.Persistance;
using Domain.Comments;
using Application.Features.Comments.Dtos;

namespace Application.Features.Auth.Commands
{
    public class CreateCommentCommand : IRequest<ErrorOr<BaseResponse<CommentDto>>>
    {
        public string UserId;
        public CreateCommentDto createCommentDto;
    }

    public class CreateCommentCommandHandler
        : IRequestHandler<CreateCommentCommand, ErrorOr<BaseResponse<CommentDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<BaseResponse<CommentDto>>> Handle(
            CreateCommentCommand command,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(command.UserId);

            var recipe = await _unitOfWork.RecipeRepository.GetByIdAsync(command.createCommentDto.RecipeId);

            var Comment = new Comment {
                Content = command.createCommentDto.Content,
                Author = user.Value,
                Date = new DateTime(),
                Recipe = recipe
            };

            var CommentRes = await _unitOfWork.CommentRepository.AddAsync(Comment);

            await _unitOfWork.SaveAsync();

            return new BaseResponse<CommentDto>(){
                Message="Comment created successfully",
                Value=_mapper.Map<CommentDto>(CommentRes)
            };
        }
    }
}
